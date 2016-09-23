using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3.Controllers
{
    public class HomeController : Controller
    {
        // Reference to the data manager
        private Manager m = new Manager();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Assignment 3";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Send your comments";

            return View();
        }
    }
}