using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOAD_CA_Team1.Models
{
    public class Fulltime : TourLeader
    {
        public double Salary { get; set; }
        public string LeaveEntitled { get; set; }
        public string Rank { get; set; }

        public override double GetDailyRate()
        {
            double dailyRate = 0.0;
            switch (Rank)
            {
                case "M1":
                    dailyRate = 500.0; break;
                case "M2":
                    dailyRate = 400.0; break;
                case "M3":
                    dailyRate = 300.0; break;
                default:
                    break;
            }
            return dailyRate;
        }
    }
}