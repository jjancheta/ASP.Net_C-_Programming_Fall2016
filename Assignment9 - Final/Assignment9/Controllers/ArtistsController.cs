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
            //var o = m.ArtistGetById(id.GetValueOrDefault());
            var o = m.ArtistWithMediaItemsGetById(id.GetValueOrDefault());

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
                return View(newItem);
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


        [Authorize(Roles = "Executive")]
        // GET: Artists/Edit/5
        public ActionResult Edit(int? id)
        {

            var o = m.ArtistGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = new ArtistEditForm();
                form = m.mapper.Map<ArtistBase, ArtistEditForm>(o);
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                return View(form);
            }
        }


        [Authorize(Roles = "Executive")]
        // POST: Artists/Edit/5
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int? id, ArtistEditForm newItem)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("edit", new { id = newItem.Id });
            }

            if (id.GetValueOrDefault() != newItem.Id)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("index");
            }

            // Attempt to do the update
            var editedItem = m.ArtistEdit(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.Id });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("details", new { id = newItem.Id });
            }
        }

        [Authorize(Roles = "Executive")]
        // GET: Artists/Delete/5
        public ActionResult Delete(int? id)
        {
            var o = m.ArtistWithMediaItemsGetById(id.GetValueOrDefault());
            if (o == null)
            {
                return RedirectToAction("index");
            }
            {
                return View(o);
            }
        }

        [Authorize(Roles = "Executive")]
        // POST: Artists/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var result = m.ArtistDelete(id.GetValueOrDefault());

            return RedirectToAction("index");
        }



        //*********************************************
        // ALBUM ADD

        [Authorize(Roles = "Coordinator")]
        // GET: Artists/id/AlbumAdd
        [Route("Artists/{id}/AlbumAdd")]
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
        // POST: Artists/id/AlbumAdd
        [HttpPost, ValidateInput(false)]
        [Route("Artists/{id}/AlbumAdd")]
        public ActionResult AlbumAdd(AlbumAdd newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)    //this should not be necessary anymore as validation of inputs are done in the view - unobtrosive validation
            {
                return View(newItem);
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


        //*********************************************
        // MEDIAITEM ADD

        [Authorize(Roles = "Executive")]
        // GET: Artists/id/MediaItemAdd
        [Route("Artists/{id}/MediaItemAdd")]
        public ActionResult MediaItemAdd(int? id)
        {
            var o = m.ArtistGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = new MediaItemAddForm();
                form.ArtistName = o.Name;
                form.ArtistId = o.Id;
                form.ArtistPhoto = o.UrlArtist;
                return View(form);
            }
        }

        [Authorize(Roles = "Executive")]
        // POST: Artists/id/MediaItemAdd
        [HttpPost]
        [Route("Artists/{id}/MediaItemAdd")]
        public ActionResult MediaItemAdd(MediaItemAdd newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)    //this should not be necessary anymore as validation of inputs are done in the view - unobtrosive validation
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.MediaItemAdd(newItem);

            if (addedItem == null)
            {
                //return the object to view
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", "Artists", new { id = addedItem.Id });
            }
        }



      

   
    }
}
