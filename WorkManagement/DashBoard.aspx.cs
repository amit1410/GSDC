using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using GSDC.App_Code;

namespace GSDC.WorkManagement
{
    public partial class DashBoard : System.Web.UI.Page
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataAdapter da = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Refresh", "20");
            if (!Page.IsPostBack)
            {
                if (Convert.ToString(Session["UserTeamID"]) == "7")
                {
                    if (Convert.ToString(Session["IsApprover"]) == "True")
                    {
                        BindEmployee();

                    }
                    else
                    {
                        msg.Visible = false;
                        lblMessage.Visible = true;
                        lblMessage.Text = "You are not Authorised to view this Page";

                    }

                }

                else
                {
                    msg.Visible = false;
                    lblMessage.Visible = true;
                    lblMessage.Text = "You are not Authorised to view this Page";
                }
            }
        }
        private void BindEmployee()
        {
            using (con = Connection.GetConnection())
            {
                //StringBuilder sb = new StringBuilder();
                //sb.Append(" select c.First_Name+' '+c.Last_Name EmpName,cast(CASE When (select count(*) from Master_Users where id=c.ID and EndTime is not NULL)>0 then 'Logout' else b.Activity_Desc End as varchar(50)) ActDesc, ");
                //sb.Append(" CONVERT(VARCHAR(10), a.StartTime, 108) starttime,CONVERT(VARCHAR(10), a.EndTime, 108) Endtime, ");
                //sb.Append(" DATEDIFF(MINUTE, a.StartTime, convert(time, GETDATE())) Totaltime, ");
                //sb.Append(" a.Act_Date,a.ActivityID ActivityID from User_Activity a inner join Master_Activity b ");
                //sb.Append(" on a.ActivityID=b.ActivityID inner join Master_Users c on c.id=a.UserID ");
                //sb.Append(" where c.Reporting_Manager=@UserID and a.Act_Date=convert(date, GETDATE()) and a.StartTime in(select max(StartTime) from ");
                //sb.Append(" User_Activity where Act_Date=convert(date, GETDATE()) group by UserID) ");

                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                using (cmd = new SqlCommand("DashBoardActivity", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToInt32(Session["UserID"].ToString());
                     da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    gvDashBoard.DataSource = dt;
                    gvDashBoard.DataBind();


                }
            }
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
            BindSubChild(gvProducts.ToolTip, Convert.ToInt32(ViewState["RequestID"]), gvProducts);
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
                string customerId = gvDashBoard.DataKeys[row.RowIndex].Value.ToString();
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
                    using (cmd = new SqlCommand("select a.RequestID RequestID,b.ActivityID ActivityID,d.Request_Desc Request,CONVERT(VARCHAR(10), a.StartTime, 108) StartTime,CONVERT(VARCHAR(10), a.EndTime, 108) EndTime,a.TotalTime TotalTime from User_Activity a inner join Master_Activity b on a.ActivityID=b.ActivityID inner join Master_Request d on a.RequestID=d.RequestID  where a.Act_Date=convert(date, GETDATE())  and  CONVERT(VARCHAR(10), a.StartTime, 108)=@StartTime ", con))
                    {
                        lblMessage.Visible = false;
                        gvDashBoard.Visible = true;

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
                BindSubChild(orderId, RequestId, gvSubRequest);
            }
            else
            {
                row.FindControl("pnlProducts").Visible = false;
                imgShowHide.CommandArgument = "Show";
                imgShowHide.ImageUrl = "~/images/plus.png";
            }
        }

        private void BindSubChild(string StartTime, int RequestID, GridView gvSubChildGrid)
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
                    sb.Append(" where  a.Act_Date=convert(date, Getdate()) and a.ActivityID=@ActivityID and c.RequestID=@RequestID and CONVERT(VARCHAR(10), a.StartTime, 108)=@StartTime  ");

                    // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                    using (cmd = new SqlCommand(sb.ToString(), con))
                    {
                        lblMessage.Visible = false;
                        gvDashBoard.Visible = true;

                        cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToInt32(Session["UserID"].ToString());
                        cmd.Parameters.Add("@ActivityID", SqlDbType.Int).Value = Convert.ToInt32("3");
                        cmd.Parameters.Add("@RequestID", SqlDbType.Int).Value = RequestID;
                        cmd.Parameters.Add("@StartTime", SqlDbType.VarChar).Value = StartTime;
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
       

       


    }
}