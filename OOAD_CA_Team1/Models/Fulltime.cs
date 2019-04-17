using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOAD_CA_Team1.Models
{
    public class Fulltime : Tourleaders
    {
        public double Salary { get; set; }
        public string LeaveEntitled { get; set; }
        public string Rank { get; set; }
    }
}