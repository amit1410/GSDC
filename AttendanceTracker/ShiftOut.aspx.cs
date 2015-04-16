using GSDC.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace GSDC.AttendanceTracker
{
    public partial class ShiftOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindAttendance();
            BindShift();
            using (SqlConnection con = Connection.GetConnection())
            {
                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                using (SqlCommand cmd = new SqlCommand("GetAttendanceStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userID", Convert.ToInt32(Session["UserId"].ToString())));
                    cmd.Parameters.Add(new SqlParameter("@attendanceType", Convert.ToInt32("0")));
                    cmd.Parameters.Add(new SqlParameter("@ShiftID", Convert.ToInt32("0")));

                    cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    cmd.Parameters["@ReturnVal"].Direction = ParameterDirection.ReturnValue;

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    Int32 status = (int)cmd.Parameters["@ReturnVal"].Value;
                    if (status == 1)
                    {
                        lblStatus.Text = "You are on leave";
                        return;
                    }
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["Is_Duration"].ToString() == "True")
                        {
                            ddlAttendanceType.SelectedValue = dt.Rows[0]["a_ID"].ToString();
                            ddlShift.SelectedValue = dt.Rows[0]["ShiftID"].ToString();
                            ddlAttendanceType.Enabled = false;
                            ddlShift.Enabled = false;
                         
                            if (dt.Rows[0]["Out_Time"].ToString() == "")
                            {
                                btnSubmit.Text = "Out";
                                btnSubmit.CommandArgument = "1";
                                btnSubmit.CommandName = dt.Rows[0]["ID"].ToString();
                            }
                            else
                            {
                                lblStatus.Text = "You are logout";
                                btnSubmit.Visible = false;
                                ddlAttendanceType.Enabled = false;
                                ddlShift.Enabled = false;
                            }
                        }
                        else
                        {
                            lblStatus.Text = "You have already saved the attendance";
                            btnSubmit.Enabled = false;
                            btnSubmit.Visible = false;
                            ddlAttendanceType.Enabled = false;
                            ddlShift.Enabled = false;
                        }

                       




                    }
                }
            }
        }
        private void BindAttendance()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                using (SqlCommand cmd = new SqlCommand("SELECT ID,Short_Desc FROM Master_AttendanceTypes", con))
                {

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    ddlAttendanceType.DataSource = dt;
                    ddlAttendanceType.DataBind();
                    ddlAttendanceType.Items.Insert(0, new ListItem("Select Attendance", ""));

                }
            }
        }

        private void BindShift()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                using (SqlCommand cmd = new SqlCommand("SELECT ID,ShiftID,ShiftFrom+''+ShiftTo Shifttime FROM Master_Shift", con))
                {

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    ddlShift.DataSource = dt;
                    ddlShift.DataBind();
                    ddlShift.Items.Insert(0, new ListItem("Select Shift", ""));

                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = Connection.GetConnection())
            {

                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                using (SqlCommand cmd = new SqlCommand("SaveAttendance", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userID", Convert.ToInt32(Session["UserId"].ToString())));
                    if (btnSubmit.Text == "Out")
                        cmd.Parameters.Add(new SqlParameter("@attendanceType", Convert.ToInt32(btnSubmit.CommandName)));
                    else
                        cmd.Parameters.Add(new SqlParameter("@attendanceType", Convert.ToInt32(ddlAttendanceType.SelectedValue.ToString())));
                    cmd.Parameters.Add(new SqlParameter("@ShiftType", Convert.ToInt32(ddlShift.SelectedValue.ToString())));
                    cmd.Parameters.Add(new SqlParameter("@operation", Convert.ToInt32(btnSubmit.CommandArgument)));
                    cmd.ExecuteNonQuery();
                    Response.Redirect("../Login.aspx");
                    //DataTable dt = new DataTable();
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //da.Fill(dt);

                    //ddlAttendanceType.DataSource = dt;
                    //ddlAttendanceType.DataBind();
                    //ddlAttendanceType.Items.Insert(0, new ListItem("Select Attendance", ""));

                }
            }
            Response.Redirect("~/WorkManagement/EmployeeActivity.aspx");
        }
        protected void ddlAttendanceType_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
        protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                using (SqlCommand cmd = new SqlCommand("GetAttendanceStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userID", Convert.ToInt32(Session["UserId"].ToString())));
                    cmd.Parameters.Add(new SqlParameter("@attendanceType", Convert.ToInt32(ddlAttendanceType.SelectedValue.ToString())));
                    cmd.Parameters.Add(new SqlParameter("@ShiftID", Convert.ToInt32(ddlShift.SelectedValue.ToString())));

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["Is_Duration"].ToString() == "True")
                        {
                            if (dt.Rows[0]["a_ID"].ToString() == ddlAttendanceType.SelectedValue)
                            {
                                btnSubmit.Enabled = true;
                                lblStatus.Text = "";
                            }
                            else
                            {
                                lblStatus.Text = "Please select a valid attendance type";
                                btnSubmit.Enabled = false;
                            }
                            btnSubmit.Text = "Out";
                        }
                        else
                        {
                            lblStatus.Text = "You have already saved the attendance";
                            btnSubmit.Enabled = false;
                        }
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                        lblStatus.Text = "";
                        btnSubmit.Text = "IN";
                    }

                }
            }
        }

    }

}