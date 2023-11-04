using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// This is the page that the self-registered user comes to when they click
/// the validate email link.  This page validates their account on load and
/// then prompts them to change their password on the system.
/// </summary>

public partial class Validate : System.Web.UI.Page
{
    System.Guid guid = System.Guid.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        litError.Text = "";
        API.Session.UpdateDatabaseError += Err;
        API.Session.ValidateEmailError += Err;
        API.Session.SetInitialPasswordError += Err;
        API.Session.IsAlreadyValidatedError += Err;

        if(Request.Params.Count>0)
        {
            Guid guid;
            guid = new Guid(Request.Params.Get("g").ToString());
            if (API.Session.IsAlreadyValidated(guid)) // make sure the account is not already validated
            {
                pnlAlreadyValidated.Visible = true;
                pnlComplete.Visible = false;
                pnlInvalid.Visible = false;
                pnlValidated.Visible = false;
            }
            else
            {
                if (API.Session.ValidateEmail(guid))
                {
                    pnlInvalid.Visible = false;
                    pnlValidated.Visible = true;
                    pnlComplete.Visible = false;
                    pnlAlreadyValidated.Visible = false;
                }
                else
                {
                    pnlInvalid.Visible = true;
                    pnlValidated.Visible = false;
                    pnlComplete.Visible = false;
                    pnlAlreadyValidated.Visible = false;
                }
            }
        }
    }
    protected void btnChange_Click(object sender, EventArgs e)
    {
        if (ValidatePasswords())
        {
            if (API.Session.SetInitialPassword(API.Session.GUID, txtPassword1.Text))
            {
                pnlComplete.Visible = true;
                pnlInvalid.Visible = false;
                pnlValidated.Visible = false;
                pnlAlreadyValidated.Visible = false;
            }
            else
            {
                pnlComplete.Visible = false;
                pnlInvalid.Visible = false;
                pnlValidated.Visible = true;
                pnlAlreadyValidated.Visible = false;
            }
        }
        else
        {
            pnlComplete.Visible = false;
            pnlInvalid.Visible = false;
            pnlValidated.Visible = true;
            pnlAlreadyValidated.Visible = false;
        }
    }
    protected void btnLogon_Click(object sender, EventArgs e)
    {
        Response.Redirect("./Logon.aspx");
    }
    protected void lbLogon_Click(object sender, EventArgs e)
    {
        Response.Redirect("./");
    }
    protected void lbResend_Click(object sender, EventArgs e)
    {
        Response.Redirect("./ResendValidation.aspx");
    }
    protected void lbSignup_Click(object sender, EventArgs e)
    {
        Response.Redirect("./Signup.aspx");
    }
    protected void Err(int num, string msg)
    {
        litError.Text += "Error " + num.ToString() + " - " + msg + "<br />";
    }
    protected bool ValidatePasswords()
    {
        if (txtPassword1.Text != txtPassword2.Text) { Err(1074,"Your passwords do not match."); return false; }
        if (txtPassword1.Text.IndexOfAny(Statics.caUpperAlpha) == -1) { Err(1075,"Your password must contain at least one (1) upper case letter."); return false; }
        if (txtPassword1.Text.IndexOfAny(Statics.caLowerAlpha) == -1) { Err(1076,"Your password must contain at least one (1) lower case letter."); return false; }
        if (txtPassword1.Text.IndexOfAny(Statics.caDouble) == -1) { Err(1077,"Your password must contain at leaset one (1) number."); return false; }
        if (txtPassword1.Text.IndexOfAny(Statics.caSymbol) == -1) { Err(1078,"Your password must contain at least one (1) symbol."); return false; }
        if (txtPassword1.Text.Length < Statics.MinPwdLength) { Err(1079,"Your password must be at least " + Statics.MinPwdLength.ToString() + " characters."); return false; }
        return true;
    }
}