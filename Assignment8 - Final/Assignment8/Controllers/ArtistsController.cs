using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    [Authorize]
    public class ArtistsController : Controller
    {
        private Manager m = new Manager();
        
        // GET: Artists
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.ArtistGetByIdWithAlbum(id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
           
        }


        // GET: Artists/Create
        [Authorize(Roles ="Executive")]
        public ActionResult Create()
        {
            var form = new ArtistAddForm();
            form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
            return View(form);
        }

        // POST: Artists/Create
        [Authorize(Roles = "Executive")]
        [HttpPost]
        public ActionResult Create(ArtistAdd newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.ArtistAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", new { id = addedItem.Id});
            }
        }

        //######################################
        //Add Album

        [Authorize(Roles = "Coordinator")]
        // GET: Artists/5/AddAlbum
        [Route("Artists/{id}/AddAlbum")]
        public ActionResult AddAlbum(int? id)
        {
            var o = m.ArtistGetByIdWithAlbum(id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = new AlbumAddForm();
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                form.ArtistName = o.Name;
                var selectedValues = new List<int> { o.Id };
                form.ArtistList = new MultiSelectList(m.ArtistGetAll(), "Id", "Name", selectedValues);
                form.TrackList = new MultiSelectList(m.TrackGetAll(), "Id", "Name");
                return View(form);

            }
          
        }

        // POST: Artists/5/AddAlbum
        [Authorize(Roles = "Coordinator")]
        [Route("Artists/{id}/AddAlbum")]
        [HttpPost]
        public ActionResult AddAlbum(AlbumAdd newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.AlbumAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details","Albums", new { id = addedItem.Id });
            }
        }

    /*    // GET: Artists/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Artists/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Artists/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Artists/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        } */
    }
}
