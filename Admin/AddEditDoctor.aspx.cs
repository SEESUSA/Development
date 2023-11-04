using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddEditDoctor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.GetCPSDoctorListError += Err;
        API.Session.EHPDoctor_GetDetailsError += Err;
        API.Session.EHPDoctor_SaveError += Err;

        litError.Text = "";
        chkEnabled.Checked = true;
        if (!IsPostBack)
        {
            RefreshGENames();
            ShowData();
        }
        txtEHPName.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (cboGEName.SelectedIndex == 0) { Err(1047, "You must select a Centricity Doctor first."); return; }
        if (txtEHPName.Text == "") { Err(1048, "The Friendly Name cannot be blank."); return; }
        API.EHPDoctor data = new API.EHPDoctor();
        data.CPSID = int.Parse(cboGEName.SelectedItem.Value);
        data.Enabled = chkEnabled.Checked;
        data.FacilityID = API.Session.AdminEHPFacilityID;
        data.ID = API.Session.AdminEHPDoctorID;
        data.Name = txtEHPName.Text;
        data.State = API.Session.AdminState;
        data.Save();
        Response.Redirect("~/Admin/Doctors.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/Doctors.aspx");
    }
    protected void Err(int ErrNum, string Msg)
    {
        litError.Text += "Error " + ErrNum.ToString() + " - " + Msg + "<br />";
    }
    protected void ShowData()
    {
        if (API.Session.AdminEHPDoctorID > 0)
        {
            API.EHPDoctor data = new API.EHPDoctor(API.Session.AdminEHPDoctorID);
            if (data != null)
            {
                txtEHPName.Text = data.Name;
                SetGEName(data.State, data.CPSID);
                chkEnabled.Checked = data.Enabled;
            }
        }
    }
    protected void RefreshGENames()
    {
        cboGEName.Items.Clear();
        cboGEName.Items.Add(new ListItem("Select Doctor", "0"));
        

        API.CPSDoctor[] data = API.Session.GetCPSDoctorList(API.Session.AdminState);
        if(data!= null)
        {
            for(int a = 0; a < data.Length; a++)
            {
                cboGEName.Items.Add(new ListItem("[" + data[a].State + "] " + data[a].ListName, data[a].ID.ToString()));
            }
        }
    }
    protected void SetGEName(string state, int cpsid)
    {
        if (cboGEName.Items.Count > 0)
        {
            for(int a = 0; a < cboGEName.Items.Count; a++)
            {
                if(cboGEName.Items[a].Value == cpsid.ToString())
                {
                    cboGEName.SelectedIndex = a;
                    break;
                }
            }
        }
    }

}