using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class PhonesController : Controller
    {
        //Collection of phones
        private List<PhoneBase> Phones;

        public PhonesController()
        {
            Phones = new List<PhoneBase>();
            
            //add to the collection using original syntax
            var priv = new PhoneBase();
            priv.Id = 1;
            priv.PhoneName = "Priv";
            priv.Manufacturer = "BlackBerry";
            priv.DateReleased = new DateTime(2015, 11, 6);
            priv.MSRP = 799;
            priv.ScreenSize = 5.43;
            Phones.Add(priv);       //add to the collection

            //add to the collection using the newer object initializer syntax
            var galaxy = new PhoneBase
            {
                Id = 2,
                PhoneName = "Galaxy S6",
                Manufacturer = "Samsung",
                DateReleased = new DateTime(2015, 4, 10),
                MSRP = 649,
                ScreenSize = 5.1
            };
            Phones.Add(galaxy);     //add to the collection

            //using object initializer syntax, directly as the argument to the Phones.Add() method
            Phones.Add(new PhoneBase
            {
                Id = 3,
                PhoneName = "iPhone 6s",
                Manufacturer = "Apple",
                DateReleased = new DateTime(2015,9,25),
                MSRP = 649,
                ScreenSize = 4.7
            });
        }

       
        // GET: Phones
        public ActionResult Index()
        {
            return View(Phones);
        }

        // GET: Phones/Details/5
        public ActionResult Details(int id)      //what is this int id argument?
        {
            return View(Phones[id-1]);
        }

        // GET: Phones/Create
        public ActionResult Create()
        {
            return View(new PhoneBase());
        }

        // POST: Phones/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {

            // TODO: Add insert logic here
            var newItem = new PhoneBase();
            newItem.Id = Phones.Count + 1;     //last record id + 1 for new record to be added
            
            //configure the string properties
            newItem.PhoneName = collection["PhoneName"];
            newItem.Manufacturer = collection["Manufacturer"];
            
            //configure the date; it comes into the method as a string
            newItem.DateReleased = Convert.ToDateTime(collection["DateReleased"]);

            //configure the numbers
            int msrp;
            double ss;
            bool isNumber;

            //convert strings to numeric data
            isNumber = Int32.TryParse(collection["MSRP"], out msrp);
            newItem.MSRP = msrp;

            isNumber = double.TryParse(collection["ScreenSize"], out ss);
            newItem.ScreenSize = ss;

            //add to the collection
            Phones.Add(newItem);
            return View("Details", newItem);       //calls the Details method, and pass the newly added record as an argument - OK
        }   
    }
}
