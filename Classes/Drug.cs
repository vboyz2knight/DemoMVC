using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Classes
{
    public class Drug
    {
        [Required]
        public string DrugType { get;  set; }
        [Required]
        public string DrugGeneric { get;  set; }
        [Required]
        public string DrugBrand { get;  set; }
        [Required]
        public string Action { get;  set; }
        [Required]
        public string Therapeutic { get; set; }
        [Required]
        public string Indication { get; set; }

    }
}