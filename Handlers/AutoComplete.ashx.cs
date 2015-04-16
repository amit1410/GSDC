using GSDC.App_Code;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace GSDC.Handlers
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class AutoComplete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string prefixText = context.Request.QueryString["q"];

            using (SqlConnection conn = Connection.GetConnection())
            {

                
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "Select concat(First_Name,' ',Last_Name,'(',User_Name,')') Name From FileMaster where User_Name like('%" + prefixText + "%')";

                    cmd.Connection = conn;

                    StringBuilder sb = new StringBuilder();

                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {

                            sb.Append(sdr["Name"])

                            .Append(Environment.NewLine);

                        }

                    }

                    conn.Close();

                    context.Response.Write(sb.ToString());

                }

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}