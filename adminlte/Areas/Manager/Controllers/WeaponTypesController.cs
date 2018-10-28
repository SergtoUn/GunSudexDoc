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
    public class WeaponTypesController : Controller
    {
        private GunSudexDbContext db = new GunSudexDbContext();

        // GET: Manager/WeaponTypes
        public ActionResult Index()
        {
            return View(db.WeaponTypes.ToList());
        }

        // GET: Manager/WeaponTypes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeaponType weaponType = db.WeaponTypes.Find(id);
            if (weaponType == null)
            {
                return HttpNotFound();
            }
            return View(weaponType);
        }

        // GET: Manager/WeaponTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/WeaponTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WeaponTypeID,WeaponTypeDescr")] WeaponType weaponType)
        {
            if (ModelState.IsValid)
            {
                weaponType.WeaponTypeID = Guid.NewGuid();
                db.WeaponTypes.Add(weaponType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(weaponType);
        }

        // GET: Manager/WeaponTypes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeaponType weaponType = db.WeaponTypes.Find(id);
            if (weaponType == null)
            {
                return HttpNotFound();
            }
            return View(weaponType);
        }

        // POST: Manager/WeaponTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WeaponTypeID,WeaponTypeDescr")] WeaponType weaponType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weaponType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(weaponType);
        }

        // GET: Manager/WeaponTypes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeaponType weaponType = db.WeaponTypes.Find(id);
            if (weaponType == null)
            {
                return HttpNotFound();
            }
            return View(weaponType);
        }

        // POST: Manager/WeaponTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            WeaponType weaponType = db.WeaponTypes.Find(id);
            db.WeaponTypes.Remove(weaponType);
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
