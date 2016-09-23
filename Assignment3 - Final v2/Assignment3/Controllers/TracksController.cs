using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3.Controllers
{
    public class TracksController : Controller
    {
        private Manager m = new Manager();
        // GET: Tracks
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        public ActionResult AllPop()
        {
            return View("Index", m.TrackGetAllPop());
        }

        public ActionResult DeepPurple()
        {
            return View("Index", m.TrackGetDeepPurple());
        }

        public ActionResult LongestTrack()
        {
            return View("Index", m.TrackGetAllTop100Longest());
        }
    }
}
