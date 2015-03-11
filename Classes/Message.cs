using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Classes
{
    public class Message
    {
        //All fields in message are required else throw ArgumentNullException
        [Required(ErrorMessage = "Subject is required.")]
        public string Subject { get; private set; }

        [EmailAddress(ErrorMessage = "Not a valid email.")]
        [Required(ErrorMessage = "From Email is required.")]
        public string From { get; private set; }

        [Required(ErrorMessage = "To Email is required.")]
        [EmailAddress(ErrorMessage = "Not a valid email.")]
        public string To { get; private set; }

        [Required(ErrorMessage = "Body is required.")]
        public string Body { get; private set; }

        public Message(string subject, string from, string to, string body)
        {
            if (!String.IsNullOrEmpty(subject))
            {
                this.Subject = subject;
            }
            else
            {
                throw new ArgumentNullException("Subject in message is empty or null.");
            }

            if (!String.IsNullOrEmpty(from))
            {
                this.From = from;
            }
            else
            {
                throw new ArgumentNullException("From in message is empty or null.");
            }

            if (!String.IsNullOrEmpty(to))
            {
                this.To = to;
            }
            else
            {
                throw new ArgumentNullException("To in message is empty or null.");
            }

            if (!String.IsNullOrEmpty(body))
            {
                this.Body = body;
            }
            else
            {
                throw new ArgumentNullException("Body in message is empty or null.");
            }
        }
    }
}