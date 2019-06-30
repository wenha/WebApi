using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var indexHtml = HttpContext.Server.MapPath("/index.html");
            if (System.IO.File.Exists(indexHtml))
            {
                return Content(System.IO.File.ReadAllText(indexHtml), "text/html");
            }

            return Redirect("/apis/index");
        }
    }
}
