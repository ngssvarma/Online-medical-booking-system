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


public partial class Admin_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        if (txtOldPassword.Text.Trim() == "")
        {
            lblMessage.Text = "Old Password Cannot be empty";
            return;
        }

        if (txtNewPassword.Text.Trim() == "")
        {
            lblMessage.Text = "New Password Cannot be empty";
            return;
        }

        if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
        {
            lblMessage.Text ="password does not match";
            return;
        }
        if(checkPassword(txtOldPassword.Text.Trim()))
        {
            try
            {
                string connStr = ConfigurationManager.AppSettings["conn"].ToString();
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string query = "";

                    HttpCookie Usertype = Request.Cookies["cUsertype"];
                    if (Usertype.Value == "Admin")
                    {
                        query = "UPDATE Admin SET Password='"+txtConfirmPassword.Text.Trim()+"' WHERE Email_id=@Email_id";
                    }
                    if (Usertype.Value == "Doctor")
                    {
                        query = "UPDATE Doctor SET Password='" + txtConfirmPassword.Text.Trim() + "' WHERE Email_id=@Email_id";
                    }
                    if (Usertype.Value == "Patients")
                    {
                        query = "UPDATE Patients SET Password='" + txtConfirmPassword.Text.Trim() + "' WHERE Email_id=@Email_id";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email_id", Request.Cookies["cUsername"].Value);
                    SqlDataReader dr = cmd.ExecuteReader();

                    Response.Redirect("~/Default.aspx");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        
        }
    }

    private Boolean checkPassword()
    {
        return true;
    }

    private bool checkPassword(string oldPassword)
    {
        try
        {
            string connStr = ConfigurationManager.AppSettings["conn"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "";

                HttpCookie Usertype = Request.Cookies["cUsertype"];
                if (Usertype.Value == "Admin")
                {
                    query = "SELECT Password FROM Admin WHERE Email_id=@Email_id";
                }
                if (Usertype.Value == "Doctor")
                {
                    query = "SELECT Password FROM Doctor WHERE Email_id=@Email_id";
                }
                if (Usertype.Value == "Patients")
                {
                    query = "SELECT Password FROM Patients WHERE Email_id=@Email_id";
                }
                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@Email_id", Request.Cookies["cUsername"].Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (txtOldPassword.Text.Trim()!=dr.GetString(0))
                    {
                        lblMessage.Text="Incorrect password";
                        return false;
                    }
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

        return false;
    }
}
