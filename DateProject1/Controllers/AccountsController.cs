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
    public class AccountsController : Controller
    {
        private datedbEntities1 db = new datedbEntities1();

        // GET: Accounts
        public ActionResult Index(string option, string search)
        {
            if (option == "Username")
            {
                return View(db.Accounts.Where(m => m.Username.StartsWith(search) || search == null).ToList());
            }
            if (option == "Email")
            {
                return View(db.Accounts.Where(m => m.Email.StartsWith(search) || search == null).ToList());
            }
            if (option == "Occupation")
            {
                return View(db.Accounts.Where(m => m.Person.Occupation.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Accounts.Where(m => m.Education.School.StartsWith(search) || search == null).ToList());
            }
            //var accounts = db.Accounts.Include(a => a.Common).Include(a => a.Education).Include(a => a.Person);
            //return View(accounts.ToList());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            ViewBag.CommonID = new SelectList(db.Commons, "CommonID", "Sports");
            ViewBag.EducationID = new SelectList(db.Educations, "EducationID", "School");
            ViewBag.PersonID = new SelectList(db.People, "PersonID", "Occupation");

            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountID,Username,Email,Password,PhoneNumber,CommonID,EducationID,PersonID")] Account account)
        {
            if (ModelState.IsValid)
            {
                var z = 0;
                foreach (var item in db.Commons)
                {
                    if (z < item.CommonID)
                    {
                        z = item.CommonID;
                    }
                }
                account.CommonID = z;

                var y = 0;
                foreach (var item in db.Educations)
                {
                    if (y < item.EducationID)
                    {
                        y = item.EducationID;
                    }
                }
                account.EducationID = y;

                var w = 0;
                foreach (var item in db.People)
                {
                    if (w < item.PersonID)
                    {
                        w = item.PersonID;
                    }
                }
                account.PersonID = w;
                db.Accounts.Add(account);
                Message x = new Message();
                x.Body = "Welcome to Class-Mates!";
                x.FromID = account.AccountID;
                x.Outbox = "No Reply";
                x.AccountID = account.AccountID;
                db.Messages.Add(x);
                db.SaveChanges();
                return RedirectToAction("../Home/ThankYou");
            }

            ViewBag.CommonID = new SelectList(db.Commons, "CommonID", "Sports, Music, Food", account.CommonID);
            ViewBag.EducationID = new SelectList(db.Educations, "EducationID", "School", account.EducationID);
            ViewBag.PersonID = new SelectList(db.People, "PersonID", "Occupation", account.PersonID);
            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommonID = new SelectList(db.Commons, "CommonID", "Sports, Music, Food", account.CommonID);
            ViewBag.EducationID = new SelectList(db.Educations, "EducationID", "School", account.EducationID);
            ViewBag.PersonID = new SelectList(db.People, "PersonID", "Occupation", account.PersonID);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountID,Username,Email,Password,PhoneNumber,CommonID,EducationID,PersonID")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommonID = new SelectList(db.Commons, "CommonID", "Sports", account.CommonID);
            ViewBag.EducationID = new SelectList(db.Educations, "EducationID", "School", account.EducationID);
            ViewBag.PersonID = new SelectList(db.People, "PersonID", "Occupation", account.PersonID);
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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

        public ActionResult Matches()
        {
            var accounts = db.Accounts.Include(a => a.Common).Include(a => a.Education).Include(a => a.Person);
            return View(accounts.ToList());
        }

        public ActionResult ToMessage()
        {
            return RedirectToAction("Create", "Messages");
        }

        public ActionResult ToProfile()
        {
            return RedirectToAction("Prof", "People");
        }
    }
}
