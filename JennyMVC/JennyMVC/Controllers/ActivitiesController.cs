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
    public class ActivitiesController : Controller
    {
        private JenniferEntities db = new JenniferEntities();

        // GET: Activities
        public ActionResult Index()
        {
            var activities = db.Activities.Include(a => a.Project).Include(a => a.Staff);
            return View(activities.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            ViewBag.projectID = new SelectList(db.Projects, "projectId", "project_name");
            ViewBag.staffID = new SelectList(db.Staffs, "staffID", "staff_email");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "activityID,projectID,activity_name,staffID,pred_start,pred_finish,act_start,act_finish,pred_cost,act_cost,activity_sequence_no")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.projectID = new SelectList(db.Projects, "projectId", "project_name", activity.projectID);
            ViewBag.staffID = new SelectList(db.Staffs, "staffID", "staff_email", activity.staffID);
            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.projectID = new SelectList(db.Projects, "projectId", "project_name", activity.projectID);
            ViewBag.staffID = new SelectList(db.Staffs, "staffID", "staff_email", activity.staffID);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "activityID,projectID,activity_name,staffID,pred_start,pred_finish,act_start,act_finish,pred_cost,act_cost,activity_sequence_no")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.projectID = new SelectList(db.Projects, "projectId", "project_name", activity.projectID);
            ViewBag.staffID = new SelectList(db.Staffs, "staffID", "staff_email", activity.staffID);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
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
