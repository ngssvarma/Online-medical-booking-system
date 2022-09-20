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

public partial class Admin_Doctor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            HttpCookie username = Request.Cookies["cUsername"];
            if (username == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (Request.QueryString["action"] == "Add")
            {
                AddDoctor();
            }
            else if (Request.QueryString["action"] == "Profile")
            {
                ViewDoctor();
            }
            else if (Request.QueryString["action"] == "Update")
            {
                EditDoctor();
            }
            else
            {
                GetDoctorList();
            }
        }
    }

    private void EditDoctor()
    {
        btnSave.Text = "Update";

        DoctorGrid.Visible = false;

        GetDoctorProfile();

        txtEmailId.Enabled = false;
        txtEmailId.CssClass = "disenable";

        trDoctorHeader.InnerHtml = "Edit Profile Information";        
    }
    

    private void ViewDoctor()
    {
        btnSave.Text = "Edit";

        DoctorGrid.Visible = false;

        GetDoctorProfile();

        trDoctorHeader.InnerHtml = "Profile Information";

        txtName.CssClass = "disenable";
        txtName.Enabled = false;

        txtDegree.Enabled = false;
        txtDegree.CssClass = "disenable";

        txtAvailableTime.Enabled = false;
        txtAvailableTime.CssClass = "disenable";

        txtEmailId.Enabled = false;
        txtEmailId.CssClass = "disenable";

        txtPassword.Enabled = false;
        txtPassword.CssClass = "disenable";

        txtConfirmPassword.Enabled = false;
        txtConfirmPassword.CssClass = "disenable";

        chkSunday.Enabled = false;
        chkMonday.Enabled = false;
        chkTuesday.Enabled = false;
        chkWednesday.Enabled = false;
        chkThursday.Enabled = false;
        chkFriday.Enabled = false;
        chkSaturday.Enabled = false;
    }

    private void GetDoctorProfile()
    {
        string connStr = ConfigurationManager.AppSettings["conn"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            HttpCookie username = Request.Cookies["cUsername"];
            string query = "";

            if (Request.QueryString["DocID"] != null)
            {
                query = "SELECT Name,Degree,Available_Days,Available_time,Email_id,Password FROM Doctor WHERE Id=" + Request.QueryString["DocID"] + "";
            }
            else
            {
                query = "SELECT Name,Degree,Available_Days,Available_time,Email_id,Password FROM Doctor WHERE Email_Id=@Email_Id";
            }

            SqlCommand cmd = new SqlCommand(query, conn);

            if (Request.QueryString["DocID"] != null)
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
                txtDegree.Text = dr.GetString(1);
                string[] days = dr.GetString(2).ToString().Split(new char[] {','});
                foreach (string day in days)
                {
                    if (day == "")
                        return;

                    switch (day)
                    {
                        case "Sunday":
                            chkSunday.Checked = true;
                            chkSunday.CssClass = "bold";
                            break;

                        case "Monday":
                            chkMonday.Checked = true;
                            chkMonday.CssClass = "bold";
                            break;

                        case "Tuesday":
                            chkTuesday.Checked = true;
                            chkTuesday.CssClass = "bold";
                            break;

                        case "Wednesday":
                            chkWednesday.Checked = true;
                            chkWednesday.CssClass = "bold";
                            break;

                        case "Thursday":
                            chkThursday.Checked = true;
                            chkThursday.CssClass = "bold";
                            break;

                        case "Friday":
                            chkFriday.Checked = true;
                            chkFriday.CssClass = "bold";
                            break;

                        case "Saturday":
                            chkSaturday.Checked = true;
                            chkSaturday.CssClass = "bold";
                            break;
                    }
                }

                txtAvailableTime.Text = String.Format("{0:hh:mm tt}", dr.GetDateTime(3));
                txtEmailId.Text = dr.GetString(4);
                txtPassword.Text=dr.GetString(5);
                txtConfirmPassword.Text = dr.GetString(5);
            }
        }
    }

    private void AddDoctor()
    {
        //txtDate.Text = DateTime.Now.ToShortDateString() + " ";
        DoctorGrid.Visible = false;
    }

    private void GetDoctorList()
    {
        AddNewDoctor.Visible = false;

        string strhtml = "";

        //strhtml += "<table>";

        bool found = false;

        string connStr = ConfigurationManager.AppSettings["conn"].ToString();

        string straction = "<a href='Doctor.aspx?DocID=@&action=Update'>Edit</a>|<a href='Doctor.aspx?DocID=@&action=Profile'>View</a>";
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string query = "SELECT ID,Name,Degree,Available_Days,Available_time,Active FROM Doctor";

            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                found = true;

                strhtml += "<tr>";

                //Name,Degree,Available_Days,Available_time,Active
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
                strhtml += String.Format("{0:hh:mm tt}", dr.GetDateTime(4));
                strhtml += "</td>";

                strhtml += "<td>";
                strhtml += straction.Replace("@", dr.GetInt32(0).ToString());
                strhtml += "</td>";

                strhtml += "</tr>";
            }
        }

        //strhtml += "</table>";

        if (!found)
        {
            strhtml = "<tr><th colspan='100'>No Doctor Found</th></tr>";
        }

        trDoctorGrid.InnerHtml = strhtml.ToString();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {


        HttpCookie Usertype = Request.Cookies["cUsertype"];

        if (btnSave.Text == "Edit")
        {
            btnSave.Text = "Update";
            if (Usertype.Value == "Admin")
            {
                Response.Redirect("~/Doctor.aspx?DocID="+Request.QueryString["DocID"]+"&action=Update");
            }
            if (Usertype.Value == "Doctor")
            {
                Response.Redirect("~/Doctor.aspx?action=Update");
            }
            return;
        }

        try
        {
            if (btnSave.Text != "Update")
            {
                if (!CheckEmail(txtEmailId.Text.Trim()))
                {
                    //lblMessage.Text = "Email exits.Select Other";
                    return;
                }
            }

            if (txtName.Text.Trim() == "")
            {
                lblMessage.Text = "Please enter Name";
                return;
            }

            if (txtDegree.Text.Trim() == "")
            {
                lblMessage.Text = "Please enter Doctor degree";
                return;
            }

            if (txtPassword.Text.Trim() == "")
            {
                lblMessage.Text = "Please enter password to continue";
                return;
            }

            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                lblMessage.Text = "Password do not match";
                return;
            }
            string strAvailable_days = "";

            int count = 0;


            if (chkSunday.Checked)
            {
                count++;
                strAvailable_days += "," + chkSunday.Text;
            }

            if (chkMonday.Checked)
            {
                count++;
                strAvailable_days += "," + chkMonday.Text;
            }


            if (chkTuesday.Checked)
            {
                count++;
                strAvailable_days += "," + chkTuesday.Text;
            }


            if (chkWednesday.Checked)
            {
                count++;
                strAvailable_days += "," + chkWednesday.Text;
            }


            if (chkThursday.Checked)
            {
                count++;
                strAvailable_days += "," + chkThursday.Text;
            }

            if (chkFriday.Checked)
            {
                count++;
                strAvailable_days += "," + chkFriday.Text;
            }

            if (chkSaturday.Checked)
            {
                count++;
                strAvailable_days += "," + chkSaturday.Text;
            }


            if (count == 0)
            {
                lblMessage.Text = "Please select atleast one day";
                return;
            }

            if (txtAvailableTime.Text.Trim() == "")
            {
                lblMessage.Text = "Please enter AvailableTime";
                return;
            }

            try
            {
                String.Format("{0:hh:mm tt}", DateTime.Parse(DateTime.Now.ToShortDateString() + " " + txtAvailableTime.Text));
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Available Time must in hh:mm am/pm format.\n" + ex.Message;
                return;
            }



            string connStr = ConfigurationManager.AppSettings["conn"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "";

                if (btnSave.Text == "Update")
                {
                    query = "UPDATE Doctor SET Name=@Name,Degree=@Degree,Available_days=@Available_days,Available_time=@Available_time,Password=@Password WHERE  Email_Id=@Email_Id";
                }
                else if (btnSave.Text == "Save")
                {
                    query = "INSERT INTO Doctor(Name,Degree,Available_days,Available_time,Email_id,Password,Active) VALUES(@Name,@Degree,@Available_days,@Available_time,@Email_id,@Password,@Active)";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                HttpCookie username = Request.Cookies["cUsername"];
                //@Name,@Degree,@Available_days,@Available_time,@Email_id,@Password,@Active
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Degree", txtDegree.Text.Trim());
                cmd.Parameters.AddWithValue("@Available_days", strAvailable_days.Substring(1));
                cmd.Parameters.AddWithValue("@Available_time", DateTime.Parse(DateTime.Now.ToShortDateString() + " " + txtAvailableTime.Text.Trim()));
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
            }
            if (Usertype.Value == "Admin")
            {
                Response.Redirect("~/Doctor.aspx");
                return;
            }

            Response.Redirect("~/Doctor.aspx?action=Profile");
        }
        catch (Exception ex)
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

            string query = "SELECT Email_id FROM Doctor WHERE Email_Id=@Email_Id";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Email_Id", email);

            SqlDataReader dr=cmd.ExecuteReader();

            
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

