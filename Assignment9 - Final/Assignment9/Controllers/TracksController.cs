using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
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
                // Pass the object to the view
                return View(o);
            }
           
        }

        /*  // GET: Tracks/Create
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
          }*/


        [Authorize(Roles = "Clerk")]
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
                // Pass the object to the view
                var form = new TrackEditForm();
                form = m.mapper.Map<TrackBase, TrackEditForm>(o);
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                form.AudioUpload = $"/clip/{id}";
                return View(form);
            }
        }

        [Authorize(Roles = "Clerk")]
        // POST: Tracks/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, TrackEditForm editItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(editItem);
            }

            // Process the input
            var o = m.TrackEdit(editItem);

            if (o == null)
            {
                //return the object to view
                var form = new TrackEditForm();
                form = m.mapper.Map<TrackBase, TrackEditForm>(o);
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                form.AudioUpload = $"/clip/{id}";
                return View(form);
            }
            else
            {
                return RedirectToAction("Details", "Tracks", new { id = o.Id });
            }
        }

        [Authorize(Roles = "Clerk")]
        // GET: Tracks/Delete/5
        public ActionResult Delete(int? id)
        {
            var o = m.TrackGetById(id.GetValueOrDefault());
            if(o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
           
        }

        [Authorize(Roles = "Clerk")]
        // POST: Tracks/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, HttpPostedFileBase file)
        {
            var result = m.TrackDelete(id.GetValueOrDefault(), file);

            return RedirectToAction("index");
        }
    }
}
