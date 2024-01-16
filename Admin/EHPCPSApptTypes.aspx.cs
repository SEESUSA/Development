using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EHPCPSApptTypes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.GetEHPCPSApptTypeListError += Err;
        API.Session.GetEHPCPSApptTypeCPSIDError += Err;
        API.Session.GetGECPSApptTypeDisplayError += Err;
        lbNew.Visible = true;
        litError.Text = "";
        if (!IsPostBack)
        {
            RefreshEHPCPSApptTypes();
            ShowData();
        }
    }

    protected void cboEHPCPSApptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbEdit.Visible = cboEHPCPSApptType.SelectedIndex > 0;
        lbDelete.Visible = cboEHPCPSApptType.SelectedIndex > 0;
        pnlEHPCPSApptTypeInfo.Visible = cboEHPCPSApptType.SelectedIndex > 0;
        if (cboEHPCPSApptType.SelectedIndex > 0)
        {
            int x = cboEHPCPSApptType.SelectedItem.Text.IndexOf("[");
            API.Session.AdminEHPCPSApptTypeID = int.Parse(cboEHPCPSApptType.SelectedItem.Value.ToString());
            API.Session.AdminEHPCPSApptType = cboEHPCPSApptType.SelectedItem.Text.Substring(0, x - 1);
            ShowData();
        }
        else
        {
            API.Session.AdminEHPCPSApptType = "";
            API.Session.AdminEHPCPSApptTypeID = 0;
            lblGEName.Text = "";
        }
    }
    protected void lbNew_Click(object sender, EventArgs e)
    {
        API.Session.AdminEHPCPSApptType = "";
        API.Session.AdminEHPCPSApptTypeID = 0;
        Response.Redirect("~/Admin/AddEditEHPCPSApptType.aspx");
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/AddEditEHPCPSApptType.aspx");
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/DeleteEHPCPSApptType.aspx");
    }
    protected void RefreshEHPCPSApptTypes()
    {
        cboEHPCPSApptType.Items.Clear();
        cboEHPCPSApptType.Items.Add(new ListItem("Select Centricity Appt Type", "0"));
        API.EHPCPSApptType[] data = API.Session.GetEHPCPSApptTypeList(API.Session.AdminEHPApptTypeID);
        if (data != null)
        {
            for (int a = 0; a < data.Length; a++)
            {
                cboEHPCPSApptType.Items.Add(new ListItem(data[a].Display, data[a].ID.ToString()));
            }
        }
    }
    protected void ShowData()
    {
        if (API.Session.AdminEHPCPSApptTypeID > 0)
        {
            int cpsid = API.Session.GetEHPCPSApptTypeCPSID(API.Session.AdminEHPCPSApptTypeID);
            lblGEName.Text = API.Session.GetGECPSApptTypeDisplay(API.Session.AdminState, cpsid);
            API.Session.AdminGECPSApptType = lblGEName.Text;
            API.Session.AdminGECPSApptTypeID = cpsid;
            SetGEApptType();
            pnlEHPCPSApptTypeInfo.Visible = true;
        }
        else
        {
            lblGEName.Text = "";
            pnlEHPCPSApptTypeInfo.Visible = false;
            API.Session.AdminGECPSApptType = "";
            API.Session.AdminGECPSApptTypeID = 0;
        }
        lbEdit.Visible = cboEHPCPSApptType.SelectedIndex > 0;
        lbDelete.Visible = cboEHPCPSApptType.SelectedIndex > 0;
        pnlEHPCPSApptTypeInfo.Visible = cboEHPCPSApptType.SelectedIndex > 0;
    }
    protected void SetGEApptType()
    {
        if (cboEHPCPSApptType.Items.Count > 0)
        {
            for(int a = 0; a < cboEHPCPSApptType.Items.Count; a++)
            {
                if(cboEHPCPSApptType.Items[a].Value == API.Session.AdminEHPCPSApptTypeID.ToString())
                {
                    cboEHPCPSApptType.SelectedIndex = a;
                    break;
                }
            }
        }
    }
    protected void Err(int num, string msg)
    {
        litError.Text += "Error " + msg + "<br />";//+ num.ToString() + " - " 
    }
}