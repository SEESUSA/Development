using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangePassword : System.Web.UI.Page
{
    /// <summary>
    /// This page allows the user to change their password in the ehportal database.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        SetupTasks();
        API.Session.ValidateCurrentPasswordError += Err;
        litError.Text = "";
        if (!IsPostBack)
        {

        }
    }
    protected void SetupTasks()
    {
        // swaps between normal tasks or admin tasks based on the user type

        // <uc1:CheckLogon ID="CheckLogon1" runat="server" />
        //if (API.Session.IsAdmin)
        //{
        //    pnlAdminTasks.Visible = true; pnlTasks.Visible = false;
        //}
        //else
        //{
        //    pnlAdminTasks.Visible = false; pnlTasks.Visible = true;
        //}
    }
    protected void btnChange_Click(object sender, EventArgs e)
    {
        if (txtCPassword.Text == "" || txtPass1.Text == "" || txtPass2.Text == "") { Err(1088, "None of the fields can be blank."); return; }
        if (!API.Session.ValidateCurrentPassword(txtCPassword.Text)) { Err(1082, "Your current password is incorrect."); return; }
        if (txtPass1.Text != txtPass2.Text) { Err(1080, "Your passwords do not match."); return; }
        if (txtPass1.Text.IndexOfAny(Statics.caDouble)== -1) { Err(1083, "Your password must have at least one (1) number it int."); return; }
        if (txtPass1.Text.IndexOfAny(Statics.caUpperAlpha) == -1) { Err(1084, "Your password must have at least one (1) upper case letter in it."); return; }
        if (txtPass1.Text.IndexOfAny(Statics.caLowerAlpha) == -1) { Err(1085, "Your password must have at least one (1) lower case letter in it."); return; }
        if (txtPass1.Text.IndexOfAny(Statics.caSymbol) == -1) { Err(1086, "Your password must have at least one (1) symbol in it."); return; }
        if (txtPass1.Text.Length < Statics.MinPwdLength) { Err(1087, "Your password must have at least " + Statics.MinPwdLength.ToString() + " characters in it."); return; }
        if (API.Session.ResetPassword( txtPass1.Text))//API.Session.GUID,
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(),
            //"alert",
            //"alert('Password changed successfully');window.location ='Logoff.aspx';",
            //true);

            ScriptManager.RegisterStartupScript(this,this.GetType(), "ShowModalScript", "$('#myModal').css('display', 'block');", true);
            //  ClientScript.RegisterStartupScript(this.GetType(), "showModal", script, true);
            

            //if (API.Session.IsAdmin) Response.Redirect("~/Admin/");
            //else Response.Redirect("~/");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (API.Session.IsAdmin) Response.Redirect("~/Admin/");
        else Response.Redirect("~/");    
    }
    protected void Err(int num, string msg)
    {
        litError.Text += "Error " + num.ToString() + " - " + msg + "<br />";
    }
}