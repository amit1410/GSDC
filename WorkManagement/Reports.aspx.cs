using System;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GSDC.WorkManagement
{
    public partial class Reports : System.Web.UI.Page
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataAdapter da = new SqlDataAdapter();
        SqlDataReader dr = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportParameter[] param = new ReportParameter[1];
                param[0] = new ReportParameter("userID", "4");

                // ReportCredentials ReportCredentials = new ReportCredentials();
                ReportViewer1.ShowCredentialPrompts = false;
                ServerReport ser = ReportViewer1.ServerReport;
                ser.ReportServerCredentials = new ReportCredentials();
                //  ser.ProcessingMode = ProcessingMode.Remote;
                ser.ReportServerUrl = new System.Uri(ConfigurationManager.AppSettings["Reportserver"].ToString());
                ser.ReportPath =
                        "/TestReport/EmployeeWiseTask1";
                ser.SetParameters(param);
                ReportViewer1.Visible = true;
               // ser.Refresh();
            }
        }
        
        //protected void btnBack_Click(object sender, EventArgs e)
        //{
        //    divForm.Visible = true;
        //    divViewer.Visible = false;
        //}

        //[Serializable]
        //public sealed class MyReportServerCredentials : IReportServerCredentials
        //{
        //    public WindowsIdentity ImpersonationUser
        //    {
        //        get
        //        {    // Use the default Windows user.  Credentials will be
        //            // provided by the NetworkCredentials property.
        //            return null;
        //        }
        //    }

        //    public ICredentials NetworkCredentials
        //    {
        //        get
        //        {
        //            // Read the user information from the Web.config file.  
        //            // By reading the information on demand instead of 
        //            // storing it, the credentials will not be stored in 
        //            // session, reducing the vulnerable surface area to the
        //            // Web.config file, which can be secured with an ACL.

        //            // User name
        //            string userName = ConfigurationManager.AppSettings["username"];

        //            if (string.IsNullOrEmpty(userName))
        //                throw new Exception("Missing user name from web.config file");

        //            // Password
        //            string password = ConfigurationManager.AppSettings["dom"];

        //            if (string.IsNullOrEmpty(password))
        //                throw new Exception("Missing password from web.config file");

        //            // Domain
        //            string domain = ConfigurationManager.AppSettings["Reportserver"];

        //            if (string.IsNullOrEmpty(domain))
        //                throw new Exception("Missing domain from web.config file");

        //            return new NetworkCredential(userName, password, domain);
        //        }
        //    }

        //    public bool GetFormsCredentials(out Cookie authCookie,
        //                out string userName, out string password,
        //                out string authority)
        //    {
        //        authCookie = null;
        //        userName = null;
        //        password = null;
        //        authority = null;

        //        // Not using form credentials
        //        return false;
        //    }
        //}

    }

    public class ReportCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
    {
        string _userName, _password, _domain;
        public ReportCredentials()
        {
            _userName = ConfigurationManager.AppSettings["username"].ToString();
            _password = ConfigurationManager.AppSettings["dom"].ToString();
            _domain = ConfigurationManager.AppSettings["ServerName"].ToString();
        }
        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }
        public System.Net.ICredentials NetworkCredentials
        {
            get
            {
                return new System.Net.NetworkCredential("admin.amit", "Welcome@1234", "168.63.211.32");
            }
        }
        public bool GetFormsCredentials(out System.Net.Cookie authCoki, out string userName, out string password, out string authority)
        {
            userName = _userName;
            password = _password;
            authority = _domain;
            authCoki = new System.Net.Cookie(".ASPXAUTH", ".ASPXAUTH", "/", "hhi");
            return true;
        }
    }
    
}