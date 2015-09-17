using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoMVC.Classes
{
    public class Equation
    {
        [Required]
        public string Abbreviation{get;set;}
        [Required]
        public string Id{get;set;}
        [Required]
        public string Description{get;set;}
        [Required]
        public string EquationExpression { get; set; }
    }
}