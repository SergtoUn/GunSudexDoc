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
    public class ExpertsController : Controller
    {
        private GunSudexDbContext db = new GunSudexDbContext();

        // GET: Manager/Experts
        public ActionResult Index()
        {
            var experts = db.Experts.Include(e => e.LabRoom).Include(e => e.Position).Include(e => e.Role);
            return View(experts.ToList());
        }

        // GET: Manager/Experts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expert expert = db.Experts.Find(id);
            if (expert == null)
            {
                return HttpNotFound();
            }
            return View(expert);
        }

        // GET: Manager/Experts/Create
        public ActionResult Create()
        {
            ViewBag.LabRoomID = new SelectList(db.LabRooms, "LabRoomID", "LabRoomNumber");
            
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "PositionDescr");
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleDescr");
            return View();
        }

        // POST: Manager/Experts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpertID,PositionID,RoleID,LabRoomID,FamilyName,FirstName,MidName,Email,MPhone,Portrait,IP")] Expert expert)
        {
            if (ModelState.IsValid)
            {
                expert.ExpertID = Guid.NewGuid();
                db.Experts.Add(expert);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LabRoomID = new SelectList(db.LabRooms, "LabRoomID", "LabRoomNumber", expert.LabRoomID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "PositionDescr", expert.PositionID);
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleDescr", expert.RoleID);
            return View(expert);
        }

        // GET: Manager/Experts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expert expert = db.Experts.Find(id);
            if (expert == null)
            {
                return HttpNotFound();
            }
            ViewBag.LabRoomID = new SelectList(db.LabRooms, "LabRoomID", "LabRoomNumber", expert.LabRoomID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "PositionDescr", expert.PositionID);
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleDescr", expert.RoleID);
            return View(expert);
        }

        // POST: Manager/Experts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpertID,PositionID,RoleID,LabRoomID,FamilyName,FirstName,MidName,Email,MPhone,Portrait,IP")] Expert expert)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LabRoomID = new SelectList(db.LabRooms, "LabRoomID", "LabRoomNumber", expert.LabRoomID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "PositionDescr", expert.PositionID);
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleDescr", expert.RoleID);
            return View(expert);
        }

        // GET: Manager/Experts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expert expert = db.Experts.Find(id);
            if (expert == null)
            {
                return HttpNotFound();
            }
            return View(expert);
        }

        // POST: Manager/Experts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Expert expert = db.Experts.Find(id);
            db.Experts.Remove(expert);
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
