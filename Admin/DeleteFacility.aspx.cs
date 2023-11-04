using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DeleteFacility : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.EHPFacility_GetDetailsError += Err;
        API.Session.EHPFacility_DeleteError += Err;
        litError.Text = "";
        if (!IsPostBack)
        {
        }
    }
    protected void DeleteFacility()
    {
        API.EHPFacility data = new API.EHPFacility(API.Session.AdminEHPFacilityID);
        if (data != null)
        {
            data.Delete();
        }
    }
    protected void Err(int num, string msg)
    {
        litError.Text += "Error " + num.ToString() + " - " + msg + "<br />";
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        if (API.Session.AdminEHPFacilityID > 0)
        {
            DeleteFacility();
            API.Session.AdminState = "";
            API.Session.AdminEHPFacility = "";
            API.Session.AdminEHPFacilityID = 0;
            API.Session.AdminEHPDoctor = "";
            API.Session.AdminEHPDoctorID = 0;
            API.Session.AdminEHPApptType = "";
            API.Session.AdminEHPApptTypeID = 0;
            API.Session.AdminEHPCPSApptType = "";
            API.Session.AdminEHPCPSApptTypeID = 0;
            API.Session.AdminGECPSApptTypeID = 0;
            API.Session.AdminGECPSApptType = "";
            Response.Redirect("~/Admin/Facilities.aspx");
        }
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/Facilities.aspx");
    }
}