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
    public class JudicialStatusController : Controller
    {
        private GunSudexDbContext db = new GunSudexDbContext();

        // GET: Manager/JudicialStatus
        public ActionResult Index()
        {
            return View(db.JudicialStatuses.ToList());
        }

        // GET: Manager/JudicialStatus/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JudicialStatus judicialStatus = db.JudicialStatuses.Find(id);
            if (judicialStatus == null)
            {
                return HttpNotFound();
            }
            return View(judicialStatus);
        }

        // GET: Manager/JudicialStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/JudicialStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JudicialStatusID,JudicialStatus1,JudicialStatusName")] JudicialStatus judicialStatus)
        {
            if (ModelState.IsValid)
            {
                judicialStatus.JudicialStatusID = Guid.NewGuid();
                db.JudicialStatuses.Add(judicialStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(judicialStatus);
        }

        // GET: Manager/JudicialStatus/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JudicialStatus judicialStatus = db.JudicialStatuses.Find(id);
            if (judicialStatus == null)
            {
                return HttpNotFound();
            }
            return View(judicialStatus);
        }

        // POST: Manager/JudicialStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JudicialStatusID,JudicialStatus1,JudicialStatusName")] JudicialStatus judicialStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(judicialStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(judicialStatus);
        }

        // GET: Manager/JudicialStatus/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JudicialStatus judicialStatus = db.JudicialStatuses.Find(id);
            if (judicialStatus == null)
            {
                return HttpNotFound();
            }
            return View(judicialStatus);
        }

        // POST: Manager/JudicialStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            JudicialStatus judicialStatus = db.JudicialStatuses.Find(id);
            db.JudicialStatuses.Remove(judicialStatus);
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
