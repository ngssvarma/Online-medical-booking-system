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

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            lblMessage.Text = "";
            bool found = false;
            string connStr = ConfigurationManager.AppSettings["conn"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "";
                string query1 = "";
                if (lstType.SelectedValue == "Admin")
                {
                    query = "SELECT ID,Password,Email_id,Last_login FROM Admin WHERE Email_id=@Email_id";
                    query1 = "UPDATE Admin SET Last_login=@Last_login WHERE Email_id=@Email_id";
                }

                else if (lstType.SelectedValue == "Doctor")
                {
                    query = "SELECT ID, Password,Email_id,Last_login,Active FROM Doctor WHERE Email_id=@Email_id";
                    query1 = "UPDATE Doctor SET Last_login=@Last_login WHERE Email_id=@Email_id";
                }
                else if (lstType.SelectedValue == "Patients")
                {
                    query = "SELECT ID, Password,Email_id,Last_login,Active FROM Patients WHERE Email_id=@Email_id";
                    query1 = "UPDATE Patients SET Last_login=@Last_login WHERE Email_id=@Email_id";
                }

                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Email_id", txtUsername.Text);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    found = true;
                    if (txtPassword.Text.Trim() != dr.GetString(1))
                    {
                        lblMessage.Text = "Password Inccorrect for " +txtUsername.Text.Trim();
                        break;
                    }
                    if (lstType.SelectedValue != "Admin")
                    {
                        try
                        {
                            bool Isactive = dr.GetBoolean(4);
                            if (!Isactive)
                            {
                                lblMessage.Text = "Account is not active for " + txtUsername.Text.Trim() + "\nContact Administrator";
                                break;
                            }
                        }
                        catch
                        {
                            lblMessage.Text = "Account is not active for " + txtUsername.Text.Trim() + "\nContact Administrator";
                            break;
                        }
                    }
                    //Email it Method
                    //lblMessage.Text = "Password is : " + dr.GetString(0);
                    
                    HttpCookie username = new HttpCookie("cUsername");
                    username.Value = dr.GetString(2);
                    Response.Cookies.Add(username);

                    HttpCookie usertype= new HttpCookie("cUsertype");
                    usertype.Value = lstType.SelectedValue;
                    Response.Cookies.Add(usertype);

                    HttpCookie ID = new HttpCookie("cID");
                    ID.Value = dr.GetInt32(0).ToString();
                    Response.Cookies.Add(ID);

                    HttpCookie last_login = new HttpCookie("cLast_login");
                    try
                    {
                        last_login.Value = dr.GetDateTime(3).ToString();
                        Response.Cookies.Add(last_login);
                    }
                    catch
                    {
                        
                    }
                    
                    dr.Close();

                    using (SqlCommand cmd01 = new SqlCommand(query1, conn))
                    {

                        cmd01.Parameters.AddWithValue("@Email_id", txtUsername.Text);
                        cmd01.Parameters.AddWithValue("@Last_login", DateTime.Now);

                        cmd01.ExecuteNonQuery();
                    }

                    break;

                }

                if (!found)
                {
                    lblMessage.Text = "Username not found for " + lstType.SelectedValue;
                    return;                       
                }


                if (lblMessage.Text == "")
                {
                    if (lstType.SelectedValue == "Admin")
                        Response.Redirect("~/Default.aspx");

                    else if (lstType.SelectedValue == "Doctor")
                        Response.Redirect("~/Default.aspx");

                    else if (lstType.SelectedValue == "Patients")
                        Response.Redirect("~/Default.aspx");
                }


            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }



    }
    protected void lstType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
