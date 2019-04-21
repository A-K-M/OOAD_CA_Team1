using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OOAD_CA_Team1.ViewModels
{
    public class TourLeadCostViewModel
    {
        [Required(ErrorMessage = "Please enter number of days")]
        [Display(Name = "Number of days")]
        public string NoOfDays { get; set; }
     
        [Required(ErrorMessage = "Please select tour lead")]
        [Display(Name = "Tour Lead")]
        public int SelectedTourLead { get; set; }

        public SelectList TourLeads { get; set; }
    }
}