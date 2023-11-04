using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddChild : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Events
        API.Session.AdminLoadChildError += Err;
        API.Session.AdminSaveMasterChildError += Err;
        API.Session.RegisterUserError += Err;
        API.Session.GetEmailAddressError += Err;
        btnCancel.Click += BtnCancel_Click;
        btnSave.Click += BtnSave_Click;
        if (API.Session.AdminMasterAccountID == 0) Response.Redirect("~/Admin/VIP.aspx");
        if (!IsPostBack)
        {
            if (Request.QueryString["ChildID"].ToString() != "") API.Session.AdminChildAccountID = int.Parse(Request.QueryString["ChildID"].ToString());
            LoadChild();
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (ValidateSave())
        {
            if (API.Session.AdminChildAccountID == 0 && API.Session.NPIAlreadyRegistered(txtNPI.Text))
            {
                string sEmail = API.Session.GetEmailAddressByNPI(txtNPI.Text);
                Err(1162, "That NPI Number is already registered to " + sEmail + " in the portal database.<br /><br />");
            }
            else
            {
                API.Session.AdminSaveMasterChild(API.Session.AdminMasterAccountID, API.Session.AdminChildAccountID, txtName.Text, txtNPI.Text, txtEmailAddress.Text);
                Response.Redirect("~/Admin/VIP.aspx");
            }
        }
    }
    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/VIP.aspx");
    }
    private bool ValidateSave()
    {
        bool bReturn = true;
        if (txtName.Text == "") { Err(1146, "Child Account Name cannot be blank.");bReturn = false; }
        if (txtNPI.Text == "") { Err(1147, "Child Account NPI cannot be blank."); bReturn = false; }
        return bReturn;
    }
    protected void Err(int ErrNum, string Msg)
    {
        litError.Text += "Error " + ErrNum.ToString() + " - " + Msg + "<br />";
    }
    private void LoadChild()
    {
        if (API.Session.AdminChildAccountID > 0)
        {
            API.ChildAcct oChild = API.Session.AdminLoadChild();
            txtName.Text = oChild.Name;
            txtNPI.Text = oChild.NPI;
            txtEmailAddress.Text = oChild.EmailAddress;
        }
    }

}