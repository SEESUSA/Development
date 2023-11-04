using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Facilities : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.GetEHPFacilityListError += Err;
        API.Session.EHPFacility_GetDetailsError += Err;
        API.Session.GetCPSFacilityDisplayNameError += Err;

        litError.Text = "";
        lbNew.Visible = true;
        if (!IsPostBack)
        {
            RefreshEHPFacilities();
            SetEHPFacility();
        }
    }

    #region Event Handlers
    protected void cboEHPFacility_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbEdit.Visible = cboEHPFacility.SelectedIndex > 0;
        lbDelete.Visible= cboEHPFacility.SelectedIndex > 0;
        lbDoctors.Visible= cboEHPFacility.SelectedIndex > 0;
        ShowFacilityData();
        API.Session.AdminEHPDoctor = "";
        API.Session.AdminEHPDoctorID = 0;
        API.Session.AdminEHPApptType = "";
        API.Session.AdminEHPApptTypeID = 0;
        API.Session.AdminEHPCPSApptType = "";
        API.Session.AdminEHPCPSApptTypeID = 0;
    }
    protected void Err(int ErrorNumber, string Msg)
    {
        litError.Text = "Error " + ErrorNumber.ToString() +  " - " + Msg + "<br />";
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/DeleteFacility.aspx");
    }
    protected void lbDoctors_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/Doctors.aspx");
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/AddEditFacility.aspx");
    }
    protected void lbNew_Click(object sender, EventArgs e)
    {
        API.Session.AdminState = "";
        API.Session.AdminEHPFacilityID = 0;
        API.Session.AdminEHPFacility = "";
        API.Session.AdminEHPDoctor = "";
        API.Session.AdminEHPDoctorID = 0;
        API.Session.AdminEHPApptType = "";
        API.Session.AdminEHPApptTypeID = 0;
        API.Session.AdminEHPCPSApptType = "";
        API.Session.AdminEHPCPSApptTypeID = 0;
        Response.Redirect("~/Admin/AddEditFacility.aspx");
    }
    #endregion

    #region Functions
    protected void RefreshEHPFacilities()
    {
        cboEHPFacility.Items.Clear();
        cboEHPFacility.Items.Add(new ListItem("Select Facility", "0"));

        API.EHPFacility[] data = API.Session.GetEHPFacilityList();
        if (data != null)
        {
            for (int a = 0; a < data.Length; a++)
            {
                cboEHPFacility.Items.Add(new ListItem("[" + data[a].State + "] " + data[a].Name, data[a].FacilityID.ToString()));
            }
        }
    }
    protected void SetEHPFacility()
    {
        if (API.Session.AdminEHPFacilityID > 0)
        {
            for (int a = 0; a < cboEHPFacility.Items.Count; a++)
            {
                if (int.Parse(cboEHPFacility.Items[a].Value) == API.Session.AdminEHPFacilityID)
                {
                    cboEHPFacility.SelectedIndex = a;
                    ShowFacilityData();
                    break;
                }
            }
        }
        lbEdit.Visible = cboEHPFacility.SelectedIndex > 0;
        lbDelete.Visible = cboEHPFacility.SelectedIndex > 0;
        lbDoctors.Visible = cboEHPFacility.SelectedIndex > 0;
    }
    protected void ShowFacilityData()
    {
        if (cboEHPFacility.SelectedIndex > 0)
        {
            API.EHPFacility data = new API.EHPFacility(int.Parse(cboEHPFacility.SelectedItem.Value));
            if(data!= null)
            {
                lblEHPName.Text = "[" + data.State + "] " + data.Name;
                lblGEName.Text = API.Session.GetCPSFacilityDisplayName(data.State, data.CPSID);
                lblEnabled.Text = data.Enabled.ToString();
                API.Session.AdminState = data.State;
                API.Session.AdminEHPFacilityID = data.FacilityID;
                API.Session.AdminEHPFacility = data.Name;
            }
            pnlFacilityInfo.Visible = true;
        }
        else
        {
            lblEHPName.Text = "";
            lblEnabled.Text = "";
            lblGEName.Text = "";
            API.Session.AdminState = "";
            API.Session.AdminEHPFacilityID = 0;
            API.Session.AdminEHPFacility = "";
            API.Session.AdminEHPDoctorID = 0;
            API.Session.AdminEHPDoctor = "";
            API.Session.AdminEHPApptType = "";
            API.Session.AdminEHPApptTypeID = 0;
            API.Session.AdminEHPCPSApptTypeID = 0;
            API.Session.AdminEHPCPSApptType = "";
            pnlFacilityInfo.Visible = false;
        }
    }
    #endregion

}