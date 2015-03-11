using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class ContactModels
    {
        [Required(ErrorMessage="First name is required.")]
        [StringLength(50,ErrorMessage="Must be under 50 characters.")]
        public string FirstName { get; set; }

        [StringLength(50,ErrorMessage="Must be under 50 characters.")]
        [Required(ErrorMessage="Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage="Email name is required.")]
        [EmailAddress(ErrorMessage="Not a valid email.")]
        public string Email { get; set; }

        [Required(ErrorMessage="Comment name is required.")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
    }
}