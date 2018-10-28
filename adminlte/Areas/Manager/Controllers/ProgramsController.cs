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
    public class ProgramsController : Controller
    {
        private GunSudexDbContext db = new GunSudexDbContext();

        // GET: Manager/Programs
        public ActionResult Index()
        {
            var programs = db.Programs.Include(p => p.ItemType);
            return View(programs.ToList());
        }

        // GET: Manager/Programs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Program program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }

        // GET: Manager/Programs/Create
        public ActionResult Create()
        {
            ViewBag.ItemTypeID = new SelectList(db.ItemTypes, "ItemTypeID", "ItemType1");
            return View();
        }

        // POST: Manager/Programs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProgramID,ItemTypeID,Standard,ProgramNameFull,ProgramNameShort")] Program program)
        {
            if (ModelState.IsValid)
            {
                program.ProgramID = Guid.NewGuid();
                db.Programs.Add(program);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemTypeID = new SelectList(db.ItemTypes, "ItemTypeID", "ItemType1", program.ItemTypeID);
            return View(program);
        }

        // GET: Manager/Programs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Program program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemTypeID = new SelectList(db.ItemTypes, "ItemTypeID", "ItemType1", program.ItemTypeID);
            return View(program);
        }

        // POST: Manager/Programs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProgramID,ItemTypeID,Standard,ProgramNameFull,ProgramNameShort")] Program program)
        {
            if (ModelState.IsValid)
            {
                db.Entry(program).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemTypeID = new SelectList(db.ItemTypes, "ItemTypeID", "ItemType1", program.ItemTypeID);
            return View(program);
        }

        // GET: Manager/Programs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Program program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }

        // POST: Manager/Programs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Program program = db.Programs.Find(id);
            db.Programs.Remove(program);
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
