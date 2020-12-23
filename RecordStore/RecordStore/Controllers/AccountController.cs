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
        

        ApplicationUser user = new ApplicationUser();


        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private void FindUser()
        {
            user = UserManager.FindById(User.Identity.GetUserId());
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
            FindUser();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.User = user;

            IEnumerable<PurchaseRecord> purchaserecord = db.PurchaseRecords
                .Include(a => a.Purchase)
                .Include(a => a.Record.Album.Artist)
                .OrderBy(p => p.Purchase.UserId == user.Id);
            ViewBag.Purchases = purchaserecord;
            return View();
        }

        public RedirectToRouteResult CreatPurchase()
        {
            FindUser();
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Purchase");
            }
        }

        public ActionResult Purchase()
        {
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult Purchase(Purchase model)
        {

            if (ModelState.IsValid)
            {
                Cart cart = GetCart();
                Decimal sum = cart.ComputeTotalValue();
                Purchase purchase = new Purchase {
                    UserId = user.Id, Name = model.Name, Address = model.Address, 
                    PhoneNumber = model.PhoneNumber, Sum = sum, Date = DateTime.Today };
                
                foreach (var line in cart.Lines)
                {
                    purchase.Records.Add(line.Record);
                }
                db.Purchases.Add(purchase);
                db.SaveChanges();

                return RedirectToAction("Payment", new { sum });
            }

            return RedirectToAction("Cart", "Home");
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

        public ActionResult Payment(decimal Sum)
        { 
            return View();
        }

    }
}

