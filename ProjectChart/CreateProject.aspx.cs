using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GSDC.App_Code;
using System.Text;
using System.Web.Services;
using System.Collections.Generic;

namespace GSDC.ProjectChart
{
    public partial class CreateProject : System.Web.UI.Page
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataAdapter da = new SqlDataAdapter();
        SqlDataReader dr = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                string Task = HttpUtility.UrlDecode(Request.QueryString["task"]);
                string TaskEnable = HttpUtility.UrlDecode(Request.QueryString["taskedit"]);

                BindStrategyGridView();
                BindServiceDesignGridView();
                BindServiceTransitionGridView();
                BindServiceOperationGridView();
                BindCSI();
                GetStratagyAverage();
                GetServiceDesignAverage();
                GetServiceTransitionAverage();
                GetServiceOperationAverage();
                GetCSIAverage();
                if (Task == "edit")
                {
                    Session["ProjID"] = HttpUtility.UrlDecode(Request.QueryString["ProjectID"]);
                    pnlStrategy.Visible = true;
                    pnlSerDesign.Visible = true;
                    pnlTransition.Visible = true;
                    pnlOperation.Visible = true;
                    pnlCSI.Visible = true;
                    FillProjectDetails();

                    pnldetails.Enabled = false;
                }

                if (TaskEnable == "editenable")
                {


                    FillProjectDetails();
                    pnldetails.Enabled = true;
                    pnlStrategy.Visible = true;
                    pnlSerDesign.Visible = true;
                    pnlSerDesign.Enabled = true;
                    pnlStrategy.Enabled = true;
                    pnlTransition.Enabled = true;
                    pnlTransition.Visible = true;
                    pnlOperation.Enabled = true;
                    pnlOperation.Visible = true;
                    pnlCSI.Enabled = true;
                    pnlCSI.Visible = true;
                    btnSubmit.Text = "Update";


                }
            }


        }


        public void BindStrategyGridView()
        {
            SqlCommand cmd2 = null;
            SqlCommand cmd3 = null;
            DataTable dt = new DataTable();
            try
            {
                using (con = Connection.GetProjectChartConnection())
                {



                    using (cmd = new SqlCommand("SELECT ProjectID FROM GDSC_StratagicData where ProjectID=@ProjectID and ServiceType=@ServiceType", con))
                    {

                        cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"]);
                        cmd.Parameters.Add("@ServiceType", SqlDbType.VarChar).Value = "SS";

                        da = new SqlDataAdapter(cmd);
                        da.Fill(dt);



                    }

                    if (dt.Rows.Count <= 0)
                    {

                        using (cmd2 = new SqlCommand("SELECT ID,ProcessID,Process,SubProcessID,SubProcess,ProjectID,Comment,Status,StatusPercent FROM GDSC_ServiceStrategy", con))
                        {


                            gviewServiceStrategy.DataSource = cmd2.ExecuteReader();
                            gviewServiceStrategy.DataBind();


                        }
                        cmd2.Dispose();
                        cmd2 = null;
                    }
                    else
                    {

                        using (cmd3 = new SqlCommand("SELECT a.ID ID,a.Status Status,a.StatusPercent StatusPercent,a.Comment Comment,b.ProcessID ProcessID ,b.SubProcessID SubProcessID,a.ProjectID ProjectID,process,subprocess FROM GDSC_StratagicData a right outer join GDSC_ServiceStrategy b on a.processID=b.processid and a.subprocessid=b.SubProcessID where a.ProjectID=@ProjectID and a.ServiceType=@ServiceType", con))
                        {

                            cmd3.Parameters.Add("@ProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"].ToString());
                            cmd3.Parameters.Add("@ServiceType", SqlDbType.VarChar).Value = "SS";

                            gviewServiceStrategy.DataSource = cmd3.ExecuteReader();
                            gviewServiceStrategy.DataBind();


                        }
                        cmd3.Dispose();
                        cmd3 = null;
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

        public void BindServiceDesignGridView()
        {
            SqlCommand cmd2 = null;
            SqlCommand cmd3 = null;
            DataTable dt = new DataTable();
            try
            {
                using (con = Connection.GetProjectChartConnection())
                {



                    using (cmd = new SqlCommand("SELECT ProjectID FROM GDSC_StratagicData where ProjectID=@ProjectID and ServiceType=@ServiceType", con))
                    {

                        cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"]);
                        cmd.Parameters.Add("@ServiceType", SqlDbType.VarChar).Value = "SD";

                        da = new SqlDataAdapter(cmd);
                        da.Fill(dt);



                    }

                    if (dt.Rows.Count <= 0)
                    {

                        using (cmd2 = new SqlCommand("SELECT ID,ProcessID,Process,SubProcessID,SubProcess,ProjectID,Comment,Status,StatusPercent FROM GDSC_ServiceDesign", con))
                        {


                            gvServiceDesign.DataSource = cmd2.ExecuteReader();
                            gvServiceDesign.DataBind();


                        }
                        cmd2.Dispose();
                        cmd2 = null;
                    }
                    else
                    {

                        using (cmd3 = new SqlCommand("SELECT a.ID ID,a.Status Status,a.StatusPercent StatusPercent,a.Comment Comment,b.ProcessID ProcessID ,b.SubProcessID SubProcessID,a.ProjectID ProjectID,process,subprocess FROM GDSC_StratagicData a right outer join GDSC_ServiceDesign b on a.processID=b.processid and a.subprocessid=b.SubProcessID where a.ProjectID=@ProjectID and a.ServiceType=@ServiceType", con))
                        {

                            cmd3.Parameters.Add("@ProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"].ToString());
                            cmd3.Parameters.Add("@ServiceType", SqlDbType.VarChar).Value = "SD";

                            gvServiceDesign.DataSource = cmd3.ExecuteReader();
                            gvServiceDesign.DataBind();


                        }
                        cmd3.Dispose();
                        cmd3 = null;
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

        public void BindServiceTransitionGridView()
        {
            SqlCommand cmd2 = null;
            SqlCommand cmd3 = null;
            DataTable dt = new DataTable();
            try
            {
                using (con = Connection.GetProjectChartConnection())
                {



                    using (cmd = new SqlCommand("SELECT ProjectID FROM GDSC_StratagicData where ProjectID=@ProjectID and ServiceType=@ServiceType", con))
                    {

                        cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"]);
                        cmd.Parameters.Add("@ServiceType", SqlDbType.VarChar).Value = "ST";

                        da = new SqlDataAdapter(cmd);
                        da.Fill(dt);



                    }

                    if (dt.Rows.Count <= 0)
                    {

                        using (cmd2 = new SqlCommand("SELECT ID,ProcessID,Process,SubProcessID,SubProcess,ProjectID,Comment,Status,StatusPercent FROM GDSC_ServiceTransition", con))
                        {


                            gvServiceTransition.DataSource = cmd2.ExecuteReader();
                            gvServiceTransition.DataBind();


                        }
                        cmd2.Dispose();
                        cmd2 = null;
                    }
                    else
                    {

                        using (cmd3 = new SqlCommand("SELECT a.ID ID,a.Status Status,a.StatusPercent StatusPercent,a.Comment Comment,b.ProcessID ProcessID ,b.SubProcessID SubProcessID,a.ProjectID ProjectID,process,subprocess FROM GDSC_StratagicData a right outer join GDSC_ServiceTransition b on a.processID=b.processid and a.subprocessid=b.SubProcessID where a.ProjectID=@ProjectID and a.ServiceType=@ServiceType", con))
                        {

                            cmd3.Parameters.Add("@ProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"].ToString());
                            cmd3.Parameters.Add("@ServiceType", SqlDbType.VarChar).Value = "ST";

                            gvServiceTransition.DataSource = cmd3.ExecuteReader();
                            gvServiceTransition.DataBind();


                        }
                        cmd3.Dispose();
                        cmd3 = null;
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

        public void BindServiceOperationGridView()
        {
            SqlCommand cmd2 = null;
            SqlCommand cmd3 = null;
            DataTable dt = new DataTable();
            try
            {
                using (con = Connection.GetProjectChartConnection())
                {



                    using (cmd = new SqlCommand("SELECT ProjectID FROM GDSC_StratagicData where ProjectID=@ProjectID and ServiceType=@ServiceType", con))
                    {

                        cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"]);
                        cmd.Parameters.Add("@ServiceType", SqlDbType.VarChar).Value = "SO";

                        da = new SqlDataAdapter(cmd);
                        da.Fill(dt);



                    }

                    if (dt.Rows.Count <= 0)
                    {

                        using (cmd2 = new SqlCommand("SELECT ID,ProcessID,Process,SubProcessID,SubProcess,ProjectID,Comment,Status,StatusPercent FROM GDSC_ServiceOperation", con))
                        {


                            gvServiceOperation.DataSource = cmd2.ExecuteReader();
                            gvServiceOperation.DataBind();


                        }
                        cmd2.Dispose();
                        cmd2 = null;
                    }
                    else
                    {

                        using (cmd3 = new SqlCommand("SELECT a.ID ID,a.Status Status,a.StatusPercent StatusPercent,a.Comment Comment,b.ProcessID ProcessID ,b.SubProcessID SubProcessID,a.ProjectID ProjectID,process,subprocess FROM GDSC_StratagicData a right outer join GDSC_ServiceOperation b on a.processID=b.processid and a.subprocessid=b.SubProcessID where a.ProjectID=@ProjectID and a.ServiceType=@ServiceType", con))
                        {

                            cmd3.Parameters.Add("@ProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"].ToString());
                            cmd3.Parameters.Add("@ServiceType", SqlDbType.VarChar).Value = "SO";

                            gvServiceOperation.DataSource = cmd3.ExecuteReader();
                            gvServiceOperation.DataBind();


                        }
                        cmd3.Dispose();
                        cmd3 = null;
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

        public void BindCSI()
        {
            SqlCommand cmd2 = null;
            SqlCommand cmd3 = null;
            DataTable dt = new DataTable();
            try
            {
                using (con = Connection.GetProjectChartConnection())
                {



                    using (cmd = new SqlCommand("SELECT ProjectID FROM GDSC_StratagicData where ProjectID=@ProjectID and ServiceType=@ServiceType", con))
                    {

                        cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"]);
                        cmd.Parameters.Add("@ServiceType", SqlDbType.VarChar).Value = "SI";

                        da = new SqlDataAdapter(cmd);
                        da.Fill(dt);



                    }

                    if (dt.Rows.Count <= 0)
                    {

                        using (cmd2 = new SqlCommand("SELECT ID,ProcessID,Process,SubProcessID,SubProcess,ProjectID,Comment,Status,StatusPercent FROM GDSC_CSImprovement", con))
                        {


                            gvCSI.DataSource = cmd2.ExecuteReader();
                            gvCSI.DataBind();


                        }
                        cmd2.Dispose();
                        cmd2 = null;
                    }
                    else
                    {

                        using (cmd3 = new SqlCommand("SELECT a.ID ID,a.Status Status,a.StatusPercent StatusPercent,a.Comment Comment,b.ProcessID ProcessID ,b.SubProcessID SubProcessID,a.ProjectID ProjectID,process,subprocess FROM GDSC_StratagicData a right outer join GDSC_CSImprovement b on a.processID=b.processid and a.subprocessid=b.SubProcessID where a.ProjectID=@ProjectID and a.ServiceType=@ServiceType", con))
                        {

                            cmd3.Parameters.Add("@ProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"].ToString());
                            cmd3.Parameters.Add("@ServiceType", SqlDbType.VarChar).Value = "SI";

                            gvCSI.DataSource = cmd3.ExecuteReader();
                            gvCSI.DataBind();


                        }
                        cmd3.Dispose();
                        cmd3 = null;
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

        public void FillProjectDetails()
        {

            try
            {

                using (con = Connection.GetProjectChartConnection())
                {

                    using (cmd = new SqlCommand("SELECT * FROM GDSC_ProjectMaster where ProjectID=" + Session["ProjID"].ToString() + "", con))
                    {

                        cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"].ToString());

                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            txtProjectname.Text = dr["ProjectName"].ToString();
                            txtCreateDate.Text = dr["ProjectCreationDate"].ToString().Substring(0, 9);
                            txtEndDate.Text = dr["ProjectEndTime"].ToString().Substring(0, 9);
                            txtExpectedDate.Text = dr["ProjectExpectedEnd"].ToString().Substring(0, 9);
                            //  txtDescription.Text = dr["ProjectDescription"].ToString();
                            txtStartDate.Text = dr["ProjectStartDate"].ToString().Substring(0, 9);
                            TxtSSPOC.Text = dr["SSPOC"].ToString();
                            txtSDSPOC.Text = dr["SDPOC"].ToString();
                            txtSTSPOC.Text = dr["STPOC"].ToString();
                            txtSOSPOC.Text = dr["SOSPOC"].ToString();
                            txtCSISPOC.Text = dr["CSIPOC"].ToString();

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

                dr = null;
                cmd = null;

            }
        }
        protected void btnAdd_click(object sender, EventArgs e)
        {

            bool Result = false;

            try
            {

                using (con = Connection.GetProjectChartConnection())
                {


                    if (btnSubmit.Text == "Update")
                    {
                        try
                        {
                            string filename = Path.GetFileName(fileUpload1.PostedFile.FileName);
                            fileUpload1.SaveAs(Server.MapPath("Files/" + filename));
                            StringBuilder sb = new StringBuilder();
                            sb.Append("Update GDSC_ProjectMaster set ProjectName=@ProjectName,");
                            sb.Append(" ProjectStartDate=@PstartDate,ProjectEndTime=@PEndDate,ProjectCreationDate=@PCreateDate,ProjectExpectedEnd=@PExpDate,ProjectOwner=@Powner,CreateDate=@CreateDate,CreatedOwner=@Createdowner,UserID=@UserID, ");
                            sb.Append(" FileName=@Name,FilePath=@FilePath,SSPOC=@SSSPOC,SDPOC=@SDSPOC,STPOC=@STSPOC,SOSPOC=@SOSPOC,CSIPOC=@CSISPOC where ProjectID=@ProjectID");


                            Random a = new Random();

                            cmd = new SqlCommand(sb.ToString(), con);
                            cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = Session["ProjID"].ToString();
                            cmd.Parameters.Add("@ProjectName", SqlDbType.NVarChar).Value = txtProjectname.Text;
                            // cmd.Parameters.Add("@ProjectDesc", SqlDbType.NVarChar).Value = txtDescription.Text;
                            cmd.Parameters.Add("@PstartDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtStartDate.Text);
                            cmd.Parameters.Add("@PEndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtEndDate.Text);
                            cmd.Parameters.Add("@PCreateDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtCreateDate.Text);
                            cmd.Parameters.Add("@PExpDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtExpectedDate.Text);
                            cmd.Parameters.Add("@Powner", SqlDbType.VarChar).Value = Session["UserFullName"];
                            cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = System.DateTime.Now;
                            cmd.Parameters.Add("@Createdowner", SqlDbType.VarChar).Value = Session["UserFullName"];
                            cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = Session["UserID"];
                            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = filename;
                            cmd.Parameters.Add("@FilePath", SqlDbType.NVarChar).Value = "Files/" + filename;

                            cmd.Parameters.Add("@SSSPOC", SqlDbType.NVarChar).Value = Convert.ToString(TxtSSPOC.Text);
                            cmd.Parameters.Add("@SDSPOC", SqlDbType.NVarChar).Value = Convert.ToString(txtSDSPOC.Text);
                            cmd.Parameters.Add("@STSPOC", SqlDbType.NVarChar).Value = Convert.ToString(txtSTSPOC.Text);
                            cmd.Parameters.Add("@SOSPOC", SqlDbType.NVarChar).Value = Convert.ToString(txtSOSPOC.Text);
                            cmd.Parameters.Add("@CSISPOC", SqlDbType.NVarChar).Value = Convert.ToString(txtCSISPOC.Text);

                            cmd.ExecuteNonQuery();
                            Response.Redirect("ListProjectChart.aspx");
                            //btnSubmit.Enabled = false;
                            Result = true;
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {

                            cmd.Dispose();


                        }
                    }
                    else
                    {
                        try
                        {
                            string filename = Path.GetFileName(fileUpload1.PostedFile.FileName);
                            fileUpload1.SaveAs(Server.MapPath("Files/" + filename));
                            StringBuilder sb = new StringBuilder();
                            sb.Append("Insert into GDSC_ProjectMaster (ProjectID,ProjectName,ProjectStartDate,");
                            sb.Append("ProjectEndTime ,ProjectCreationDate,ProjectExpectedEnd,ProjectOwner,CreateDate,CreatedOwner,UserID,FileName,FilePath,SSPOC,SDPOC,STPOC,SOSPOC,CSIPOC)");
                            sb.Append("values(@ProjectID,@ProjectName,@PstartDate,@PEndDate,@PCreateDate,@PExpDate,@Powner,@CreateDate,@Createdowner,@UserID,@Name,@FilePath,@SSSPOC,@SDSPOC,@STSPOC,@SOSPOC,@CSISPOC)");


                            Random a = new Random();

                            SqlCommand cmd = new SqlCommand(sb.ToString(), con);
                            cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = a.Next(1, int.MaxValue);
                            cmd.Parameters.Add("@ProjectName", SqlDbType.NVarChar).Value = txtProjectname.Text;
                            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = filename;
                            cmd.Parameters.Add("@FilePath", SqlDbType.NVarChar).Value = "Files/" + filename;
                            cmd.Parameters.AddWithValue("@Path", "Files/" + filename);
                            // cmd.Parameters.Add("@ProjectDesc", SqlDbType.NVarChar).Value = txtDescription.Text;
                            cmd.Parameters.Add("@PstartDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtStartDate.Text);
                            cmd.Parameters.Add("@PEndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtEndDate.Text);
                            cmd.Parameters.Add("@PCreateDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtCreateDate.Text);
                            cmd.Parameters.Add("@PExpDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtExpectedDate.Text);
                            cmd.Parameters.Add("@Powner", SqlDbType.VarChar).Value = Session["UserFullName"];
                            cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = System.DateTime.Now;
                            cmd.Parameters.Add("@Createdowner", SqlDbType.VarChar).Value = Session["UserFullName"];
                            cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = Session["UserID"];

                            cmd.Parameters.Add("@SSSPOC", SqlDbType.NVarChar).Value = Convert.ToString(TxtSSPOC.Text);
                            cmd.Parameters.Add("@SDSPOC", SqlDbType.NVarChar).Value = Convert.ToString(txtSDSPOC.Text);
                            cmd.Parameters.Add("@STSPOC", SqlDbType.NVarChar).Value = Convert.ToString(txtSTSPOC.Text);
                            cmd.Parameters.Add("@SOSPOC", SqlDbType.NVarChar).Value = Convert.ToString(txtSOSPOC.Text);
                            cmd.Parameters.Add("@CSISPOC", SqlDbType.NVarChar).Value = Convert.ToString(txtCSISPOC.Text);

                            cmd.ExecuteNonQuery();
                            Result = true;


                            Response.Redirect("ListProjectChart.aspx?Result=" + Result);
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {


                            da.Dispose();
                            cmd = null;
                            da = null;


                        }
                    }
                }

            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }



            //}
        }
        public void ValidateData()
        {
            if (txtProjectname.Text == "" || txtProjectname.Text == null)
            {

            }

        }

        protected void gviewServiceStrategy_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lb = (Label)(e.Row.FindControl("lblStatusPercent"));
                DropDownList dp = (DropDownList)e.Row.FindControl("ddlStatus");
                Label llb = (Label)e.Row.FindControl("Status");
                dp.Items.FindByValue(lb.Text).Selected = true;
                if (Session["UserID"].ToString() == lb.Text)
                {
                    LinkButton lnk = (LinkButton)e.Row.FindControl("popup");
                    lnk.Enabled = false;
                }
            }


        }

        protected void gvServiceDesign_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lb = (Label)(e.Row.FindControl("lblStatusPercent"));
                DropDownList dp = (DropDownList)e.Row.FindControl("ddlDesignStatus");
                Label llb = (Label)e.Row.FindControl("Status");
                dp.Items.FindByValue(lb.Text).Selected = true;
                if (Session["UserID"].ToString() == lb.Text)
                {
                    LinkButton lnk = (LinkButton)e.Row.FindControl("popup");
                    lnk.Enabled = false;
                }
            }


        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            //GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            //string filePath = GdViewProjectChart.DataKeys[gvrow.RowIndex].Values[0].ToString();
            foreach (GridViewRow row in gviewServiceStrategy.Rows)
            {

                Control ctrl = row.FindControl("ddlStatus") as DropDownList;
                if (ctrl != null)
                {
                    DropDownList ddl1 = (DropDownList)ctrl;

                    if (ddl.ClientID == ddl1.ClientID)
                    {
                        Label txt1 = row.FindControl("lblStatusPercent") as Label;
                        txt1.Text = ddl1.SelectedValue;


                    }
                }
            }
        }


        protected void Strategy_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            try
            {
                dt.Columns.AddRange(new DataColumn[11] { new DataColumn("ID", typeof(int)),
              
                      new DataColumn("SubProcessID", typeof(int)),
                        new DataColumn("ProjectID", typeof(int)),
                  new DataColumn("ProcessID", typeof(int)),
                 
                   
                  //new DataColumn("lblProcess", typeof(string)),
                  // new DataColumn("lblSubProcess", typeof(string)),
                    new DataColumn("Status", typeof(string)),
                     new DataColumn("StatusPercent", typeof(string)),
                new DataColumn("Comment",typeof(string)),
                  new DataColumn("ServiceType",typeof(string)),
                 new DataColumn("CreateOn",typeof(DateTime)),
                 new DataColumn("updatedon",typeof(DateTime)),
                new DataColumn("createdby",typeof(string))});
                foreach (GridViewRow row in gviewServiceStrategy.Rows)
                {
                    int id = int.Parse(Convert.ToString((row.FindControl("lblID") as Label).Text));
                    int lblSubProcessID = int.Parse(Convert.ToString((row.FindControl("lblSubProcessID") as Label).Text));
                    int ProjectID = Convert.ToInt32(Session["ProjID"].ToString());
                    int lblProcessID = int.Parse(Convert.ToString((row.FindControl("lblProcessID") as Label).Text));



                    string lblstatuss = Convert.ToString((row.FindControl("ddlStatus") as DropDownList).SelectedItem);
                    string lblStatusPercent = Convert.ToString((row.FindControl("lblStatusPercent") as Label).Text);
                    string txtComment = Convert.ToString((row.FindControl("txtComment") as TextBox).Text);
                    string txtServiceType = "SS";
                    DateTime Createon = DateTime.Now;
                    DateTime updatedon = DateTime.Now;
                    string createdby = Convert.ToString(Session["UserFullName"]);
                    dt.Rows.Add(id, lblSubProcessID, ProjectID, lblProcessID, lblstatuss, lblStatusPercent, txtComment, txtServiceType, Createon, updatedon, createdby);
                }

                con = new SqlConnection();
                using (con = Connection.GetProjectChartConnection())
                {
                    using (cmd = new SqlCommand("Update_Stratagey"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@tblGDSC_StratagicData", dt);
                        cmd.Parameters.Add("@pProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"]);
                        cmd.Parameters.Add("@pServiceType", SqlDbType.VarChar).Value = "SS";

                        cmd.ExecuteNonQuery();

                    }
                }

                Response.Redirect("ListProjectChart.aspx");
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

        protected void btnServiceDesign_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.AddRange(new DataColumn[11] { new DataColumn("ID", typeof(int)),
              
                      new DataColumn("SubProcessID", typeof(int)),
                     new DataColumn("ProjectID", typeof(int)),
                  new DataColumn("ProcessID", typeof(int)),
                 
                  //new DataColumn("lblProcess", typeof(string)),
                  // new DataColumn("lblSubProcess", typeof(string)),
                    new DataColumn("Status", typeof(string)),
                     new DataColumn("StatusPercent", typeof(string)),
                new DataColumn("Comment",typeof(string)),
                new DataColumn("ServiceType",typeof(string)),
                 new DataColumn("CreateOn",typeof(DateTime)),
                 new DataColumn("updatedon",typeof(DateTime)),
                new DataColumn("createdby",typeof(string))});
                foreach (GridViewRow row in gvServiceDesign.Rows)
                {
                    int id = int.Parse(Convert.ToString((row.FindControl("lblID") as Label).Text));
                    int lblSubProcessID = int.Parse(Convert.ToString((row.FindControl("lblSubProcessID") as Label).Text));
                    int ProjectID = Convert.ToInt32(Session["ProjID"].ToString());
                    int lblProcessID = int.Parse(Convert.ToString((row.FindControl("lblProcessID") as Label).Text));
                  

                    string lblstatuss = Convert.ToString((row.FindControl("ddlDesignStatus") as DropDownList).SelectedItem);
                    string lblStatusPercent = Convert.ToString((row.FindControl("lblStatusPercent") as Label).Text);
                    string txtComment = Convert.ToString((row.FindControl("txtComment") as TextBox).Text);
                    string txtServiceType = "SD";
                    DateTime Createon = DateTime.Now;
                    DateTime updatedon = DateTime.Now;
                    string createdby = Convert.ToString(Session["UserFullName"]);

                    dt.Rows.Add(id, lblSubProcessID, ProjectID, lblProcessID, lblstatuss, lblStatusPercent, txtComment, txtServiceType, Createon, updatedon, createdby);
                }

                con = new SqlConnection();
                using (con = Connection.GetProjectChartConnection())
                {
                    using (cmd = new SqlCommand("Update_Stratagey"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@tblGDSC_StratagicData", dt);
                        cmd.Parameters.Add("@pProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"]);
                        cmd.Parameters.Add("@pServiceType", SqlDbType.VarChar).Value = "SD";

                        cmd.ExecuteNonQuery();

                    }
                }

                Response.Redirect("ListProjectChart.aspx");
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

        protected void ddlDesignStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;

            foreach (GridViewRow row in gvServiceDesign.Rows)
            {

                Control ctrl = row.FindControl("ddlDesignStatus") as DropDownList;
                if (ctrl != null)
                {
                    DropDownList ddl1 = (DropDownList)ctrl;

                    if (ddl.ClientID == ddl1.ClientID)
                    {
                        Label txt1 = row.FindControl("lblStatusPercent") as Label;
                        txt1.Text = ddl1.SelectedValue;


                    }
                }
            }
        }

        protected void ddlServiceTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;

            foreach (GridViewRow row in gvServiceTransition.Rows)
            {

                Control ctrl = row.FindControl("ddlServiceTrans") as DropDownList;
                if (ctrl != null)
                {
                    DropDownList ddl1 = (DropDownList)ctrl;

                    if (ddl.ClientID == ddl1.ClientID)
                    {
                        Label txt1 = row.FindControl("lblStatusPercent") as Label;
                        txt1.Text = ddl1.SelectedValue;


                    }
                }
            }
        }

        protected void gvServiceTransition_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lb = (Label)(e.Row.FindControl("lblStatusPercent"));
                DropDownList dp = (DropDownList)e.Row.FindControl("ddlServiceTrans");
                Label llb = (Label)e.Row.FindControl("Status");
                dp.Items.FindByValue(lb.Text).Selected = true;
                if (Session["UserID"].ToString() == lb.Text)
                {
                    LinkButton lnk = (LinkButton)e.Row.FindControl("popup");
                    lnk.Enabled = false;
                }
            }
        }

        protected void btnTransition_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.AddRange(new DataColumn[11] { new DataColumn("ID", typeof(int)),
                new DataColumn("SubProcessID", typeof(int)),
                     new DataColumn("ProjectID", typeof(int)),
                  new DataColumn("ProcessID", typeof(int)),
                 
                  //new DataColumn("lblProcess", typeof(string)),
                  // new DataColumn("lblSubProcess", typeof(string)),
                    new DataColumn("Status", typeof(string)),
                     new DataColumn("StatusPercent", typeof(string)),
                new DataColumn("Comment",typeof(string)),
                new DataColumn("ServiceType",typeof(string)),
                 new DataColumn("CreateOn",typeof(DateTime)),
                 new DataColumn("updatedon",typeof(DateTime)),
                new DataColumn("createdby",typeof(string))});
                foreach (GridViewRow row in gvServiceTransition.Rows)
                {
                    int id = int.Parse(Convert.ToString((row.FindControl("lblID") as Label).Text));

                    int lblProcessID = int.Parse(Convert.ToString((row.FindControl("lblProcessID") as Label).Text));
                    int lblSubProcessID = int.Parse(Convert.ToString((row.FindControl("lblSubProcessID") as Label).Text));
                    int ProjectID = Convert.ToInt32(Session["ProjID"].ToString());

                    string lblstatuss = Convert.ToString((row.FindControl("ddlServiceTrans") as DropDownList).SelectedItem);
                    string lblStatusPercent = Convert.ToString((row.FindControl("lblStatusPercent") as Label).Text);
                    string txtComment = Convert.ToString((row.FindControl("txtComment") as TextBox).Text);
                    string txtServiceType = "ST";

                    DateTime Createon = DateTime.Now;
                    DateTime updatedon = DateTime.Now;
                    string createdby = Convert.ToString(Session["UserFullName"]);
                    dt.Rows.Add(id, lblSubProcessID, ProjectID, lblProcessID, lblstatuss, lblStatusPercent, txtComment, txtServiceType, Createon, updatedon, createdby);
                }

                con = new SqlConnection();
                using (con = Connection.GetProjectChartConnection())
                {
                    using (cmd = new SqlCommand("Update_Stratagey"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@tblGDSC_StratagicData", dt);
                        cmd.Parameters.Add("@pProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"]);
                        cmd.Parameters.Add("@pServiceType", SqlDbType.VarChar).Value = "ST";

                        cmd.ExecuteNonQuery();

                    }
                }

                Response.Redirect("ListProjectChart.aspx");
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

        protected void ddlServiceOps_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;

            foreach (GridViewRow row in gvServiceOperation.Rows)
            {

                Control ctrl = row.FindControl("ddlServiceOps") as DropDownList;
                if (ctrl != null)
                {
                    DropDownList ddl1 = (DropDownList)ctrl;

                    if (ddl.ClientID == ddl1.ClientID)
                    {
                        Label txt1 = row.FindControl("lblStatusPercent") as Label;
                        txt1.Text = ddl1.SelectedValue;


                    }
                }
            }
        }

        protected void gvServiceOperation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lb = (Label)(e.Row.FindControl("lblStatusPercent"));
                DropDownList dp = (DropDownList)e.Row.FindControl("ddlServiceOps");
                Label llb = (Label)e.Row.FindControl("Status");
                dp.Items.FindByValue(lb.Text).Selected = true;
                if (Session["UserID"].ToString() == lb.Text)
                {
                    LinkButton lnk = (LinkButton)e.Row.FindControl("popup");
                    lnk.Enabled = false;
                }
            }
        }

        protected void btnOperation_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.AddRange(new DataColumn[11] { new DataColumn("ID", typeof(int)),
              
                      new DataColumn("SubProcessID", typeof(int)),
                     new DataColumn("ProjectID", typeof(int)),
                  new DataColumn("ProcessID", typeof(int)),
                 
                  //new DataColumn("lblProcess", typeof(string)),
                  // new DataColumn("lblSubProcess", typeof(string)),
                    new DataColumn("Status", typeof(string)),
                     new DataColumn("StatusPercent", typeof(string)),
                new DataColumn("Comment",typeof(string)),
                new DataColumn("ServiceType",typeof(string)),
                 new DataColumn("CreateOn",typeof(DateTime)),
                 new DataColumn("updatedon",typeof(DateTime)),
                new DataColumn("createdby",typeof(string))});
                foreach (GridViewRow row in gvServiceOperation.Rows)
                {
                    int id = int.Parse(Convert.ToString((row.FindControl("lblID") as Label).Text));

                    int lblProcessID = int.Parse(Convert.ToString((row.FindControl("lblProcessID") as Label).Text));
                    int lblSubProcessID = int.Parse(Convert.ToString((row.FindControl("lblSubProcessID") as Label).Text));
                    int ProjectID = Convert.ToInt32(Session["ProjID"].ToString());

                    string lblstatuss = Convert.ToString((row.FindControl("ddlServiceOps") as DropDownList).SelectedItem);
                    string lblStatusPercent = Convert.ToString((row.FindControl("lblStatusPercent") as Label).Text);
                    string txtComment = Convert.ToString((row.FindControl("txtComment") as TextBox).Text);
                    string txtServiceType = "SO";
                    DateTime Createon = DateTime.Now;
                    DateTime updatedon = DateTime.Now;
                    string createdby = Convert.ToString(Session["UserFullName"]);
                    dt.Rows.Add(id, lblSubProcessID, ProjectID, lblProcessID, lblstatuss, lblStatusPercent, txtComment, txtServiceType, Createon, updatedon, createdby);
                }

                con = new SqlConnection();
                using (con = Connection.GetProjectChartConnection())
                {
                    using (cmd = new SqlCommand("Update_Stratagey"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@tblGDSC_StratagicData", dt);
                        cmd.Parameters.Add("@pProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"]);
                        cmd.Parameters.Add("@pServiceType", SqlDbType.VarChar).Value = "SO";

                        cmd.ExecuteNonQuery();

                    }
                }

                Response.Redirect("ListProjectChart.aspx");
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

        protected void btnCSI_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.AddRange(new DataColumn[11] { new DataColumn("ID", typeof(int)),
                new DataColumn("SubProcessID", typeof(int)),
                     new DataColumn("ProjectID", typeof(int)),
                  new DataColumn("ProcessID", typeof(int)),
                 
                  //new DataColumn("lblProcess", typeof(string)),
                  // new DataColumn("lblSubProcess", typeof(string)),
                    new DataColumn("Status", typeof(string)),
                     new DataColumn("StatusPercent", typeof(string)),
                new DataColumn("Comment",typeof(string)),
                new DataColumn("ServiceType",typeof(string)),
                 new DataColumn("CreateOn",typeof(DateTime)),
                 new DataColumn("updatedon",typeof(DateTime)),
                new DataColumn("createdby",typeof(string))});
                foreach (GridViewRow row in gvCSI.Rows)
                {
                    int id = int.Parse(Convert.ToString((row.FindControl("lblID") as Label).Text));

                    int lblProcessID = int.Parse(Convert.ToString((row.FindControl("lblProcessID") as Label).Text));
                    int lblSubProcessID = int.Parse(Convert.ToString((row.FindControl("lblSubProcessID") as Label).Text));
                    int ProjectID = Convert.ToInt32(Session["ProjID"].ToString());

                    string lblstatuss = Convert.ToString((row.FindControl("ddlCSI") as DropDownList).SelectedItem);
                    string lblStatusPercent = Convert.ToString((row.FindControl("lblStatusPercent") as Label).Text);
                    string txtComment = Convert.ToString((row.FindControl("txtComment") as TextBox).Text);
                    string txtServiceType = "SI";
                    DateTime Createon = DateTime.Now;
                    DateTime updatedon = DateTime.Now;
                    string createdby = Convert.ToString(Session["UserFullName"]);
                    dt.Rows.Add(id,  lblSubProcessID, ProjectID,lblProcessID, lblstatuss, lblStatusPercent, txtComment, txtServiceType, Createon, updatedon, createdby);
                }

                con = new SqlConnection();
                using (con = Connection.GetProjectChartConnection())
                {
                    using (cmd = new SqlCommand("Update_Stratagey"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@tblGDSC_StratagicData", dt);
                        cmd.Parameters.Add("@pProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"]);
                        cmd.Parameters.Add("@pServiceType", SqlDbType.VarChar).Value = "SI";

                        cmd.ExecuteNonQuery();

                    }
                }

                Response.Redirect("ListProjectChart.aspx");
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

        protected void gvCSI_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lb = (Label)(e.Row.FindControl("lblStatusPercent"));
                DropDownList dp = (DropDownList)e.Row.FindControl("ddlCSI");
                Label llb = (Label)e.Row.FindControl("Status");
                dp.Items.FindByValue(lb.Text).Selected = true;
                if (Session["UserID"].ToString() == lb.Text)
                {
                    LinkButton lnk = (LinkButton)e.Row.FindControl("popup");
                    lnk.Enabled = false;
                }
            }
        }

        protected void ddlCSI_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;

            foreach (GridViewRow row in gvCSI.Rows)
            {

                Control ctrl = row.FindControl("ddlCSI") as DropDownList;
                if (ctrl != null)
                {
                    DropDownList ddl1 = (DropDownList)ctrl;

                    if (ddl.ClientID == ddl1.ClientID)
                    {
                        Label txt1 = row.FindControl("lblStatusPercent") as Label;
                        txt1.Text = ddl1.SelectedValue;


                    }
                }
            }
        }

        public void GetStratagyAverage()
        {
            DataTable dt = new DataTable();
            try
            {

                using (con = Connection.GetProjectChartConnection())
                {

                    using (cmd = new SqlCommand("select AVG(cast(REPLACE(statuspercent,'%','') as int)) PercentAvg  from GDSC_StratagicData where ProjectID=@ppProjectID and ServiceType=@ppServiceType group by ProjectID", con))
                    {

                        cmd.Parameters.Add("@ppProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"]);
                        cmd.Parameters.Add("@ppServiceType", SqlDbType.VarChar).Value = "SS";

                        da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            string averageper = dt.Rows[0][0].ToString();
                            lblAvgpercent.Text = averageper + "%";
                        }
                        else
                        {

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
                dt = null;

            }
        }
        public void GetServiceDesignAverage()
        {
            DataTable dt = new DataTable();
            try
            {

                using (con = Connection.GetProjectChartConnection())
                {

                    using (cmd = new SqlCommand("select AVG(cast(REPLACE(statuspercent,'%','') as int)) PercentAvg  from GDSC_StratagicData where ProjectID=@ppProjectID and ServiceType=@ppServiceType  group by ProjectID", con))
                    {

                        cmd.Parameters.Add("@ppProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"]);
                        cmd.Parameters.Add("@ppServiceType", SqlDbType.VarChar).Value = "SD";

                        da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            string averageper = dt.Rows[0][0].ToString();
                            lblAveragePerctDesign.Text = averageper + "%";
                        }
                        else
                        {

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
                dt = null;

            }
        }

        public void GetServiceTransitionAverage()
        {
            DataTable dt = new DataTable();
            try
            {

                using (con = Connection.GetProjectChartConnection())
                {

                    using (cmd = new SqlCommand("select AVG(cast(REPLACE(statuspercent,'%','') as int)) PercentAvg  from GDSC_StratagicData where ProjectID=@ppProjectID and Servicetype=@ppServiceType group by ProjectID", con))
                    {

                        cmd.Parameters.Add("@ppProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"]);
                        cmd.Parameters.Add("@ppServiceType", SqlDbType.VarChar).Value = "ST";
                        da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            string averageper = dt.Rows[0][0].ToString();
                            lblAverageperctTrans.Text = averageper + "%";
                        }
                        else
                        {

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
                dt = null;

            }
        }

        public void GetServiceOperationAverage()
        {
            DataTable dt = new DataTable();
            try
            {

                using (con = Connection.GetProjectChartConnection())
                {

                    using (cmd = new SqlCommand("select AVG(cast(REPLACE(statuspercent,'%','') as int)) PercentAvg  from GDSC_StratagicData where ProjectID=@ppProjectID and ServiceType=@ppServiceType group by ProjectID", con))
                    {

                        cmd.Parameters.Add("@ppProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"]);
                        cmd.Parameters.Add("@ppServiceType", SqlDbType.VarChar).Value = "SO";
                        da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            string averageper = dt.Rows[0][0].ToString();
                            lblAveragePerctOps.Text = averageper + "%";
                        }
                        else
                        {

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
                dt = null;

            }
        }

        public void GetCSIAverage()
        {
            DataTable dt = new DataTable();
            try
            {

                using (con = Connection.GetProjectChartConnection())
                {

                    using (cmd = new SqlCommand("select AVG(cast(REPLACE(statuspercent,'%','') as int)) PercentAvg  from GDSC_StratagicData where ProjectID=@ppProjectID and ServiceType=@ppServiceType group by ProjectID", con))
                    {

                        cmd.Parameters.Add("@ppProjectID", SqlDbType.Int).Value = Convert.ToInt32(Session["ProjID"]);
                        cmd.Parameters.Add("@ppServiceType", SqlDbType.VarChar).Value = "SI";

                        da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            string averageper = dt.Rows[0][0].ToString();
                            lblAveragePerctCSI.Text = averageper + "%";
                        }
                        else
                        {

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
                dt = null;

            }
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetEmployee(string prefixText)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = new SqlDataAdapter();
            SqlDataReader dr = null;
            DataTable dt = new DataTable();
            using (con = Connection.GetConnection())
            {

                using (cmd = new SqlCommand("SELECT First_Name + ' ' + Last_Name FROM Master_Users where First_Name like @First_Name+'%' or Last_Name like @First_Name+'%'", con))
                {
                    cmd.Parameters.AddWithValue("@First_Name", prefixText);


                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    List<string> Employee = new List<string>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Employee.Add(dt.Rows[i][0].ToString());
                    }
                    return Employee;


                }


            }

        }


    }
}