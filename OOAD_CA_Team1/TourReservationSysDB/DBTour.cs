using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using OOAD_CA_Team1.Models;
using OOAD_CA_Team1.TourReservationSysDB;

namespace OOAD_CA_Team1.TourReservationSysDB
{
    public class DBTour
    {
        public static List<Tour> GetTourList()
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "  select t.TourId,t.TourPackageId,t.StartDate, t.TourLeaderId,p.TourPackageName,p.TourPrice,p.MaxPax,p.MinPax, t.status " +
                "from Tours t, TourPackages p where t.TourPackageId = p.TourPackageId";

            List<Tour> tourlist = new List<Tour>();
            DataTable tbl = db.GetData(cmd);
            if (tbl != null)
            {
                foreach (DataRow r in tbl.Rows)
                {
                    Tour tours = new Tour();
                    tours.TourId = Convert.ToInt32(r[0].ToString());
                    tours.TourPackageId = Convert.ToInt32(r[1].ToString());
                    tours.StartDate = Convert.ToDateTime(r[2].ToString());
                    if (r[3] == DBNull.Value)
                    {
                        tours.TourLeaderName = "Unassigned";
                    }
                    else
                    {
                        tours.TourLeaderId = Convert.ToInt32(r[3].ToString());
                        tours.TourLeaderName = DBTourLeader.GetTourLeaderNameById(tours.TourLeaderId);
                    }
                    tours.TourPackageName = r[4].ToString();
                    tours.Price = Convert.ToDouble(r[5].ToString());
                    tours.MinPassenger = Convert.ToInt32(r[6].ToString());
                    tours.MaxPassenger = Convert.ToInt32(r[7].ToString());
                    tours.EndDate = GetEndDate(tours.TourId);
                    tours.Status = Convert.ToInt32(r[8].ToString());
                    tours.StatusString = ((TourStatus)tours.Status).ToString();
                    tourlist.Add(tours);
                }
            }
            return tourlist;
        }

        public static Tour GetTourDetailsById(int id)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"select t.TourId,t.TourPackageId,t.StartDate, t.TourLeaderId
,p.TourPackageName,p.TourPrice,p.MaxPax,p.MinPax, t.status
from Tours t, TourPackages p where t.TourPackageId = p.TourPackageId and t.TourId =" + id;

            Tour tours = new Tour();
            DataTable tbl = db.GetData(cmd);
            if (tbl != null)
            {
                foreach (DataRow r in tbl.Rows)
                {
                    tours.TourId = Convert.ToInt32(r[0].ToString());
                    tours.TourPackageId = Convert.ToInt32(r[1].ToString());
                    tours.StartDate = Convert.ToDateTime(r[2].ToString());
                    if (r[3] == DBNull.Value)
                    {
                        tours.TourLeaderName = "Unassigned";
                    }
                    else
                    {
                        tours.TourLeaderId = Convert.ToInt32(r[3].ToString());
                        tours.TourLeaderName = DBTourLeader.GetTourLeaderNameById(tours.TourLeaderId);
                    }
                    tours.TourPackageName = r[4].ToString();
                    tours.Price = Convert.ToDouble(r[5].ToString());
                    tours.MinPassenger = Convert.ToInt32(r[6].ToString());
                    tours.MaxPassenger = Convert.ToInt32(r[7].ToString());
                    tours.EndDate = GetEndDate(tours.TourId);
                    tours.Status = Convert.ToInt32(r[8].ToString());
                    tours.StatusString = ((TourStatus)tours.Status).ToString();
                }
            }
            return tours;
        }
        public static List<Tour> GetTourListByLeaderId(int tl_id)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"select TourId, StartDate,Status from Tours where tourleaderid =" + tl_id;
            List<Tour> tourlist = new List<Tour>();
            DataTable tbl = db.GetData(cmd);
            if (tbl != null)
            {
                foreach (DataRow r in tbl.Rows)
                {
                    Tour tours = new Tour();
                    tours.TourId = Convert.ToInt32(r[0].ToString());
                    tours.StartDate = Convert.ToDateTime(r[1].ToString());
                    tours.EndDate = GetEndDate(tours.TourId);
                    tours.Status = Convert.ToInt32(r[2].ToString());
                    tourlist.Add(tours);
                }
            }
            return tourlist;
        }

        public static string GetDistination(int pid)
        {

            string tolocation = "";
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"select MainDestination from TourPackages where TourPackageId = " + pid;
            DataTable tbl = db.GetData(cmd);

            if (tbl != null)
            {
                tolocation = Convert.ToString(tbl.Rows[0][0]);
            }
            return tolocation;

        }

        public static DateTime GetEndDate(int tid)
        {
            DateTime end ;
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"select DATEADD(DAY," + 
"(  select NumOfDays from TourPackages where TourPackageId = (select TourPackageId from Tours where TourId ="+tid+" ) ), "+
"(select StartDate as enddate from Tours where TourId = "+tid+") )";
            DataTable tbl = db.GetData(cmd);

            if (tbl != null)
            {
                end = Convert.ToDateTime(tbl.Rows[0][0]);
            }
            else {
                end = DateTime.Now;
            }

            return end;
        }

        public enum TourStatus
        {
            Open = 1,
            Full = 2,
            Departed = 3,
            Completed = 4,
            Cancelled = 5
        }


    }
}