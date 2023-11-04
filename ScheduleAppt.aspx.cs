using System;
using System.Web.UI.WebControls;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Xml;
using System.Net;
using System.Data;
using System.Web;

public partial class ScheduleAppt : System.Web.UI.Page
{

    /// <summary>
    /// This page is the scheduling appointment wizard.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    
    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.GetLocationsError += Err;
        API.Session.GetEHPFacilityListError += Err;
        API.Session.GetEHPDoctorListError += Err;
        API.Session.GetEHPApptTypeListError += Err;
        API.Session.GetCPSScheduleIDsError += Err;
        API.Session.GetOpenApptSlotsError += Err;
        API.Session.GetRefDrPatientsError += Err;
        API.Session.GetApptStartError += Err;
        API.Session.GetApptStopError += Err;
        API.Session.GetCompanyIDError += Err;
        API.Session.BookAppointmentError += Err;
        API.Session.GetFacilityContactListError += Err;
        API.Session.SendEmailError += Err;
        API.Session.GetFacilityContactDetailsError += Err;
        API.Session.UpdatePatientPhoneNumberError += Err;
        API.Session.GetPatientPhoneNumberError += Err;

        litError.Text = "";
        if(!IsPostBack)
        {
            RefreshLocations();
            if (API.Session.SWLocationID > 0) SetLocation(API.Session.SWLocationID);
            if (API.Session.SWEHPFacilityID > 0) SetFacility(API.Session.SWCPSFacilityID);
            if (API.Session.SWEHPDoctorID > 0) SetDoctor(API.Session.SWCPSDoctorID);
            if (API.Session.SWEHPApptTypeID > 0) SetApptType(API.Session.SWEHPApptTypeID);
            if (API.Session.SWCPSApptSlotID > 0) SetApptSlot(API.Session.SWCPSApptSlotID);
            if (API.Session.SWPatientProfileID > 0) SetPatient(API.Session.SWPatientProfileID);
            SetEye(API.Session.SWEye);
        }
        
    }
    protected void cboLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboLocation.Items.Count > 0)
        {
            if (cboLocation.SelectedIndex > 0)
            {
                string[] fields = cboLocation.SelectedItem.Value.Split('^');
                if(fields!= null)
                {
                    if (fields.Length > 0)
                    {
                        API.Session.SWState = fields[0];
                        API.Session.SWLocationID = int.Parse(fields[1]);
                        API.Session.SWLocation = cboLocation.SelectedItem.Text;
                        RefreshEHPFacilities();
                        //RefreshEHPFacilities_API();
                        //RefreshApptTypes_API();
                    }
                }
            }
        }
    }
    protected void cboEHPFacility_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearSteps("Facility");
        if (cboEHPFacility.Items.Count > 0)
        {
            if (cboEHPFacility.SelectedIndex > 0)
            {
                string[] fields = cboEHPFacility.SelectedItem.Value.Split('^');
                if(fields != null)
                {
                    if (fields.Length > 0)
                    {
                        API.Session.SWEHPFacilityID = int.Parse(fields[0]);
                        API.Session.SWCPSFacilityID = int.Parse(fields[1]);
                        API.Session.SWFacility = cboEHPFacility.SelectedItem.Text;
                        RefreshEHPDoctors();
                    }
                }
            }
        }
    }
    protected void RefreshLocations()
    {
        ClearSteps("Locations");
        cboLocation.Items.Clear();
        cboLocation.Items.Add(new ListItem("Select Your Location", "0"));
        API.Location[] data = API.Session.GetLocations(API.Session.GUID);
        if (data != null)
        {
            for (int a = 0; a < data.Length; a++)
            {
                string state = "";
                if (data[a].Database == "Birmingham") state = "AL";
                if (data[a].Database == "Nashville") state = "TN";

               // API.Session.stateForMasterUsers = state;
                cboLocation.Items.Add(new ListItem(data[a].Display, state + "^" + data[a].ID.ToString()));
            }
        }
        lbApptSlotMore.Visible = false;
        lbNewPatient.Visible = false;
    }

    protected void RefreshEHPFacilities()
    {
        ClearSteps("Facility");
        cboEHPFacility.Items.Clear();
        cboEHPFacility.Items.Add(new ListItem("Select Facility", "0"));
        API.EHPFacility[] data = API.Session.GetEHPFacilityList();
        if (data != null)
        {
            for (int a = 0; a < data.Length; a++)
            {
                if (!data[a].Enabled) continue;
                if (data[a].State != API.Session.SWState) continue;
                cboEHPFacility.Items.Add(new ListItem(data[a].Name, data[a].FacilityID.ToString() + "^" + data[a].CPSID.ToString()));
            }
        }
        lbApptSlotMore.Visible = false;
        lbNewPatient.Visible = false;
    }
    protected void RefreshEHPFacilities_API()
    {
        #region Get Facility
        DataSet ds = new DataSet();
        var responseString = "";
        WebRequest request = WebRequest.Create(Statics.URL_GETFAcility);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Headers.Add("Authorization", "Bearer Jd5hklg7R7qB7O8VuXHVot0Ejc6r_lj9");///API.Session.AccessToken);
        HttpWebResponse httpWebResponse = null;
        httpWebResponse = (HttpWebResponse)request.GetResponse();

        using (Stream reader = httpWebResponse.GetResponseStream())
        {
            StreamReader sr = new StreamReader(reader);
            responseString = sr.ReadToEnd();
            sr.Close();
        }

        try
        {
            XmlDocument xd = new XmlDocument();
            responseString = "{ \"rootNode\": {" + responseString.Trim().TrimStart('{').TrimEnd('}') + "} }";
            xd = (XmlDocument)JsonConvert.DeserializeXmlNode(responseString);
            
            ds.ReadXml(new XmlNodeReader(xd));
            //  return ds; 
        }
        catch (Exception ex)
        { throw new ArgumentException(ex.Message); }



        #endregion



        ClearSteps("Facility");
        cboEHPFacility.Items.Clear();
        cboEHPFacility.Items.Add(new ListItem("Select Facility", "0"));
        
        if(ds!=null)
        { if (ds.Tables["locations"].Rows.Count>0)
            {
                foreach(DataRow dr in ds.Tables["locations"].Rows)
                {//Remaining to check enable.need to check when get details.
                   // if (ds.Tables["address"].Rows[0]["state_name"].ToString() != API.Session.SWState) continue;
                    cboEHPFacility.Items.Add(new ListItem(dr["name"].ToString(), dr["id"].ToString() + "^" + dr["id"].ToString()));
                }
            }
        }
        lbApptSlotMore.Visible = false;
        lbNewPatient.Visible = false;
    }
    protected void Err(int num, string msg)
    {
        litError.Text += "Error " + num.ToString() + " - " + msg + "<br />";
    }
    protected void RefreshEHPDoctors()
    {
        ClearSteps("Doctor");
        cboEHPDoctor.Items.Clear();
        cboEHPDoctor.Items.Add(new ListItem("Select Doctor", "0"));
        API.EHPDoctor[] data = API.Session.GetEHPDoctorList(API.Session.SWEHPFacilityID);
        if(data!= null)
        {
            for(int a = 0; a < data.Length; a++)
            {
                if (!data[a].Enabled) continue;
                cboEHPDoctor.Items.Add(new ListItem(data[a].Name, data[a].ID.ToString() + "^" + data[a].CPSID.ToString()));
            }
        }
        lbApptSlotMore.Visible = false;
        lbNewPatient.Visible = false;
    }
    protected void cboEHPDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearSteps("Doctor");
        if (cboEHPDoctor.Items.Count > 0)
        {
            if (cboEHPDoctor.SelectedIndex > 0)
            {
                string[] fields = cboEHPDoctor.SelectedItem.Value.Split('^');
                if(fields!= null)
                {
                    if (fields.Length > 0)
                    {
                        API.Session.SWEHPDoctorID = int.Parse(fields[0]);
                        API.Session.SWCPSDoctorID = int.Parse(fields[1]);
                        API.Session.SWDoctor = cboEHPDoctor.SelectedItem.Text;
                        RefreshApptTypes();
                    }
                }
            }
        }


    }
    protected void RefreshApptTypes()
    {
        ClearSteps("ApptType");
        cboApptType.Items.Clear();
        cboApptType.Items.Add(new ListItem("Select Appointment Type", "0"));
        API.EHPApptType[] data = API.Session.GetEHPApptTypeList(API.Session.SWEHPDoctorID);
        if (data != null)
        {
            for (int a = 0; a < data.Length; a++)
            {
                if (!data[a].Enabled) continue;
                cboApptType.Items.Add(new ListItem(data[a].Name, data[a].ID.ToString()));
            }
        }
        lbApptSlotMore.Visible = false;
        lbNewPatient.Visible = false;
    }
    protected void RefreshApptTypes_API()
    {
        ClearSteps("ApptType");
        cboApptType.Items.Clear();
        cboApptType.Items.Add(new ListItem("Select Appointment Type", "0"));
        

        var responseString1 = "";
        WebRequest request1 = WebRequest.Create(Statics.URL_GETApptType);
        request1.Method = "GET";
        request1.ContentType = "application/json";
        request1.Headers.Add("Authorization", "Bearer Jd5hklg7R7qB7O8VuXHVot0Ejc6r_lj9");///API.Session.AccessToken);
        HttpWebResponse httpWebResponse1 = null;
        httpWebResponse1 = (HttpWebResponse)request1.GetResponse();

        using (Stream reader = httpWebResponse1.GetResponseStream())
        {
            StreamReader sr = new StreamReader(reader);
            responseString1 = sr.ReadToEnd();
            sr.Close();
        }


        try
        {
            if (!(responseString1.Trim().StartsWith("{")) && (!responseString1.Trim().EndsWith("}")))
            {
                responseString1 = "{" + responseString1 + "}";
            }
            XmlDocument xd = new XmlDocument();
            responseString1 = "{ \"rootNode\": {\"root\":" + responseString1.Trim().TrimStart('{').TrimEnd('}') + "} }";
            xd = (XmlDocument)JsonConvert.DeserializeXmlNode(responseString1);
            DataSet ds = new DataSet();
            ds.ReadXml(new XmlNodeReader(xd));
            //  return ds; 
        }
        catch (Exception ex)
        { throw new ArgumentException(ex.Message); }


        //API.EHPApptType[] data = API.Session.GetEHPApptTypeList(API.Session.SWEHPDoctorID);
        //if (data != null)
        //{
        //    for (int a = 0; a < data.Length; a++)
        //    {
        //        if (!data[a].Enabled) continue;
        //        cboApptType.Items.Add(new ListItem(data[a].Name, data[a].ID.ToString()));
        //    }
        //}


        lbApptSlotMore.Visible = false;
        lbNewPatient.Visible = false;
    }
    protected void cboApptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearSteps("ApptType");
        if (cboApptType.Items.Count > 0)
        {
            if (cboApptType.SelectedIndex > 0)
            {
                API.Session.SWEHPApptTypeID = int.Parse(cboApptType.SelectedItem.Value);
                API.Session.SWCPSApptTypeIDs = API.Session.GetCPSApptTypeIDs(API.Session.SWEHPApptTypeID);
                API.Session.SWApptType = cboApptType.SelectedItem.Text;
                RefreshApptSlots();
            }
        }
    }
    protected void RefreshApptSlots()
    {
        ClearSteps("ApptSlot");
        cboApptSlot.Items.Clear();
        cboApptSlot.Items.Add(new ListItem("Select Appointment Slot", "0"));

        API.ApptSlot[] data = API.Session.GetOpenApptSlots(Statics.SWInitNumApptSlots, DateTime.Today.AddDays(Statics.SWMinimumDaysOut), DateTime.MaxValue, API.Session.SWState, API.Session.SWCPSDoctorID, API.Session.SWCPSFacilityID, API.Session.SWCPSApptTypeIDs);
        if (data != null)
        {
            for (int a = 0; a < data.Length; a++)
            {
                cboApptSlot.Items.Add(new ListItem(data[a].Start.ToString(), data[a].ApptSlotID.ToString()));
            }
        }
        lbApptSlotMore.Visible = true;
        lbNewPatient.Visible = false;
    }
    protected void RefreshApptSlots(int slots)
    {
        ClearSteps("ApptSlot");
        cboApptSlot.Items.Clear();
        cboApptSlot.Items.Add(new ListItem("Select Appointment Slot", "0"));

        API.ApptSlot[] data = API.Session.GetOpenApptSlots(slots, DateTime.Today.AddDays(Statics.SWMinimumDaysOut), DateTime.MaxValue, API.Session.SWState, API.Session.SWCPSDoctorID, API.Session.SWCPSFacilityID, API.Session.SWCPSApptTypeIDs);
        if (data != null)
        {
            for (int a = 0; a < data.Length; a++)
            {
                cboApptSlot.Items.Add(new ListItem(data[a].Start.ToString(), data[a].ApptSlotID.ToString()));
            }
        }
        lbApptSlotMore.Visible = false;
        lbNewPatient.Visible = false;
    }
    protected void cboApptSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearSteps("ApptSlot");
        if(cboApptSlot.Items.Count > 0)
        {
            if (cboApptSlot.SelectedIndex > 0)
            {
                API.Session.SWCPSApptSlotID = int.Parse(cboApptSlot.SelectedItem.Value);
                API.Session.SWApptSlot = cboApptSlot.SelectedItem.Text;
                RefreshPatients();
            }
        }
    }
    protected void lbApptSlotMore_Click(object sender, EventArgs e)
    {
        RefreshApptSlots(Statics.SWMoreNumApptSlots);
    }
    protected void RefreshPatients()
    {
        ClearSteps("Patient");
        cboPatient.Items.Clear();
        cboPatient.Items.Add(new ListItem("Select Patient", "0"));
        API.CPSPatient[] data = API.Session.GetRefDrPatients(API.Session.SWState, API.Session.SWLocationID);
        if(data!= null)
        {
            for(int a = 0; a < data.Length; a++)
            {
                cboPatient.Items.Add(new ListItem(data[a].Last + ", " + data[a].First.TrimEnd() + " " + data[a].Middle + " - " + data[a].DOB.ToShortDateString() + " - " + data[a].SSN4, data[a].PatientProfileID.ToString()));
            }
        }
        lbNewPatient.Visible = true;
    }
    protected void ClearSteps(string startingpoint)
    {
        switch (startingpoint)
        {
            case "Location":
                cboEHPFacility.Items.Clear();
                goto case "Facility";
            case "Facility":
                cboEHPDoctor.Items.Clear();
                goto case "Doctor";
            case "Doctor":
                cboApptType.Items.Clear();
                goto case "ApptType";
            case "ApptType":
                cboApptSlot.Items.Clear();
                goto case "ApptSlot";
            case "ApptSlot":
                cboPatient.Items.Clear();
                goto case "Patient";
            case "Patient":
                rbBoth.Checked = true;
                rbEyeLeft.Checked = false;
                rbEyeRight.Checked = false;
                txtPhone.Text = "";
                break;
            default:
                break;
        }
    }
    protected void cboPatient_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearSteps("Patient");
        if (cboPatient.Items.Count > 0)
        {
            if (cboPatient.SelectedIndex > 0)
            {
                API.Session.SWPatientProfileID = int.Parse(cboPatient.SelectedItem.Value);
                API.Session.SWPatient = cboPatient.SelectedItem.Text;
                txtPhone.Text = API.Session.GetPatientPhoneNumber(API.Session.SWState, API.Session.SWPatientProfileID);
            }
        }
    }
    protected void rbBoth_CheckedChanged(object sender, EventArgs e)
    {
        API.Session.SWEye = API.Eye.Both;
        SetEye(API.Eye.Both);
    }
    protected void rbEyeRight_CheckedChanged(object sender, EventArgs e)
    {
        API.Session.SWEye = API.Eye.Right;
        SetEye(API.Eye.Right);
    }
    protected void rbEyeLeft_CheckedChanged(object sender, EventArgs e)
    {
        API.Session.SWEye = API.Eye.Left;
        SetEye(API.Eye.Left);
    }
    protected void lbNewPatient_Click(object sender, EventArgs e)
    {
        API.Session.SWPatientProfileID = 0;
        API.Session.SWPatient = "";
        Response.Redirect("~/NewPatient.aspx");
    }
    protected void btnBookAppt_Click(object sender, EventArgs e)
    {
        if (ValidateFields())
        {
            API.Session.SWReason = txtReason.Text;
            string phone = txtPhone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            API.Session.UpdatePatientPhoneNumber(API.Session.SWPatientProfileID, phone);
            API.Session.BookAppointment();
           // SendClinicEmail();
            //SendReferringEmail();
            Response.Redirect("~/ApptDetails.aspx");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/");
    }
    protected bool ValidateFields()
    {
        if (cboLocation.Items.Count <= 1) { Err(1099, "You must select a Referring From location first."); return false; }
        if (cboLocation.SelectedIndex == 0) { Err(1100, "You must select a Referring From location first."); return false; }
        if (cboEHPFacility.Items.Count <= 1) { Err(1101, "You must select a Facility first."); return false; }
        if (cboEHPFacility.SelectedIndex == 0) { Err(1102, "You must select a Facility first."); return false; }
        if (cboEHPDoctor.Items.Count <= 1) { Err(1103, "You must select a Doctor first."); return false; }
        if (cboEHPDoctor.SelectedIndex == 0) { Err(1104, "You must select a Doctor first.");return false; }
        if (cboApptType.Items.Count <= 1) { Err(1105, "You must select an Appointment Type first."); return false; }
        if (cboApptType.SelectedIndex == 0) { Err(1106, "You must select an Appointment Type first."); return false; }
        if (cboApptSlot.Items.Count <= 1) { Err(1107, "You must select an Appointment Slot first."); return false; }
        if (cboApptSlot.SelectedIndex == 0) { Err(1108, "You must select an Appointment Slot first."); return false; }
        if (cboPatient.Items.Count <= 1) { Err(1109, "You must select a Patient first."); return false; }
        if (cboPatient.SelectedIndex == 0) { Err(1110, "You must select a Patient first."); return false; }
        if (!rbBoth.Checked && !rbEyeLeft.Checked && !rbEyeRight.Checked) { Err(1111, "You must select an Eye first."); return false; }
        if (txtPhone.Text == "") { Err(1133, "You must provide the patient's current phone number."); return false; }
        if (txtPhone.Text.Replace("(", "").Replace(" ", "").Replace(")", "").Replace("-", "").Length < 10) { Err(1134, "You must provide the patient's current phone number."); return false; }
        if (txtReason.Text == "") { Err(1135, "You must provide the Reason for Visit."); return false; }

        return true;
    }
    protected void SetLocation(int locationid)
    {
        if (cboLocation.Items.Count > 1)
        {
            for (int a = 1; a < cboLocation.Items.Count; a++)
            {
                string[] fields = cboLocation.Items[a].Value.Split('^');
                if (fields != null)
                {
                    if (fields[1] == locationid.ToString())
                    {
                        cboLocation.SelectedIndex = a;
                         RefreshEHPFacilities();
                        //RefreshEHPFacilities_API();
                        break;
                    }
                }
            }
        }
    }
    protected void SetFacility(int cpsfacilityid)
    {
        if (cboEHPFacility.Items.Count > 1)
        {
            for (int a = 1; a < cboEHPFacility.Items.Count; a++)
            {
                string[] fields = cboEHPFacility.Items[a].Value.Split('^');
                if(fields!= null)
                {
                    if(fields[1] == cpsfacilityid.ToString())
                    {
                        cboEHPFacility.SelectedIndex = a;
                        RefreshEHPDoctors();
                        break;
                    }
                }
            }
        }
    }
    protected void SetDoctor(int cpsdoctorid)
    {
        if (cboEHPDoctor.Items.Count > 1)
        {
            for(int a = 1; a < cboEHPDoctor.Items.Count; a++)
            {
                string[] fields = cboEHPDoctor.Items[a].Value.Split('^');
                if(fields!= null)
                {
                    if (fields[1] == cpsdoctorid.ToString())
                    {
                        cboEHPDoctor.SelectedIndex = a;
                        RefreshApptTypes();
                        break;
                    }
                }
            }
        }

    }
    protected void SetApptType(int appttypeid)
    {
        if (cboApptType.Items.Count > 1)
        {
            for(int a = 1; a < cboApptType.Items.Count; a++)
            {
                if(cboApptType.Items[a].Value == appttypeid.ToString())
                {
                    cboApptType.SelectedIndex = a;
                    RefreshApptSlots();
                    break;
                }
            }
        }
    }
    protected void SetApptSlot(int apptslotid)
    {
        if(cboApptSlot.Items.Count>1)
        {
            for(int a = 1; a < cboApptSlot.Items.Count; a++)
            {
                if(cboApptSlot.Items[a].Value == apptslotid.ToString())
                {
                    cboApptSlot.SelectedIndex = a;
                    RefreshPatients();
                    break;
                }
            }
        }
    }
    protected void SetPatient(int patientid)
    {
        if (cboPatient.Items.Count > 1)
        {
            for (int a = 1; a < cboPatient.Items.Count; a++)
            {
                if (cboPatient.Items[a].Value == patientid.ToString())
                {
                    cboPatient.SelectedIndex = a;
                    break;
                }
            }
        }
    }
    protected void SetEye(API.Eye eye)
    {
        switch (eye)
        {
            case API.Eye.Both:
                rbBoth.Checked = true;
                rbEyeLeft.Checked = false;
                rbEyeRight.Checked = false;
                break;
            case API.Eye.Left:
                rbBoth.Checked = false;
                rbEyeLeft.Checked = true;
                rbEyeRight.Checked = false;
                break;
            case API.Eye.Right:
                rbBoth.Checked = false;
                rbEyeLeft.Checked = false;
                rbEyeRight.Checked = true;
                break;
            default:
                break;
        }
    }
    protected void SendClinicEmail()
    {
        string[] destinations = API.Session.GetFacilityContactList(API.Session.SWEHPFacilityID);
        string subject = "[SECURE] New Online Appointment Scheduled";
        string msg = "<html><body>";
        string eye = "";
        EHP.Formatting f = new EHP.Formatting();

        if (rbBoth.Checked) eye = "both";
        if (rbEyeLeft.Checked) eye = "left";
        if (rbEyeRight.Checked) eye = "right";
        msg += "An online appointment was just created in the CareCloud PMS for your clinic.<br><br>";
        msg += "Please verify the information is correct and there are no conflicts or other issues with this appointment.  ";
        msg += "You may also need to collect more information from the patient before their appointment.<BR><BR>";
        msg += "Date / Time the Appointment was created: " + DateTime.Now.ToString() + "<BR>";
        msg += "Referring From: " + cboLocation.SelectedItem.Text + "<br>";
        msg += "Your Facility: " + cboEHPFacility.SelectedItem.Text + "<br>";
        msg += "Your Doctor: " + cboEHPDoctor.SelectedItem.Text + "<br>";
        msg += "Appointment Type: " + cboApptType.SelectedItem.Text + "<br>";
        msg += "Appointment Slot: " + cboApptSlot.SelectedItem.Text + "<br>";
        msg += "Patient: " + cboPatient.SelectedItem.Text + "<br>";
        msg += "Current Phone Number: " + f.PhoneNumber(txtPhone.Text) + "<br>";
        msg += "Eye: " + eye + "<br>";
        msg += "Reason for visit: " + txtReason.Text + "<br>";
        msg += "<BR><BR>If you feel that the program is in error, please forward this email to dave.cooper@eyehealthpartners.com.<BR><BR>";
        msg += "</body></html>";
        //Temporary Commented

        //if (destinations!= null)
        //{
        //    for(int a = 0; a < destinations.Length; a++)
        //    {
        //        string[] fields = destinations[a].Split('^');
        //        if(fields!= null)
        //        {
        //            if (fields.Length > 0)
        //            {
        //                /*
        //                fields[0] = ID #
        //                fields[1] = Name
        //                fields[2] = Email Address
        //                */
        //                API.Session.SendEmail(fields[1], fields[2], subject, msg);

        //            }
        //        }
        //    }
        //}
        API.Session.SendEmail("Uma Dixit", "udixit@pipartners.com", subject, msg);
    }
    protected void SendReferringEmail()
    {
        string subject = "[SECURE] Online Appointment Confirmation";
        string eye = "";
        if (rbBoth.Checked) eye = "both";
        if (rbEyeLeft.Checked) eye = "left";
        if (rbEyeRight.Checked) eye = "right";
        string msg = "<html><body>";
        msg += "This email is to confirm that your appointment has been scheduled in our system for your patient.  ";
        msg += "Due to HIPAA, PHI, and HiTech laws, the patient name will not be disclosed in this email.  If you need ";
        msg += "to cancel or reschedule this appointment, please contact our clinic at the number below.<BR><BR>";
        msg += "If you did not schedule this appointment and believe it was made in error, please contact our clinc at the number below.<BR><BR>";
        msg += "Date / Time the Appointment was created: " + DateTime.Now.ToString() + "<BR>";
        msg += "Referring From: " + cboLocation.SelectedItem.Text + "<br>";
        msg += "Facility: " + cboEHPFacility.SelectedItem.Text + "<br>";
        msg += "Doctor: " + cboEHPDoctor.SelectedItem.Text + "<br>";
        msg += "Appointment Type: " + cboApptType.SelectedItem.Text + "<br>";
        msg += "Appointment Slot: " + cboApptSlot.SelectedItem.Text + "<br>";
        msg += "Eye: " + eye + "<br>";
        msg += "Reason for visit: " + txtReason.Text + "<br>";
        msg += "<BR><BR>Please fax your referral note to our fax number below.<BR>";
        msg += "<BR><BR>Facility Contact Information:<BR>";
        string details = API.Session.GetFacilityContactDetails(API.Session.SWState, API.Session.SWCPSFacilityID);
        if(details!= null)
        {
            string[] fields = details.Split('^');
            if(fields!= null)
            {
                if (fields.Length > 0)
                {
                    EHP.Formatting h = new EHP.Formatting();
                    msg += "Address: " + fields[0] + "<br>";
                    msg += "City, State, Zip: " + fields[1] + ", " + fields[2] + " " + fields[3] + "<br>";
                    if(fields[4]!="") msg += "Phone: " + h.PhoneNumber(fields[4]) + "<BR>";
                    if(fields[5]!="") msg += "Fax: " + h.PhoneNumber(fields[5]) + "<BR>";
                }
            }
        }
        msg += "</body></html>";
        API.Session.SendEmail("", API.Session.Email, subject, msg);
    }

}