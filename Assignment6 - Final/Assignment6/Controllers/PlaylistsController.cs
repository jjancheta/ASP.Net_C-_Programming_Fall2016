using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment6.Controllers
{
    public class PlaylistsController : Controller
    {
        private Manager m = new Manager();
        // GET: Playlists
        public ActionResult Index()
        {
            return View(m.PlaylistGetAll());
        }

        // GET: Playlists/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.PlaylistGetByIdWithTracks(id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
        }

        // GET: Playlists/Create
        public ActionResult Create()
        {
            var form = new PlaylistWithTracksAddForm();
            form.TracksList = new MultiSelectList(m.TrackGetAll(), "TrackId", "NameFull");
            return View(form);
        }

        // POST: Playlists/Create
        [HttpPost]
        public ActionResult Create(PlaylistWithTracksAdd newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.PlaylistAndTracksAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.PlaylistId });
            }
        }

        // GET: Playlists/Edit/5
        public ActionResult Edit(int? id)
        {
            var o = m.PlaylistGetByIdWithTracks(id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var trackForm = m.mapper.Map<PlaylistWithTracks, PlaylistEditWithTracksForm>(o);

                // For the multi select list, configure the "selected" items
                // Notice the use of the Select() method, 
                // which allows us to select/return/use only some properties from the source
                var selectedValues = o.Tracks.Select(t => t.TrackId);

                trackForm.TracksList = new MultiSelectList(m.TrackGetAll(),"TrackId", "NameFull", selectedValues);
                return View(trackForm);
            }
            
        }

        // POST: Playlists/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, PlaylistEditWithTracks newItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id = newItem.PlaylistId });
            }

            if(id.GetValueOrDefault() != newItem.PlaylistId)
            {
                return RedirectToAction("Index");
            }

            var editedItem = m.PlaylistEditTracks(newItem);

            if (editedItem == null)
            {
                return RedirectToAction("Edit", new { id = newItem.PlaylistId });
            }
            else
            {
                return RedirectToAction("Details", new { id = editedItem.PlaylistId });
            }
        }

        /*
        // GET: Playlists/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Playlists/Delete/5
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
