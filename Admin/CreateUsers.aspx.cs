using GSDC.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GSDC.Admin
{
    public partial class CreateUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                using (SqlConnection con = Connection.GetConnection())
                {
                    GetRoles(con);
                    GetTeams(con);
                }
                CreateTable();
            }
        }
        private void GetRoles(SqlConnection con)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT ID,Role_Name FROM Master_Roles where Leave_Approver=0", con))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                ViewState["Roles"] = dt;
            }
        }
        private void GetTeams(SqlConnection con)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT ID,Team_Name FROM Master_Teams", con))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                ddlTeams.DataSource = dt;
                ddlTeams.DataBind();

            }
        }

        protected void gvEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList dl = (DropDownList)e.Row.FindControl("ddlRole");
                dl.DataSource = (DataTable)ViewState["Roles"];
                dl.DataBind();
            }
        }

        protected void ddlTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT mu.ID, concat(mu.Last_Name,', ',mu.First_Name) Name FROM Master_Users mu INNER JOIN Master_Roles mr ON mu.Role_ID=mr.ID AND mr.Leave_Approver=1  WHERE  Team_ID=" + ddlTeams.SelectedValue, con))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    ddlManagers.DataSource = dt;
                    ddlManagers.DataBind();


                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("User_Name", typeof(string)),
                                                    new DataColumn("First_Name", typeof(string)),
                                                    new DataColumn("Last_Name",typeof(string)) ,
                                                    new DataColumn("Password",typeof(string)) ,
                                                    new DataColumn("Employee_Code",typeof(string)),
                                                    new DataColumn("Role_ID",typeof(Int32)),
                                                    new DataColumn("Team_ID",typeof(Int32)),
                                                    new DataColumn("Reporting_Manager",typeof(Int32))});
            foreach (GridViewRow row in gvEmployees.Rows)
            {
                if (((TextBox)row.FindControl("txtFirstName")).Text!="")
                {
                    string strFm = ((TextBox)row.FindControl("txtFirstName")).Text;
                    string strLn = ((TextBox)row.FindControl("txtLastName")).Text;
                    int roleid = int.Parse(((DropDownList)row.FindControl("ddlRole")).SelectedValue);

                    string name = row.Cells[2].Text;
                    string country = row.Cells[3].Text;
                    dt.Rows.Add(strFm.ToLower() + "." + strLn.ToLower(), strFm, strLn, "test1234", ((TextBox)row.FindControl("txtEmpID")).Text, roleid, int.Parse(ddlTeams.SelectedValue), int.Parse(ddlManagers.SelectedValue));
                }
            }
            if (dt.Rows.Count > 0)
            {


                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["GSDCConnectionString2"].ToString(), SqlBulkCopyOptions.FireTriggers))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.Master_Users";

                        //[OPTIONAL]: Map the DataTable columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("User_Name", "User_Name");
                        sqlBulkCopy.ColumnMappings.Add("First_Name", "First_Name");
                        sqlBulkCopy.ColumnMappings.Add("Last_Name", "Last_Name");

                        sqlBulkCopy.ColumnMappings.Add("Password", "Password");
                        sqlBulkCopy.ColumnMappings.Add("Employee_Code", "Employee_Code");
                        sqlBulkCopy.ColumnMappings.Add("Role_ID", "Role_ID");
                        sqlBulkCopy.ColumnMappings.Add("Team_ID", "Team_ID");
                        sqlBulkCopy.ColumnMappings.Add("Reporting_Manager", "Reporting_Manager");
                       // if (con.State == ConnectionState.Closed)
                       //     con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                      //  con.Close();
                    }
                
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["EmpRecords"];



        }
        private void CreateTable()
        {
            DataTable dtRecords = new DataTable("Records");
            dtRecords.Columns.Add("First_Name");
            dtRecords.Columns.Add("Last_Name");
            dtRecords.Columns.Add("EmpID");
            dtRecords.Columns.Add("RoleID");
            DataRow dr;

            for (int i = 0; i < 15; i++)
            {
                dr = dtRecords.NewRow();
                dtRecords.Rows.Add(dr);
            }
            ViewState["EmpRecords"] = dtRecords;
            gvEmployees.DataSource = dtRecords;
            gvEmployees.DataBind();
        }
    }
}