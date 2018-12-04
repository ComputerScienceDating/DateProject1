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
    public class HomeController : Controller
    {
        private datedbEntities1 db = new datedbEntities1();

        public ActionResult ThankYou()
        {
            return View();
        }
        public ActionResult Index()
        {
            var messages = db.Messages.Include(m => m.Account).Include(m => m.Account1);
            
            return View(messages.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ToProfile(int? id)
        {
            foreach (var item in db.Accounts)
            {
                if (item.AccountID == id)
                {
                    ViewBag.OtherProfile = item.Email;
                }
            }
            var accounts = db.Accounts.Include(a => a.Common).Include(a => a.Education).Include(a => a.Person);
            return View(accounts.ToList());
        }
    }
}