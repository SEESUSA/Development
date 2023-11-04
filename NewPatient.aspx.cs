using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewPatient : System.Web.UI.Page
{

/// <summary>
/// This page is called when the referring doctor is adding a new 
/// patient to the centricity database.  It is also used to transfer
/// a patient to the referring provider.
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>


    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.PatientExistsError += Err;
        API.Session.CreateNewPatientError += Err;
        API.Session.TransferPatientReferringDoctorError += Err;
        API.Session.GetSignatureSourceMIDError += Err;

        litError.Text = "";
        if (!IsPostBack)
        {
            RefreshGender();
        }

        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateData())
        {
            // create the new patient object - it will be used to search the CPS database
            // and to add the new patient into the CPS database if not found.
            API.CPSPatient newpatient = new API.CPSPatient();
            newpatient.DOB = DateTime.Parse(txtDOB.Text);
            newpatient.First = txtFirstName.Text;
            newpatient.Gender = cboGender.SelectedItem.Value;
            newpatient.Last = txtLastName.Text;
            newpatient.Phone = txtPhone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

            // check to see if the patient already exists in CPS database
            API.CPSPatient existingpatient = API.Session.PatientExists(API.Session.SWState, newpatient);
            if (existingpatient.PatientProfileID > 0)
            {
                // Patient was found - transfer to another page to validate patient SSN
                if (existingpatient.SSN != "000000000")
                {
                    API.Session.SWExistingPatient = existingpatient;
                    if(litError.Text=="")
                        Response.Redirect("~/ValidatePatient.aspx");
                }
                else
                {
                    // transfer patient to the new doctor _ RefDrID
                    API.Session.TransferPatientReferringDoctor(API.Session.SWState, existingpatient, API.Session.SWLocationID);
                    if(litError.Text=="")
                        Response.Redirect("~/ScheduleAppt.aspx");
                }
            }
            else
            {
                // Patient not found - add to the CPS database
                API.Session.CreateNewPatient(API.Session.SWState, newpatient, API.Session.SWLocationID);
                if(litError.Text=="")
                    Response.Redirect("~/ScheduleAppt.aspx");
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ScheduleAppt.aspx");
    }
    protected void RefreshGender()
    {
        cboGender.Items.Clear();
        cboGender.Items.Add(new ListItem("Select Gender", "0"));
        cboGender.Items.Add(new ListItem("Male", "Male"));
        cboGender.Items.Add(new ListItem("Female", "Female"));
    }
    protected void Err(int number, string msg)
    {
        litError.Text += "Error: " + number.ToString() + " - " + msg + "<br />";
    }
    protected bool ValidateData()
    {
        if (txtFirstName.Text == "") { Err(1112, "You must provide the First Name.");  return false; }
        if (txtLastName.Text == "") { Err(1113, "You must provide the Last Name.");return false; }
        if (cboGender.SelectedIndex == 0) { Err(1114, "You must select the Gender first."); return false; }
        if (txtDOB.Text == "") { Err(1115, "You must provide the Date of Birth."); return false; }
        if (txtPhone.Text == "") { Err(1116, "You must provide the Phone number."); return false; }
        return true;
    }
}

