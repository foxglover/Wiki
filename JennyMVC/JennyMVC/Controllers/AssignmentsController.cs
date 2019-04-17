using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JennyMVC;

namespace JennyMVC.Controllers
{
    public class AssignmentsController : Controller
    {
        private JenniferEntities db = new JenniferEntities();

        // GET: Assignments
        public ActionResult Index()
        {
            var assignments = db.Assignments.Include(a => a.Activity).Include(a => a.Staff);
            return View(assignments.ToList());
        }

        // GET: Assignments/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // GET: Assignments/Create
        public ActionResult Create()
        {
            ViewBag.activityID = new SelectList(db.Activities, "activityID", "activity_name");
            ViewBag.staffID = new SelectList(db.Staffs, "staffID", "staff_email");
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "taskID,staffID,task_name,activityID,pred_start,act_start,pred_finish,act_finish,pred_cost,act_cost,task_sequence")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Assignments.Add(assignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.activityID = new SelectList(db.Activities, "activityID", "activity_name", assignment.activityID);
            ViewBag.staffID = new SelectList(db.Staffs, "staffID", "staff_email", assignment.staffID);
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.activityID = new SelectList(db.Activities, "activityID", "activity_name", assignment.activityID);
            ViewBag.staffID = new SelectList(db.Staffs, "staffID", "staff_email", assignment.staffID);
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "taskID,staffID,task_name,activityID,pred_start,act_start,pred_finish,act_finish,pred_cost,act_cost,task_sequence")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.activityID = new SelectList(db.Activities, "activityID", "activity_name", assignment.activityID);
            ViewBag.staffID = new SelectList(db.Staffs, "staffID", "staff_email", assignment.staffID);
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Assignment assignment = db.Assignments.Find(id);
            db.Assignments.Remove(assignment);
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
