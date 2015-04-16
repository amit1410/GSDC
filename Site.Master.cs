using GSDC.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GSDC
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            //var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            //Guid requestCookieGuidValue;
            //if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            //{
            //    // Use the Anti-XSRF token from the cookie
            //    _antiXsrfTokenValue = requestCookie.Value;
            //    Page.ViewStateUserKey = _antiXsrfTokenValue;
            //}
            //else
            //{
            //    // Generate a new Anti-XSRF token and save to the cookie
            //    _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            //    Page.ViewStateUserKey = _antiXsrfTokenValue;

            //    var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            //    {
            //        HttpOnly = true,
            //        Value = _antiXsrfTokenValue
            //    };
            //    if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            //    {
            //        responseCookie.Secure = true;
            //    }
            //    Response.Cookies.Set(responseCookie);
            //}
            if (Session["UserID"] == null)
                RequestLogin();
            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    // Set Anti-XSRF token
            //    ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            //    ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            //}
            //else
            //{
            //    // Validate the Anti-XSRF token
            //    if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
            //        || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            //    {
            //        throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            //    }
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCancel_Click(object sender, EventArgs e) {
            //((TextBox)HeadLoginView.FindControl("txtConfirmPassword")).Text = "";
            //((TextBox)HeadLoginView.FindControl("txtNewPassword")).Text = "";
            //((TextBox)HeadLoginView.FindControl("txtOldPassword")).Text = "";
            //ModelPopU
            //((ModalPopupExtender)HeadLoginView.FindControl("ModalPopupExtender1")).Text = "";

            txtConfirmPassword.Text = "";
            txtNewPassword.Text = "";
            txtOldPassword.Text = "";
            lblStatus.Text = "";
            ModalPopupExtender1.Hide();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("ResetPassword", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userID", Convert.ToInt32(Session["UserId"].ToString()));
                    cmd.Parameters.AddWithValue("@password", txtOldPassword.Text);
                    cmd.Parameters.AddWithValue("@newpassword", txtNewPassword.Text);

                    SqlParameter parmOUT = new SqlParameter("@return", SqlDbType.Int);
                    parmOUT.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parmOUT);
                    cmd.ExecuteNonQuery();
                    //int returnVALUE = (int)cmd.Parameters["@return"].Value;

                    int status = (int)cmd.Parameters["@return"].Value;
                    if (status == 0)
                    {
                        lblStatus.Text = "You have reset password successfuly";
                        lblStatus.ForeColor = Color.Green;
                       
                        btnCancel_Click(null, null);
                    }
                    else
                    {

                        lblStatus.Text = "Invalid old password";
                        lblStatus.ForeColor = Color.Red;
                        txtConfirmPassword.Text = txtConfirmPassword.Text;
                        txtNewPassword.Text = txtNewPassword.Text;
                    }
                    
                }
            }
           
        }
        
        protected void RequestLogin()
        {
            string OriginalUrl = HttpContext.Current.Request.RawUrl;
            string LoginPageUrl = "/Login.aspx";
            HttpContext.Current.Response.Redirect(String.Format("{0}?ReturnUrl={1}",
            LoginPageUrl, OriginalUrl));
        }
    }
}