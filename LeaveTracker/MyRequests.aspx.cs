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
    public partial class MyRequests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindGrid();
        }
        private void BindGrid()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                using (SqlCommand cmd = new SqlCommand("GetUserRequests", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userID", int.Parse(Session["UserID"].ToString())));
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    gvRequests.DataSource = dt;
                    gvRequests.DataBind();

                }
            }

        }

        protected void gvRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton lnk = (LinkButton)e.Row.FindControl("lnkCancel");
                Label lblRequestStatus = (Label)e.Row.FindControl("lblRequestStatus");
                if (lnk.CommandName == ((int)CommonMethods.Status.Rejected).ToString())
                {
                    lblRequestStatus.Text = "Rejected";
                    lnk.Visible = false;
                }
                else if (lnk.CommandName == ((int)CommonMethods.Status.Approved).ToString())
                    lblRequestStatus.Text = "Approved";
                else if (lnk.CommandName == ((int)CommonMethods.Status.Cancelled).ToString())
                    lblRequestStatus.Text = "Cancelled";
                else
                    lblRequestStatus.Text = CommonMethods.Status.Pending.ToString();

                if ((lnk.CommandName != "0" && lnk.CommandName != "3") && Convert.ToDateTime(e.Row.Cells[2].Text) > Convert.ToDateTime(DateTime.Now.ToString()))
                    lnk.Visible = true;

            }
        }
        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            using (SqlConnection con = Connection.GetConnection())
            {
                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                using (SqlCommand cmd = new SqlCommand("UpdateEmployeeLeaveStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@userID", int.Parse(Session["UserID"].ToString())));
                    cmd.Parameters.AddWithValue("@leaveID", int.Parse(lnk.CommandArgument));
                    cmd.Parameters.AddWithValue("@leaveStatus", "");

                    cmd.Parameters.AddWithValue("@userComment", "");
                    cmd.Parameters.AddWithValue("@updatedBy", "u");
                    cmd.Parameters.AddWithValue("@requestStatus", 0);
                    cmd.ExecuteNonQuery();

                    BindGrid();
                }
            }


        }
        protected string GetDate(string status, string comment, string updatedDate)
        {
            if (status == "Pending")
            {
                return status;
            }
            else if (status == "Rejected")
                return status + "  " + updatedDate.ToDateFormat() + "<br/><a href='javascript:void(0)' onclick=\"alert('" + comment + "')\">" + comment.TrimString(30) + "</a>";
            else
            {

                return status + "  " + updatedDate.ToDateFormat();
            }

        }

    }
}