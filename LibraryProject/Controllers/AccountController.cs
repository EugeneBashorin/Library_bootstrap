using LibraryProject.Configurations;
using LibraryProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    public class AccountController : Controller
    {
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
                ApplicationUser user = new ApplicationUser();
                user.UserName = model.Email;
                user.Email = model.Email;

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, ConfigurationData._USER_ROLE);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
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
            ViewBag.logoutLinkElement = ConfigurationData._ATTRIBUTES_STATE_OFF;
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong login or password.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    } 
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login","Account");
        }


        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed()
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            IdentityResult result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}