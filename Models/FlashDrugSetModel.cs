using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoMVC.Classes;

namespace DemoMVC.Models
{
    public class FlashDrugSetModel
    {
        //array of drug brands that will use in the SET
        public string[] userDrugInfoArray { get; set; }
        public int currentArrayIndex { get; set; }
        public Drug currentDisplayDrug { get; set; }
    }
}