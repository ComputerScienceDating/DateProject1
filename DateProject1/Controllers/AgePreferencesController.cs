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
    public class AgePreferencesController : Controller
    {
        private datedbEntities1 db = new datedbEntities1();

        // GET: AgePreferences
        public ActionResult Index()
        {
            return View(db.AgePreferences.ToList());
        }

        // GET: AgePreferences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgePreference agePreference = db.AgePreferences.Find(id);
            if (agePreference == null)
            {
                return HttpNotFound();
            }
            return View(agePreference);
        }

        // GET: AgePreferences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgePreferences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AgePreferenceID,MinAge,MaxAge")] AgePreference agePreference)
        {
            if (ModelState.IsValid)
            {
                db.AgePreferences.Add(agePreference);
                db.SaveChanges();
                return RedirectToAction("../People/Create");
            }

            return View(agePreference);
        }

        // GET: AgePreferences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgePreference agePreference = db.AgePreferences.Find(id);
            if (agePreference == null)
            {
                return HttpNotFound();
            }
            return View(agePreference);
        }

        // POST: AgePreferences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AgePreferenceID,MinAge,MaxAge")] AgePreference agePreference)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agePreference).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agePreference);
        }

        // GET: AgePreferences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgePreference agePreference = db.AgePreferences.Find(id);
            if (agePreference == null)
            {
                return HttpNotFound();
            }
            return View(agePreference);
        }

        // POST: AgePreferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AgePreference agePreference = db.AgePreferences.Find(id);
            db.AgePreferences.Remove(agePreference);
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
