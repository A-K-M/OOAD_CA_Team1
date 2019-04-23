using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OOAD_CA_Team1.Models
{
    public class Tour
    {
        [Display(Name = "Tour ID")]
        public int TourId { get; set; }
        [Display(Name = "Tour Package ID")]
        public int TourPackageId { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]

        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        [Display(Name = "Tour Leader ID")]
        public int TourLeaderId { get; set; }
        [Display(Name = "Tour Leader Cost")]
        public double TourLeaderCost { get; set; }
        [Display(Name = "Min Passenger")]
        public int MinPassenger { get; set; }
        [Display(Name = "Max Passenger")]
        public int MaxPassenger { get; set; }
        public int Status { get; set; }
        [Display(Name = "Tour Package Name")]
        public string TourPackageName { get; set; }
        [Display(Name = "Tour Leader Name")]

        public string TourLeaderName { get; set; }
        public string StatusString { get; set; }

     

    



    }
}