using RecordStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace RecordStore.Controllers
{
    public class HomeController : Controller
    {
        RecordContext db = new RecordContext();

        public ActionResult Index()
        {
            /*IEnumerable<Artist> artists = db.Artists;
            ViewBag.Artists = artists;
            IEnumerable<Album> albums = db.Albums;
            ViewBag.Albums = albums;*/
            return View("~/Views/Home/Index.cshtml");
        }

        public ActionResult Artists()
        {
            IEnumerable<Artist> artists = db.Artists;
            ViewBag.Artists = artists;
            return View("~/Views/Home/Artists.cshtml");
        }

        [HttpGet]
        public ActionResult Albums(int id, string artist)
        {
            ViewBag.ArtistId = id;
            IEnumerable<Album> albums = db.Albums;
            ViewBag.Albums = albums;
            ViewBag.Artist = artist;           
            return View("~/Views/Home/Albums.cshtml");
        }

        [HttpGet]
        public ActionResult Records(int id)
        {
            ViewBag.AlbumId = id;
            IEnumerable<Record> records = db.Records.Include(a => a.Album);
            ViewBag.Records = records;
            return View("~/Views/Home/Records.cshtml");
        }
    }
}