using GSDC.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GSDC.LeaveTracker
{
    public partial class NewRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               

                lblManager.Text = Session["UserMangerFullName"].ToString().Trim() == "," ? "" : Session["UserMangerFullName"].ToString().Trim();
                lblName.Text = Session["UserFullName"].ToString();
                lblRole.Text = Session["UserRole"].ToString();
                lblTeam.Text = Session["UserTeam"].ToString();
                using (SqlConnection con = Connection.GetConnection())
                {
                    //  BindGrid();
                    // Get all master leave types
                    BindLeaveTypes(con);
                    // Get user for the same manager
                    BindDeputy(con);
                    // Get employee leaves i.e. consumed, balance
                    BindLeaves(con);
                    using (SqlCommand cmd = new SqlCommand("CheckPendingRequest", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@userID", Convert.ToInt32(Session["UserId"].ToString())));
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        int count = (int)cmd.ExecuteScalar();
                        con.Close();
                        if (count > 0)
                        {
                            btnCheckDeputy.Visible = false;
                            lblDeputyStatus.Text = "You can not apply for new request.Your request is already in pending state.";

                        }
                    }
                }

                //    calExtenderEnd.StartDate = DateTime.Now;
                //  calExtenderStart.StartDate = DateTime.Now;
            }
        }
        /// <summary>
        /// Get master leave types
        /// </summary>
        /// <param name="con">Sql server connection</param>
        private void BindLeaveTypes(SqlConnection con)
        {

            //        SqlCommand cmd = new SqlCommand(@"Select concat(leave_type,' (',Short_Desc,')') Leave, mlt.ID from Master_Leave_Types mlt 
            //                                        inner join Employee_Leaves el on el.Leave_ID=mlt.Id  
            //                                        Where (el.Total_Leaves+Carried_Forwarded) >el.Consumed_Leaves and mlt.Is_Active=1 and el.User_ID=" + Session["UserID"].ToString() + " and Applied_Year='" + DateTime.Now.Year + "'", con);
            using (SqlCommand cmd = new SqlCommand("GetLeaveTypes", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userID", Convert.ToInt32(Session["UserId"].ToString())));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (con.State == ConnectionState.Open)
                    con.Close();
                ddlLeaveTypes.DataSource = dt;
                ddlLeaveTypes.DataBind();
                ddlLeaveTypes.Items.Insert(0, new ListItem("Select Leave Type", ""));
                // ddlLeaveTypes.SelectedIndex = dt.Rows.Count;
            }
        }
        protected void btnTest_Click(object sender, EventArgs e)
        { 
        
        }
        /// <summary>
        /// Get employee leave details i.e. total, balance
        /// </summary>
        /// <param name="con">Sql server connection</param>
        private void BindLeaves(SqlConnection con)
        {
            //        SqlCommand cmd = new SqlCommand(@"Select concat(leave_type,' (',Short_Desc,')') Leave,(el.Total_Leaves+Carried_Forwarded-Consumed_Leaves) Balance_Leaves,(el.Total_Leaves+Carried_Forwarded) Total_Leaves from Master_Leave_Types mlt 
            //                                        inner join Employee_Leaves el on el.Leave_ID=mlt.Id  
            //                                        Where mlt.Is_Active=1 and el.User_ID=" + Session["UserID"].ToString() + " and Applied_Year='" + DateTime.Now.Year + "'", con);
            using (SqlCommand cmd = new SqlCommand("GetEmployeeBalaceLeaves", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userID", Convert.ToInt32(Session["UserId"].ToString())));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvLeaves.DataSource = dt;
                gvLeaves.DataBind();
            }
        }
        /// <summary>
        /// Get users from same manager for deputy 
        /// </summary>
        /// <param name="con"></param>
        private void BindDeputy(SqlConnection con)
        {
            // SqlCommand cmd = new SqlCommand("select concat(Last_Name,',',First_Name) Deputy,Id from Master_Users Where Manager_ID=" + Session["UserManagerID"].ToString() + " and role_id=" + Session["UserRoleID"].ToString() + " and id !=" + Session["UserID"].ToString() + "", con);
            using (SqlCommand cmd = new SqlCommand("GetDeputy", con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userID", Convert.ToInt32(Session["UserId"].ToString())));
                cmd.Parameters.Add(new SqlParameter("@teamID", Convert.ToInt32(Session["UserTeamID"].ToString())));
                cmd.Parameters.Add(new SqlParameter("@managerID", Convert.ToInt32(Session["UserManagerID"].ToString())));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (con.State == ConnectionState.Open)
                    con.Close();
                ddlDeputy.DataSource = dt;
                ddlDeputy.DataBind();
                ddlDeputy.Items.Insert(0, new ListItem("Select Deputy", ""));
                // ddlDeputy.SelectedIndex = dt.Rows.Count;
            }
        }
        private void SetLeaveBeforeStatus(int days)
        {
            lblDeputyStatus.Text= "Please apply atleast " + days + " days before for this category.";
        }
        protected void btnSubmit_click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                DateTime startDate = Convert.ToDateTime(txtStartDate.Text);
                int days = (startDate - DateTime.Now).Days;
                string strLeaveType=ddlLeaveTypes.SelectedItem.Text;
                if (strLeaveType.Substring(strLeaveType.IndexOf('(') + 1) == "CL)" && days < CommonMethods.CL_AppliedBefore)
                    SetLeaveBeforeStatus( CommonMethods.CL_AppliedBefore);
                else if (strLeaveType.Substring(strLeaveType.IndexOf('(') + 1) == "EL)" && days < CommonMethods.EL_AppliedBefore)
                     SetLeaveBeforeStatus(CommonMethods.EL_AppliedBefore);
                else if (strLeaveType.Substring(strLeaveType.IndexOf('(') + 1) == "SL)" && days <= CommonMethods.SL_AppliedBefore)
                     SetLeaveBeforeStatus(CommonMethods.SL_AppliedBefore);
                else if (strLeaveType.Substring(strLeaveType.IndexOf('(') + 1) == "CompL)" && days <= CommonMethods.Comp_AppliedBefore)
                     SetLeaveBeforeStatus(CommonMethods.Comp_AppliedBefore);
                else
                {
                    using (SqlConnection con = Connection.GetConnection())
                    {

                        int count = 0;
                        SqlCommand cmd = new SqlCommand("CheckPendingRequest", con);

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@userID", Convert.ToInt32(Session["UserId"].ToString())));
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        count = (int)cmd.ExecuteScalar();
                        con.Close();
                        if (count > 0)
                        {
                            btnCheckDeputy.Visible = false;
                            lblDeputyStatus.Text = "You can not apply for new request.Your request is already in pending state.";

                        }
                        else
                        {

                            DateTime endDate = Convert.ToDateTime(txtEndDate.Text);
                            count = (endDate - startDate).Days + 1;
                            count = CommonMethods.LeaveDaysCount(startDate, endDate, true, new List<DateTime>());

                            // SqlCommand cmd = new SqlCommand("SaveEmployeeLeave,insert into Employee_Leaves_Status OUTPUT INSERTED.ID values(" + ddlLeaveTypes.SelectedValue + ",'" + txtStartDate.Text + "','" + txtEndDate.Text + "'," + count + "," + ddlDeputy.SelectedValue + ",'Pending','',''," + Session["UserManagerID"] + ",'Pending','',''," + Session["UserID"] + ",'" + DateTime.Now + "')", con);
                            cmd = new SqlCommand("SaveEmployeeLeave", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@leaveID", 0);
                            cmd.Parameters.AddWithValue("@leaveTypeID", int.Parse(ddlLeaveTypes.SelectedValue));
                            cmd.Parameters.AddWithValue("@startDate", txtStartDate.Text);
                            cmd.Parameters.AddWithValue("@endDate", txtEndDate.Text);
                            cmd.Parameters.AddWithValue("@leaveCount", count);

                            cmd.Parameters.AddWithValue("@deputyID", int.Parse(ddlDeputy.SelectedValue));
                            cmd.Parameters.AddWithValue("@deputyLeaveStatus", "Pending");
                            cmd.Parameters.AddWithValue("@deputyComment", "");

                            cmd.Parameters.AddWithValue("@managerID", int.Parse(Session["UserManagerID"].ToString()));
                            cmd.Parameters.AddWithValue("@managerLeaveStatus", "Pending");
                            cmd.Parameters.AddWithValue("@managerComment", "");

                            cmd.Parameters.AddWithValue("@createdBy", int.Parse(Session["UserID"].ToString()));
                            cmd.Parameters.AddWithValue("@updatedBy", 'u');
                            cmd.Parameters.Add("@insertedLeaveID", SqlDbType.Int);
                            cmd.Parameters["@insertedLeaveID"].Direction = ParameterDirection.Output;

                            if (con.State == ConnectionState.Closed)
                                con.Open();
                            cmd.ExecuteNonQuery();

                            Int32 newId = Convert.ToInt32(cmd.Parameters["@insertedLeaveID"].Value.ToString());
                            if (newId == -1)
                                lblDeputyStatus.Text = "You have no quata for this leave type.";
                            else
                            {
                                cmd = new SqlCommand("select User_Name,concat(Last_Name,', ',First_Name) FullName from Master_Users where id=" + ddlDeputy.SelectedValue, con);
                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                string strLeaveDetails = CommonMethods.LeaveDetails(newId.ToString(), ddlLeaveTypes.SelectedItem.Text, Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text));

                                string strSubject = "Deputy role assigned";
                                StringBuilder strBody = new StringBuilder("Hi " + dt.Rows[0]["FullName"].ToString() + "</br></br>");
                                strBody.AppendLine(Session["UserFullName"] + " has assigned you as deputy role during leave duration.");
                                strBody.AppendLine(strLeaveDetails);
                                try
                                {
                                    CommonMethods commonMethods = new CommonMethods();
                                    commonMethods.SendEmail(Session["UserEmail"].ToString(), dt.Rows[0]["User_Name"].ToString(), strSubject, strBody.ToString());
                                }
                                catch
                                {
                                }
                                if (con.State == ConnectionState.Open)
                                    con.Close();
                                Response.Redirect("MyRequests.aspx");
                            }
                        }

                    }
                }
            }
        }
        protected void btnCheckDeputy_click(object sender, EventArgs e)
        {
            if (txtStartDate.Text.Substring(6) != DateTime.Now.Year.ToString() || txtEndDate.Text.Substring(6) != DateTime.Now.Year.ToString())
            {
                lblDeputyStatus.Text = "Please apply for current year.";

            }
            else
            {
                using (SqlConnection con = Connection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("CheckDuplicateDeputyDate", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userID", Convert.ToInt32(Session["UserId"].ToString()));
                        cmd.Parameters.AddWithValue("@deputyID", Convert.ToInt32(ddlDeputy.SelectedValue));
                        cmd.Parameters.AddWithValue("@startDate", String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(txtStartDate.Text)));
                        cmd.Parameters.AddWithValue("@endDate", String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(txtEndDate.Text)));

                        cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                        cmd.Parameters["@ReturnVal"].Direction = ParameterDirection.ReturnValue;

                        int count = 0;
                        try
                        {
                            cmd.ExecuteNonQuery();
                            count = (int)cmd.Parameters["@ReturnVal"].Value;
                        }
                        catch (Exception ex)
                        {
                            string str = ex.Message;
                        }
                        if (count == -1)
                            lblDeputyStatus.Text = "You are deputy during this period.Please select another dates.";
                        else if (count >= 2)
                        {
                            lblDeputyStatus.Text = ddlDeputy.SelectedItem.Text + " is not available.Please select another deputy.";
                        }
                        else if (count == 1)
                            lblDeputyStatus.Text = "You have already applied for this date.";
                        else
                            btnSubmit_click(null, null);
                    }
                    //   SqlCommand cmd = new SqlCommand("select id from Employee_Leave_Status where Employee_ID=" + ddlDeputy.SelectedValue + " and Start_Date='" + txtStartDate.Text + "'", con);

                }
            }
        }
    }
}