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
    }
}