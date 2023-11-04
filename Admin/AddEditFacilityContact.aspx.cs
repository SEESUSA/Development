using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddEditFacilityContact : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.GetFacilityContactDetailsError += Err;
        API.Session.AddUpdateFacilityContactError += Err;
        litError.Text = "";
        if (Request.Params.Count > 0)
        {
            API.Session.AdminEHPFacilityContactID = int.Parse(Request.Params.Get("ID").ToString());
        }
        if (!IsPostBack)
        {
            if (API.Session.AdminEHPFacilityContactID > 0) LoadData();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtName.Text == "") { Err(1090, "Name cannot be blank."); return; }
        if (txtEmailAddress.Text == "") { Err(1091, "EmailAddress cannot be blank."); return; }
        API.Session.AddUpdateFacilityContact(API.Session.AdminEHPFacilityContactID, API.Session.AdminEHPFacilityID, txtName.Text, txtEmailAddress.Text);
        Response.Redirect("~/Admin/AddEditFacility.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/AddEditFacility.aspx");
    }
    protected void Err(int num, string msg)
    {
        litError.Text += "Error " + num.ToString() + " - " + msg + "<br />";
    }
    protected void LoadData()
    {
        string data = API.Session.GetFacilityContactDetails(API.Session.AdminEHPFacilityContactID);
        if(data != "")
        {
            string[] items = data.Split(new char[] { '^' });
            if(items!= null)
            {
                txtName.Text = items[0];
                txtEmailAddress.Text = items[1];
            }
        }
    }
}