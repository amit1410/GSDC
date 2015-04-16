using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GSDC.LeaveTracker
{
    public partial class LeaveTracker : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsApprover"].ToString() == "True")
                liManager.Visible = true;
        }
    }
}