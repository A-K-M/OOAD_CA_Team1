using OOAD_CA_Team1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OOAD_CA_Team1.TourReservationSysDB
{
    public class DBTourLeader
    {
        public virtual List<TourLeader> GetTourLeads()
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

        public static string GetTourLeaderNameById(int TourLeaderId)
        {

            string TourLeaderName = "";
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"select name from TourLeaders where TourLeaderId = " + TourLeaderId;
            DataTable tbl = db.GetData(cmd);

            if (tbl != null)
            {
                TourLeaderName = Convert.ToString(tbl.Rows[0][0]);
            }
            return TourLeaderName;

        }
        public static List<TourLeader> GetAvailableTourLeaders(int tid, int pid)
        {

            List<TourLeader> tl_list = new List<TourLeader>();
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"select * from TourLeaders order by Name";
            DataTable tbl = db.GetData(cmd);
            if (tbl != null)
            {
                foreach (DataRow r in tbl.Rows)
                {
                    TourLeader tl = new TourLeader();
                    tl.TourleaderId = Convert.ToInt32(r[0].ToString());
                    tl.Name = Convert.ToString(r[1].ToString());
                    if (CheckLeader(tid, tl.TourleaderId))
                    {
                        if (CheckPt(tl.TourleaderId))
                        {
                            if (CheckPtDestination(tl.TourleaderId, pid))
                            {
                                tl_list.Add(tl);
                            }
                        }
                        else
                        {
                            tl_list.Add(tl);
                        }

                    }
                }
            }
            return tl_list;
        }
        public static void AssignTourleader(int tid, int tl_id)
        {
            //bool check = CheckAvailable(tid, tl_id);
            //if (check)
            //{
            SqlConnection con = new SqlConnection();
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"update tours set TourLeaderId = " + tl_id + " where TourId = " + tid + ";";
            db.SetData(cmd);
            //}
        }
        public static bool CheckLeader(int tid, int tl_id)
        {
            bool Isok = true;
            List<Tour> tours = new List<Tour>();
            tours = DBTour.GetTourListByLeaderId(tl_id);
            Tour newtour = new Tour();
            newtour = DBTour.GetTourDetailsById(tid);
            DateTime NewStartDate = Convert.ToDateTime(newtour.StartDate);
            DateTime NewEndDate = Convert.ToDateTime(newtour.EndDate);
            if (tours.Count() != 0)
            {
                foreach (Tour t in tours.Where(x => x.TourId != tid))
                {
                    DateTime OldStartDate = Convert.ToDateTime(t.StartDate);
                    DateTime OldEndDate = Convert.ToDateTime(t.EndDate);

                    //if (NewStartDate == OldStartDate)
                    //{
                    //    Isok = false;
                    //    break;
                    //}
                    //else
                    if (NewStartDate >= OldStartDate && NewStartDate <= OldEndDate)
                    {
                        Isok = false;
                        break;
                    }
                    else if (NewEndDate >= OldStartDate && NewEndDate <= OldEndDate) //just in case
                    {
                        Isok = false;
                        break;
                    }
                }
            }
            return Isok;
        }
        public static bool CheckPt(int tl_id)
        {
            TourLeader tl = new TourLeader();
            SqlConnection con = new SqlConnection();
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"select count(*) from TourLeaders t, ParttimeLeaders pt  where t.TourLeaderId = pt.TourLeaderId  and t.TourLeaderId = " + tl_id;
            db.SetData(cmd);
            DataTable tbl = db.GetData(cmd);
            if (Convert.ToInt32(tbl.Rows[0][0]) > 0)
            {
                return true;
            }
            return false;
        }
        public static bool CheckPtDestination(int tl_id, int pid)
        {

            string Distination = DBTour.GetDistination(pid);
            SqlConnection con = new SqlConnection();
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select count(*) from ParttimeLeaders where TourLeaderId = " + tl_id + " and (DistinationsOpted like '%," + Distination + ",%' OR  DistinationsOpted like '%," + Distination + "' OR  DistinationsOpted like '" + Distination + ",%' OR  DistinationsOpted like '" + Distination + "')";
            //cmd.CommandText = @"select count(*) from ParttimeLeaders where TourLeaderId = " + tl_id + " and DistinationsOpted like '%," + Distination + ",%' OR  DistinationsOpted like '%," + Distination + "' OR  DistinationsOpted like '" + Distination + ,"%'";
            db.SetData(cmd);
            DataTable tbl = db.GetData(cmd);
            if (Convert.ToInt32(tbl.Rows[0][0]) > 0)
            {
                return true;
            }
            return false;
        }
    }

    public class DBFulltimeTourLeaderList : DBTourLeader
    {
        public override List<TourLeader> GetTourLeads()
        {
            var db = new DBConnect();
            var dtLeads = db.GetData(new SqlCommand("SELECT * FROM FulltimeLeaders"));
            var leads = new List<TourLeader>();
            foreach (DataRow row in dtLeads.Rows)
            {
                var lead = new Fulltime();
                lead.TourleaderId = Convert.ToInt32(row[0].ToString());
                lead.Salary = Convert.ToDouble(row[1].ToString());
                lead.LeaveEntitled = row[2].ToString();
                lead.Rank = row[3].ToString();
                leads.Add(lead);
            }
            return leads;
        }
    }
    public class DBParttimeTourLeaderList : DBTourLeader
    {
        public override List<TourLeader> GetTourLeads()
        {
            var db = new DBConnect();
            var dtLeads = db.GetData(new SqlCommand("SELECT * FROM ParttimeLeaders"));
            var leads = new List<TourLeader>();

            foreach (DataRow row in dtLeads.Rows)
            {
                var lead = new Parttime();
                lead.TourleaderId = Convert.ToInt32(row[0].ToString());
                lead.DailySalaryRate = Convert.ToDouble(row[1].ToString());
                lead.DistinationsOpted = row[2].ToString();
                leads.Add(lead);
            }
            return leads;
        }
    }
}