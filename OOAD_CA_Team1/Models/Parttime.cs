using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOAD_CA_Team1.Models
{
    public class Parttime : TourLeader
    {
        public double DailySalaryRate { get; set; }
        public string DistinationsOpted { get; set; }

        public override double GetDailyRate()
        {
            return DailySalaryRate;
        }
    }
}