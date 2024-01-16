using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


/// <summary>
/// This page is used to reset a user password
/// </summary>

public partial class ResetPassword : System.Web.UI.Page
{
    Guid guid = Guid.Empty;
    string email = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.ResetPasswordError += Err;
        litError.Text = "";
        if(Request.Params.Count > 0)
        {
           // guid = new Guid(Request.Params.Get("g").ToString());
            email = Request.Params.Get("email").ToString();
            email = API.Session.Decrypt(email.ToString().Trim());
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("./");
    }
    protected void btnLogon_Click(object sender, EventArgs e)
    {
          Response.Redirect("./");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (ValidatePassword())
        {
            if (email!=string.Empty)//(guid != Guid.Empty)
            {
                API.Session.Email = email;
                if (API.Session.ResetPassword(txtPassword1.Text))
                {
                    pnlComplete.Visible = true;
                    pnlStart.Visible = false;
                }
            }
            //else
            //{
            //    if(API.Session.ResetPasswordForgot( txtPassword1.Text, email))
            //    {
            //        pnlComplete.Visible = true;
            //        pnlStart.Visible = false;
            //    }
            //}
        }
    }
    protected void Err(int num, string msg)
    {
        litError.Text += "Error " + msg + "<br />";//+ num.ToString() + " - " 
    }
    protected bool ValidatePassword()
    {
        if (txtPassword1.Text != txtPassword2.Text) { Err(1062,"Passwords do not match each other."); return false; }
        if (txtPassword1.Text.IndexOfAny(Statics.caDouble) == -1) { Err(1063,"Password must have at least one (1) number."); return false; }
        if (txtPassword1.Text.IndexOfAny(Statics.caUpperAlpha) == -1) { Err(1064,"Password must have at least one (1) upper case letter."); return false; }
        if (txtPassword1.Text.IndexOfAny(Statics.caLowerAlpha) == -1) { Err(1065,"Password must have at least one (1) lower case letter."); return false; }
        if (txtPassword1.Text.IndexOfAny(Statics.caSymbol) == -1) { Err(1066,"Password must have at least one (1) symbol."); return false; }
        return true;
    }
}