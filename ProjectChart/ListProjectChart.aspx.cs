using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using GSDC.App_Code;
using AjaxControlToolkit;

namespace GSDC.ProjectChart
{
    public partial class ListProjectChart : System.Web.UI.Page
    {

        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataAdapter da = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {

                BindProjectCharter();
            }
            if (Request.QueryString["Result"] != null)
            {
                if (Convert.ToBoolean(Request.QueryString["Result"]) == true)
                {
                    divMessage.Visible = true;
                    //lblMessage.Text = "Project Detals Save Successfully";
                }

            }


        }


        public void BindProjectCharter()
        {
            DataTable dt = new DataTable();
            StringBuilder sbs = new StringBuilder();
            sbs.Append(" select b.ProjectID,sum(PercentAvg) ProgressPercent,b.ProjectName,convert(varchar, b.CreateDate,3) CreateDate,b.CreatedOwner,b.ID, ");
            sbs.Append(" convert(varchar,b.ProjectCreationDate,3) ProjectCreationDate,convert(varchar,b.ProjectStartDate,3) ProjectStartDate, ");
            sbs.Append(" convert(varchar,b.ProjectEndTime,3) ProjectEndTime,b.UserID,convert(varchar,b.ProjectExpectedEnd,3) ProjectExpectedEnd,b.ProjectOwner,b.FilePath FilePath ");
            sbs.Append(" from ");
            sbs.Append(" (select ProjectID,AVG(cast(REPLACE(statuspercent,'%','') as int)/5) PercentAvg,ServiceType  from GDSC_StratagicData ");

            sbs.Append("  group by ServiceType,GDSC_StratagicData.ProjectID) ");
            sbs.Append(" innerquery right outer join GDSC_ProjectMaster b on ");
            sbs.Append(" innerquery.ProjectID=b.ProjectID ");
            sbs.Append(" group by b.ProjectID,b.ProjectName, ");
            sbs.Append(" b.CreateDate,b.CreatedOwner,b.ID, ");
            sbs.Append(" b.ProjectCreationDate,b.ProjectStartDate, ");
            sbs.Append(" b.ProjectEndTime,b.UserID,b.ProjectExpectedEnd,b.ProjectOwner,b.FilePath ");

            try
            {
                using (con = Connection.GetProjectChartConnection())
                {

                    // using (cmd = new SqlCommand("SELECT ID,ProjectID,ProjectName,convert(varchar, ProjectStartDate ,3) ProjectStartDate,convert(varchar, ProjectEndTime ,3) ProjectEndTime,convert(varchar, ProjectCreationDate ,3) ProjectCreationDate,convert(varchar, ProjectExpectedEnd ,3) ProjectExpectedEnd,ProjectOwner,UserID,ProgressPercent FROM GDSC_ProjectMaster", con))
                    using (cmd = new SqlCommand(sbs.ToString(), con))
                    {

                        da = new SqlDataAdapter(cmd);
                        da.Fill(dt);

                        GdViewProjectChart.DataSource = dt;
                        GdViewProjectChart.DataBind();


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

        protected void GdViewProjectChart_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gvProducts = (sender as GridView);
            gvProducts.PageIndex = e.NewPageIndex;
            BindProjectCharter();
        }
        protected void popup_Click(object sender, EventArgs e)
        {
            LinkButton btndetails = sender as LinkButton;

            //get reference to the row selected
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;

            Session["ProjID"] = GdViewProjectChart.DataKeys[gvrow.RowIndex].Values[1].ToString();

            Response.Redirect("CreateProject.aspx?taskedit=editenable&ProjectID=" + Session["ProjID"].ToString() + "");


        }

        protected void GdViewProjectChart_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lb = (Label)(e.Row.FindControl("lblUserID"));
                    //Label LabelProgressBar1 = (Label)(e.Row.FindControl("LabelProgressBar2"));


                    if (Session["UserID"].ToString() == lb.Text)
                    {
                        LinkButton lnk = (LinkButton)e.Row.FindControl("popup");
                        lnk.Enabled = true;
                    }

                    //Label lprogress = (Label)e.Row.FindControl("lblProgress");
                    //LabelProgressBar1.Text = lprogress.Text + "%";
                    //LabelProgressBar1.ForeColor = System.Drawing.Color.White;
                    //LabelProgressBar1.Font.Size = 10;

                    //if(lprogress.Text=="")
                    //{
                    //    LabelProgressBar1.Text = "0%";

                    //}
                    if (e.Row.Cells[8].Text == "0")
                        e.Row.Cells[8].BackColor = System.Drawing.Color.LightSkyBlue;
                    else if (Convert.ToInt32(e.Row.Cells[8].Text) > 75)
                    {

                        e.Row.Cells[8].BackColor = System.Drawing.Color.Green;


                    }
                    else
                        e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[8].Text += "%";
                    //e.Row.Cells[9].BackColor = System.Drawing.Color.LightSkyBlue;
                    //LabelProgressBar1.Width = new Unit(Convert.ToInt32(lprogress.Text), UnitType.Percentage);

                    //if (Convert.ToInt32(lprogress.Text) >= 75)
                    //{
                    //    e.Row.Cells[9].BackColor = System.Drawing.Color.Green;
                    //    LabelProgressBar1.BackColor = System.Drawing.Color.Green;

                    //}
                    //else
                    //{
                    //    LabelProgressBar1.BackColor = System.Drawing.Color.Red;
                    //    e.Row.Cells[9].BackColor = System.Drawing.Color.Red;

                    //}


                }
            }
            catch (Exception ex)
            {

            }
            finally
            {


            }


        }



        protected void popup1_Click(object sender, EventArgs e)
        {
            LinkButton btndetails = sender as LinkButton;

            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;

            Session["ProjID"] = GdViewProjectChart.DataKeys[gvrow.RowIndex].Values[1].ToString();

            Response.Redirect("CreateProject.aspx?task=edit&ProjectID=" + Session["ProjID"].ToString() + "");
        }

        protected void popSOAP_Click(object sender, EventArgs e)
        {

            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            string filePath = GdViewProjectChart.DataKeys[gvrow.RowIndex].Values[0].ToString();

            if (File.Exists(Server.MapPath(filePath)))
            {
                Response.ContentType = "pptx";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath.Remove(0, 6) + "\"");
                Response.TransmitFile(Server.MapPath(filePath));
            }

            else
            {
                divMessage.InnerText = "File not found";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "File Not Found", true);
            }



            Response.End();
            Response.Flush();

        }



    }
}
