using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GSDC.App_Code;

namespace GSDC.ProjectChart
{
    public partial class ProjectDetailsView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con =null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            if(!IsPostBack)
            {
                using (con = Connection.GetConnection())
                {

                    using (cmd = new SqlCommand(" select Employee_Code,First_Name,Last_Name,Role_Name,c.Team_Name Team_Name from Master_Users a inner join Master_Roles b on b.ID=a.Role_ID inner join Master_Teams c on c.ID=a.Team_ID where a.Employee_Code=@Employee_Code", con))
                    {

                        cmd.Parameters.Add("@Employee_Code", SqlDbType.VarChar).Value = Convert.ToString(Session["Employee_Code"].ToString());

                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            lblEmpCode.Text = dr["Employee_Code"].ToString();
                            lblEmpName.Text = dr["First_Name"].ToString()+" "+dr["Last_Name"].ToString();
                            lbEmpDesg.Text = dr["Role_Name"].ToString();
                            lblEmailID.Text = dr["Team_Name"].ToString(); 
                           
                          
                        }


                    }


                }
            }
        }
    }
}