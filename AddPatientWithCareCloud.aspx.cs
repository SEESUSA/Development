using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class AddPatientWithCareCloud : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.PatientExistsError += Err;
        API.Session.CreateNewPatientError += Err;
        API.Session.TransferPatientReferringDoctorError += Err;
        API.Session.GetSignatureSourceMIDError += Err;//ValidationErr
        API.Session.ValidateNewPatientError += ValidationErr;

        litError.Text = "";
        if (!IsPostBack)
        {
            RefreshGender();
            GVpatients.DataSource = null;
            GVpatients.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateData())
        {
            CreatePatient();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtFirstName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        cboGender.SelectedIndex = 0;
        txtDOB.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtSSN.Text = string.Empty;
        Response.Redirect("~/ScheduleApptWithCareCloud.aspx");
    }
    protected void RefreshGender()
    {
        cboGender.Items.Clear();
        cboGender.Items.Add(new ListItem("Select Gender", "0"));
        cboGender.Items.Add(new ListItem("Male", "Male"));
        cboGender.Items.Add(new ListItem("Female", "Female"));
        cboGender.Items.Add(new ListItem("Unknown", "Unknown"));
    }

    protected void CreatePatient()
    {
        Regex regex = new Regex(@"[^\d]");
        string phonenum = regex.Replace(txtPhone.Text, "");
        string GetSSN = regex.Replace(txtSSN.Text, "");
        ////Post API 
        var responseString = "";
            WebRequest PostRequest = WebRequest.Create(Statics.URL_CreatePatient);
            PostRequest.Method = "POST";
            PostRequest.ContentType = "application/json";
            PostRequest.Headers.Add("Authorization", API.Session.AccessToken);
            string address = string.Empty;
        string StLocation = string.Empty;
        string location = API.Session.ChState.ToString();// "AL";
        if (API.Session.ChState.ToString() == "ETN")
        {
             StLocation = "TN";
        }
        else
        {
             StLocation = API.Session.ChState.ToString();
        }
       
            string gender = string.Empty;
        //API.Session.GUID
        string[] data = API.Session.GetNPI(API.Session.Email);
        string NPI1 = "";
        if (data != null)
        {
            NPI1 = data[0];
        }
        //string[] Locarray = API.Session.refDocval.Split('^');
        //string postData = "{\"patient\":{\"first_name\":" + "\""+ txtFirstName.Text + "\"" + ",\"last_name\":" + "\""+ txtLastName.Text + "\"" + "},\"addresses\":[{\"line1\":"+ "\"" + address + "\"" + ",\"line2\":\"\",\"city\":\"ABC\",\"state\":" + "\"" + location + "\"" + ",\"zip_code\":\"00000\",\"country_name\":\"USA\",\"is_primary\":true}],\"phones\":[{\"phone_number\":" + "\"" + txtPhone.Text + "\"" + ",\"phone_type_code\":\"2\",\"extension\":\"12\",\"is_primary\":true}]}";
        DateTime originaldate;
        DateTime.TryParse(txtDOB.Text, out originaldate);
        //string postData = "{\"patient\":{\"first_name\":" + "\"" + txtFirstName.Text + "\",\"last_name\":" + "\"" + txtLastName.Text + "\",\"gender_code\":" + "\"" + cboGender.SelectedValue.ToString() + "\",\"date_of_birth\":" + "\"" + txtDOB.Text + "\"},\"addresses\":[{\"line1\":\"abc\",\"line2\":\"\",\"city\":\"ABC\",\"state\":" + "\"" + location + "\",\"zip_code\":\"00000\",\"country_name\":\"USA\",\"is_primary\":true}],\"phones\":[{\"phone_number\":" + "\"" + txtPhone.Text + "\",\"phone_type_code\":\"M\",\"is_primary\":true}]}";
        //string postData = "{\"patient\":{\"first_name\":" + "\"" + txtFirstName.Text + "\",\"last_name\":" + "\"" + txtLastName.Text + "\",\"gender_code\":" + "\"" + cboGender.SelectedValue.ToString() + "\",\"date_of_birth\":" + "\"" + txtDOB.Text + "\",\"ssn\":" + "\"" + txtSSN.Text + "\",\"referring_physician_npi\":" + Convert.ToInt32(data[0]) +"},\"addresses\":[{\"line1\":\"abc\",\"line2\":\"\",\"city\":\"ABC\",\"state\":" + "\"" + location + "\",\"zip_code\":\"00000\",\"country_name\":\"USA\",\"is_primary\":true}],\"phones\":[{\"phone_number\":" + "\"" + phonenum + "\",\"phone_type_code\":\"M\",\"is_primary\":true}]}";
        string postData = "{\"patient\":{\"first_name\":" + "\"" + txtFirstName.Text + "\",\"last_name\":" + "\"" + txtLastName.Text + "\",\"gender_code\":" + "\"" + cboGender.SelectedValue.ToString() + "\",\"date_of_birth\":" + "\"" + originaldate.ToString("yyyy-MM-dd") + "\" "+ (string.IsNullOrEmpty(txtSSN.Text) || txtSSN.Text=="" ? "" : ",\"ssn\":" + "\"" + GetSSN + "\"") + "},\"addresses\":[{\"line1\":\"abc\",\"line2\":\"\",\"city\":\"ABC\",\"state\":" + "\"" + StLocation + "\",\"zip_code\":\"00000\",\"country_name\":\"USA\",\"is_primary\":true}],\"phones\":[{\"phone_number\":" + "\"" + phonenum + "\",\"phone_type_code\":\"C\",\"is_primary\":true}]}";
        
        using (var streamWriter = new StreamWriter(PostRequest.GetRequestStream()))
            {
                streamWriter.Write(postData);
               streamWriter.Flush();
                streamWriter.Close();

                var httpWebResponse2 = (HttpWebResponse)PostRequest.GetResponse();
                using (Stream reader = httpWebResponse2.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(reader);
                    responseString = sr.ReadToEnd();
                    sr.Close();
                }
            }
            DataSet dataSet = new DataSet();
            XmlDocument xd = new XmlDocument();
            //doctorResponse = "{ \"rootNode\": {" + doctorResponse.Trim().TrimStart('{').TrimEnd('}') + "} }";
            xd = (XmlDocument)JsonConvert.DeserializeXmlNode(responseString);

            API.Session.PatientId = xd.InnerText;
            var Result = API.Session.CreatePatient(xd.InnerText,txtFirstName.Text, txtLastName.Text, cboGender.SelectedValue.ToString(), Convert.ToDateTime(txtDOB.Text), Convert.ToString(NPI1), "abc", API.Session.CityName.ToString(), location, "00000", "USA", phonenum, txtSSN.Text);
        if (Result == "true" || Result == "")
        {
            API.Session.PatientPhone = phonenum;
            Server.Transfer("~/ScheduleApptWithCareCloud.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();

        }
        //else
        //{
        //    Response.Redirect("~/AddPatientWithCareCloud.aspx");


        //}

    }

    protected void Err(int number, string msg)
    {
        litError.Text += "Error: "+ msg + "<br /><br />";//" + number.ToString() + " - " 
    }

    protected void ValidationErr(string msg)
    {
        litError.Text += msg + "<br />";
    }

    protected bool ValidateData()
    {

        DataTable dt = new DataTable();
        if (txtFirstName.Text == "") { Err(1112, "You must provide the First Name."); return false; }
        if (txtLastName.Text == "") { Err(1113, "You must provide the Last Name."); return false; }
        if (cboGender.SelectedIndex == 0) { Err(1114, "You must select the Gender first."); return false; }
        if (txtDOB.Text == "") { Err(1115, "You must provide the Date of Birth."); return false; }
        if (txtPhone.Text == "") { Err(1116, "You must provide the Phone number."); return false; }
        Regex regex = new Regex(@"[^\d]");
        string phonenum = regex.Replace(txtPhone.Text, "");
        string location = API.Session.ChState.ToString();
        if (Convert.ToInt32(phonenum.Length) < 10 || Convert.ToInt32(phonenum.Length) > 10) { Err(1116, "You must provide the valid Phone number."); return false; }
        var Result = API.Session.ValidatePatient(txtFirstName.Text, txtLastName.Text, cboGender.SelectedValue.ToString(), Convert.ToDateTime(txtDOB.Text), phonenum, txtSSN.Text, API.Session.CityName.ToString(), location);
        if (Result == "false" || Result=="")
        {

           
            DataTable dt1 = new DataTable();
            //string location = API.Session.ChState.ToString();
            dt = API.Session.GetPatient(txtFirstName.Text, txtLastName.Text, cboGender.SelectedValue.ToString(), Convert.ToDateTime(txtDOB.Text), phonenum, txtSSN.Text, API.Session.CityName.ToString(), location);
            //dt.Columns.AddRange(new DataColumn[3] { new DataColumn("PID"), new DataColumn("PatientSSN"), new DataColumn("PatientName") });
            //foreach (DataRow row in dt1.Rows)
            //{
            //    dt.Rows.Add(row["PID"].ToString(), row["PatientSSN"].ToString(), row["PatientName"].ToString());

            //}

            GVpatients.DataSource = dt;
            GVpatients.DataBind();
            return false;
        }
        else if(Result == "SSNExists")
        {
            dt = null;
            GVpatients.DataSource = dt;
            GVpatients.DataBind();
            return false;
        }
        else
        {
            return true;
        }

    }


    protected void GVpatients_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVpatients.Rows[rowIndex]; 
            //Get the value of column from the DataKeys using the RowIndex.
            API.Session.patientId = Convert.ToString(GVpatients.DataKeys[rowIndex].Values[0]);
            API.Session.PatientPhone = Convert.ToString(row.Cells[3].Text);
            Response.Redirect("~/ScheduleApptWithCareCloud.aspx");
        }
    }
}