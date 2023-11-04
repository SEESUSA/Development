using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddEditApptType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.EHPApptType_GetDetailsError += Err;
        API.Session.EHPApptType_SaveError += Err;

        litError.Text = "";
        if (!IsPostBack)
        {
            ShowData();
        }
        txtEHPName.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtEHPName.Text != "")
        {
            API.EHPApptType data = new API.EHPApptType();
            data.DoctorID = API.Session.AdminEHPDoctorID;
            data.Enabled = chkEnabled.Checked;
            data.ID = API.Session.AdminEHPApptTypeID;
            data.Name = txtEHPName.Text;
            data.Save();
            Response.Redirect("~/Admin/ApptTypes.aspx");
        }
        else
        {
            Err(1046, "Appt Type name cannot be blank.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/ApptTypes.aspx");
    }
    protected void Err(int ErrNum, string Msg)
    {
        litError.Text += "Error " + ErrNum.ToString() + " - " + Msg + "<br />";
    }
    protected void ShowData()
    {
        if (API.Session.AdminEHPApptTypeID > 0)
        {
            API.EHPApptType data = new API.EHPApptType(API.Session.AdminEHPApptTypeID);
            if (data != null)
            {
                txtEHPName.Text = data.Name;
                chkEnabled.Checked = data.Enabled;
            }
        }
    }
}