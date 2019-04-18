using OOAD_CA_Team1.DAO;
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
            double cost = 0;
            var repo = new EnquiryTourLeadCostRepository();
            var fullTimeLeads = repo.GetFullTimeLeads();
            var partTimeLeads = repo.GetPartTimeLeads();

            if (fullTimeLeads.Exists(l => l.TourleaderId == tourLeadId))
            {
                cost = CalcCost(fullTimeLeads.SingleOrDefault(l => l.TourleaderId == tourLeadId).Salary, days);
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
    }
}