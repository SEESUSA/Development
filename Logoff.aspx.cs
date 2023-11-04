using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logoff : System.Web.UI.Page
{

    /// <summary>
    /// Abandons the user's session and redirects to the starting point.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("./");
    }
}