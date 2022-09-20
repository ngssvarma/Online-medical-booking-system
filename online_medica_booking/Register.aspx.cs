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

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnPatientRegister_Click(object sender, EventArgs e)
    {
        try
        {

            string connStr = ConfigurationManager.AppSettings["conn"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "INSERT INTO Patients(Name,Address,City,Zip,Contact_no,Email_id,Password) VALUES(@Name,@Address,@City,@Zip,@Contact_no,@Email_id,@Password)";

                SqlCommand cmd = new SqlCommand(query, conn);
                //@Name,@Address,@City,@Zip,@Contact_no,@Email_id,@Password
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@City", txtCity.Text.Trim());
                cmd.Parameters.AddWithValue("@Zip", txtZip.Text.Trim());
                cmd.Parameters.AddWithValue("@Contact_no", txtContactNo.Text.Trim());
                cmd.Parameters.AddWithValue("@Email_id", txtEmailId.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

                cmd.ExecuteNonQuery();
            }
        }
        catch(Exception ex) 
        {
            lblMessage.Text = ex.Message;
        }
            
    }
}
