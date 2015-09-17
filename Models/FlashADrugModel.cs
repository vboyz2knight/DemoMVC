using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DemoMVC.Classes;

namespace DemoMVC.Models
{
    public class FlashADrugModel
    {
        public int? drugPosition { get; set; }

        public Drug displayDrug { get; set; }
    }
}