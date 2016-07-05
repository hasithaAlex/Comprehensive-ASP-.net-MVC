using System.Web.Mvc;

namespace simpleBlog.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName { get { return "admin"; } }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", controller= "User", id = UrlParameter.Optional }
            );
        }
    }
}