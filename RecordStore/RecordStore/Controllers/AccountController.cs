using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Data.Entity;
=======
>>>>>>> 406e379bced60430c121c4509d9b369056c208c9
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RecordStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
<<<<<<< HEAD
using static RecordStore.Models.Purchase;
=======
>>>>>>> 406e379bced60430c121c4509d9b369056c208c9

namespace RecordStore.Controllers
{
    public class AccountController : Controller
    {
<<<<<<< HEAD
        RecordContext db = new RecordContext();

        ApplicationUser user;
=======
>>>>>>> 406e379bced60430c121c4509d9b369056c208c9
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
<<<<<<< HEAD
                user = new ApplicationUser { UserName = model.UserName, Email = model.Email, Name = model.Name};
=======
                ApplicationUser user = new ApplicationUser { UserName = model.UserName, Email = model.Email};
>>>>>>> 406e379bced60430c121c4509d9b369056c208c9
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
<<<<<<< HEAD
                user = await UserManager.FindAsync(model.UserName, model.Password);
=======
                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
>>>>>>> 406e379bced60430c121c4509d9b369056c208c9
                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong email or password.");
                }
                else
<<<<<<< HEAD
                { 
=======
                {
>>>>>>> 406e379bced60430c121c4509d9b369056c208c9
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
<<<<<<< HEAD

        public async Task<ActionResult> Account()
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                user = await UserManager.FindByIdAsync(userId);

            }
            ViewBag.User = user;

            /*ICollection<Purchase> purchases = null;
            IEnumerable<Purchase> allpurchases = db.Purchases.Includ(a => a.Album.Artist);
            if (allpurchases != null)
            { 
            foreach (Purchase p in allpurchases)
                if (p.UserId == user.Id)
                {
                    purchases.Add(p);
                }
            }
            ViewBag.Purchases = purchases;
            */
            return View();
        }


        public ActionResult Purchase()
        {
            return View();
        }

        /*[HttpPost]
        public Task<ActionResult> Purchase(Purchase model)
        {
            Purchase purchase = new Purchase { Name = model.Name, Address = model.Address, PhoneNumber = model.PhoneNumber };

            return View(model);
        }*/

        public RedirectToRouteResult CreatPurchase()
        {
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Purchase");
            }

=======
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
>>>>>>> 406e379bced60430c121c4509d9b369056c208c9
        }

    }
}