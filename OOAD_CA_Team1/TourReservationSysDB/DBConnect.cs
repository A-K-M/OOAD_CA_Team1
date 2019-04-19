using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;

namespace OOAD_CA_Team1.TourReservationSysDB
{
    public class DBConnect
    {
        SqlConnection cn;


        public DBConnect()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["OOAD_CA_ConnString"].ConnectionString);
        }
        public int SetData(SqlCommand cmd)
        {
            if (cn.State == ConnectionState.Closed) cn.Open();
            cmd.Connection = cn;
            int rowsAffected = cmd.ExecuteNonQuery();
            if (cn.State == ConnectionState.Open) cn.Close();
            return rowsAffected;
        }
        public DataTable GetData(SqlCommand cmd)
        {
            DataTable t1 = new DataTable();
            cmd.Connection = cn;
            if (cn.State == ConnectionState.Closed) cn.Open();
            t1.Load(cmd.ExecuteReader());
            if (cn.State == ConnectionState.Open) cn.Close();
            return t1;
        }
    }
}