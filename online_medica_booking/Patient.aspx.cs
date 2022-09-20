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

public partial class Admin_Patient : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["action"] == "Register")
            {
                AddNewPatient01();
                return;

            }
            HttpCookie username = Request.Cookies["cUsername"];
            //if (username == null)
            //{
            //    Response.Redirect("~/Login.aspx");
            //}
            if (Request.QueryString["action"] == "Add")
            {
                AddNewPatient01();
            }
            
            else if (Request.QueryString["action"] == "Profile")
            {
                ViewPatient();
            }
            else if (Request.QueryString["action"] == "Update")
            {
                EditPatient();
            }
            else
            {
                ListOfPatients();
            }
        }
    }

    private void ViewPatient()
    {
        btnSave.Text = "Edit";

        PatientGrid.Visible = false;

        GetPatientProfile();

        trPatientHeader.InnerHtml = "Profile Information";

        txtName.CssClass = "disenable";
        txtName.Enabled = false;

        txtAddress.CssClass = "disenable";
        txtAddress.Enabled = false;

        txtCity.CssClass = "disenable";
        txtCity.Enabled = false;

        txtZip.CssClass = "disenable";
        txtZip.Enabled = false;

        txtContactNo.CssClass = "disenable";
        txtContactNo.Enabled = false;

        txtEmailId.Enabled = false;
        txtEmailId.CssClass = "disenable";

        txtPassword.Enabled = false;
        txtPassword.CssClass = "disenable";

        txtConfirmPassword.Enabled = false;
        txtConfirmPassword.CssClass = "disenable";
    }

    private void GetPatientProfile()
    {
        string connStr = ConfigurationManager.AppSettings["conn"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            HttpCookie username = Request.Cookies["cUsername"];
            string query = "";

            if (Request.QueryString["PatID"] != null)
            {
                query = "SELECT Name,Address,City, Zip, Contact_no,Email_id,Password FROM Patients WHERE Id=" + Request.QueryString["PatID"] + "";
            }
            else
            {
                query = "SELECT Name,Address,City, Zip, Contact_no,Email_id,Password FROM Patients WHERE Email_Id=@Email_Id";
            }

            SqlCommand cmd = new SqlCommand(query, conn);

            if (Request.QueryString["PatID"] != null)
            {
            }
            else
            {
                cmd.Parameters.AddWithValue("@Email_Id", username.Value);
            }

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtName.Text = dr.GetString(0);
                txtAddress.Text = dr.GetString(1);
                txtCity.Text = dr.GetString(2);
                txtZip.Text = dr.GetString(3);
                txtContactNo.Text = dr.GetString(4);
                txtEmailId.Text = dr.GetString(5);
                txtPassword.Text = dr.GetString(6);
                txtConfirmPassword.Text = dr.GetString(6);
            }
        }
    }

    private void EditPatient()
    {
        btnSave.Text = "Update";

        PatientGrid.Visible = false;

        GetPatientProfile();

        txtEmailId.Enabled = false;
        txtEmailId.CssClass = "disenable";

        trPatientHeader.InnerHtml = "Edit Profile Information";  
    }

    private void ListOfPatients()
    {
        AddNewPatient.Visible = false;

        string strhtml = "";

        //strhtml += "<table>";

        bool found = false;

        string connStr = ConfigurationManager.AppSettings["conn"].ToString();

        string straction = "";
        
        
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string query = "SELECT ID, Name,Address,City,Zip,Contact_no FROM Patients";

            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                found = true;

                strhtml += "<tr>";

                //Name,Address,City,Zip,Contact_no
                strhtml += "<td>";
                strhtml += dr.GetString(1);
                strhtml += "</td>";

                strhtml += "<td>";
                strhtml += dr.GetString(2);
                strhtml += "</td>";

                strhtml += "<td>";
                strhtml += dr.GetString(3);
                strhtml += "</td>";

                strhtml += "<td>";
                strhtml += dr.GetString(4);
                strhtml += "</td>";

                //strhtml += "<td>";
                //strhtml += dr.GetString(5);
                //strhtml += "</td>";

                strhtml += "</tr>";
            }
        }

        //strhtml += "</table>";

        if (!found)
        {
            strhtml = "<tr><th colspan='100'>No Patient Found</th></tr>";
        }

        trPatientGrid.InnerHtml = strhtml.ToString();

    }

    private void AddNewPatient01()
    {
        PatientGrid.Visible = false;
        AddNewPatient.Visible = true;
    }
    protected void btnPatientRegister_Click(object sender, EventArgs e)
    {
        HttpCookie Usertype = Request.Cookies["cUsertype"];

        if (btnSave.Text == "Edit")
        {
            btnSave.Text = "Update";
            if (Usertype.Value == "Admin")
            {
                Response.Redirect("~/Patient.aspx?PatID=" + Request.QueryString["PatID"] + "&action=Update");
            }
            if (Usertype.Value == "Patients")
            {
                Response.Redirect("~/Patient.aspx?action=Update");
            }
            return;
        }

        if (btnSave.Text != "Update")
        {
            if (!CheckEmail(txtEmailId.Text.Trim()))
            {
                return;
            }
        }

        if (txtName.Text.Trim() == "")
        {
            lblMessage.Text = "Please enter Name";
            return;
        }

        if (txtAddress.Text.Trim() == "")
        {
            lblMessage.Text = "Please enter address";
            return;
        }

        if (txtCity.Text.Trim() == "")
        {
            lblMessage.Text = "Please enter city name";
            return;
        }

        if (txtZip.Text.Trim() == "")
        {
            lblMessage.Text = "Please enter zip code";
            return;
        }

        if (txtContactNo.Text.Trim() == "")
        {
            lblMessage.Text = "Please enter contact no";
            return;
        }
        else
        {
            //try
            //{
            //    Int32.Parse(txtContactNo.Text.Trim());
            //}
            //catch
            //{
            //    lblMessage.Text = "Contact no should be numeric only";
            //    return;
            //}
        }

        if (txtPassword.Text.Trim() == "")
        {
            lblMessage.Text = "Please enter password";
            return;
        }


        if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
        {
            lblMessage.Text = "Password not match";
            return;
        }
       
        try
        {
            string connStr = ConfigurationManager.AppSettings["conn"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "";

                if (btnSave.Text == "Update")
                {
                    query = "UPDATE Patients SET Name=@Name,Address=@Address,City=@City,Zip=@Zip,Contact_no=@Contact_no,Password=@Password WHERE  Email_Id=@Email_Id";
                }
                else if (btnSave.Text == "Save")
                {
                    query = "INSERT INTO Patients(Name,Address,City,Zip,Contact_no,Email_id,Password,Active) VALUES(@Name,@Address,@City,@Zip,@Contact_no,@Email_id,@Password,@Active)";
                }
                SqlCommand cmd = new SqlCommand(query, conn);
                HttpCookie username = Request.Cookies["cUsername"];
                //@Name,@Address,@City,@Zip,@Contact_no,@Email_id,@Password
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@City", txtCity.Text.Trim());
                cmd.Parameters.AddWithValue("@Zip", txtZip.Text.Trim());
                cmd.Parameters.AddWithValue("@Contact_no", txtContactNo.Text.Trim());
                if (btnSave.Text == "Update")
                {
                    cmd.Parameters.AddWithValue("@Email_id", username.Value);
                }
                else if (btnSave.Text == "Save")
                {
                    cmd.Parameters.AddWithValue("@Email_id", txtEmailId.Text.Trim());
                }
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@Active", Convert.ToBoolean(ConfigurationManager.AppSettings["Active"].ToString()));

                cmd.ExecuteNonQuery();

                if (Usertype != null)
                {
                    if (Usertype.Value == "Admin")
                    {
                        Response.Redirect("~/Patient.aspx");
                        return;
                    }
                }

                if (Request.QueryString["action"] == "Register")
                {
                    Response.Redirect("~/Login.aspx");
                    return;
                }

                Response.Redirect("~/Patient.aspx?action=Profile");
            }
        }
        catch(Exception ex) 
        {
            lblMessage.Text = ex.Message;
        }
            
    }

    private bool CheckEmail(string email)
    {
        if (email == "")
        {
            lblMessage.Text = "Eamil cannot be empty";
            return false;
        }
        string connStr = ConfigurationManager.AppSettings["conn"].ToString();
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string query = "SELECT Email_id FROM Patients WHERE Email_Id=@Email_Id";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Email_Id", email);

            SqlDataReader dr = cmd.ExecuteReader();


            while (dr.Read())
            {
                lblMessage.Text = "Email exits.Select Other";
                return false;
            }

            dr.Close();

            return true;
        }

    }
}
