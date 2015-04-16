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
    public partial class Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (PreviousPage != null)
                {
                    ViewState["UpdateAttendance"] = null;
                    DropDownList hfMonth = (DropDownList)Page.PreviousPage.Form.FindControl("MainContent").FindControl("ContentAttendanceTracker").FindControl("ddlMonths");
                    HiddenField hfEmpID = (HiddenField)Page.PreviousPage.Form.FindControl("MainContent").FindControl("ContentAttendanceTracker").FindControl("hfEmpID");
                    HiddenField hfEmpName = (HiddenField)Page.PreviousPage.Form.FindControl("MainContent").FindControl("ContentAttendanceTracker").FindControl("hfEmpName");
                    lblEmpName.Text = hfEmpName.Value;
                    btnSave.CommandArgument = hfEmpID.Value;
                    btnSave.CommandName = hfMonth.SelectedValue;
                    BindAttendance();
                    GetEmployeeLeaveDetails(Convert.ToInt32(hfEmpID.Value), hfMonth.SelectedValue);


                }
                else
                    Response.Redirect("ListAttendance.aspx");
            }
            else
            {
                // GetEmployeeLeaveDetails(Convert.ToInt32(btnSave.CommandArgument), btnSave.CommandName);
            }
        }

        protected void gvAttendanceList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlAttendanceTypes");
                HiddenField hf = (HiddenField)e.Row.FindControl("hfAttedanceType");
                DataTable dt = (DataTable)ViewState["Attendance"];
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.SelectedValue = hf.Value;
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

                    ViewState["Attendance"] = dt;
                    //ddlAttendanceType.DataSource = dt;
                    //ddlAttendanceType.DataBind();
                    //ddlAttendanceType.Items.Insert(0, new ListItem("Select Attendance", ""));

                }
            }
        }
        private void GetEmployeeLeaveDetails(int empID, string strMonth)
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                int currentYear = DateTime.Now.Year;
                string strEndDate = "";
                int days = DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToInt32(strMonth));
                if (strMonth != "12")
                    strEndDate = currentYear + "-" + strMonth + "-" + days;
                else
                    strEndDate = (currentYear + 1) + "-" + (Convert.ToInt32(strMonth) + 1) + "-01";

                // string strQuery = "Select * from Employee_Leaves_Status els inner join Master_Leave_Types mlt on mlt.id=els.Leave_ID where els.Created_By=" + Session["UserID"].ToString();
                using (SqlCommand cmd = new SqlCommand("GetLeaveDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userID", empID));
                    cmd.Parameters.Add(new SqlParameter("@startDate", (currentYear + "-" + strMonth + "-01")));
                    cmd.Parameters.Add(new SqlParameter("@endDate", strEndDate));
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    gvAttendanceList.DataSource = dt;
                    gvAttendanceList.DataBind();
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ViewState["UpdateAttendance"] != null)
            {
                DataTable tbIDs = (DataTable)ViewState["UpdateAttendance"];
                using (SqlConnection con = Connection.GetConnection())
                {
                    SqlCommand cmd;
                    foreach (DataRow dr in tbIDs.Rows)
                    {
                        cmd = new SqlCommand("UPDATE User_Attendance SET Attendance_TypeID=" + dr["AttendanceTypeId"].ToString() + " where ID=" + dr["Id"].ToString(), con);
                        cmd.ExecuteNonQuery();
                    }
                }
                GetEmployeeLeaveDetails(Convert.ToInt32(btnSave.CommandArgument), btnSave.CommandName);
            }
        }
        protected void ddlAttendanceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tbIDs;
            if (ViewState["UpdateAttendance"] == null)
            {
                tbIDs = new DataTable("IDs");
                tbIDs.Columns.Add("Id", typeof(int));
                tbIDs.Columns.Add("AttendanceTypeId", typeof(int));
            }
            else
                tbIDs = (DataTable)ViewState["UpdateAttendance"];
            DropDownList ddl = (DropDownList)sender;
            GridViewRow gvRow = (GridViewRow)ddl.NamingContainer;
            int id = Convert.ToInt32(gvAttendanceList.DataKeys[gvRow.RowIndex].Value);

            DataRow[] HRow = tbIDs.Select("Id = " + id);
            if (HRow.Count() > 0)
            {
                HRow[0]["AttendanceTypeId"] = Convert.ToInt32(ddl.SelectedValue);
            }
            else
            {

                DataRow dr = tbIDs.NewRow();
                dr["AttendanceTypeId"] = Convert.ToInt32(ddl.SelectedValue);
                dr["Id"] = Convert.ToInt32(gvAttendanceList.DataKeys[gvRow.RowIndex].Value);
                tbIDs.Rows.Add(dr);
            }
            ViewState["UpdateAttendance"] = tbIDs;
        }
    }
}