using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Controllers
{
    public class EmployeesController : Controller
    {
        private Manager m = new Manager();
        
        // GET: Employees
        public ActionResult Index()
        {
            return View(m.EmployeeGetAll());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.EmployeeGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View(new EmployeeAdd());
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(EmployeeAdd newEmployee)
        {
            // TODO: Add insert logic here
            if (!ModelState.IsValid)
            {
                return View(newEmployee);
            }

            var addedEmployee = m.EmployeeAdd(newEmployee);

            if (addedEmployee == null)
            {
                return View(newEmployee);
            }
            else
            {
                return RedirectToAction("details", new { id = addedEmployee.EmployeeId });
            }

        }   
    }
}
