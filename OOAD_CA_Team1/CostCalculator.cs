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
            //var repo = new DBTourLeader();
            var fulltimeRepo = new DBFulltimeTourLeaderList();
            var parttimeRepo = new DBParttimeTourLeaderList();
            var fulltimeLeads = fulltimeRepo.GetTourLeads();
            var partTimeLeads = parttimeRepo.GetTourLeads();

            if (GetTourLeadType(fulltimeLeads, tourLeadId) == TourLeadType.FullTime)
            {
                var tourLeadRank = fulltimeLeads.Find(l => l.FulltimeLeader.TourleaderId == tourLeadId).FulltimeLeader.Rank;   // GetTourLeadRank
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
                cost = CalcCost(partTimeLeads.SingleOrDefault(l => l.ParttimeLeader.TourleaderId == tourLeadId).ParttimeLeader.DailySalaryRate, days);
            }
            return cost;
        }

        private static double CalcCost(double dailyRate, int days)
        {
            return dailyRate * days;
        }

        private static TourLeadType GetTourLeadType(List<TourLeader> fulltimeLeads, int tourLeadId)
        {
            return fulltimeLeads.Exists(l => l.FulltimeLeader.TourleaderId == tourLeadId) ? TourLeadType.FullTime : TourLeadType.PartTime;
        }
    }

    public enum TourLeadType
    {
        FullTime,
        PartTime
    }
}