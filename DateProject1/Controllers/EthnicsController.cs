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
    public class EthnicsController : Controller
    {
        private datedbEntities db = new datedbEntities();

        // GET: Ethnics
        public ActionResult Index()
        {
            return View(db.Ethnics.ToList());
        }

        // GET: Ethnics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ethnic ethnic = db.Ethnics.Find(id);
            if (ethnic == null)
            {
                return HttpNotFound();
            }
            return View(ethnic);
        }

        // GET: Ethnics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ethnics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EthnicID,Ethnicity")] Ethnic ethnic)
        {
            if (ModelState.IsValid)
            {
                db.Ethnics.Add(ethnic);
                db.SaveChanges();
                return RedirectToAction("../AgePreferences/Create");
            }

            return View(ethnic);
        }

        // GET: Ethnics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ethnic ethnic = db.Ethnics.Find(id);
            if (ethnic == null)
            {
                return HttpNotFound();
            }
            return View(ethnic);
        }

        // POST: Ethnics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EthnicID,Ethnicity")] Ethnic ethnic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ethnic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ethnic);
        }

        // GET: Ethnics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ethnic ethnic = db.Ethnics.Find(id);
            if (ethnic == null)
            {
                return HttpNotFound();
            }
            return View(ethnic);
        }

        // POST: Ethnics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ethnic ethnic = db.Ethnics.Find(id);
            db.Ethnics.Remove(ethnic);
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
