using System;

public partial class Admin_CheckAdmin : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!API.Session.IsAdmin) { Response.Redirect("~/Default.aspx"); }
    }
}