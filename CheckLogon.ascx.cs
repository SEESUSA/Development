using System;

public partial class CheckLogon : System.Web.UI.UserControl
{

    /// <summary>
    /// This code is included on every .aspx page to ensure that the
    /// user is actually logged on before loading the page.  If not,
    /// the user is redirected to the logon page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!API.Session.LoggedOn) Response.Redirect("./Logon.aspx");
    }
}