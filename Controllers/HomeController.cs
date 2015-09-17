using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoMVC.Models;
using DemoMVC.Classes;
using DemoMVC.Loggers;

namespace DemoMVC.Controllers
{
    public class HomeController : Controller
    {
        private IMessagingService messagingService;
        private IMyLogger myLogger;

        public HomeController(IMessagingService msg,IMyLogger logger)
        {
            this.messagingService = msg;
            this.myLogger = logger;
        }
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Test()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()] 
        public ActionResult Contact(ContactModels contact)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Message newMessage = new Message("Contact Us Page", contact.Email, contact.FirstName + " " + contact.LastName + ": " + contact.Comment);
                    this.messagingService.SendMessage(newMessage);

                    return View("SuccessContact");
                }
                catch (Exception ex)
                {
                    myLogger.Error("Error in Home/Contact messagingService.SendMessage", ex);
                    return View("Error");
                }
            }

            return View();
        }
    }
}