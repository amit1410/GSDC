using GSDC.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GSDC.Admin
{
    public partial class ManageLeaves : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearchName.Text != "")
            {
                string strUserName = txtSearchName.Text.Split('(')[0];
                using (SqlConnection con = Connection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                    }
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    foreach (GridViewRow dr in gvLeaves.Rows)
                    {


                        TextBox txtCount = (TextBox)dr.FindControl("txtLeaveCount");

                    }
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);



                }
            }
        }
        [WebMethod]
        public static List<string> GetAutoCompleteData(string name)
        {
            
            using (SqlConnection conn = Connection.GetConnection())
            {


                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "Select concat(User_Name,'(',First_Name,' ',Last_Name,')') Name From Master_Users where User_Name like('" + name + "%')";

                    cmd.Connection = conn;

                    StringBuilder sb = new StringBuilder();

                  //  conn.Open();
                    List<string> result = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            result.Add(sdr["Name"].ToString());
                            //sb.Append(sdr["Name"])

                            //.Append(Environment.NewLine);

                        }

                    }

                    conn.Close();
                    return result;
                    //context.Response.Write(sb.ToString());

                }

            }
        }
    }
}