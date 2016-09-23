using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3.Controllers
{
    public class EmployeesController : Controller
    {
        private Manager m = new Manager();
        
        // GET: Employees
        public ActionResult Index()
        {
            return View(m.EmployeeGetAll());
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            var o = m.EmployeeGetById(id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Map EmployeeBase to EmployeeEditContactInfoForm before passing to view
                var editForm = m.mapper.Map<EmployeeBase, EmployeeEditContactInfoForm>(o);
                return View(editForm);
            }
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, EmployeeEditContactInfo newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return RedirectToAction("edit", new { id = newItem.EmployeeId });
            }

            if (id.GetValueOrDefault() != newItem.EmployeeId)
            {
                return RedirectToAction("index");
            }

            // Attempt to do the update by calling the method at the manager class
            var editedItem = m.EmployeeEditContactInfo(newItem);

            if (editedItem == null)
            {
                // display edit form again
                return RedirectToAction("edit", new { id = newItem.EmployeeId });
            }
            else
            {
                // Show the all employees including the edited data
                return RedirectToAction("index");
            }
        }
    }
}
