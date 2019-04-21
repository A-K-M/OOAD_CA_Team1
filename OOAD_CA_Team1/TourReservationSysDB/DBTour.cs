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
            cmd.CommandText = "select t.*,p.TourPackageName from Tours t, TourPackages p where t.PackageId = p.TourPackageId ";

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
                    tours.EndDate = Convert.ToDateTime(r[3].ToString());
                    tours.Price = Convert.ToDouble(r[4].ToString());
                    if (r[5] == DBNull.Value)
                    {
                        tours.TourLeaderName = "Unassigned";
                    }
                    else
                    {
                        tours.TourLeaderId = Convert.ToInt32(r[5].ToString());
                        tours.TourLeaderName = DBTourLeader.GetTourLeaderNameById(tours.TourLeaderId);
                    }
                    if (tbl.Rows[0][6] != null)
                    {
                        tours.TourLeaderCost = Convert.ToDouble(r[6].ToString());
                    }
                    else
                    {
                        tours.TourLeaderCost = 0.0;
                    }
                    tours.MinPassenger = Convert.ToInt32(r[7].ToString());
                    tours.MaxPassenger = Convert.ToInt32(r[8].ToString());
                    tours.Status = Convert.ToInt32(r[9].ToString());
                    tours.StatusString = ((TourStatus)tours.Status).ToString();

                    tours.TourPackageName = r[10].ToString();

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
            cmd.CommandText = @"select t.*,p.TourPackageName from Tours t, TourPackages p where t.PackageId = p.TourPackageId and t.tourid =" + id;

            Tour tours = new Tour();
            DataTable tbl = db.GetData(cmd);
            if (tbl != null)
            {
                foreach (DataRow r in tbl.Rows)
                {
                    tours.TourId = Convert.ToInt32(r[0].ToString());
                    tours.TourPackageId = Convert.ToInt32(r[1].ToString());
                    tours.StartDate = Convert.ToDateTime(r[2].ToString());
                    tours.EndDate = Convert.ToDateTime(r[3].ToString());
                    tours.Price = Convert.ToDouble(r[4].ToString());
                    if (r[5] == DBNull.Value)
                    {
                        tours.TourLeaderName = "Unassigned";
                    }
                    else
                    {
                        tours.TourLeaderId = Convert.ToInt32(r[5].ToString());
                        tours.TourLeaderName = DBTourLeader.GetTourLeaderNameById(tours.TourLeaderId);
                    }
                    if (tbl.Rows[0][6] != null)
                    {
                        tours.TourLeaderCost = Convert.ToDouble(r[6].ToString());
                    }
                    else
                    {
                        tours.TourLeaderCost = 0.0;
                    }
                    tours.MinPassenger = Convert.ToInt32(r[7].ToString());
                    tours.MaxPassenger = Convert.ToInt32(r[8].ToString());
                    tours.Status = Convert.ToInt32(r[9].ToString());
                    tours.TourPackageName = r[10].ToString();
                }
            }
            return tours;
        }
        public static List<Tour> GetTourListByLeaderId(int tl_id)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"select TourId, StartDate,EndDate,Status from Tours where tourleaderid =" + tl_id;
            List<Tour> tourlist = new List<Tour>();
            DataTable tbl = db.GetData(cmd);
            if (tbl != null)
            {
                foreach (DataRow r in tbl.Rows)
                {
                    Tour tours = new Tour();
                    tours.TourId = Convert.ToInt32(r[0].ToString());
                    tours.StartDate = Convert.ToDateTime(r[1].ToString());
                    tours.EndDate = Convert.ToDateTime(r[2].ToString());
                    tours.Status = Convert.ToInt32(r[3].ToString());
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
            cmd.CommandText = @"select ToLocation from TourPackages where TourPackageId = " + pid;
            DataTable tbl = db.GetData(cmd);

            if (tbl != null)
            {
                tolocation = Convert.ToString(tbl.Rows[0][0]);
            }
            return tolocation;

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