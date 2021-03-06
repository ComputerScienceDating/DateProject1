﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DateProject1.Models;
using Microsoft.AspNet.Identity;
using PagedList;
namespace DateProject1.Controllers
{
    public class PeopleController : Controller
    {
        private datedbEntities1 db = new datedbEntities1();

        // GET: People
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string option, string search, int? pageNumber)
        {
            if (option == "FirstName")
            {
                return View(db.People.Where(r => r.FirstName.StartsWith(search) || search == null).ToList().ToPagedList(pageNumber ?? 1, 5));
            }
            else if (option == "LastName")
            {
                return View(db.People.Where(r => r.LastName.StartsWith(search) || search == null).ToList().ToPagedList(pageNumber ?? 1, 5));
            }
            else if (option == "Gender")
            {
                return View(db.People.Where(r => r.Gender.StartsWith(search) || search == null).ToList().ToPagedList(pageNumber ?? 1, 5));
            }
            else
            {
                return View(db.People.Where(r => r.SexOrientation.StartsWith(search) || search == null).ToList().ToPagedList(pageNumber ?? 1, 5));
            }
            //var people = db.People.Include(p => p.AgePreference);
            // return View(people.ToList());
        }

        public ActionResult Prof()
        {
            return View(db.Accounts.Where(a => a.Email == User.Identity.Name).ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            ViewBag.AgePreferenceID = new SelectList(db.AgePreferences, "AgePreferenceID", "AgePreferenceID");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonID,EthnicID,AgePreferenceID,EthnicPref,Occupation,FirstName,LastName,DOB,SexOrientation,Gender,NickName,Photo,Bio")] Person person, HttpPostedFileBase image_file)
        {

            if (ModelState.IsValid)
            {
                if (image_file != null)
                {
                    person.Photo = new byte[image_file.ContentLength];
                    image_file.InputStream.Read(person.Photo, 0, image_file.ContentLength);
                }
                var x = 0;
                foreach (var item in db.AgePreferences)
                {
                    if (x < item.AgePreferenceID)
                    {
                        x = item.AgePreferenceID;
                    }
                }
                person.AgePreferenceID = x;
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("../Accounts/Create");
            }

            ViewBag.AgePreferenceID = new SelectList(db.AgePreferences, "AgePreferenceID", "AgePreferenceID", person.AgePreferenceID);
            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgePreferenceID = new SelectList(db.AgePreferences, "AgePreferenceID", "AgePreferenceID", person.AgePreferenceID);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonID,EthnicID,AgePreferenceID,EthnicPref,Occupation,FirstName,LastName,DOB,SexOrientation,Gender,NickName,Photo,Bio")] Person person , HttpPostedFileBase image_file)
        {
            if (ModelState.IsValid)
            {
                if (image_file != null)
                {
                    person.Photo = new byte[image_file.ContentLength];
                    image_file.InputStream.Read(person.Photo, 0, image_file.ContentLength);
                }
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Prof");
            }
            ViewBag.AgePreferenceID = new SelectList(db.AgePreferences, "AgePreferenceID", "AgePreferenceID", person.AgePreferenceID);
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
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
