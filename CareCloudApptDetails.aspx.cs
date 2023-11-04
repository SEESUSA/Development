using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CareCloudApptDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FillDetails();
    }

    private void FillDetails()
    {
        try 
        {
            lblFacility.Text = API.Session.FacilityDetails;
            lblEye.Text= API.Session.EyeDetails;
            lblDoctor.Text = API.Session.Doctor;
            lblFax.Text = API.Session.Fax;
            lblPhone.Text = API.Session.Phone;
            lblRefDr.Text = API.Session.ReferralDoctor;
            lblApptType.Text = API.Session.AppointmentType;
            lblApptSlot.Text = API.Session.Datetime;

        }
        catch (Exception ex) { }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        API.Session.ReferralDoctorValue = 0;
        API.Session.FacilityValue = 0;
        API.Session.DoctorValue = 0;
        API.Session.AppointmentTypeValue = 0;
        API.Session.SloatValue = 0;
        Response.Redirect("~/");
    }
}