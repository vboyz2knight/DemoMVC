using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using DemoMVC.Models;

namespace DemoMVC.Classes
{
    public class MyEmailService:IMessagingService
    {
        public void SendMessage(Message message)
        {
            MailMessage msg = new MailMessage(message.From,message.To,message.Subject,message.Body);
            SmtpClient smtp = new SmtpClient();
            
            msg.IsBodyHtml = false;
            
            smtp.Send(msg);
            msg.Dispose();
        }
    }
}