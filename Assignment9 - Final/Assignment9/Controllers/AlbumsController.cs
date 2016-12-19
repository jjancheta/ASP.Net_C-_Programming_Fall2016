using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    [Authorize]
    public class AlbumsController : Controller
    {
        private Manager m = new Manager();
        // GET: Albums
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.AlbumGetById(id.GetValueOrDefault());

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


        [Authorize(Roles = "Coordinator")]
        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            var o = m.AlbumGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                var form = new AlbumEditForm();
                form = m.mapper.Map<AlbumWithDetails, AlbumEditForm>(o);
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                foreach (var a in o.Artists)
                {
                    form.ArtistId = a.Id;
                    form.ArtistName = a.Name;
                    form.ArtistPhoto = a.UrlArtist;
                }
                return View(form);
            }
        }

        [Authorize(Roles = "Coordinator")]
        // POST: Albums/Edit/5
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int? id, AlbumEditForm editItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("edit", new { id = editItem.Id });
            }

            if (id.GetValueOrDefault() != editItem.Id)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("index");
            }

            // Attempt to do the update
            var editedItem = m.AlbumEdit(editItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = editItem.Id });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("details", new { id = editItem.Id });
            }
        }

        [Authorize(Roles = "Coordinator")]
        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            var o = m.AlbumGetById(id.GetValueOrDefault());

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


        [Authorize(Roles = "Coordinator")]
        // POST: Albums/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var result = m.AlbumDelete(id.GetValueOrDefault());

            return RedirectToAction("index");
        }

        /*  // GET: Albums/Create
          public ActionResult Create()
          {
              return View();
          }

          // POST: Albums/Create
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


        //*********************************************
        // TRACK ADD

        [Authorize(Roles ="Clerk")]
        // GET: Albums/id/TrackAdd
        [Route("Albums/{id}/TrackAdd")]
        public ActionResult TrackAdd(int? id)
        {
            var o = m.AlbumGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = new TrackAddForm();
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                form.AlbumName = o.Name;
                form.AlbumId = o.Id;
                form.AlbumCover = o.UrlAlbum;
                return View(form);
            }
        }

        [Authorize(Roles = "Clerk")]
        // POST: Albums/id/TrackAdd
        [Route("Albums/{id}/TrackAdd")]
        [HttpPost]
        public ActionResult TrackAdd(TrackAddWithMedia newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.TrackAdd(newItem);

            if (addedItem == null)
            {
                //return the object to view
                var form = m.mapper.Map<TrackAddWithMedia, TrackAddForm>(newItem);
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                return View(form);
            }
            else
            {
                return RedirectToAction("Details", "Tracks", new { id = addedItem.Id });
            }
        }

        
    }
}
