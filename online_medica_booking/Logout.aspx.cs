using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpCookie username = Request.Cookies["cUsername"];
            username.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(username);

            HttpCookie last_login = Request.Cookies["cLast_login"];
            last_login.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(last_login);

            HttpCookie Usertype = Request.Cookies["cUsertype"];
            Usertype.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(Usertype);
        }
        catch
        {

        }

        Response.Redirect("~/Login.aspx");
    }
}
