using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using DemoMVC.Models;
using System.Configuration;

namespace DemoMVC.Classes
{
    public class MyEmailService:IMessagingService
    {
        public void SendMessage(Message message)
        {
            var smtp = new System.Net.Mail.SmtpClient();
            smtp.EnableSsl = true;

            string sendErrorEmailTo = System.Configuration.ConfigurationManager.AppSettings["sendErrorEmailTo"];

            MailMessage msg = new MailMessage(message.From, sendErrorEmailTo, message.Subject, message.Body);            
            
            msg.IsBodyHtml = false;
            
            smtp.Send(msg);
            msg.Dispose();
        }
    }
}