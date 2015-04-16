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
    public partial class ListAttendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int currentMonth = DateTime.Now.Month;
                BindGrid(currentMonth.ToString());
                string[] strMonths = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "dec" };

                for (int i = 0; i < strMonths.Length; i++)
                {
                    if (i == currentMonth)
                    {
                        break;
                    }
                    else
                    {
                        ddlMonths.Items.Add(new ListItem(strMonths[i], (i + 1).ToString("00")));
                        ddlMonths.SelectedIndex = i;
                    }
                }

            }
        }

        protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid(ddlMonths.SelectedValue);
        }
        private void BindGrid(string strMonth)
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
                using (SqlCommand cmd = new SqlCommand("GetMonthlyAttendance", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userID", Convert.ToInt32(Session["UserId"].ToString())));
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
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            DataTable tbCategories = new DataTable("IDs");
            tbCategories.Columns.Add("Id", typeof(int));
            DataRow drRow;
            foreach (GridViewRow dr in gvAttendanceList.Rows)
            {
                if (dr.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)dr.FindControl("chk");
                    if (chk.Checked)
                    {
                        drRow = tbCategories.NewRow();
                        drRow["Id"] = chk.ValidationGroup;
                        tbCategories.Rows.Add(drRow);
                    }
                }
            }
            if (tbCategories.Rows.Count > 0)
            {
                using (SqlConnection con = Connection.GetConnection())
                {
                    int currentYear = DateTime.Now.Year;
                    string strEndDate = "";
                    int days = DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToInt32(ddlMonths.SelectedValue));
                    if (btnApprove.CommandName != "12")
                        strEndDate = currentYear + "-" + ddlMonths.SelectedValue + "-" + days;
                    else
                        strEndDate = (currentYear + 1) + "-" + (Convert.ToInt32(ddlMonths.SelectedValue) + 1) + "-01";
                    using (SqlCommand cmd = new SqlCommand("ApproveAttendance", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@empIDs", tbCategories);
                        cmd.Parameters.Add(new SqlParameter("@startDate", (currentYear + "-" + ddlMonths.SelectedValue + "-01")));
                        cmd.Parameters.Add(new SqlParameter("@endDate", strEndDate));
                        cmd.ExecuteNonQuery();
                    }
                }
                BindGrid(ddlMonths.SelectedValue);
            }
        }
        protected void lnkEmpID_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            hfEmpID.Value = lnk.CommandArgument;
            hfEmpName.Value = lnk.CommandName;
        }
    }
}
