using OOAD_CA_Team1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OOAD_CA_Team1.DAO
{
    public class EnquiryTourLeadCostRepository
    {
        public List<TourLeader> GetTourLeads()
        {
            var leads = new List<TourLeader>();
            var db = new DBConnect();
            var dtLeads = db.GetData(new SqlCommand("SELECT * FROM TourLeaders"));

            for (int i = 0; i < dtLeads.Rows.Count; i++)
            {
                var lead = new TourLeader();
                var row = dtLeads.Rows[i];
                lead.TourleaderId = Convert.ToInt32(row["TourLeaderId"]);
                lead.Name = row["Name"].ToString();
                lead.ContactNumber = row["ContactNumber"].ToString();
                lead.Email = row["Email"].ToString();
                leads.Add(lead);
            }
            return leads;
        }

        public List<Fulltime> GetFullTimeLeads()
        {
            var db = new DBConnect();
            var dtLeads = db.GetData(new SqlCommand("SELECT * FROM FulltimeLeaders"));
            var leads = new List<Fulltime>();
            for (int i = 0; i < dtLeads.Rows.Count; i++)
            {
                var lead = new Fulltime();
                var row = dtLeads.Rows[i];
                lead.TourleaderId = Convert.ToInt32(row["TourLeaderId"]);
                lead.Salary = Convert.ToDouble(row["Salary"]);
                lead.Rank = row["Rank"].ToString();
                lead.LeaveEntitled = row["LeaveEntitled"].ToString();
                leads.Add(lead);
            }
            return leads;
        }

        public List<Parttime> GetPartTimeLeads()
        {
            var db = new DBConnect();
            var dtLeads = db.GetData(new SqlCommand("SELECT * FROM ParttimeLeaders"));
            var leads = new List<Parttime>();
            for (int i = 0; i < dtLeads.Rows.Count; i++)
            {
                var lead = new Parttime();
                var row = dtLeads.Rows[i];
                lead.TourleaderId = Convert.ToInt32(row["TourLeaderId"]);
                lead.DailySalaryRate = Convert.ToDouble(row["DailySalaryRate"]);
                lead.DistinationsOpted = row["DistinationsOpted"].ToString();
                leads.Add(lead);
            }
            return leads;
        }

    }
}