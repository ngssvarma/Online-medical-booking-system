using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            bool found=false;
            string connStr = ConfigurationManager.AppSettings["conn"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "";
                if (lstType.SelectedValue == "Admin")
                    query = "SELECT Password FROM Admin WHERE Email_id=@Email_id";

                else if (lstType.SelectedValue == "Doctor")
                    query = "SELECT Password FROM Doctor WHERE Email_id=@Email_id";

                else if (lstType.SelectedValue == "Patients")
                    query = "SELECT Password FROM Patients WHERE Email_id=@Email_id";
                
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Email_id", txtUsername.Text);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    found = true;
                    //Email it Method
                    lblMessage.Text="Password is : "+dr.GetString(0);
                    break;
                }

                if (!found)
                {
                    lblMessage.Text = "Username not found for " + lstType.SelectedValue;
                }
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
}
