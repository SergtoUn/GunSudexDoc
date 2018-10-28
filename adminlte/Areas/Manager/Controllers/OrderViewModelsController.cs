using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeaponDoc.Areas.Manager.Models;
using WeaponDoc.Models;

namespace WeaponDoc.Areas.Manager.Controllers
{
    public class OrderViewModelsController : Controller
    {
        private GunSudexDbContext db = new GunSudexDbContext();

        // GET: Manager/OrderViewModels
        public ActionResult Index()
        {

            List<OrderViewModel> ovmitems = (from cd in db.CallDetails
                                             join c in db.Calls on cd.CallID equals c.CallID
                                             join o in db.Orders on cd.CallID equals o.CallID
                                             join i in db.Items on cd.ItemID equals i.ItemID
                                             join st in db.ItemSubtypes on i.ItemSubtypeID equals st.ItemSubtypeID
                                             join it in db.ItemTypes on st.ItemTypeID equals it.ItemTypeID
                                             join p in db.Programs on it.ItemTypeID equals p.ItemTypeID
                                             where (c.CallStatus >= 1)
                                             select (new OrderViewModel { Id = new Guid(), callID = cd.CallID, ContractData = (c.DocNumber + (c.ContractDate).ToString()), ItemName = (i.ItemSubtype.ItemSubtype1 + i.ItemName), Program = p.ProgramNameShort, QtyLeft = QuantityLeft(cd.CallID, cd.ItemID, cd.ItemQty)})).ToList();



            return View(ovmitems);
        }

        private int QuantityLeft(Guid callID, Guid itemID, int itemQty)
        {
            int qleft;

            List <Call> callslist = db.Calls.ToList();

            

            //var call = (from c in db.Calls
            //            where c.CallID == callID
            //            select new { c).FirstOrDefault();

            var itemqty = db.CallDetails.Where(t => t.CallID == callID).Select(t => new { t.ItemID, t.ItemQty }).ToDictionary(t => t.ItemID, t => t.ItemQty);

            //long orderqty = (from o in db.Orders
            //                where ((o.CallID == callID) && (o.ItemID == itemID))
            //                select (o.OrderNumber)).Sum();



            //var orditem = (from o in db.Orders
            //               where (o.CallID == callID)
            //               select new { });

            var orditemqty = db.Orders.Where(t => t.CallID == callID).GroupBy(c => c.ItemID).Select(group => new { item = group.Key, Sum = group.Sum(x => Math.Round(Convert.ToDecimal(x.OrderNumber), 2)) }).ToDictionary(k => k.item, k => k.Sum);

            var itemordered = (from o in orditemqty
                    where o.Key == itemID
                    select o.Value).FirstOrDefault();

            var itemcall = (from c in itemqty
                           where c.Key == itemID
                           select c.Value).FirstOrDefault();


            //throw new NotImplementedException();
            return ((int)(itemcall - itemordered));
        }

        // GET: Manager/OrderViewModels/Details/5
        //public ActionResult Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    OrderViewModel orderViewModel = db.OrderViewModels.Find(id);
        //    if (orderViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(orderViewModel);
        //}

        // GET: Manager/OrderViewModels/Create
        public ActionResult Create(Guid ovmId)
        {
            var calls = (from c in db.Calls
                         join cd in db.CallDetails on c.CallID equals cd.CallID
                                join i in db.Items on cd.ItemID equals i.ItemID
                                join st in db.ItemSubtypes on i.ItemSubtypeID equals st.ItemSubtypeID
                                join it in db.ItemTypes on st.ItemTypeID equals it.ItemTypeID
                                join p in db.Programs on it.ItemTypeID equals p.ItemTypeID
                                where (c.CallStatus == 1)
                                select (new { c.CallDate })).ToList();


                                //select (new OrderViewModel { Id = o.OrderID, callID = c.CallID, ContractData = (c.Number + (c.ContractDate).ToString()), ItemName = (i.ItemSubtype.ItemSubtype1 + i.ItemName), Program = p.ProgramNameShort, QtyLeft = QuantityLeft(c.CallID, c.ItemID, c.ItemQty) })).ToList();


            return View();
        }

        // POST: Manager/OrderViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CallID,ItemName,CallQty,OrderQty,ExpertFamName,FinalDate")] OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                orderViewModel.Id = Guid.NewGuid();
                //db.OrderViewModels.Add(orderViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderViewModel);
        }

        // GET: Manager/OrderViewModels/Edit/5
        //public ActionResult Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    OrderViewModel orderViewModel = db.OrderViewModels.Find(id);
        //    if (orderViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(orderViewModel);
        //}

        // POST: Manager/OrderViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CallID,ItemName,CallQty,OrderQty,ExpertFamName,FinalDate")] OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderViewModel);
        }

        // GET: Manager/OrderViewModels/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    OrderViewModel orderViewModel = db.OrderViewModels.Find(id);
        //    if (orderViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(orderViewModel);
        //}

        // POST: Manager/OrderViewModels/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    OrderViewModel orderViewModel = db.OrderViewModels.Find(id);
        //    db.OrderViewModels.Remove(orderViewModel);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
