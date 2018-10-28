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
    public class CalculationsController : Controller
    {
        private GunSudexDbContext db = new GunSudexDbContext();

        // GET: Manager/Calculations
        public ActionResult Index()
        {
            return View(db.Calculations.ToList());
        }

        // GET: Manager/Calculations/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calculation calculation = db.Calculations.Find(id);
            if (calculation == null)
            {
                return HttpNotFound();
            }
            return View(calculation);
        }

        // GET: Manager/Calculations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/Calculations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CurentDate,HourFee,ConsumablesCost,EquipmentMaintanance")] Calculation calculation)
        {
            if (ModelState.IsValid)
            {
                db.Calculations.Add(calculation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(calculation);
        }

        // GET: Manager/Calculations/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calculation calculation = db.Calculations.Find(id);
            if (calculation == null)
            {
                return HttpNotFound();
            }
            return View(calculation);
        }

        // POST: Manager/Calculations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CurentDate,HourFee,ConsumablesCost,EquipmentMaintanance")] Calculation calculation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calculation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(calculation);
        }

        // GET: Manager/Calculations/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calculation calculation = db.Calculations.Find(id);
            if (calculation == null)
            {
                return HttpNotFound();
            }
            return View(calculation);
        }

        // POST: Manager/Calculations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            Calculation calculation = db.Calculations.Find(id);
            db.Calculations.Remove(calculation);
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
