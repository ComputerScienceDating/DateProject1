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
    public class MessagesController : Controller
    {
        private datedbEntities1 db = new datedbEntities1();
        private int FID;
        // GET: Messages
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string option, string search)
        {
            if (option == "UsernameFID")
            {
                return View(db.Messages.Where(m => m.Account.Username.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Messages.Where(m => m.Account1.Username.StartsWith(search) || search == null).ToList());
            }
            //var messages = db.Messages.Include(m => m.Account).Include(m => m.Account1);
            //return View(messages.ToList()); 
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create(int? id)
        {
            var store = db.Accounts.Where(a => a.Email == User.Identity.Name).FirstOrDefault();
            ViewBag.FromID = store.Username;
            ViewBag.FID = store.Messages1;
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Username");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MessageID,AccountID,Body,Outbox,FromID")] Message message)
        {
            if (ModelState.IsValid)
            {
                var store = db.Accounts.Where(a => a.Email == User.Identity.Name).FirstOrDefault();
                ViewBag.FID = store.AccountID;
                message.FromID = ViewBag.FID;
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Outgoing");
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Username", message.AccountID);
            return View(message);
        }

        public ActionResult Inbox(string option, string search)
        {
            if (option == "Username")
            {
                return View(db.Messages.Where(m => (m.Account1.Username.StartsWith(search) || search == null) && m.Account.Email == User.Identity.Name).ToList());
            }
            else
            {
                return View(db.Messages.Where(m => (m.Outbox.StartsWith(search) || search == null) && m.Account.Email == User.Identity.Name).ToList());
            }
        }
        public ActionResult Outgoing(string option, string search)
        {
            if (option == "Username")
            {
                return View(db.Messages.Where(m => (m.Account.Username.StartsWith(search) || search == null) && m.Account1.Email == User.Identity.Name).ToList());
            }
            else
            {
                return View(db.Messages.Where(m => (m.Outbox.StartsWith(search) || search == null) && m.Account1.Email == User.Identity.Name).ToList());
            }
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.FromID = new SelectList(db.Accounts, "AccountID", "Username", message.FromID);
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Username", message.AccountID);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MessageID,AccountID,Body,Outbox,FromID")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Outgoing");
            }
            ViewBag.FromID = new SelectList(db.Accounts, "AccountID", "Username", message.FromID);
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Username", message.AccountID);
            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Inbox");
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
