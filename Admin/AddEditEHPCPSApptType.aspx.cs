using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddEditEHPCPSApptType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.EHPCPSApptType_GetDetailsError += Err;
        API.Session.EHPCPSApptType_SaveError += Err;
        API.Session.GetGECPSApptTypesError += Err;
        API.Session.GetEHPCPSApptTypeCPSIDError += Err;
        litError.Text = "";
        if (!IsPostBack)
        {
            RefreshGEApptTypes();
            if (API.Session.AdminEHPCPSApptTypeID > 0)
            {
                SetGEApptType();
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (cboGEApptType.SelectedIndex > 0)
        {
            API.EHPCPSApptType data = new API.EHPCPSApptType();
            data.CPSID = int.Parse(cboGEApptType.SelectedItem.Value);
            data.EHPApptTypeID = API.Session.AdminEHPApptTypeID;
            data.ID = API.Session.AdminEHPCPSApptTypeID;
            data.State = API.Session.AdminState;
            data.Save();
            Response.Redirect("~/Admin/EHPCPSApptTypes.aspx");
        }
        else
        {
            Err(1049, "You must select a Centricity Appt Type first.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/EHPCPSApptTypes.aspx");
    }
    protected void RefreshGEApptTypes()
    {
        cboGEApptType.Items.Clear();
        cboGEApptType.Items.Add(new ListItem("Select Appt Type", "0"));

        API.GECPSApptType[] data = API.Session.GetGECPSApptTypes(API.Session.AdminState);
        if(data!= null)
        {
            for (int a = 0; a < data.Length; a++)
            {
                cboGEApptType.Items.Add(new ListItem(data[a].Name + " [" + data[a].Duration + "]", data[a].ID.ToString()));
            }
        }
    }
    protected void SetGEApptType()
    {
        int cpsid = API.Session.GetEHPCPSApptTypeCPSID(API.Session.AdminEHPCPSApptTypeID);
        if (cboGEApptType.Items.Count > 0)
        {
            for(int a = 0; a < cboGEApptType.Items.Count; a++)
            {
                if(cboGEApptType.Items[a].Value == cpsid.ToString())
                {
                    cboGEApptType.SelectedIndex = a;
                    break;
                }
            }
        }
    }
    protected void Err(int ErrNum, string Msg)
    {
        litError.Text += "Error " + ErrNum.ToString() + " - " + Msg + "<br />";
    }
}