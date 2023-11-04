using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserSignup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Err(int ErrNum, string Msg)
    {
        litError.Text += "Error " + ErrNum.ToString() + " - " + Msg + "<br />";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtFirstName.Text == "") { Err(1051, "The First Name cannot be blank."); return; }
        if (txtLastName.Text == "") { Err(1051, "The Last Name cannot be blank."); return; }
        if (txtEmailAddress.Text == "") { Err(1051, "The Email cannot be blank."); return; }

        Guid obj = Guid.NewGuid();

        var Result = API.Session.CreateSignupUser(txtFirstName.Text + " " + txtLastName.Text,txtEmailAddress.Text, obj.ToString());

        API.Session.SendEmailCreateSignupUser(obj);

        Response.Redirect("~/Admin/");

    }
}