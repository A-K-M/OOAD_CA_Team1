using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using OOAD_CA_Team1.Models;

namespace OOAD_CA_Team1.DAO
{
    //public class GetName
    //{
    //    public vis GetNameById(int Id)
    //    {
    //        string name = "";
    //        return name;
    //    }
    //}
    public class TourDAO 
    {
        public static List<Tours> GetTourList()
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select t.*,p.TourPackageName from Tours t, TourPackages p where t.PackageId = p.TourPackageId ";

            List<Tours> tourlist = new List<Tours>();
            DataTable tbl = db.GetData(cmd);
            if (tbl != null)
            {
                foreach (DataRow r in tbl.Rows)
                {
                    Tours tours = new Tours();

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
                        tours.TourLeaderName = TourDAO.GetTourLeaderNameById(tours.TourLeaderId);
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
        public static Tours GetTourInfoById(int id)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"select t.*,p.TourPackageName from Tours t, TourPackages p where t.PackageId = p.TourPackageId and t.tourid ="+id;

            Tours tours = new Tours();
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
                        tours.TourLeaderName = TourDAO.GetTourLeaderNameById(tours.TourLeaderId);
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
        public static List<Tourleaders> GetTourLeaderById()
        {
            List<Tourleaders> tl_list = new List<Tourleaders>();
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"select * from TourLeaders order by Name";
            DataTable tbl = db.GetData(cmd);
            if (tbl != null)
            {
                foreach (DataRow r in tbl.Rows)
                {
                    Tourleaders tl = new Tourleaders();
                    var test = Convert.ToInt32(r[0].ToString());
                    tl.TourleaderId = Convert.ToInt32(r[0].ToString());
                    tl.Name = Convert.ToString(r[1].ToString());
                    tl_list.Add(tl);
                }
            }
            return tl_list;
        }
        public static void AssignTourleader(int tid, int tl_id)
        {
            SqlConnection con = new SqlConnection();
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"update tours set TourLeaderId = "+tl_id+" where TourId = "+tid+";";
            db.SetData(cmd);
        }
    }
}