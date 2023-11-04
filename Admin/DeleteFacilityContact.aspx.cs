using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DeleteFacilityContact : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        litError.Text = "";
        if (Request.Params.Count > 0)
        {
            API.Session.AdminEHPFacilityContactID = int.Parse(Request.Params.Get("ID").ToString());
        }
        if (!IsPostBack)
        {
            DeleteContact();
        }
    }
    protected void DeleteContact()
    {
        if (API.Session.AdminEHPFacilityContactID > 0)
        {
            API.Session.DeleteFacilityContact(API.Session.AdminEHPFacilityContactID);
            Response.Redirect("~/Admin/AddEditFacility.aspx");
        }
    }
    protected void Err(int num, string msg)
    {
        litError.Text += "Error " + num.ToString() + " - " + msg + "<br />";
    }
}