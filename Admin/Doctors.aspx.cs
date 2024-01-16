using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Doctors : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.EHPDoctor_GetDetailsError += Err;
        API.Session.GetCPSDoctorDisplayNameError += Err;
        API.Session.GetEHPDoctorListError += Err;
        lbNew.Visible = true;
        litError.Text = "";
        if (!IsPostBack)
        {
            RefreshEHPDoctors();
            if (API.Session.AdminEHPDoctorID > 0)
            {
                SetDoctor();
                ShowData();
            }
        }
    }
    protected void cboEHPDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbNew.Visible = true;
        lbEdit.Visible = cboEHPDoctor.SelectedIndex > 0;
        lbDelete.Visible = cboEHPDoctor.SelectedIndex > 0;
        lbApptTypes.Visible = cboEHPDoctor.SelectedIndex > 0;

        if (cboEHPDoctor.SelectedIndex > 0)
        {
            API.Session.AdminEHPDoctorID = int.Parse(cboEHPDoctor.SelectedItem.Value);
            API.Session.AdminEHPDoctor = cboEHPDoctor.SelectedItem.Text;
            ShowData();
            API.Session.AdminEHPApptTypeID = 0;
            API.Session.AdminEHPApptType = "";
            API.Session.AdminEHPCPSApptTypeID = 0;
            API.Session.AdminEHPCPSApptType = "";
        }
        else
        {
            API.Session.AdminEHPDoctorID = 0;
            API.Session.AdminEHPDoctor = "";
            API.Session.AdminEHPApptTypeID = 0;
            API.Session.AdminEHPApptType = "";
            API.Session.AdminEHPCPSApptTypeID = 0;
            API.Session.AdminEHPCPSApptType = "";
        }
    }
    protected void lbNew_Click(object sender, EventArgs e)
    {
        API.Session.AdminEHPDoctorID = 0;
        API.Session.AdminEHPDoctor = "";
        Response.Redirect("~/Admin/AddEditDoctor.aspx");
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/AddEditDoctor.aspx");
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/DeleteDoctor.aspx");
    }
    protected void lbApptTypes_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/ApptTypes.aspx");
    }
    protected void RefreshEHPDoctors()
    {
        cboEHPDoctor.Items.Clear();
        cboEHPDoctor.Items.Add(new ListItem("Select Doctor", "0"));

        API.EHPDoctor[] data = API.Session.GetEHPDoctorList(API.Session.AdminEHPFacilityID);
        if(data!= null)
        {
            for(int a = 0; a < data.Length; a++)
            {
                cboEHPDoctor.Items.Add(new ListItem(data[a].Name, data[a].ID.ToString()));
            }
        }
    }
    protected void ShowData()
    {
        API.EHPDoctor data = new API.EHPDoctor(API.Session.AdminEHPDoctorID);
        if(data!= null)
        {
            lblEHPName.Text = data.Name;
            lblGEName.Text = API.Session.GetCPSDoctorDisplayName(data.State, data.CPSID);
            lblEnabled.Text = data.Enabled.ToString();
        }
        pnlDoctorInfo.Visible = true;
    }
    protected void Err(int num, string msg)
    {
        litError.Text += "Error "  + msg + "<br />";//+ num.ToString() + " - "
    }
    protected void SetDoctor()
    {
        if (API.Session.AdminEHPDoctorID > 0)
        {
            if (cboEHPDoctor.Items.Count > 0)
            {
                for(int a = 0; a < cboEHPDoctor.Items.Count; a++)
                {
                    if (cboEHPDoctor.Items[a].Value == API.Session.AdminEHPDoctorID.ToString())
                    {
                        cboEHPDoctor.SelectedIndex = a;
                        lbEdit.Visible = cboEHPDoctor.SelectedIndex > 0;
                        lbDelete.Visible = cboEHPDoctor.SelectedIndex > 0;
                        lbApptTypes.Visible = cboEHPDoctor.SelectedIndex > 0;
                        break;
                    }
                }
            }
        }
    }
}