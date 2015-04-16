using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GSDC.App_Code;


namespace GSDC.ProjectChart
{
    public partial class ProjectChart : System.Web.UI.MasterPage
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataAdapter da = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //if (Convert.ToInt32(Session["UserTeamID"] .ToString()) == 19)
                //{
                //    lnkButton.Enabled = true;
                //}

                //else
                //{
                //    lnkButton.Enabled = false;
                //}

            }

           
        }
        protected void lnkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateProject.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectDetailsView.aspx");
        }

        protected void lnlDownload_Click(object sender, EventArgs e)
        {
            string filePath = "~\\DownloadFile\\SOAP.pptx";

            if (File.Exists(Server.MapPath(filePath)))
            {
                Response.ContentType = "pptx";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath.Remove(0, 14) + "\"");
                Response.TransmitFile(Server.MapPath(filePath));
            }

            else
            {
               
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "File Not Found", true);
            }



            Response.End();
            Response.Flush();
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListProjectChart.aspx");
        }

       

     
    }
}