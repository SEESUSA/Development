using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddEditFacility : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.EHPFacility_SaveError += Err;
        API.Session.EHPFacility_GetDetailsError += Err;
        API.Session.GetCPSFacilityListError += Err;

        lbNewContact.Visible = API.Session.AdminEHPFacilityID > 0;

        litError.Text = "";
        if (!IsPostBack)
        {
            RefreshGEFacilitiets();
            ShowData();
            LoadContacts();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/Facilities.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (cboGEName.SelectedIndex == 0) { Err(1050, "You must select the Centricity Facility first."); return; }
        if (txtEHPName.Text == "") { Err(1051, "The Friendly Name cannot be blank."); return; }

        API.EHPFacility data = new API.EHPFacility();
        data.CPSID = int.Parse(cboGEName.SelectedItem.Value.Substring(2));
        data.Enabled = chkEnabled.Checked;
        data.State = cboGEName.SelectedItem.Value.Substring(0, 2);
        data.Name = txtEHPName.Text;
        data.FacilityID = API.Session.AdminEHPFacilityID;
        data.Save();
        Response.Redirect("~/Admin/Facilities.aspx");
    }
    protected void RefreshGEFacilitiets()
    {
        cboGEName.Items.Clear();
        cboGEName.Items.Add(new ListItem("Select Facility", "0"));

        API.CPSFacility[] data = API.Session.GetCPSFacilityList();
        if(data!= null)
        {
            for(int a = 0; a < data.Length; a++)
            {
                cboGEName.Items.Add(new ListItem("[" + data[a].State + "] " + data[a].ListName, data[a].State + data[a].ID.ToString()));
            }
        }
    }
    protected void Err(int ErrNum, string Msg)
    {
        litError.Text += "Error " + ErrNum.ToString() + " - " + Msg + "<br />";
    }
    protected void ShowData()
    {
        if (API.Session.AdminEHPFacilityID > 0)
        {
            API.EHPFacility data = new API.EHPFacility(API.Session.AdminEHPFacilityID);
            if (data != null)
            {
                txtEHPName.Text = data.Name;
                SetGEName(data.State, data.CPSID);
                chkEnabled.Checked = data.Enabled;
            }
        }
    }
    protected void SetGEName(string state, int cpsid)
    {
        if (cboGEName.Items.Count > 0)
        {
            for(int a = 0; a < cboGEName.Items.Count; a++)
            {
                if(cboGEName.Items[a].Value == state + cpsid.ToString())
                {
                    cboGEName.SelectedIndex = a;
                    break;
                }
            }
        }
    }
    protected void lbNewContact_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/AddEditFacilityContact.aspx?ID=0");
    }
    protected void LoadContacts()
    {
        if (API.Session.AdminEHPFacilityID > 0)
        {
            string[] data = API.Session.GetFacilityContactList(API.Session.AdminEHPFacilityID);
            if(data!= null)
            {
                for(int a = 0; a < data.Length; a++)
                {
                    string[] items = data[a].Split(new char[] { '^' });
                    if (items != null)
                    {
                        litContacts.Text += "<tr><td>";
                        litContacts.Text += "<a href=\"AddEditFacilityContact.aspx?ID=" + items[0] + "\">Edit</a>";
                        litContacts.Text += " | ";
                        litContacts.Text += "<a href=\"DeleteFacilityContact.aspx?ID=" + items[0] + "\">Delete</a>";
                        litContacts.Text += "</td><td>" + items[1] + "</td><td>" + items[2] + "</td></tr>";
                        items = null;
                    }
                }
            }
        }
    }
}