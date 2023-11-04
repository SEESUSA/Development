using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


/// <summary>
/// When the referring provider does not have the patient in their list of patients, 
/// they enter the info as a new patient.  The system takes that new patient data and
/// searches centricity for matches.  If we find a match, the user is redirected to this
/// page.  This page informs them that we have found a matching patient; however, we want
/// the provider to enter the patient's SSN to verify it is the right patient.  This page
/// validates that information.
/// 
/// </summary>

public partial class ValidatePatient : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        litError.Text = "";
        if (!IsPostBack)
        {

        }
    }
    protected void Err(int num, string msg)
    {
        litError.Text += "Error " + num.ToString() + " - " + msg + "<br />";
    }
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        if (ValidateData())
        {
            string ssn = txtSSN1.Text + txtSSN2.Text + txtSSN3.Text;
            if (ssn == API.Session.SWExistingPatient.SSN)
            {
                API.Session.TransferPatientReferringDoctor(API.Session.SWState, API.Session.SWExistingPatient, API.Session.SWLocationID);
                Response.Redirect("~/ScheduleAppt.aspx");
            }
            else
            {
                Err(1125, "The social security number you entered does not match this patient's information on file.");
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ScheduleAppt.aspx");
    }
    protected bool ValidateData()
    {
        if (txtSSN1.Text == "") { Err(1120, "You must provide the complete social security number first."); return false; }
        if (txtSSN2.Text == "") { Err(1121, "You must provide the complete social security number first."); return false; }
        if (txtSSN3.Text == "") { Err(1122, "You must provide the complete social security number first."); return false; }
        string ssn = txtSSN1.Text + txtSSN2.Text + txtSSN3.Text;
        if (ssn.Length != 9) { Err(1123, "You must provide the complete social security number first."); return false; }
        if (ssn.IndexOfAny(Statics.caAlpha) > -1 || ssn.IndexOfAny(Statics.caSymbol) > -1) { Err(1124, "The social security number can only contain numbers - no letters or symbols allowed."); return false; }

        return true;
    }
}