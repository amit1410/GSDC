using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using GSDC.App_Code;

namespace GSDC.LeaveTracker
{
    public partial class LeaveDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (PreviousPage != null)
                BindGrid();
            else
                Response.Redirect("LeaveReport.aspx");
        }
        private void BindGrid()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                HiddenField hfLeaveType = (HiddenField)Page.PreviousPage.Form.FindControl("MainContent").FindControl("ContentLeaveTracker").FindControl("hfLeaveType");
                HiddenField hfReportCategory = (HiddenField)Page.PreviousPage.Form.FindControl("MainContent").FindControl("ContentLeaveTracker").FindControl("hfReportCategory");

                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                string strSP = "GetDetailedLeaveReport";
                gvdDetailedLeaves.Visible = false;
                gvmDetailedLeaves.Visible = false;
                gvOutage.Visible = false;
                if (hfReportCategory.Value == "o")
                {
                    Page.Title = "Outage Details";
                    strSP = "GetTodayLeaveDetails";
                    gvOutage.Visible = true;
                    BindOutageDetails(con, hfLeaveType.Value);
                }
                else if (hfReportCategory.Value == "d")
                {
                    Page.Title = "Today's Leave Details";
                    gvdDetailedLeaves.Visible = true;
                    BindTodayDetails(con, hfLeaveType.Value);
                }
                else
                {
                    Page.Title = "Monthly Leave Details";
                    gvmDetailedLeaves.Visible = true;
                    BindMonthDetails(con, hfLeaveType.Value);
                }

            }
        }
        private void BindTodayDetails(SqlConnection con, string strLeaveType)
        {
            using (SqlCommand cmd = new SqlCommand("GetDetailedLeaveReport", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userID", int.Parse(Session["UserID"].ToString())));

                cmd.Parameters.Add(new SqlParameter("@leaveType", strLeaveType));
                cmd.Parameters.Add(new SqlParameter("@reportType", "d"));


                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                gvdDetailedLeaves.DataSource = dt;
                gvdDetailedLeaves.DataBind();

            }
        }
        private void BindMonthDetails(SqlConnection con, string strLeaveType)
        {
            using (SqlCommand cmd = new SqlCommand("GetDetailedLeaveReport", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userID", int.Parse(Session["UserID"].ToString())));

                cmd.Parameters.Add(new SqlParameter("@leaveType", strLeaveType));
                cmd.Parameters.Add(new SqlParameter("@reportType", "m"));


                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                gvmDetailedLeaves.DataSource = dt;
                gvmDetailedLeaves.DataBind();

            }
        }
        private void BindOutageDetails(SqlConnection con, string outageDate)
        {
            using (SqlCommand cmd = new SqlCommand("GetTodayLeaveDetails", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userID", int.Parse(Session["UserID"].ToString())));
                cmd.Parameters.Add(new SqlParameter("@date", outageDate));

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                gvOutage.DataSource = dt;
                gvOutage.DataBind();

            }
        }
    }
}