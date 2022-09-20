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

public partial class Admin_Admin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie username = Request.Cookies["cUsername"];        
        if (username == null)
        {
            

        }
        else
        {
            lblUsername.Text = username.Value;
        }

        HttpCookie last_login = Request.Cookies["cLast_login"];
        if (last_login == null)
        {
            lblLastAccess.Text = "first time login";
        }
        else
        {
            if (last_login.Value != null)
                lblLastAccess.Text = last_login.Value;
            else
                lblLastAccess.Text = "first time login";
        }

        HttpCookie Usertype = Request.Cookies["cUsertype"];
        if (Usertype == null)
        {
            menuAdmin.InnerHtml = "";
            //menuAdmin.Visible = false;
            //menuDoctor.Visible = false;
            //menuPatients.Visible = false;
            tdStatus.Visible = false;
        }
        else
        {
            if (Usertype.Value == "Admin")
            {
                menuAdmin.InnerHtml = "<a href='Default.aspx'>Home</a> | <a href='Doctor.aspx'>Doctor</a> | <a href='Patient.aspx'> Patients</a> |<a href='appointment.aspx'> Appointment</a> |<a href='changepassword.aspx'> Change Password</a> |<a href='Logout.aspx'> Logout</a>";
                //menuAdmin.Visible = true;
                //menuDoctor.Visible = false;
                //menuPatients.Visible = false;
            }
            else if (Usertype.Value == "Doctor")
            {
                menuAdmin.InnerHtml = "<a href='Default.aspx'>Home</a> |<a href='appointment.aspx'> Appointment</a> |<a href='Patient.aspx'> Patients</a> | <a href='Doctor.aspx?action=Profile'>Profile</a>|<a href='changepassword.aspx'> Change Password</a> |<a href='Logout.aspx'> Logout</a>";

                //menuAdmin.Visible = false;
                //menuDoctor.Visible = true;
                //menuPatients.Visible = false;
            }
            else if (Usertype.Value == "Patients")
            {
                menuAdmin.InnerHtml = "<a href='Default.aspx'>Home</a> |<a href='appointment.aspx'> Appointment</a>  | <a href='Patient.aspx?action=Profile'>Profile</a>|<a href='changepassword.aspx'> Change Password</a> |<a href='Logout.aspx'> Logout</a>";

                //menuAdmin.Visible = false;
                //menuDoctor.Visible = false;
                //menuPatients.Visible = true;
            }
            else
            {
                menuAdmin.InnerHtml = "";
                //menuDoctor.Visible = false;
                //menuPatients.Visible = false;
            }

        }


    }
}
