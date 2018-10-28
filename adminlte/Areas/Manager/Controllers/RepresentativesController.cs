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
    public class RepresentativesController : Controller
    {
        private GunSudexDbContext db = new GunSudexDbContext();

        // GET: Manager/Representatives
        public ActionResult Index()
        {
            return View(db.Representatives.ToList());
        }

        // GET: Manager/Representatives/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Representative representative = db.Representatives.Find(id);
            if (representative == null)
            {
                return HttpNotFound();
            }
            return View(representative);
        }

        // GET: Manager/Representatives/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/Representatives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FamilyName,FirstName,MidName,RepresentativeID,Position,PhoneNumber")] Representative representative)
        {
            if (ModelState.IsValid)
            {
                representative.RepresentativeID = Guid.NewGuid();
                db.Representatives.Add(representative);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(representative);
        }

        // GET: Manager/Representatives/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Representative representative = db.Representatives.Find(id);
            if (representative == null)
            {
                return HttpNotFound();
            }
            return View(representative);
        }

        // POST: Manager/Representatives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FamilyName,FirstName,MidName,RepresentativeID,Position,PhoneNumber")] Representative representative)
        {
            if (ModelState.IsValid)
            {
                db.Entry(representative).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(representative);
        }

        // GET: Manager/Representatives/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Representative representative = db.Representatives.Find(id);
            if (representative == null)
            {
                return HttpNotFound();
            }
            return View(representative);
        }

        // POST: Manager/Representatives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Representative representative = db.Representatives.Find(id);
            db.Representatives.Remove(representative);
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
