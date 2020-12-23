using RecordStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using static RecordStore.Models.Cart;
using System.Web.Routing;

namespace RecordStore.Controllers
{
    public class HomeController : Controller
    {
        RecordContext db = new RecordContext();


        public ActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpGet]
        public ActionResult Artists()
        {
            IEnumerable<Artist> artists = db.Artists;
            ViewBag.Artists = artists;
            return View("~/Views/Home/Artists.cshtml");
        }

        [HttpGet]
        public ActionResult Albums(int Id)
        {
            IEnumerable<Artist> allartists = db.Artists.Include(a => a.Albums);
            Artist artist = allartists.FirstOrDefault(a => a.Id == Id);
            ViewBag.Artist = artist;

            return View("~/Views/Home/Albums.cshtml");
        }

        [HttpGet]
        public ActionResult Records(int Id)
        {
            IEnumerable<Album> allalbums = db.Albums.Include(a => a.Records).Include(a => a.Artist);
            Album album = allalbums.FirstOrDefault(a => a.Id == Id);
            ViewBag.Album = album;
            return View("~/Views/Home/Records.cshtml");
        }

        public ViewResult Cart()
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart()
            }) ; 
        }

        public RedirectToRouteResult AddToCart(int Id)
        {
            Record record = db.Records
                .FirstOrDefault(r => r.Id == Id);
            record.Id = Id;

            if (record != null)
            {
                GetCart().AddItem(record, 1);
            }

            return RedirectToAction("Records", new { Id = record.AlbumId });        
        }

        public RedirectToRouteResult RemoveFromCart(int Id)
        {
            Record record = db.Records
                .FirstOrDefault(r => r.Id == Id);

            if (record != null)
            {
                GetCart().RemoveLine(record);
            }
            return RedirectToAction("Cart");
        }

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}
   