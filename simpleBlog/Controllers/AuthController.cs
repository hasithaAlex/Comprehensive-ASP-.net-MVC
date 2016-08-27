using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NHibernate.Linq;
using simpleBlog.Models;
using simpleBlog.ViewModels;

namespace simpleBlog.Controllers
{
    public class AuthController : Controller
    {


        public ActionResult Login()
        {
            return View( new AuthLogin
            {
                Message = "Login to Your Account"
            });
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");
        }

        [HttpPost] 
        public ActionResult Login(AuthLogin form, string returnUrl)
        {
            form.Message = "opps! Login to Your Account";

            var user = Database.Session.Query<User>().FirstOrDefault(u => u.UserName == form.UserName);
            
            if(user == null)
                simpleBlog.Models.User.FakeHash(); 

            if(user == null || !user.CheckPassword(form.Password))   
                ModelState.AddModelError("Username","Username or password is incorrect");

            if (!ModelState.IsValid)
                return View(form);

            //return string to html view  
            /*return Content("The form is Valied");*/
            

            //custom validation
            /*if (form.UserName == "Admin")
            {
                ModelState.AddModelError("UserName","User name isn't 100% cooler");
                return View(form);
            }
            */


            //save cookies for browser
            FormsAuthentication.SetAuthCookie(user.UserName,true);


            //retuen to if url have redirect url
            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);


            //return to home
            return RedirectToRoute("Home");

        }

    }
}