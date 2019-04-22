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
            var fulltimeLeads = new DBFulltimeTourLeaderList().GetTourLeads();
            var partTimeLeads = new DBParttimeTourLeaderList().GetTourLeads();
            var allLeads = fulltimeLeads.Union(partTimeLeads);
            var lead = allLeads.SingleOrDefault(l => l.TourleaderId == tourLeadId);
            cost = CalcCost(lead.GetDailyRate(), days);
            return cost;
        }

        private static double CalcCost(double dailyRate, int days)
        {
            return dailyRate * days;
        }
    }
}