using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ApptTypes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.EHPApptType_GetDetailsError += Err;
        API.Session.GetEHPApptTypeListError += Err;
        litError.Text = "";
        lbNew.Visible = true;
        if (!IsPostBack)
        {
            RefreshEHPApptTypes();
            if (API.Session.AdminEHPApptTypeID > 0)
            {
                SetApptType(API.Session.AdminEHPApptTypeID);
                ShowData();
            }
        }
    }
    protected void Err(int ErrNum, string Msg)
    {
        litError.Text += "Error " + ErrNum.ToString() + " - " + Msg + "<br />";
    }
    protected void cboEHPApptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbEdit.Visible = cboEHPApptType.SelectedIndex > 0;
        lbDelete.Visible= cboEHPApptType.SelectedIndex > 0;
        lbGEApptType.Visible= cboEHPApptType.SelectedIndex > 0;
        pnlApptTypeInfo.Visible= cboEHPApptType.SelectedIndex > 0;
        if (cboEHPApptType.SelectedIndex > 0)
        {
            API.Session.AdminEHPApptType = cboEHPApptType.SelectedItem.Text;
            API.Session.AdminEHPApptTypeID = int.Parse(cboEHPApptType.SelectedItem.Value);
            API.Session.AdminEHPCPSApptType = "";
            API.Session.AdminEHPCPSApptTypeID = 0;
            ShowData();
        }
        else
        {
            API.Session.AdminEHPApptType = "";
            API.Session.AdminEHPApptTypeID = 0;
            API.Session.AdminEHPCPSApptType = "";
            API.Session.AdminEHPCPSApptTypeID = 0;
        }
    }
    protected void lbNew_Click(object sender, EventArgs e)
    {
        API.Session.AdminEHPApptType = "";
        API.Session.AdminEHPApptTypeID = 0;
        Response.Redirect("~/Admin/AddEditApptType.aspx");
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/AddEditApptType.aspx");
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/DeleteApptType.aspx");
    }
    protected void lbGEApptType_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/EHPCPSApptTypes.aspx");
    }
    protected void RefreshEHPApptTypes()
    {
        cboEHPApptType.Items.Clear();
        cboEHPApptType.Items.Add(new ListItem("Select Appt Type", "0"));
        if (API.Session.AdminEHPDoctorID > 0)
        {
            API.EHPApptType[] data = API.Session.GetEHPApptTypeList(API.Session.AdminEHPDoctorID);
            if(data!= null)
            {
                for(int a = 0; a < data.Length; a++)
                {
                    cboEHPApptType.Items.Add(new ListItem(data[a].Name, data[a].ID.ToString()));
                }
            }
        }
    }
    protected void ShowData()
    {
        if (API.Session.AdminEHPApptTypeID > 0)
        {
            API.EHPApptType data = new API.EHPApptType(API.Session.AdminEHPApptTypeID);
            if (data != null)
            {
                lblEHPName.Text = data.Name;
                lblEnabled.Text = data.Enabled.ToString();
                pnlApptTypeInfo.Visible = true;
            }
        }
        lbEdit.Visible = cboEHPApptType.SelectedIndex > 0;
        lbDelete.Visible = cboEHPApptType.SelectedIndex > 0;
        lbGEApptType.Visible = cboEHPApptType.SelectedIndex > 0;
        pnlApptTypeInfo.Visible = cboEHPApptType.SelectedIndex > 0;
    }
    protected void SetApptType(int id)
    {
        if (cboEHPApptType.Items.Count > 0)
        {
            for(int a = 0; a < cboEHPApptType.Items.Count; a++)
            {
                if(cboEHPApptType.Items[a].Value == id.ToString())
                {
                    cboEHPApptType.SelectedIndex = a;
                    break;
                }
            }
        }
    }
}