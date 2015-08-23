using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoMVC.Models
{
    public class BudgetTrackerController : Controller
    {
        // GET: BudgetTracker
        public ActionResult Index()
        {
            return View();
        }
    }
}