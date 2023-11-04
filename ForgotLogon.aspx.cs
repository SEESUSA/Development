using System;

public partial class ForgotLogon : System.Web.UI.Page
{

    /// <summary>
    /// This is the typical I forgot my logon credentials page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.ResetPasswordError += Err;
        API.Session.GetGUIDError += Err;
        litError.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/");
    }
    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        if (txtEmailAddress.Text == "") { Err(1055,"Email Address cannot be blank."); return; }
        Guid guid = API.Session.GetGUID(txtEmailAddress.Text);
        if(guid != Guid.Empty) Response.Redirect("./ResendValidation.aspx?t=pwd&g=" + guid.ToString() + "&email=" + txtEmailAddress.Text.ToString());
        else Err(1056,"There was a problem resetting your password.  Please verify your email address and try again.");
    }
    protected bool ValidateData()
    {
        if (txtEmailAddress.Text == "") { Err(1057,"Email Address cannot be blank."); return false; }
        return true;
    }
    protected void Err(int num, string msg)
    {
        litError.Text += "Error " + num.ToString() + " - " + msg + "<br />";
    }
}