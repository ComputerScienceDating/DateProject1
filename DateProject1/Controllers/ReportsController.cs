using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DateProject1.Models;

namespace DateProject1.Controllers
{
    public class ReportsController : Controller
    {
        private int selectID = 1;
        private datedbEntities1 db = new datedbEntities1();

        // GET: Reports
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string option, string search)
        {
            if (option == "Username")
            {
                return View(db.Reports.Where(r => r.Account.Username.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Reports.Where(r => r.Reason.StartsWith(search) || search == null).ToList());
            }
        }

        // GET: Reports/Details/5
        [Authorize (Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: Reports/Create
        // GET: Reports/Create
        [Authorize]
        public ActionResult Create(string option, string reason, int? id, Message report)
        {
            report = db.Messages.Find(id);
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Username");
            ViewBag.AID = report.Account1.Username;
            if (option == "Inappropriate")
            {
                ViewBag.Reason = "Inappropriate";
            }
            else if (option == "Bot")
            {
                ViewBag.Reason = "Bot";
            }
            else if (option == "Underage")
            {
                ViewBag.Reason = "Underage";
            }
            else
            {
                ViewBag.Reason = "Harassment";
            }
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportID,AccountID")] Report report, string returnURL, string option, string reason, Message reportM, int? id)
        {
            if (ModelState.IsValid)
            {
                reportM = db.Messages.Find(id);
                report.AccountID = reportM.Account1.AccountID;
                if (option == "Inappropriate")
                {
                    Session["Reason"] = "Inappropriate";
                    ViewBag.Reason = "Inappropriate";
                    report.Reason = "Inappropriate";
                }
                else if (option == "Bot")
                {
                    Session["Reason"] = "Bot";
                    ViewBag.Reason = "Bot";
                    report.Reason = "Bot";
                }
                else if (option == "Underage")
                {
                    Session["Reason"] = "Underage";
                    ViewBag.Reason = "Underage";
                    report.Reason = "Underage";
                }
                else
                {
                    Session["Reason"] = "Harassment";
                    ViewBag.Reason = "Harassment";
                    report.Reason = "Harassment";
                }

                db.Reports.Add(report);
                db.SaveChanges();
                return RedirectToAction("Inbox", "Messages");
            }
            var v = db.Accounts.OrderByDescending(m => m.Username).First();
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Username", v);
            return View(report);
        }

        // GET: Reports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Username", report.AccountID);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportID,AccountID,Reason")] Report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Username", report.AccountID);
            return View(report);
        }

        // GET: Reports/Delete/5
        [Authorize (Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Report report = db.Reports.Find(id);
            db.Reports.Remove(report);
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
