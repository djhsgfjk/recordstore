using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RecordStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using static RecordStore.Models.Purchase;

namespace RecordStore.Controllers
{
    public class AccountController : Controller
    {
        RecordContext db = new RecordContext();

        ApplicationUser user;
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
                user = new ApplicationUser { UserName = model.UserName, Email = model.Email, Name = model.Name};
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
                user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong email or password.");
                }
                else
                { 
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

        }

    }
}