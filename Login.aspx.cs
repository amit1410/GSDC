using GSDC.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace GSDC
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void Login1_Authenticate(object sender, EventArgs e)
        {
            SqlConnection con=null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                con = Connection.GetConnection();
                //        SqlCommand cmd = new SqlCommand(@"select ms.First_Name,ms.Last_Name,ms.Id,Role_Name,ms.Role_ID,ms.Team_ID,Team_Name, mu.Id MID,mu.First_Name MFName,mu.Last_Name MLName 
                //                                        from Master_Users ms 
                //                                        inner join Master_Roles mr on ms.Role_ID =mr.Id 
                //                                        inner join Master_Teams mt on ms.Team_ID=mt.Id
                //                                        inner join Team_Managers tm on tm.Team_ID=mt.ID
                //                                        inner join Master_Users mu on  mu.ID=tm.Manager_ID  where ms.User_Name='" + UserName.Text.ToString() + "' and ms.Password='" + Password.Text.ToString() + "' and mr.Is_Active=1 and mt.Is_Active=1 and ms.Is_Active=1", con);

                cmd = new SqlCommand("GetUserDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@username", UserName.Text.Trim().ToLower()));
                cmd.Parameters.Add(new SqlParameter("@password", Password.Text.ToString()));
                da = new SqlDataAdapter(cmd);
                
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Session["UserFullName"] = dt.Rows[0]["Last_Name"].ToString().ToTitleCase() + ", " + dt.Rows[0]["First_Name"].ToString().ToTitleCase();
                    Session["UserRole"] = dt.Rows[0]["Role_Name"].ToString();
                    Session["UserID"] = dt.Rows[0]["Id"].ToString();
                    Session["UserEmail"] = UserName.Text.ToString();
                    Session["Employee_Code"] = dt.Rows[0]["Employee_Code"].ToString();//added by amit agarwal for get Employee code to show Employee Imformation
                    Session["UserMangerFullName"] = dt.Rows[0]["MLName"].ToString().ToTitleCase() + ", " + dt.Rows[0]["MFName"].ToString().ToTitleCase();
                    string strMID = dt.Rows[0]["MID"].ToString();
                    Session["UserManagerID"] = strMID == "" ? "0" : strMID;
                    Session["UserRoleID"] = dt.Rows[0]["Role_ID"].ToString();
                    Session["UserTeam"] = dt.Rows[0]["Team_Name"].ToString().ToTitleCase(ExtensionHelper.TitleCase.Words);
                    Session["UserTeamID"] = dt.Rows[0]["Team_ID"].ToString();
                    Session["IsApprover"] = dt.Rows[0]["Leave_Approver"].ToString();
                    Session["IsAttendentAuth"] = dt.Rows[0]["IsAttendenceAuth"].ToString();
                    FormsAuthentication.SetAuthCookie(UserName.Text, RememberMe.Checked);
                    // Response.Redirect(Login1.DestinationPageUrl);
                    //Response.Redirect("/LeaveTracker/MyApprovals.aspx");
                    if (Request.QueryString["ReturnUrl"] != null)
                    {
                        HttpContext.Current.Response.Redirect(Request.QueryString["ReturnUrl"]);
                        // IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                    }
                    //else if (Convert.ToString(Session["IsAttendentAuth"])=="1")
                    //{
                    //    Response.Redirect("Default.aspx");
                    //}
                    
                    //else
                    //{
                    //    Response.Redirect("AttendanceTracker/Attendance.aspx");
                    //}
                    Response.Redirect("Default.aspx");

                }
                else
                {
                    FailureText.Text = "Invalid username or password.";
                    ErrorMessage.Visible = true;
                }
            }
            catch(Exception exception)
            {}
            finally
            {
                con.Close();
                cmd.Dispose();
                da.Dispose();
                dt = null;

            }


        }
        protected void Login1_LoginError(object sender, EventArgs e)
        {
            //if (ViewState["LoginErrors"] == null)
            //    ViewState["LoginErrors"] = 0;

            //int ErrorCount = (int)ViewState["LoginErrors"] + 1;
            //ViewState["LoginErrors"] = ErrorCount;

            //if ((ErrorCount > 3) && (Login1.PasswordRecoveryUrl != string.Empty))
            //    Response.Redirect(Login1.PasswordRecoveryUrl);
        }
    }
}