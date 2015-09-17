using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoMVC.Models
{
    public class TankDurationViewModel
    {
        [Required]
        
        [Range(typeof(decimal),"0.01","99999")]
        public int gauge_pressure_psi { get; set; }
        [Required]
        [Range(typeof(decimal), "0.01", "99999")]
        public int liter_flow_L { get; set; }
        
        public decimal selectedTankFactor { get;set; }
        public List<SelectListItem> tank_factor_list { get; set; }

        public double? myAnswer { get; set; }

        public TankDurationViewModel()
        {
            this.tank_factor_list = new List<SelectListItem>();
            this.tank_factor_list.Add(new SelectListItem { Text = "E", Value = "0.28" });
            this.tank_factor_list.Add(new SelectListItem { Text = "H", Value = "3.14" });
        }
    }
}