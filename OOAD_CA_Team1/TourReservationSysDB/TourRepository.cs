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
    public class TourRepository 
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
                        tours.TourLeaderName = TourRepository.GetTourLeaderNameById(tours.TourLeaderId);
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

                    tourlist.Add(tours);
                }
            }
            return tourlist;
        }
        public static string GetTourLeaderNameById(int TourLeaderId) {

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
        public static Tour GetTourInfoById(int id)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"select t.*,p.TourPackageName from Tours t, TourPackages p where t.PackageId = p.TourPackageId and t.tourid ="+id;

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
                        tours.TourLeaderName = TourRepository.GetTourLeaderNameById(tours.TourLeaderId);
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
        public static List<TourLeader> GetTourLeaders(int tid)
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
                    if (CheckLeader(tid, tl.TourleaderId) )
                    {
                        tl_list.Add(tl);
                    }
                }
            }
            return tl_list;
        }

        //Wrong One with linq

        // to get tour leaders filtered by available dates
        //public static List<TourLeader> GetAavilableTourLeaders(DateTime startDate, DateTime endDate)
        //{
        //    List<Tour> tours = GetTourList();

        //    List<int> UnavailableLeaders = tours
        //                                .Where(tour => (startDate >= tour.StartDate && startDate <= tour.EndDate)
        //                                && (endDate >= tour.StartDate && endDate <= tour.EndDate))
        //                                .Select(tour => tour.TourLeaderId)
        //                                .ToList();

        //    //var UnavailableLeaderIDS = from tour in tours
        //    //               where(startDate == tour.StartDate) &&
        //    //               (startDate >= tour.StartDate && startDate <= tour.EndDate)
        //    //               && (endDate >= tour.StartDate && endDate <= tour.EndDate)
        //    //               select tour.TourLeaderId;

        //    var list = GetTourLeaders().Where(leader => !UnavailableLeaders.Contains(leader.TourleaderId)).ToList();
        //    return list;
        //    //List<TourLeader> TourLeaders = GetTourLeaders();
        //    //var TourLeaders_List = (from tl in TourLeaders where !UnavailableLeaders.Contains(tl.TourleaderId) select tl).ToList();
        //    //return TourLeaders_List;
        //}

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
            tours = GetTourListByLeaderId(tl_id);

            Tour newtour = new Tour();
            newtour = GetTourInfoById(tid);
            DateTime NewStartDate = Convert.ToDateTime(newtour.StartDate);
            DateTime NewEndDate = Convert.ToDateTime(newtour.EndDate);

            if (tours.Count() != 0)
            {
                foreach (Tour t in tours)
                {
                    DateTime OldStartDate = Convert.ToDateTime(t.StartDate);
                    DateTime OldEndDate = Convert.ToDateTime(t.EndDate);
                    
                    if (NewStartDate == OldStartDate)
                    {
                        Isok = false;
                        break;
                    }
                    else if (NewStartDate > OldStartDate && NewStartDate < OldEndDate)
                    {
                        Isok = false;
                        break;
                    }
                    else if (NewEndDate > OldStartDate && NewEndDate < OldEndDate) //just in case
                    {
                        Isok = false;
                        break;
                    }
                }
            }
            return Isok;
        }


    }
}