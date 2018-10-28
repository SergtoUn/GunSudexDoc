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
    public class ItemSubtypesController : Controller
    {
        private GunSudexDbContext db = new GunSudexDbContext();

        // GET: Manager/ItemSubtypes
        public ActionResult Index()
        {
            var itemSubtypes = db.ItemSubtypes.Include(i => i.ItemType);
            return View(itemSubtypes.ToList());
        }

        // GET: Manager/ItemSubtypes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemSubtype itemSubtype = db.ItemSubtypes.Find(id);
            if (itemSubtype == null)
            {
                return HttpNotFound();
            }
            return View(itemSubtype);
        }

        // GET: Manager/ItemSubtypes/Create
        public ActionResult Create()
        {
            ViewBag.ItemTypeID = new SelectList(db.ItemTypes, "ItemTypeID", "ItemType1");
            return View();
        }

        // POST: Manager/ItemSubtypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemSubtypeID,ItemSubtype1,ItemTypeID")] ItemSubtype itemSubtype)
        {
            if (ModelState.IsValid)
            {
                itemSubtype.ItemSubtypeID = Guid.NewGuid();
                db.ItemSubtypes.Add(itemSubtype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemTypeID = new SelectList(db.ItemTypes, "ItemTypeID", "ItemType1", itemSubtype.ItemTypeID);
            return View(itemSubtype);
        }

        // GET: Manager/ItemSubtypes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemSubtype itemSubtype = db.ItemSubtypes.Find(id);
            if (itemSubtype == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemTypeID = new SelectList(db.ItemTypes, "ItemTypeID", "ItemType1", itemSubtype.ItemTypeID);
            return View(itemSubtype);
        }

        // POST: Manager/ItemSubtypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemSubtypeID,ItemSubtype1,ItemTypeID")] ItemSubtype itemSubtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemSubtype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemTypeID = new SelectList(db.ItemTypes, "ItemTypeID", "ItemType1", itemSubtype.ItemTypeID);
            return View(itemSubtype);
        }

        // GET: Manager/ItemSubtypes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemSubtype itemSubtype = db.ItemSubtypes.Find(id);
            if (itemSubtype == null)
            {
                return HttpNotFound();
            }
            return View(itemSubtype);
        }

        // POST: Manager/ItemSubtypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ItemSubtype itemSubtype = db.ItemSubtypes.Find(id);
            db.ItemSubtypes.Remove(itemSubtype);
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
