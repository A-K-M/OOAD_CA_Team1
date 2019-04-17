using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOAD_CA_Team1.Models
{
    public class Tours
    {
        public int TourId { get; set; }
        public int TourPackageId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public double Price { get; set; }
        public int TourLeaderId { get; set; }
        public double TourLeaderCost { get; set; }
        public int MinPassenger { get; set; }
        public int MaxPassenger { get; set; }
        public int Status { get; set; }
    }
}