using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GSDC.App_Code;

namespace GSDC.WorkManagement
{
    public partial class EmployeeActivity : System.Web.UI.Page
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataAdapter da = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Refresh", "180");
            if (!IsPostBack)
            
            {
               
                BindEmployeeStatus();
            
                BindActivity();
                BindEmployeData();
               
                lblMessage.Visible = false;
                try
                {
                    using (con = Connection.GetConnection())
                    {
                        using (cmd = new SqlCommand("ValidateActivity", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@userID", Convert.ToInt32(Session["UserId"].ToString())));
                            cmd.Parameters.Add(new SqlParameter("@ActivityID", Convert.ToInt32("0")));

                            cmd.Parameters.Add(new SqlParameter("@operation", Convert.ToInt32("1")));

                            DataTable dt = new DataTable();
                            da = new SqlDataAdapter(cmd);
                            da.Fill(dt);
                            if (dt.Rows.Count == 0)
                            {
                                lblMessage.Visible = true;
                                lblMessage.Text = "Please Punch in Your Attendence First";
                                ddlActivityType.Enabled = false;

                                btmstart.Enabled = false;

                            }
                            else if (Convert.ToString(dt.Rows[0]["Out_Time"]) != "")
                            {
                                lblMessage.Visible = true;
                                lblMessage.Text = "You have already Logged Out.";
                                ddlActivityType.Enabled = false;

                                btmstart.Enabled = false;
                            }


                        }

                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                    cmd.Dispose();
                    da.Dispose();

                }

                try
                {
                    using (con = Connection.GetConnection())
                    {
                        using (cmd = new SqlCommand("ValidateActivity", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@userID", Convert.ToInt32(Session["UserId"].ToString())));
                            cmd.Parameters.Add(new SqlParameter("@ActivityID", Convert.ToInt32("0")));

                            cmd.Parameters.Add(new SqlParameter("@operation", Convert.ToInt32("2")));

                            DataTable dt = new DataTable();
                            da = new SqlDataAdapter(cmd);
                            da.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                ddlActivityType.SelectedValue = dt.Rows[0]["Activity"].ToString();
                                inpHide .Value= dt.Rows[0]["Totaltime"].ToString();
                                ddlActivityType.Enabled = false;
                                btmstart.Enabled = false;
                                btmstart.Visible = false;
                                btnStop.Visible = true;
                                btnStop.Enabled = true;
                            
                            if (ddlActivityType.SelectedValue == "3")
                            {
                                BindWorkonRequest();

                                ddlRequest.SelectedValue = dt.Rows[0]["Request"].ToString();
                                BindWorkonSubRequest();
                                ddlSubRequest.SelectedValue = dt.Rows[0]["SubRequest"].ToString();
                                ddlRequest.Visible = true;
                                ddlSubRequest.Visible = true;
                                ddlRequest.Enabled = false;
                                ddlSubRequest.Enabled = false;
                            }
                            //   ScriptManager.RegisterStartupScript(this, typeof(string), "SHOW_ALERT", "changeColor()", true);
                            //  ScriptManager.RegisterStartupScript(GetType(), "Javascript", "javascript:changeColor(); ", true);
                            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:changeColor(); ", true);
                            }

                            else
                            {
                                SelectFirstIdle();
                            }

                        }

                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                    cmd.Dispose();
                    da.Dispose();

                }


            }

            else
            {
              
            }
        }

        
        protected void GvEmployeeStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        private void BindEmployeeStatus()
        {
            DataTable dt = new DataTable();
            StringBuilder sbs = new StringBuilder();
            sbs.Append(" select First_Name+' '+Last_name EmpName,b.Role_Name RoleName,c.Team_Name TeamName,e.Attendance_Type AttendenceType,f.ShiftFrom+''+f.ShiftTo ShiftTime from Master_Users a inner join Master_Roles b ");
            sbs.Append(" on a.Role_ID=b.ID inner join Master_Teams c on c.ID=a.Team_ID inner join User_Attendance d on a.ID=d.UserID ");
            sbs.Append(" inner join Master_AttendanceTypes e on e.ID=d.Attendance_TypeID inner join Master_Shift f on d.ShiftID=f.ID where UserID=@UserID and CONVERT (DATE, d.In_Time)=CONVERT (DATE, GETDATE()) ");


            try
            {
                using (con = Connection.GetConnection())
                {

                    // using (cmd = new SqlCommand("SELECT ID,ProjectID,ProjectName,convert(varchar, ProjectStartDate ,3) ProjectStartDate,convert(varchar, ProjectEndTime ,3) ProjectEndTime,convert(varchar, ProjectCreationDate ,3) ProjectCreationDate,convert(varchar, ProjectExpectedEnd ,3) ProjectExpectedEnd,ProjectOwner,UserID,ProgressPercent FROM GDSC_ProjectMaster", con))
                    using (cmd = new SqlCommand(sbs.ToString(), con))
                    {
                        DataSet ds = new DataSet();
                        cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToInt32(Session["UserID"].ToString());
                        da = new SqlDataAdapter(cmd);
                        da.Fill(ds);

                        DlstEmployeeHist.DataSource = ds.Tables[0];
                        DlstEmployeeHist.DataBind();


                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
                cmd.Dispose();
                da.Dispose();
                dt = null;
            }

        }

        private void BindActivity()
        {
            DataTable dt = new DataTable();
            try
            {
                using (con = Connection.GetConnection())
                {
                    // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                    using (cmd = new SqlCommand("SELECT ID,ActivityID,Activity_Desc FROM Master_Activity", con))
                    {



                        da = new SqlDataAdapter(cmd);
                        da.Fill(dt);

                        ddlActivityType.DataSource = dt;
                        ddlActivityType.DataBind();
                        ddlActivityType.Items.Insert(0, new ListItem("Select Activity", "0"));
                        ddlActivityType.SelectedValue = "12";

                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
                cmd.Dispose();
                da.Dispose();
                dt = null;
            }
        }

        protected void ddlActivityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlActivityType.SelectedValue == "3")
            {
                ddlRequest.Enabled = true;
                ddlRequest.Visible = true;
                //ddlSubRequest.Visible = true;
                //ddlSubRequest.Enabled = true;
                btmstart.Visible = true;
               // btnStop.Visible = true;
                btnSignOut.Visible = false;
                GdEmpReport.Visible = true;
              //  div1.Visible = true;
                BindWorkonRequest();
            }

            else if(ddlActivityType.SelectedValue=="11")
            {
                btmstart.Visible = false;
                btnStop.Visible = false;
                btnSignOut.Visible = true; 
                GdEmpReport.Visible = false;
                //div1.Visible = false;

               // Response.Redirect("../AttendanceTracker/ShiftOut.aspx");
            }
           
            else
            {
                ddlRequest.Visible = false;
                ddlSubRequest.Visible = false;
                btmstart.Visible = true;
                // btnStop.Visible = true;
                btnSignOut.Visible = false;
                GdEmpReport.Visible = true;
               // div1.Visible = true;

            }
        }


        private void BindWorkonRequest()
        {

            using (SqlConnection con = Connection.GetConnection())
            {
                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                using (SqlCommand cmd = new SqlCommand("SELECT ID,RequestID,Request_Desc FROM Master_Request ", con))
                {

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    ddlRequest.DataSource = dt;
                    ddlRequest.DataBind();
                    ddlRequest.Items.Insert(0, new ListItem("Select Request From", "0"));

                }
            }
        }

        private void BindWorkonSubRequest()
        {

            using (SqlConnection con = Connection.GetConnection())
            {
                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                using (SqlCommand cmd = new SqlCommand("SELECT ID,SubRequestID,SubRequest_Desc FROM Master_SubRequest where RequestID=@RequestID ", con))
                {


                    DataTable dt = new DataTable();
                    cmd.Parameters.Add("@RequestID", SqlDbType.Int).Value = Convert.ToInt32(ddlRequest.SelectedValue.ToString());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    ddlSubRequest.DataSource = dt;
                    ddlSubRequest.DataBind();
                    ddlSubRequest.Items.Insert(0, new ListItem("Select Sub Request From", "0"));

                }
            }
        }

        protected void btmstart_Click(object sender, EventArgs e)
        {
            if (ValidateActivity())
            {
                lblMessage.Visible = false;
                lblMessage.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "SHOW_ALERT", "changeColor()", true);
                btmstart.Visible = false;
                btnStop.Visible = true;
                string TotalBreak = string.Empty;
                using (SqlConnection con = Connection.GetConnection())
                {

                    // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                    using (SqlCommand cmd = new SqlCommand("SaveActivity", con))
                    {
                        SqlDataReader dr = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@userID", Convert.ToInt32(Session["UserId"].ToString())));
                        cmd.Parameters.Add(new SqlParameter("@ActivityID", Convert.ToInt32(ddlActivityType.SelectedValue)));

                        if (ddlActivityType.SelectedValue == "3")
                        {
                            cmd.Parameters.Add(new SqlParameter("@operation", Convert.ToInt32("0")));
                            cmd.Parameters.Add(new SqlParameter("@RequestID", Convert.ToInt32(ddlRequest.SelectedValue)));
                            cmd.Parameters.Add(new SqlParameter("@SubRequestID", Convert.ToInt32(ddlSubRequest.SelectedValue)));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@operation", Convert.ToInt32("1")));
                            cmd.Parameters.Add(new SqlParameter("@RequestID", Convert.ToInt32("0")));
                            cmd.Parameters.Add(new SqlParameter("@SubRequestID", Convert.ToInt32("0")));
                        }

                        cmd.ExecuteNonQuery();
                        ddlActivityType.Enabled = false;
                        inpHide.Value = "0";
                        if (ddlActivityType.SelectedValue == "3")
                        {
                            ddlRequest.Enabled = false;
                            ddlSubRequest.Enabled = false;
                        }
                        ddlActivityType.Enabled = false;


                    }
                }

                BindEmployeData();
            }




        }



        private bool ValidateActivity()
        {
            bool Result = true;

            if (ddlActivityType.SelectedValue == "0")
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please select any Activity";
                Result = false;
            }

            if (ddlRequest.SelectedValue == "0")
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please select any Request";
                Result = false;
            }

            if (ddlSubRequest.SelectedValue == "0")
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please select any Sub Request";
                Result = false;
            }

            else
            {
                using (SqlConnection con = Connection.GetConnection())
                {

                    // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                    using (SqlCommand cmd = new SqlCommand("ValidateActivity", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@userID", Convert.ToInt32(Session["UserId"].ToString())));
                        cmd.Parameters.Add(new SqlParameter("@ActivityID", Convert.ToInt32(ddlActivityType.SelectedValue)));

                        cmd.Parameters.Add(new SqlParameter("@operation", Convert.ToInt32("0")));

                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);



                        //if (ddlActivityType.SelectedValue == "1")
                        //{

                        //    if (dt.Rows.Count >= 2)
                        //    {
                        //        lblMessage.Visible = true;
                        //        lblMessage.Text = "You have already avail Two Break of 15 Minute";
                        //        Result = false;
                        //    }
                        //}
                        //else if (ddlActivityType.SelectedValue == "2")
                        //{
                        //    if (dt.Rows.Count >= 1)
                        //    {
                        //        lblMessage.Visible = true;
                        //        lblMessage.Text = "You have already avail Break of 30 Minute";
                        //        Result = false;
                        //    }
                        //}
                    }
                }
            }

            return Result;

        }
        private void BindEmployeData()
        {
            DataTable dt = new DataTable();
            try
            {
               
                using (con = Connection.GetConnection())
                {
                    // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                    using (cmd = new SqlCommand("select b.ActivityID ActivityID, b.Activity_Desc Activity,CONVERT(VARCHAR(10), a.StartTime, 108) StartTime,CONVERT(VARCHAR(10), a.EndTime, 108) EndTime,a.TotalTime TotalTime from User_Activity a inner join Master_Activity b on a.ActivityID=b.ActivityID where a.UserID=@UserID and a.Act_Date=convert(date, GETDATE()) order by a.StartTime desc  ", con))
                    {
                        lblMessage.Visible = false;
                        GdEmpReport.Visible = true;

                        cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToInt32(Session["UserID"].ToString());
                        da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        if(dt.Rows.Count>0)
                        {
                            GdEmpReport.DataSource = dt;
                            GdEmpReport.DataBind();
                          //  div1.Visible = true;
                        }
                       

                    }
                }
            }

            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
                //cmd.Dispose();
                //da.Dispose();

                //cmd = null;
                //da = null;

                //dt = null;
            }
        }
        protected void btnStop_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "SHOW_ALERT", "stoptimer()", true);
            btmstart.Visible = true;
            btnStop.Visible = false;
            using (SqlConnection con = Connection.GetConnection())
            {

                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                using (SqlCommand cmd = new SqlCommand("SaveActivity", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userID", Convert.ToInt32(Session["UserId"].ToString())));
                    cmd.Parameters.Add(new SqlParameter("@ActivityID", Convert.ToInt32(ddlActivityType.SelectedValue)));

                    if (ddlActivityType.SelectedValue == "3")
                    {
                        cmd.Parameters.Add(new SqlParameter("@operation", Convert.ToInt32("2")));
                        cmd.Parameters.Add(new SqlParameter("@RequestID", Convert.ToInt32(ddlRequest.SelectedValue)));
                        cmd.Parameters.Add(new SqlParameter("@SubRequestID", Convert.ToInt32(ddlSubRequest.SelectedValue)));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@operation", Convert.ToInt32("4")));
                        cmd.Parameters.Add(new SqlParameter("@RequestID", Convert.ToInt32("0")));
                        cmd.Parameters.Add(new SqlParameter("@SubRequestID", Convert.ToInt32("0")));

                    }

                    cmd.ExecuteNonQuery();
                    ddlActivityType.Enabled = true;
                    ddlActivityType.SelectedValue = "0";
                    inpHide.Value = "";
                    ddlRequest.Visible = false;
                    ddlSubRequest.Visible = false;

                    btmstart.Enabled = true;

                }
                BindEmployeData();
            }

        }

        protected void ddlRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSubRequest.Visible = true;
            ddlSubRequest.Enabled = true;
            BindWorkonSubRequest();
        }

        protected void ddlSubRequest_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      

        protected void GdEmpReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label lbtext = (Label)e.Row.FindControl("lblActID");
                ImageButton img = (ImageButton)e.Row.FindControl("imgOrdersShow");
                if (lbtext.Text != "3")
                {
                    img.Visible = false;
                }
            }
            // int CountryId = Convert.ToInt32(e.Row.Cells[1].Text);

        }

        protected void gvChildGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gvRequest = (sender as GridView);
            gvRequest.PageIndex = e.NewPageIndex;
            BindChildGrid(gvRequest.ToolTip, gvRequest);
        }

        protected void gvSubChildGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gvProducts = (sender as GridView);
            gvProducts.PageIndex = e.NewPageIndex;
            BindSubChild(gvProducts.ToolTip, Convert.ToInt32(ViewState["RequestID"] ), gvProducts);
        }

        protected void imgOrdersShow_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgShowHide = (sender as ImageButton);
            GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
            if (imgShowHide.CommandArgument == "Show")
            {
                row.FindControl("pnlOrders").Visible = true;
                imgShowHide.CommandArgument = "Hide";
                imgShowHide.ImageUrl = "~/images/minus.png";
                string customerId = GdEmpReport.DataKeys[row.RowIndex].Value.ToString();
                GridView gvRequest = row.FindControl("gvChildGrid") as GridView;
                BindChildGrid(customerId, gvRequest);
            }
            else
            {
                row.FindControl("pnlOrders").Visible = false;
                imgShowHide.CommandArgument = "Show";
                imgShowHide.ImageUrl = "~/images/plus.png";
            }
        }


        private void BindChildGrid(string StartTime, GridView gvChildGrid)
        {
            gvChildGrid.ToolTip = StartTime;
         
            DataTable dt = new DataTable();
            try
            {
                using (con = Connection.GetConnection())
                {
                    // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                    using (cmd = new SqlCommand("select a.RequestID RequestID,b.ActivityID ActivityID,d.Request_Desc Request,CONVERT(VARCHAR(10), a.StartTime, 108) StartTime,CONVERT(VARCHAR(10), a.EndTime, 108) EndTime,a.TotalTime TotalTime from User_Activity a inner join Master_Activity b on a.ActivityID=b.ActivityID inner join Master_Request d on a.RequestID=d.RequestID  where a.UserID=@UserID and a.Act_Date=convert(date, GETDATE())  and  CONVERT(VARCHAR(10), a.StartTime, 108)=@StartTime order by a.StartTime desc ", con))
                    {
                        lblMessage.Visible = false;
                        GdEmpReport.Visible = true;

                        cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToInt32(Session["UserID"].ToString());
                        cmd.Parameters.Add("@StartTime", SqlDbType.VarChar).Value = StartTime;
                        da = new SqlDataAdapter(cmd);
                        da.Fill(dt);

                        gvChildGrid.DataSource = dt;
                        gvChildGrid.DataBind();


                    }
                }
            }

            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
                cmd.Dispose();
                da.Dispose();

                cmd = null;
                da = null;

                dt = null;
            }
        }

        protected void imgProductsShow_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgShowHide = (sender as ImageButton);
            GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
            if (imgShowHide.CommandArgument == "Show")
            {
                row.FindControl("pnlProducts").Visible = true;
                imgShowHide.CommandArgument = "Hide";
                imgShowHide.ImageUrl = "~/images/minus.png";
                string orderId = Convert.ToString((row.NamingContainer as GridView).DataKeys[row.RowIndex].Values[0].ToString());
                int RequestId = Convert.ToInt32((row.NamingContainer as GridView).DataKeys[row.RowIndex].Values[1].ToString());
                GridView gvSubRequest = row.FindControl("gvSubChildGrid") as GridView;
                BindSubChild(orderId,RequestId, gvSubRequest);
            }
            else
            {
                row.FindControl("pnlProducts").Visible = false;
                imgShowHide.CommandArgument = "Show";
                imgShowHide.ImageUrl = "~/images/plus.png";
            }
        }

        private void BindSubChild(string StartTime,int RequestID, GridView gvSubChildGrid)
        {
            gvSubChildGrid.ToolTip = StartTime.ToString();
            ViewState["RequestID"] = Convert.ToString(RequestID);
         
            DataTable dt = new DataTable();
            try
            {
                using (con = Connection.GetConnection())
                {

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" select a.UserID,d.RequestID RequestID,c.SubRequestID SubRequestID,b.ActivityID ActivityID,c.SubRequest_Desc SubRequest,CONVERT(VARCHAR(10), a.StartTime, 108) ");
                    sb.Append(" StartTime,CONVERT(VARCHAR(10), a.EndTime, 108) EndTime,a.TotalTime TotalTime  from User_Activity a inner join Master_Activity b on a.ActivityID=b.ActivityID  ");
                    sb.Append(" inner join Master_Request d on a.RequestID=d.RequestID inner join Master_SubRequest c on c.SubRequestID=a.SubRequestID  ");
                    sb.Append(" where a.UserID=@UserID and a.Act_Date=convert(date, Getdate()) and a.ActivityID=@ActivityID and c.RequestID=@RequestID and CONVERT(VARCHAR(10), a.StartTime, 108)=@StartTime order by a.StartTime desc   ");

                    // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                    using (cmd = new SqlCommand(sb.ToString(), con))
                    {
                        lblMessage.Visible = false;
                        GdEmpReport.Visible = true;

                        cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToInt32(Session["UserID"].ToString());
                        cmd.Parameters.Add("@ActivityID", SqlDbType.Int).Value = Convert.ToInt32("3");
                        cmd.Parameters.Add("@RequestID", SqlDbType.Int).Value = RequestID;
                        cmd.Parameters.Add("@StartTime", SqlDbType.VarChar).Value =StartTime;
                        da = new SqlDataAdapter(cmd);
                        da.Fill(dt);

                        gvSubChildGrid.DataSource = dt;
                        gvSubChildGrid.DataBind();


                    }
                }
            }

            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
                cmd.Dispose();
                da.Dispose();

                cmd = null;
                da = null;

                dt = null;
            }
        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {

           
            using (SqlConnection con = Connection.GetConnection())
            {
               
                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                using (SqlCommand cmd = new SqlCommand("SaveAttendance", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userID", Convert.ToInt32(Session["UserId"].ToString())));
                   
                        cmd.Parameters.Add(new SqlParameter("@attendanceType", Convert.ToInt32("0")));
               
                  
                    cmd.Parameters.Add(new SqlParameter("@ShiftType", Convert.ToInt32("0")));
                    cmd.Parameters.Add(new SqlParameter("@operation", Convert.ToInt32("1")));
                    cmd.ExecuteNonQuery();
                    Response.Redirect("../Login.aspx");
                   
                
                }
            }
        }

        protected void GdEmpReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GdEmpReport.PageIndex = e.NewPageIndex;
            BindEmployeData();
        }

        
        private void SelectFirstIdle()
        {
            if (ValidateActivity())
            {
                lblMessage.Visible = false;
                lblMessage.Text = "";

                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:changeColor(); ", true);
                btmstart.Visible = false;
                btnStop.Visible = true;
                string TotalBreak = string.Empty;
                using (SqlConnection con = Connection.GetConnection())
                {

                    // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                    using (SqlCommand cmd = new SqlCommand("SaveActivity", con))
                    {
                        SqlDataReader dr = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@userID", Convert.ToInt32(Session["UserId"].ToString())));
                        cmd.Parameters.Add(new SqlParameter("@ActivityID", Convert.ToInt32(ddlActivityType.SelectedValue)));

                        if (ddlActivityType.SelectedValue == "3")
                        {
                            cmd.Parameters.Add(new SqlParameter("@operation", Convert.ToInt32("0")));
                            cmd.Parameters.Add(new SqlParameter("@RequestID", Convert.ToInt32(ddlRequest.SelectedValue)));
                            cmd.Parameters.Add(new SqlParameter("@SubRequestID", Convert.ToInt32(ddlSubRequest.SelectedValue)));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@operation", Convert.ToInt32("1")));
                            cmd.Parameters.Add(new SqlParameter("@RequestID", Convert.ToInt32("0")));
                            cmd.Parameters.Add(new SqlParameter("@SubRequestID", Convert.ToInt32("0")));
                        }

                        cmd.ExecuteNonQuery();
                        ddlActivityType.Enabled = false;
                        inpHide.Value = "0";
                      
                        ddlActivityType.Enabled = false;


                    }
                }

                BindEmployeData();
            }
        }
       


    }
}