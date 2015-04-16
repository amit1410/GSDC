using GSDC.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GSDC.Admin
{
    public partial class CreateRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnUpload_Click(object sender, EventArgs e) {
            if (FileUpload1.HasFile)
            {

                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);

                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];



                string FilePath = Server.MapPath(FolderPath + FileName);

                FileUpload1.SaveAs(FilePath);

               // Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SaveRole", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@role", txtRole.Text.Trim().ToTitleCase());
                    cmd.Parameters.AddWithValue("@approver", chkApprover.Checked);

                    cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    cmd.Parameters["@ReturnVal"].Direction = ParameterDirection.ReturnValue;

                    int count = 0;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        count = (int)cmd.Parameters["@ReturnVal"].Value;
                        if (count == 1)
                        {
                            lblStatus.Text = "Role already exist.";
                            lblStatus.ForeColor = Color.Red;
                        }
                        else if (count == 0)
                        {
                            lblStatus.Text = "Role has been save.";
                            lblStatus.ForeColor = Color.Green;
                            txtRole.Text = "";
                            chkApprover.Checked = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message;
                    }
                }
            }
        }
    }
}