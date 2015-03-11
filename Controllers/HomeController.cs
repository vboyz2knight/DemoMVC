using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoMVC.Models;
using DemoMVC.Classes;

namespace DemoMVC.Controllers
{
    public class HomeController : Controller
    {
        private IMessagingService messagingService;

        public HomeController(IMessagingService msg)
        {
            this.messagingService = msg;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

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
                    var smtp = new System.Net.Mail.SmtpClient();
                    var host = smtp.Host;

                    Message newMessage = new Message("Contact Us Page", contact.Email, host, contact.FirstName + " " + contact.LastName + ": " + contact.Comment);
                    this.messagingService.SendMessage(newMessage);

                    return View("SuccessContact");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }

            return View();
        }
    }
}