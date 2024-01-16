using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ResendValidation : System.Web.UI.Page
{

    /// <summary>
    /// This page is used to resend the validation email to the user
    /// </summary>

    string type = "";
    Guid guid = Guid.Empty;
    string email = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        litError.Text = "";
        API.Session.SendEmailError += Err;
        API.Session.GetEmailAddressError += Err;

        if (Request.Params.Count > 0)
        {
            type = Request.Params.Get("t").ToString();
            guid = new Guid(Request.Params.Get("g").ToString());
            email = Request.Params.Get("email").ToString();
        }
        if (type == "pwd" && guid != Guid.Empty && email != "") SendPwdReset();
        else
        {
            txtEmailAddress.Text = API.Session.GetEmailAddress(guid);
            pnlPwdReset.Visible = false;
            pnlValidation.Visible = true;
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (txtEmailAddress.Text == "") { Err(1061, "Email Address cannot be blank."); return; }
        API.Session.SendEmail(API.EmailType.Validate, guid);
        pnlValidateResent.Visible = true;
        pnlValidation.Visible = false;
        pnlPwdReset.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("./");
    }
    protected void SendPwdReset()
    {
        API.Session.SendEmailForgotPassword(API.EmailType.PwdReset,guid, email);
        pnlValidation.Visible = false;
        pnlPwdReset.Visible = true;
    }
    private void Err(int num, string msg)
    {
        litError.Text += "Error " + msg + "<br />";// + num.ToString() + " - "
    }
}