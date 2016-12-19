using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
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
            var o = m.AlbumGetByIdWithDetails(id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
        }


        //######################################
        //Add track

        [Authorize(Roles ="Clerk")]
        [Route("Albums/{id}/AddTrack")]
        // GET: Albums/id/AddTrack
        public ActionResult AddTrack(int? id)
        {
            var a = m.AlbumGetByIdWithDetails(id.GetValueOrDefault());
            if(a == null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = new TrackAddForm();
                form.AlbumName = a.Name;
                form.AlbumId = a.Id;
                form.UrlAlbumCover = a.UrlAlbum;
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                return View(form);
            }
        }


        [Authorize(Roles = "Clerk")]
        [Route("Albums/{id}/AddTrack")]
        // POST: Albums/id/AddTrack
        [HttpPost]
        public ActionResult AddTrack(TrackAdd newItem)
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
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", "Tracks", new { id = addedItem.Id });
            }
        }

   /*     // GET: Albums/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Albums/Edit/5
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

        // GET: Albums/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Albums/Delete/5
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
        }*/
    }
}
