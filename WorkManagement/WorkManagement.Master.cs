using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GSDC.WorkManagement
{
    public partial class WorkManagement : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Convert.ToString(Session["IsApprover"]) =="True")
            {
                
                A2.Visible = true;
            }
        }

       
        

        protected void lnkButton_Click(object sender, EventArgs e)
        {

            Response.Redirect("Reporting.aspx");
        }
    }
}