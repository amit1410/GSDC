using GSDC.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GSDC.LeaveTracker
{
    public partial class LeaveReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["IsApprover"].ToString() != "True")
                Response.Redirect("MyRequests.aspx");
        }
        protected void btnToday_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                using (SqlCommand cmd = new SqlCommand("GetDetailedLeaveReport", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userID", int.Parse(Session["UserID"].ToString())));
                    cmd.Parameters.Add(new SqlParameter("@leaveType", hfLeaveType.Value));
                    cmd.Parameters.Add(new SqlParameter("@reportType", "d"));
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    gvdDetailedLeaves.DataSource = dt;
                    gvdDetailedLeaves.DataBind();

                }
            }
        }
        protected void btnMonthly_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                using (SqlCommand cmd = new SqlCommand("GetDetailedLeaveReport", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userID", int.Parse(Session["UserID"].ToString())));
                    cmd.Parameters.Add(new SqlParameter("@leaveType", hfLeaveType.Value));
                    cmd.Parameters.Add(new SqlParameter("@reportType", "m"));
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    gvmDetailedLeaves.DataSource = dt;
                    gvmDetailedLeaves.DataBind();

                }
            }
        }
        protected void btnMonthlyOutage_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                using (SqlCommand cmd = new SqlCommand("GetTodayLeaveDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userID", int.Parse(Session["UserID"].ToString())));
                    cmd.Parameters.Add(new SqlParameter("@date", hfLeaveType.Value));

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    gvOutage.DataSource = dt;
                    gvOutage.DataBind();

                }
            }
        }
    }
}