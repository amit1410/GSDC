using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GSDC.App_Code;

namespace GSDC.Handlers
{
    /// <summary>
    /// Summary description for GetLeaveReport
    /// </summary>
    public class GetLeaveReport : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string serEmployee = "";

            List<LeaveDetail> listLeave = new List<LeaveDetail>();
            StringBuilder jsonBuilder = new StringBuilder();
            using (System.Data.SqlClient.SqlConnection con = Connection.GetConnection())
            {
                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();

                using (System.Data.SqlClient.SqlCommand cmd = new SqlCommand("GetLeaveReport", con))
                {
                    // string leaveType = context.Request.QueryString["leaveType"];
                    string reportType = context.Request.QueryString["reportType"];
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userID", int.Parse(context.Session["UserID"].ToString())));
                    cmd.Parameters.Add(new SqlParameter("@reportType", reportType));
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
                        listLeave.Add(new LeaveDetail { LeaveType = dr["Short_Desc"].ToString(), LeaveCount = Convert.ToInt32(dr["Leaves_Count"].ToString()) });

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