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

public partial class Admin_Appointment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        divAddRemarks.Visible=false;
        if (!IsPostBack)
        {
            HttpCookie Usertype = Request.Cookies["cUsertype"];
            if (Usertype.Value != "Admin")
            {
                //trforAdmin.Visible = false;
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["action"] == "Add")
                {
                    AddNewAppointment01();
                }
                else if (Request.QueryString["action"] == "Edit")
                {
                    EditAppointment(Request.QueryString["AppID"]);
                }
                else if (Request.QueryString["action"] == "AddRemarks")
                {
                    AddRemarks(Request.QueryString["AppID"]);
                }
                else if (Request.QueryString["action"] == "ViewRemarks")
                {
                    ViewRemarks(Request.QueryString["AppID"]);
                } 
                else
                {
                    AddNewAppointment.Visible = false;
                    ListOfAppointment();
                }
            }
        }
    }

    private void ViewRemarks(string p)
    {
        AppointmentGrid.Visible = false;
        AddNewAppointment.Visible = false;
        txtRemarksDate.Text = DateTime.Now.ToShortDateString();
        divAddRemarks.Visible = true;

        try
        {
            string connStr = ConfigurationManager.AppSettings["conn"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT Doc_remarks FROM Appointment WHERE ID='" + Request.QueryString["AppID"] + "'";
                SqlCommand cmd = new SqlCommand(query, conn);               

                SqlDataReader dr= cmd.ExecuteReader();

                while (dr.Read())
                {
                    try
                    {
                        txtRemarks.Text = dr.GetString(0);
                    }
                    catch
                    {
                        txtRemarks.Text = "";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }


    }

    private void AddRemarks(string AppID)
    {
        AppointmentGrid.Visible = false;
        AddNewAppointment.Visible = false;
        txtRemarksDate.Text = DateTime.Now.ToShortDateString();
        divAddRemarks.Visible = true;
    }

    private void EditAppointment(string AppID)
    {
        GetDataofAppointment(AppID);
        txtDate.Enabled = false;
        txtDate.BorderStyle = BorderStyle.None;

        txtTime.Enabled = false;
        txtTime.BorderStyle = BorderStyle.None;

        txtStatus.Enabled = false;
        txtStatus.BorderStyle = BorderStyle.None;

        ddlDoctor.Enabled = false;
        ddlDoctor.BorderStyle = BorderStyle.None;

        ddlPatients.Enabled = false;
        ddlPatients.BorderStyle = BorderStyle.None;
    }

    private void GetDataofAppointment(string AppID)
    {
        throw new NotImplementedException();
    }

    private void ListOfAppointment()
    {
        GetListOfDoctor();
        GetListOfPatients();

        string strhtml = "";

        //strhtml += "<table>";

        bool found = false;

        string straction = "<a href='Appointment.aspx?AppID=@&action=AddRemarks'>Add Remarks</a>|<a href='Appointment.aspx?AppID=@&action=ViewRemarks'>View Remarks</a>";        
        
        string connStr = ConfigurationManager.AppSettings["conn"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string query="";
            HttpCookie Usertype = Request.Cookies["cUsertype"];
            if (Usertype.Value == "Admin")
            {
                query = "SELECT * FROM Appointment_Views ORDER BY [Date],[Time]";
            }
            if (Usertype.Value == "Doctor")
            {
                query = "SELECT * FROM Appointment_Views WHERE Doc_id ='" + Request.Cookies["cID"].Value + "' ORDER BY [Date],[Time]";
            }
            if (Usertype.Value == "Patients")
            {
                query = "SELECT * FROM Appointment_Views WHERE Pat_id ='" + Request.Cookies["cID"].Value + "' ORDER BY [Date],[Time]";
            }

            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();

            int count = 0;

            HttpCookie usertype = new HttpCookie("cUsertype");
            if (Usertype.Value != "Doctor")
            {
                thDoctorRemarks.Visible = false;
            }

            while (dr.Read())
            {
                count++;
                found = true;

                strhtml += "<tr>";

                //App_Date,Time,Doc_Id,Pat_id,Status
                strhtml += "<td>";
                strhtml += count;
                strhtml += "</td>";

                strhtml += "<td>";
                strhtml += String.Format("{0:dd-MM-yyyy}",dr.GetDateTime(0));
                strhtml += "</td>";

                strhtml += "<td>";
                strhtml += String.Format("{0:hh:mm tt}", dr.GetDateTime(1));
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

                if (Usertype.Value == "Doctor")
                {
                    strhtml += "<td>";
                    strhtml += straction.Replace("@",dr.GetInt32(7).ToString());
                    strhtml += "</td>";
                }
                


                strhtml += "</tr>";
            }
        }

        //strhtml += "</table>";

        if (!found)
        {
            strhtml = "<tr><th colspan='100'>No Appointment Found</th></tr>";
        }

        trAppointmentGrid.InnerHtml = strhtml.ToString();


    }

    private void AddNewAppointment01()
    {
        AppointmentGrid.Visible = false;
        AddNewAppointment.Visible = true;
        GetListOfDoctor();
        GetListOfPatients();
    }

    private void GetListOfPatients()
    {
        string connStr = ConfigurationManager.AppSettings["conn"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
            try
            {
                conn.Open();
                string query = "";
                HttpCookie Usertype = Request.Cookies["cUsertype"];
                if (Usertype.Value == "Patients")
                {
                    query = "SELECT ID, Name FROM Patients WHERE ID=" + Request.Cookies["cID"].Value + ";";
                }
                else
                {
                    query = "SELECT ID, Name FROM Patients ";
                }

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                if (Request.QueryString["action"] == "Add")
                {
                    ddlPatients.DataSource = dr;
                    ddlPatients.Items.Clear();
                    ddlPatients.DataTextField = "Name";
                    ddlPatients.DataValueField = "ID";
                    ddlPatients.DataBind();
                }
                else
                {
                    ddlSearchPatients.DataSource = dr;
                    ddlSearchPatients.Items.Clear();
                    ddlSearchPatients.DataTextField = "Name";
                    ddlSearchPatients.DataValueField = "ID";
                    ddlSearchPatients.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }

    }

    private void GetListOfDoctor()
    {
        string connStr = ConfigurationManager.AppSettings["conn"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            try
            {
                conn.Open();
                string query = "";

                HttpCookie Usertype = Request.Cookies["cUsertype"];
                if (Usertype.Value == "Doctor")
                {
                    query = "SELECT ID, Name FROM Doctor WHERE ID=" + Request.Cookies["cID"].Value + ";";
                }
                else
                {
                    query = "SELECT ID, Name FROM Doctor";
                }

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                if (Request.QueryString["action"] == "Add")
                {
                    ddlDoctor.DataSource = dr;
                    ddlDoctor.Items.Clear();
                    ddlDoctor.DataTextField = "Name";
                    ddlDoctor.DataValueField = "ID";
                    ddlDoctor.DataBind();
                }
                else
                {
                    ddlSearchDoctor.DataSource = dr;
                    ddlSearchDoctor.Items.Clear();
                    ddlSearchDoctor.DataTextField = "Name";
                    ddlSearchDoctor.DataValueField = "ID";
                    ddlSearchDoctor.DataBind();
                }


                dr.Close();

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    protected void btnPatientRegister_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime.Parse(txtDate.Text);
        }
        catch
        {
            lblMessage.Text = "Input Date is not Valid";
            return;
        }

        if (txtTime.Text.Trim() == "")
        {
            lblMessage.Text = "Please Enter the time in hh:mm am/pm format";
            return;
        }

        try
        {
            String.Format("{0:hh:mm tt}", DateTime.Parse(txtDate.Text).ToShortDateString() + " " + txtTime.Text);
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Appintment Time must in hh:mm am/pm format.\n" + ex.Message;
            return;
        }

        if (ddlDoctor.SelectedValue == "")
        {
            lblMessage.Text = "Doctor is not present";
            return;
        }

        if (ddlPatients.SelectedValue == "")
        {
            lblMessage.Text = "Patients is not present";
            return;
        }

        if (btnAddAppointment.Text == "Add")
        {
            try
            {
                string connStr = ConfigurationManager.AppSettings["conn"].ToString();
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "INSERT INTO Appointment(Date,Time,Doc_id,Pat_id,Status) VALUES(@Date,@Time,@Doc_id,@Pat_id,@Status)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    //@Date,@Time,@Doc_id,@Pat_id,@Status,@Doc_remarks,@Doc_entry_time
                    cmd.Parameters.AddWithValue("@Date", txtDate.Text.Trim());
                    cmd.Parameters.AddWithValue("@Time", txtTime.Text.Trim());
                    cmd.Parameters.AddWithValue("@Doc_id", ddlDoctor.SelectedValue);
                    cmd.Parameters.AddWithValue("@Pat_id", ddlPatients.SelectedValue);
                    cmd.Parameters.AddWithValue("@Status", txtStatus.Text.Trim());

                    cmd.ExecuteNonQuery();

                    Response.Redirect("~/Appointment.aspx");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        else if (btnAddAppointment.Text == "Edit")
        {
            
        }
        else if (btnAddAppointment.Text == "Save")
        { }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strhtml = "";

        //strhtml += "<table>";

        bool found = false;

       
        string connStr = ConfigurationManager.AppSettings["conn"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string query = "";
            HttpCookie Usertype = Request.Cookies["cUsertype"];

            string strorderby = " ORDER BY [Date],[Time]";

            if (Usertype.Value == "Admin")
            {
                query = "SELECT * FROM Appointment_Views ";
            }
            if (Usertype.Value == "Doctor")
            {
                query = "SELECT * FROM Appointment_Views WHERE Doc_id ='" + Request.Cookies["cID"].Value + "'";
            }
            if (Usertype.Value == "Patients")
            {
                query = "SELECT * FROM Appointment_Views WHERE Pat_id ='" + Request.Cookies["cID"].Value + "'";
            }

            if (ddlSearchBy.SelectedValue == "Doctor")
            {
                if (Usertype.Value == "Admin")
                {
                    query += " WHERE Doc_ID='" + ddlSearchDoctor.SelectedValue + "'";
                }
                else
                {
                    query += " AND Doc_ID='" + ddlSearchDoctor.SelectedValue + "'";
                }
            }

            else if (ddlSearchBy.SelectedValue == "Patient")
            {
                if (Usertype.Value == "Admin")
                {
                    query += " WHERE Pat_ID='" + ddlSearchPatients.SelectedValue + "'";
                }
                else
                {
                    query += " AND Pat_ID='" + ddlSearchPatients.SelectedValue + "'";
                }
            }

            else
            {
                if (Usertype.Value == "Admin")
                {
                    query += " WHERE Doc_ID='" + ddlSearchDoctor.SelectedValue + "'";
                }
                else
                {
                    query += " AND Doc_ID='" + ddlSearchDoctor.SelectedValue + "'";
                }
                query += " AND Pat_ID='" + ddlSearchPatients.SelectedValue + "'";
            }

            query += strorderby;
            
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();

            int count = 0;

            while (dr.Read())
            {
                count++;
                found = true;

                strhtml += "<tr>";

                //App_Date,Time,Doc_Id,Pat_id,Status
                strhtml += "<td>";
                strhtml += count;
                strhtml += "</td>";

                strhtml += "<td>";
                strhtml += String.Format("{0:dd-MM-yyyy}", dr.GetDateTime(0));
                strhtml += "</td>";

                strhtml += "<td>";
                strhtml += String.Format("{0:hh:mm tt}", dr.GetDateTime(1));
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


                strhtml += "</tr>";
            }
        }

        //strhtml += "</table>";

        if (!found)
        {
            strhtml = "<tr><th colspan='100'>No Appointment Found</th></tr>";
        }

        trAppointmentGrid.InnerHtml = strhtml.ToString();
    }
    protected void btnAddRemarks_Click(object sender, EventArgs e)
    {
        if (txtRemarks.Text.Trim()=="")
        {
            lblMessage.Text = "Please Enter the remarks";
            return;
        }
        try
        {
            string connStr = ConfigurationManager.AppSettings["conn"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "UPDATE Appointment SET Doc_remarks=@Doc_remarks,Doc_entry_time=getdate() WHERE ID="+Request.QueryString["AppID"]+"";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Doc_remarks",txtRemarks.Text.Trim());
                cmd.Parameters.AddWithValue("@Doc_entry_time", txtRemarksDate.Text.Trim());

                cmd.ExecuteNonQuery();

                Response.Redirect("~/Appointment.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
}

     //private bool CheckEmail(string email)
    //{
    //    string connStr = ConfigurationManager.AppSettings["conn"].ToString();
    //    using (SqlConnection conn = new SqlConnection(connStr))
    //    {
    //        conn.Open();

    //        string query = "SELECT Email_id FROM Patients WHERE Email_Id=@Email_Id";

    //        SqlCommand cmd = new SqlCommand(query, conn);

    //        cmd.Parameters.AddWithValue("@Email_Id", email);

    //        SqlDataReader dr = cmd.ExecuteReader();


    //        while (dr.Read())
    //        {
    //            return false;
    //        }

    //        dr.Close();

    //        return true;
    //    }

    

