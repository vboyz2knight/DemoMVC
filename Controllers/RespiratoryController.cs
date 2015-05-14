using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoMVC.Controllers
{
    public class RespiratoryController : Controller
    {
        // GET: Respiratory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InitialVentSettings()
        {
            return View();
        }
    }
}