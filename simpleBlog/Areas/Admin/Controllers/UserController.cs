using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using simpleBlog.Infrastructure;

namespace simpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("users")]
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}