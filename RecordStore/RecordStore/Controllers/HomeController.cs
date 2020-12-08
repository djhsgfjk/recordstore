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
        public ActionResult Albums(int id)
        {
            IEnumerable<Artist> allartists = db.Artists.Include(a => a.Albums);
            Artist artist = new Artist();
            foreach (Artist a in allartists)
                if (a.Id == id)
                {
                    artist = a;
                    break;
                }
            ViewBag.Artist = artist;
            return View("~/Views/Home/Albums.cshtml");
        }

        [HttpGet]
        public ActionResult Records(int id)
        {
            IEnumerable<Album> allalbums = db.Albums.Include(a => a.Records).Include(a => a.Artist);
            Album album = new Album();
            foreach (Album a in allalbums)
                if (a.Id == id)
                { 
                    album = a;
                    break;
                }
            ViewBag.Album = album;
            return View("~/Views/Home/Records.cshtml");
        }
    }
}