using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        [HttpPost]
        public ActionResult Login(AuthLogin form)
        {
            form.Message = "Login to Your Account";
            
            if(!ModelState.IsValid)
                return View(form);

            if (form.UserName == "Admin")
            {
                ModelState.AddModelError("UserName","User name isn't 100% cooler");
                return View(form);
            }

            return Content("The form is Valied");
        }

    }
}