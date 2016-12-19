using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
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
            var o = m.ArtistGetById(id.GetValueOrDefault());

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

        [Authorize(Roles ="Executive")]
        // GET: Artists/Create
        public ActionResult Create()
        {
            var form = new ArtistAddForm();
            form.GenreList = new SelectList(m.GenreGetAll(),"Name","Name");   
            return View(form);
        }

        [Authorize(Roles = "Executive")]
        // POST: Artists/Create
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(ArtistAdd newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                //return the object to view
                var form = m.mapper.Map<ArtistAdd, ArtistAddForm>(newItem);
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                return View(form);
            }

            // Process the input
            var addedItem = m.ArtistAdd(newItem);

            if (addedItem == null)
            {
                //return the object to view
                var form = m.mapper.Map<ArtistAdd, ArtistAddForm>(newItem);
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                return View(form);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.Id });
            }
        }


        //*********************************************
        // ALBUM ADD

        [Authorize(Roles = "Coordinator")]
        // GET: Artists/id/AddAlbum
        [Route("Artists/{id}/AddAlbum")]
        public ActionResult AlbumAdd(int? id)
        {
            var o = m.ArtistGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = new AlbumAddForm();
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                form.ArtistName = o.Name;
                form.ArtistId = o.Id;
                form.ArtistPhoto = o.UrlArtist;
                return View(form);
            }
        }

        [Authorize(Roles = "Coordinator")]
        // POST: Artists/id/AddAlbum
        [HttpPost, ValidateInput(false)]
        [Route("Artists/{id}/AddAlbum")]
        public ActionResult AlbumAdd(AlbumAdd newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                //return the object to view
                var form = m.mapper.Map<AlbumAdd, AlbumAddForm>(newItem);
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                return View(form);
            }

            // Process the input
            var addedItem = m.AlbumAdd(newItem);

            if (addedItem == null)
            {
                //return the object to view
                var form = m.mapper.Map<AlbumAdd, AlbumAddForm>(newItem);
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                return View(form);
            }
            else
            {
                return RedirectToAction("Details", "Albums",new { id = addedItem.Id });
            }
        }



        [Authorize(Roles = "Executive")]
        // GET: Artists/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        [Authorize(Roles = "Executive")]
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

        [Authorize(Roles = "Executive")]
        // GET: Artists/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        [Authorize(Roles = "Executive")]
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
        }
    }
}
