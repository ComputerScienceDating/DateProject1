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
    public class CommonsController : Controller
    {
        private datedbEntities1 db = new datedbEntities1();

        // GET: Commons
        public ActionResult Index()
        {
            return View(db.Commons.ToList());
        }

        // GET: Commons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Common common = db.Commons.Find(id);
            if (common == null)
            {
                return HttpNotFound();
            }
            return View(common);
        }

        // GET: Commons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Commons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommonID,Sports,Music,Food,Books,Shows,Movies")] Common common)
        {
            if (ModelState.IsValid)
            {
                db.Commons.Add(common);
                db.SaveChanges();
                return RedirectToAction("../Educations/Create");
            }

            return View(common);
        }

        // GET: Commons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Common common = db.Commons.Find(id);
            if (common == null)
            {
                return HttpNotFound();
            }
            return View(common);
        }

        // POST: Commons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommonID,Sports,Music,Food,Books,Shows,Movies")] Common common)
        {
            if (ModelState.IsValid)
            {
                db.Entry(common).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(common);
        }

        // GET: Commons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Common common = db.Commons.Find(id);
            if (common == null)
            {
                return HttpNotFound();
            }
            return View(common);
        }

        // POST: Commons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Common common = db.Commons.Find(id);
            db.Commons.Remove(common);
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
