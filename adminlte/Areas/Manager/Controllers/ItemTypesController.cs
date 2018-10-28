using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeaponDoc.Models;

namespace WeaponDoc.Areas.Manager.Controllers
{
    public class ItemTypesController : Controller
    {
        private GunSudexDbContext db = new GunSudexDbContext();

        // GET: Manager/ItemTypes
        public ActionResult Index()
        {
            var itemTypes = db.ItemTypes.Include(i => i.WeaponType);
            var iTList = itemTypes.ToList();
            return View(iTList);
                //Json(new { data = iTList }, JsonRequestBehavior.AllowGet);
        }

        // GET: Manager/ItemTypes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemType itemType = db.ItemTypes.Find(id);
            if (itemType == null)
            {
                return HttpNotFound();
            }
            return View(itemType);
        }

        // GET: Manager/ItemTypes/Create
        public ActionResult Create()
        {
            ViewBag.WeaponTypeID = new SelectList(db.WeaponTypes, "WeaponTypeID", "WeaponTypeDescr");
            return View();
        }

        // POST: Manager/ItemTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemTypeID,ItemType1,WeaponTypeID")] ItemType itemType)
        {
            if (ModelState.IsValid)
            {
                itemType.ItemTypeID = Guid.NewGuid();
                db.ItemTypes.Add(itemType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WeaponTypeID = new SelectList(db.WeaponTypes, "WeaponTypeID", "WeaponTypeDescr", itemType.WeaponTypeID);
            return View(itemType);
        }

        // GET: Manager/ItemTypes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemType itemType = db.ItemTypes.Find(id);
            if (itemType == null)
            {
                return HttpNotFound();
            }
            ViewBag.WeaponTypeID = new SelectList(db.WeaponTypes, "WeaponTypeID", "WeaponTypeDescr", itemType.WeaponTypeID);
            return View(itemType);
        }

        // POST: Manager/ItemTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemTypeID,ItemType1,WeaponTypeID")] ItemType itemType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WeaponTypeID = new SelectList(db.WeaponTypes, "WeaponTypeID", "WeaponTypeDescr", itemType.WeaponTypeID);
            return View(itemType);
        }

        // GET: Manager/ItemTypes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemType itemType = db.ItemTypes.Find(id);
            if (itemType == null)
            {
                return HttpNotFound();
            }
            return View(itemType);
        }

        // POST: Manager/ItemTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ItemType itemType = db.ItemTypes.Find(id);
            db.ItemTypes.Remove(itemType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
