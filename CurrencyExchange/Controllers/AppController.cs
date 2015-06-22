using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurrencyExchange.Controllers
{
    public class AppController : Controller
    {
        // GET: App
        public ActionResult Template(string appTemplate)
        {
            var filePath = "~/App/" + appTemplate;

            if (System.IO.File.Exists(Server.MapPath(filePath)))
            {
                return View(filePath);
            }

            return new EmptyResult();
        }
    }
}
