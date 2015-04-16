using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;
using System.Text;
using System.Collections.Generic;
using GSDC.App_Code;

namespace GSDC.Handlers
{
    /// <summary>
    /// Summary description for GetMonthlyOutage
    /// </summary>
    public class GetMonthlyOutage : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string serEmployee = "";

            List<OutageDetails> listLeave = new List<OutageDetails>();
            StringBuilder jsonBuilder = new StringBuilder();
            using (System.Data.SqlClient.SqlConnection con = Connection.GetConnection())
            {
                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();

                using (System.Data.SqlClient.SqlCommand cmd = new SqlCommand("GetTeamOutageRate", con))
                {
                    // string leaveType = context.Request.QueryString["leaveType"];

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userID", int.Parse(context.Session["UserID"].ToString())));
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    bool firstTime = true;

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!firstTime)
                        {
                            jsonBuilder.Append(",");
                        }
                        listLeave.Add(new OutageDetails { LeaveDate = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(dr["DateList"].ToString())), LeaveOutage = decimal.Round(Convert.ToDecimal(dr["OnLeave"].ToString()) / Convert.ToInt32(dr["TotalUsers"].ToString()) * 100, 2) });

                    }

                }
            }
            System.Web.Script.Serialization.JavaScriptSerializer jSearializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            context.Response.Write(jSearializer.Serialize(listLeave));
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