using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class TrackAudioController : Controller
    {
        private Manager m = new Manager();

        // GET: TrackAudio/Details/5
        [Route("clip/{id}")]
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.TrackAudioGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Set the Content-Type header, and return the audio bytes
                return File(o.Audio, o.AudioContentType);
            }
        }

    }     
}
