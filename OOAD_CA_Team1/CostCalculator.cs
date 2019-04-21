using OOAD_CA_Team1.TourReservationSysDB;
using OOAD_CA_Team1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOAD_CA_Team1
{
    public class CostCalculator
    {
        public static double CalculateTourLeadCost(int tourLeadId, int days)
        {
            double cost = 0.0;
            var repo = new DBTourLeader();
            var fullTimeLeads = repo.GetFullTimeLeads();
            var partTimeLeads = repo.GetPartTimeLeads();

            if (GetTourLeadType(fullTimeLeads, tourLeadId) == TourLeadType.FullTime)
            {
                var tourLeadRank = fullTimeLeads.Find(l => l.TourleaderId == tourLeadId).Rank;   // GetTourLeadRank
                double dailyRate = 0.0;
                switch (tourLeadRank)
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
                cost = CalcCost(dailyRate, days);
            }
            else
            {
                cost = CalcCost(partTimeLeads.SingleOrDefault(l => l.TourleaderId == tourLeadId).DailySalaryRate, days);
            }
            return cost;
        }

        private static double CalcCost(double dailyRate, int days)
        {
            return dailyRate * days;
        }

        private static TourLeadType GetTourLeadType(List<Fulltime> fullTimeLeads, int tourLeadId)
        {
            return fullTimeLeads.Exists(l => l.TourleaderId == tourLeadId) ? TourLeadType.FullTime : TourLeadType.PartTime;
        }
    }

    public enum TourLeadType
    {
        FullTime,
        PartTime
    }
}