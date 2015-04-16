using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GSDC.App_Code
{
    public static class Connection
    {

        public static SqlConnection GetConnection()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["GSDCConnectionString"].ToString());
                con.Open();
                return con;
            
        }

        public static SqlConnection GetProjectChartConnection()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectChartConnectionString"].ToString());
            con.Open();
            return con;

        }

    }
}