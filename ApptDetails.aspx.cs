using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ApptDetails : System.Web.UI.Page
{

    /// <summary>
    /// The purpose of this page is to display on the brower's screen, all of the contact and
    /// appointment details.  the referring provider can then print this page out and give to the
    /// patient as an appointment reminder.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        litError.Text = "";
        if (!IsPostBack)
        {
            LoadDetails();
        }
    }
    protected void LoadDetails()
    {
        lblApptSlot.Text = API.Session.SWApptSlot;
        lblApptType.Text = API.Session.SWApptType;
        lblDoctor.Text = API.Session.SWDoctor;
        lblEye.Text = API.Session.SWEyeText;
        lblFacility.Text = API.Session.SWFacility;
        lblRefDr.Text = API.Session.SWLocation;
        string data = API.Session.GetFacilityContactDetails(API.Session.SWState, API.Session.SWCPSFacilityID);
        if(data!= null)
        {
            string[] fields = data.Split('^');
            if (fields != null)
            {
                if (fields.Length > 0)
                {
                    EHP.Formatting h = new EHP.Formatting();
                    lblFacAddress.Text = fields[0];
                    lblFacCSZ.Text = fields[1] + ", " + fields[2] + " " + fields[3];
                    lblPhone.Text = h.PhoneNumber(fields[4]);
                    lblFax.Text = h.PhoneNumber(fields[5]);
                }
            }
        }
     
    }


}