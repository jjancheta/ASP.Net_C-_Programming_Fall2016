using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    [Authorize]
    public class TracksController : Controller
    {
        private Manager m = new Manager();
        // GET: Tracks
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        // GET: Tracks/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.TrackGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
            
        }


        [Authorize(Roles = "Coordinator")]
        // GET: Tracks/Edit/5
        public ActionResult Edit(int? id)
        {
            var o = m.TrackGetById(id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var editForm = m.mapper.Map<TrackWithDetails, TrackEditForm>(o);
                editForm.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                return View(editForm);
            }
        }


        [Authorize(Roles = "Coordinator")]
        // POST: Tracks/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, TrackEdit newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return RedirectToAction("edit", new { id = newItem.Id});
            }

            if (id.GetValueOrDefault() != newItem.Id)
            {
                return RedirectToAction("index");
            }

            // Attempt to do the update by calling the method at the manager class
            var editedItem = m.TrackEdit(newItem);

            if (editedItem == null)
            {
                // display edit form again
                return RedirectToAction("edit", new { id = newItem.Id });
            }
            else
            {
                // Show the all employees including the edited data
                return RedirectToAction("index");
            }
        }

        [Authorize(Roles ="Coordinator")]
        // GET: Tracks/Delete/5
        public ActionResult Delete(int? id)
        {
            var o = m.TrackGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
        }

        [Authorize(Roles = "Coordinator")]
        // POST: Tracks/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection newItem)
        {
            var result = m.TrackDelete(id.GetValueOrDefault());

            // In the end, we should just redirect to the list view
            return RedirectToAction("index");
        }

        /*   // GET: Tracks/Create
       public ActionResult Create()
       {
           return View();
       }

       // POST: Tracks/Create
       [HttpPost]
       public ActionResult Create(FormCollection collection)
       {
           try
           {
               // TODO: Add insert logic here

               return RedirectToAction("Index");
           }
           catch
           {
               return View();
           }
       } */
    }
}
