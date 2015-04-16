using GSDC.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GSDC.LeaveTracker
{
    public partial class MyApprovals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindGrid();
            // SendEmail();
        }
        /// <summary>
        /// List all the requests to be approved by the user
        /// </summary>
        private void BindGrid()
        {
            SqlConnection con = Connection.GetConnection();
            //string strQuery = "Select els.id,mu.Email Requestor,concat(mu.Last_Name,', ',mu.First_Name) RequestorName,els.Created_By,els.Created_Date,Start_Date,End_Date,Leave_Type from Employee_Leaves_Status els inner join Master_Users mu on els.Created_By=mu.Id inner join Master_Leave_Types mlt on mlt.Id=els.Leave_ID where els.Deputy_Status='Pending' and els.Deputy_ID=" + Session["UserID"].ToString();
            //if (Session["UserRole"].ToString() == "Manager")
            //{
            //    strQuery = "Select els.id,mu.Email Requestor,concat(mu.Last_Name,', ',mu.First_Name) RequestorName,els.Created_By,els.Created_Date,Start_Date,End_Date,Leave_Type from Employee_Leaves_Status els inner join Master_Users mu on els.Created_By=mu.Id inner join Master_Leave_Types mlt on mlt.Id=els.Leave_ID where els.Deputy_Status='Approved' and els.Manager_Status='Pending' and els.Manager_ID=" + Session["UserID"].ToString();
            //}
            SqlCommand cmd = new SqlCommand("GetApprovalRequests", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userID", Int32.Parse(Session["UserID"].ToString()));
            cmd.Parameters.AddWithValue("@role", Convert.ToBoolean(Session["IsApprover"].ToString()) ? "approver" : "");
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            gvApprovals.DataSource = dt;
            gvApprovals.DataBind();


        }
        protected void btnSubmit_click(object sender, EventArgs e)
        {
            CommonMethods commonMethods = new CommonMethods();
            SqlConnection con = Connection.GetConnection();
            string strSubject = "";
            char updatedBy = 'd';
            int requestStatus = 1;

            if (ddlApprovalStatus.SelectedItem.Text != "Approve")
                requestStatus = 3;
            string strQuery = "Update Employee_Leaves_Status set Deputy_Status='" + ddlApprovalStatus.SelectedValue + "',Deputy_Updated_Date='" + DateTime.Now + "',Deputy_Reason='" + txtReason.Text.Trim() + "' where id=" + btnSubmit.CommandArgument;
            if (Session["IsApprover"].ToString() == "True")
            {
                // strQuery = "Update Employee_Leaves_Status set Manager_Status='" + ddlApprovalStatus.SelectedValue + "',Manager_Updated_Date='" + DateTime.Now + "',Manager_Reason='" + txtReason.Text.Trim() + "' where id=" + btnSubmit.CommandArgument;
                strSubject = "Leave request sent to your manager for acceptance";
                updatedBy = 'm';
                if (ddlApprovalStatus.SelectedItem.Text == "Approve")
                    requestStatus = 2;
            }
            // SqlCommand cmd = new SqlCommand(strQuery, con);
            SqlCommand cmd = new SqlCommand("UpdateEmployeeLeaveStatus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@leaveID", int.Parse(btnSubmit.CommandArgument));
            cmd.Parameters.AddWithValue("@leaveStatus", ddlApprovalStatus.SelectedValue);

            cmd.Parameters.AddWithValue("@userID", int.Parse(Session["UserID"].ToString()));
            cmd.Parameters.AddWithValue("@userComment", txtReason.Text.Trim());
            cmd.Parameters.AddWithValue("@updatedBy", updatedBy);
            cmd.Parameters.AddWithValue("@requestStatus", requestStatus);
            cmd.ExecuteNonQuery();

            string strLeaveDetails = CommonMethods.LeaveDetails(btnSubmit.CommandArgument, hfLeaveType.Value, Convert.ToDateTime(hfStartDate.Value), Convert.ToDateTime(hfEndDate.Value));
            //"</br></br><b>Below is your leave request details.</b></br></br>";
            //strLeaveDetails += "<div style='line-height: 2px'> Leave Request ID - " + btnSubmit.CommandArgument + "</br>";
            //strLeaveDetails += "Type of Leave - " + hfLeaveType.Value + "</br>";
            //strLeaveDetails += "Start Date - " +String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(hfStartDate.Value)) + "</br>";
            //strLeaveDetails += "End Date - " + String.Format("{0:dddd, MMMM d, yyyy}",Convert.ToDateTime(hfEndDate.Value)) + "</div></br>";

            StringBuilder strBody = new StringBuilder("Hi " + btnSubmit.CommandName.ToString() + "</br></br>");

            if (ddlApprovalStatus.SelectedItem.Text == "Approve")
            {
                if (Session["IsApprover"].ToString() == "True")
                {
                    strQuery = "Update Employee_Leaves set Consumed_Leaves=(Consumed_Leaves+" + hfLeaveCount.Value + ") where User_ID=" + hfCreatedBy.Value + " and Leave_ID=" + hfLeaveTypeID.Value;
                    cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();

                    strSubject = "Leave request is approved";
                    strBody.AppendLine("Your leave request is approved by your manager.</br>");
                    strBody.AppendLine(strLeaveDetails);
                }
                else
                {

                    strSubject = "Leave request sent to your manager for acceptance.";
                    StringBuilder strForManager = new StringBuilder("Hi " + Session["UserMangerFullName"].ToString() + "</br></br>");
                    strForManager.AppendLine(Session["UserFullName"] + " has accepted deputy role for " + lblRequestor.Text + "during leave duration.");
                    strForManager.AppendLine(strLeaveDetails);

                    strBody.AppendLine(Session["UserFullName"] + " has accepted deputy role during your leave duration.");
                    strBody.AppendLine(strLeaveDetails);
                    strQuery = "Select user_name from Master_Users where id=" + Session["UserManagerID"];
                    cmd = new SqlCommand(strQuery, con);
                    string manager = (string)cmd.ExecuteScalar();

                    commonMethods.SendEmail(Session["UserEmail"].ToString(), manager, "Leave request", strForManager.ToString());


                }
            }
            else
            {
                strSubject = "Leave request is rejected";

                if (Session["IsApprover"].ToString() == "True")
                {
                    strBody.AppendLine("Your leave request is rejected by your manager.");
                }
                else
                {
                    strBody.AppendLine("Your leave request is rejected by your deputy.");
                }
                strBody.AppendLine("Reason:" + txtReason.Text);
            }



            commonMethods.SendEmail(Session["UserEmail"].ToString(), lblRequestor.Text, strSubject, strBody.ToString());
            pnlDetails.Visible = false;
            BindGrid();

        }
        /// <summary>
        /// Get requestor details for the request to be approved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkRequestor_click(object sender, EventArgs e)
        {

            pnlDetails.Visible = true;
            LinkButton lnkButton = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lnkButton.NamingContainer;
            HiddenField hfCount = (HiddenField)gvRow.FindControl("hfLeaveCount");
            HiddenField hfLeaveID = (HiddenField)gvRow.FindControl("hfLeaveID");
            lblRequestor.Text = lnkButton.Text;
            btnSubmit.CommandArgument = lnkButton.CommandArgument;
            btnSubmit.CommandName = lnkButton.CommandName;
            lblID.Text = lnkButton.CommandArgument;
            ddlApprovalStatus.SelectedIndex = 0;
            hfLeaveType.Value = gvRow.Cells[5].Text.ToString();
            hfStartDate.Value = gvRow.Cells[4].Text.ToString();
            hfEndDate.Value = gvRow.Cells[3].Text.ToString();
            hfCreatedBy.Value = lnkButton.CssClass;
            hfLeaveCount.Value = hfCount.Value;
            hfLeaveTypeID.Value = hfLeaveID.Value;

        }

    }
}