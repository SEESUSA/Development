using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_TestEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    protected void btnSend_Click(object Sender, EventArgs e)
    {
        if (ValidateData())
        {
            API.Session.TestSendEmail(txtEmailAddress.Text);

        }
    }
    protected bool ValidateData()
    {
        if (txtEmailAddress.Text == "") return false;
        if (!txtEmailAddress.Text.Contains("@")) return false;
        return true;
    }
}