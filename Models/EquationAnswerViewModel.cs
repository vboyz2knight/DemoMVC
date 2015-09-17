using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoMVC.Classes;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoMVC.Models
{
    public class EquationAnswerViewModel
    {
        [Required]
        public Equation myEquation { get; set; }
        public double? myAnswer { get; set; }
        public Dictionary<string, string> myCollection { get; set; }
    }
}