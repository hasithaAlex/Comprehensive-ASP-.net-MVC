using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;
using simpleBlog.Areas.Admin.ViewModels;
using simpleBlog.Infrastructure;
using simpleBlog.Models;

namespace simpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("users")]
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View(new UsersIndex
            {
                                        //this Query means "NHibernate.Linq"
                                        //NHibernate few defferent ways to query a object -> HQL/derect sql/criterea API/quary Over API/Linq
                Users = Database.Session.Query<User>().ToList()
            });
        }


        public ActionResult New()
        {
            return View(new UsersNew
            {
                
            });
        }

        [HttpPost]
        public ActionResult New(UsersNew form)
        {
            if(Database.Session.Query<User>().Any(u => u.UserName == form.Username))
                ModelState.AddModelError("Username","Username must be unique");

            if (!ModelState.IsValid)
                return View(form);

            var user = new User
            {
                Email = form.Email,
                UserName = form.Username
            };

            user.SetPassword(form.Password);

            Database.Session.Save(user);
            
            return RedirectToAction("index");
        }


        public ActionResult Edit(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            return View(new UsersEdit
            {
                Username = user.UserName,
                Email = user.Email
            });
        }

        [HttpPost]
        public ActionResult Edit(int id,UsersEdit form)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            if(Database.Session.Query<User>().Any(u => u.UserName == form.Username && u.Id != id))
                ModelState.AddModelError("Username","Username must be unique");

            if (!ModelState.IsValid)
                return View(form);

            user.UserName = form.Username;
            user.Email = form.Email;
            Database.Session.Update(user);

            return RedirectToAction("index");
        }


        public ActionResult ResetPassword(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            return View(new UsersResetPassword
            {
                Username = user.UserName,
            });
        }


        [HttpPost]
        public ActionResult ResetPassword(int id, UsersResetPassword form)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            form.Username = user.UserName;

            if (!ModelState.IsValid)
                return View(form);

            user.SetPassword(form.Password);
            Database.Session.Update(user);

            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            Database.Session.Delete(user);
            return RedirectToAction("index");
        }
    }
}