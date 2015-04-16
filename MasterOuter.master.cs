using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GSDC
{
    public partial class MasterOuter : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                RequestLogin();
            if (Session["UserRole"].ToString() == "Administrator")
            {
                liAdmin.Visible = true;
            }
           
            
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                RequestLogin();
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                RequestLogin();
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