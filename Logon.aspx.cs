using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Xml;
using System.Security.Cryptography;
using System.Text;

public partial class Logon : System.Web.UI.Page
{

    /// <summary>
    /// This is the logon page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>


    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.InvalidLogonError += Err;
        API.Session.UpdateLastLogonDateError += Err; 
        API.Session.UserLogonError += Err;
        API.Session.GetGUIDError += Err;
        litError.Text = "";
        //  
       // Encrypt_MasterUsersPassword();

        if (API.Session.LoggedOn) OpenLoginScreen();//Response.Redirect("./Default.aspx");
        txtEmail.Focus();

    }

    private void OpenLoginScreen()
    { 
        //API.Session.LoggedOn = true;
        Response.Redirect("https://platform.carecloud.com/oauth2/authorize?client_id="+Statics.ClientID+"&response_type=code&redirect_uri="+Statics.RedirectUrl);

    }

    protected void btnLogon_Click(object sender, EventArgs e)
    {
        if (ValidateData())
        {
            API.Session.Email = txtEmail.Text;
            if (API.Session.UserLogon(txtEmail.Text, txtPassword.Text))
            {
                API.Session.UpdateLastLogonDate(API.Session.UserID);
                if (txtPassword.Text == Statics.NewPassword)
                {
                    Response.Redirect("./ChangePassword.aspx");
                }
                else
                {
                    if (API.Session.IsAdmin) Response.Redirect("./Admin/Default.aspx");
                    else //OpenLoginScreen();
                    Response.Redirect("./");
                }
            }
        }



        #region Create Appointment
        ////Post API 
        //var responseString = "";
        //WebRequest PostRequest = WebRequest.Create(Statics.URL_POSTCreateAppointment);
        //PostRequest.Method = "POST";
        //PostRequest.ContentType = "application/json";
        //PostRequest.Headers.Add("Authorization", API.Session.AccessToken);
        //string postData = "{\"appointment\": {\"start_time\": \"2022-04-23\", \"end_time\": \"2022-04-23\", \"appointment_status_id\": \"1\", \"location_id\":17811, \"provider_id\":25673, \"visit_reason_id\":82266, \"resource_id\":25323, \"chief_complaint\": \"string\",\"comments\": \"string\", \"patient\": {\"id\": \"3d29e42e-97d4-40a4-a36a-a76111bdca9d\"}}}";
        ////PostRequest.Credentials = new NetworkCredential("", "");
        //using (var streamWriter = new StreamWriter(PostRequest.GetRequestStream()))
        //{
        //    streamWriter.Write(postData);
        //    streamWriter.Flush();
        //    streamWriter.Close();

        //    var httpWebResponse2 = (HttpWebResponse)PostRequest.GetResponse();
        //    using (Stream reader = httpWebResponse2.GetResponseStream())
        //    {
        //        StreamReader sr = new StreamReader(reader);
        //        responseString = sr.ReadToEnd();
        //        sr.Close();
        //    }
        //}
        #endregion

        #region Get Access Token using Practice Id 
        //if (ValidateData())
        //{
        //    API.Session.Email = txtEmail.Text;

        //    //Get Access Token using Practice Id 

        //    string URI = Statics.URL_GETAccessToken;
        //    string myParameters = "practice_id=" + Statics.PracticeID + "&grant_type=password&user_name=" + txtEmail.Text + "&password=" + txtPassword.Text;
        //    using (WebClient wc = new WebClient())
        //    {
        //        wc.Headers.Add("Authorization", Statics.AuthorizationKey);
        //        wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        //        string HtmlResult = wc.UploadString(URI, myParameters);
        //        API.Session.LoggedOn = true;
        //        DataTable dt = JsonConvert.DeserializeObject<DataTable>("[" + HtmlResult + "]");

        //        API.Session.AccessToken = "Bearer " + dt.Rows[0][0].ToString();
        //    }

        //    //    Response.Redirect("./");

        //}
        #endregion

        #region Get Facility
        //var responseString = "";
        //WebRequest request = WebRequest.Create(Statics.URL_GETFAcility);
        //request.Method = "GET";
        //request.ContentType = "application/json";
        //request.Headers.Add("Authorization", API.Session.AccessToken);
        //HttpWebResponse httpWebResponse = null;
        //httpWebResponse = (HttpWebResponse)request.GetResponse();

        //using (Stream reader = httpWebResponse.GetResponseStream())
        //{
        //    StreamReader sr = new StreamReader(reader);
        //    responseString = sr.ReadToEnd();
        //    sr.Close();
        //}

        //try
        //{
        //    XmlDocument xd = new XmlDocument();
        //    responseString = "{ \"rootNode\": {" + responseString.Trim().TrimStart('{').TrimEnd('}') + "} }";
        //    xd = (XmlDocument)JsonConvert.DeserializeXmlNode(responseString);
        //    DataSet ds = new DataSet();
        //    ds.ReadXml(new XmlNodeReader(xd));
        //    //  return ds; 
        //}
        //catch (Exception ex)
        //{ throw new ArgumentException(ex.Message); }



        #endregion

        #region Get ApptType
        //var responseString1 = "";
        //WebRequest request1 = WebRequest.Create(Statics.URL_GETApptType);
        //request1.Method = "GET";
        //request1.ContentType = "application/json";
        //request1.Headers.Add("Authorization", API.Session.AccessToken);
        //HttpWebResponse httpWebResponse1 = null;
        //httpWebResponse1 = (HttpWebResponse)request1.GetResponse();

        //using (Stream reader = httpWebResponse1.GetResponseStream())
        //{
        //    StreamReader sr = new StreamReader(reader);
        //    responseString1 = sr.ReadToEnd();
        //    sr.Close();
        //}


        //try
        //{
        //    if (!(responseString1.Trim().StartsWith("{")) && (!responseString1.Trim().EndsWith("}")))
        //        {
        //        responseString1 = "{"+responseString1+"}";
        //    }
        //    XmlDocument xd = new XmlDocument();
        //    responseString1 = "{ \"rootNode\": {\"root\":" + responseString1.Trim().TrimStart('{').TrimEnd('}') + "} }";
        //    xd = (XmlDocument)JsonConvert.DeserializeXmlNode(responseString1);
        //    DataSet ds = new DataSet();
        //    ds.ReadXml(new XmlNodeReader(xd));
        //    //  return ds; 
        //}
        //catch (Exception ex)
        //{ throw new ArgumentException(ex.Message); }









        #endregion

        #region Get Doctors
        //var doctorResponse = "";
        WebRequest doctorRequest = WebRequest.Create(Statics.URL_GETDoctors);
        //doctorRequest.Method = "GET";
        //doctorRequest.ContentType = "application/json";
        //doctorRequest.Headers.Add("Authorization", API.Session.AccessToken);
        //HttpWebResponse doctorHttpWebResponse = null;
        //doctorHttpWebResponse = (HttpWebResponse)doctorRequest.GetResponse();

        //using (Stream reader = doctorHttpWebResponse.GetResponseStream())
        //{
        //    StreamReader sr = new StreamReader(reader);
        //    doctorResponse = sr.ReadToEnd();
        //    sr.Close();
        //}


        //try
        //{
        //    XmlDocument xd = new XmlDocument();
        //    doctorResponse = "{ \"rootNode\": {" + doctorResponse.Trim().TrimStart('{').TrimEnd('}') + "} }";
        //    xd = (XmlDocument)JsonConvert.DeserializeXmlNode(doctorResponse);
        //    DataSet ds = new DataSet();
        //    ds.ReadXml(new XmlNodeReader(xd));
        //    //  return ds; 
        //}
        //catch (Exception ex)
        //{ throw new ArgumentException(ex.Message); }









        #endregion
         
        #region GetLocation
          
        //var locatonResponse = "";
        //WebRequest locationRequest = WebRequest.Create(Statics.URL_GETLocation);
        //locationRequest.Method = "GET";
        //locationRequest.ContentType = "application/json";
        //locationRequest.Headers.Add("Authorization", API.Session.AccessToken);
        //HttpWebResponse locatonHttpWebResponse = null;
        //locatonHttpWebResponse = (HttpWebResponse)locationRequest.GetResponse();

        //using (Stream reader = locatonHttpWebResponse.GetResponseStream())
        //{
        //    StreamReader sr = new StreamReader(reader);
        //    locatonResponse = sr.ReadToEnd();
        //    sr.Close();
        //}


        //try
        //{
        //    XmlDocument xd = new XmlDocument();
        //    locatonResponse = "{ \"rootNode\": {" + locatonResponse.Trim().TrimStart('{').TrimEnd('}') + "} }";
        //    xd = (XmlDocument)JsonConvert.DeserializeXmlNode(locatonResponse);
        //    DataSet ds = new DataSet();
        //    ds.ReadXml(new XmlNodeReader(xd));
        //    //  return ds; 
        //}
        //catch (Exception ex)
        //{ throw new ArgumentException(ex.Message); }
        #endregion

        #region ApptSlots
        //var ApptSlotsResponse = "";
        //WebRequest ApptSlotsRequest = WebRequest.Create(Statics.URL_GETApptSlots);
        //ApptSlotsRequest.Method = "GET";
        //ApptSlotsRequest.ContentType = "application/json";
        //ApptSlotsRequest.Headers.Add("Authorization", API.Session.AccessToken);
        //HttpWebResponse ApptSlotsHttpWebResponse = null;
        //ApptSlotsHttpWebResponse = (HttpWebResponse)ApptSlotsRequest.GetResponse(); 

        //using (Stream reader = ApptSlotsHttpWebResponse.GetResponseStream())
        //{
        //    StreamReader sr = new StreamReader(reader);
        //    ApptSlotsResponse = sr.ReadToEnd();
        //    sr.Close();
        //}


        //try
        //{
        //    XmlDocument xd = new XmlDocument();
        //    ApptSlotsResponse = "{ \"rootNode\": {\"root\":" + ApptSlotsResponse.Trim().TrimStart('{').TrimEnd('}') + "} }";
        //    xd = (XmlDocument)JsonConvert.DeserializeXmlNode(ApptSlotsResponse);
        //    DataSet ds = new DataSet();
        //    ds.ReadXml(new XmlNodeReader(xd));
        //    //  return ds; 
        //}
        //catch (Exception ex)
        //{ throw new ArgumentException(ex.Message); }
        #endregion

        #region login
        // var loginResponse = "";
        //// System.Diagnostics.Process.Start("https://platform.carecloud.com/oauth2/authorize?client_id=jlNINTKlquSsTtTkBnfJVFtbL80LWuXS&response_type=code&redirect_uri=https://localhost:44383/ScheuleAppointment.aspx");
        // WebRequest loginRequest = WebRequest.Create("https://platform.carecloud.com/oauth2/authorize?client_id=jlNINTKlquSsTtTkBnfJVFtbL80LWuXS&internal_api_key=INTERNAL_SERVICE&internal_request=true&redirect_uri=https%3A%2F%2Flocalhost%3A44383%2FScheuleAppointment.aspx&response_type=code");
        // loginRequest.Method = "GET";
        // loginRequest.ContentType = "application/json";
        // loginRequest.Credentials = new NetworkCredential("udixit@pipartners.com", "Uma@carecloud123");
        //// loginRequest.Headers.Add("Authorization", API.Session.AccessToken);
        // HttpWebResponse loginHttpWebResponse = null;
        // loginHttpWebResponse = (HttpWebResponse)loginRequest.GetResponse();

        // using (Stream reader = loginHttpWebResponse.GetResponseStream())
        // {
        //     StreamReader sr = new StreamReader(reader);
        //     loginResponse = sr.ReadToEnd();
        //     sr.Close();
        // }


        //try
        //{
        //    XmlDocument xd = new XmlDocument();
        //    loginResponse = "{ \"rootNode\": {" + loginResponse.Trim().TrimStart('{').TrimEnd('}') + "} }";
        //    xd = (XmlDocument)JsonConvert.DeserializeXmlNode(loginResponse);
        //    DataSet ds = new DataSet();
        //    ds.ReadXml(new XmlNodeReader(xd));
        //    //  return ds; 
        //}
        //catch (Exception ex)
        //{ throw new ArgumentException(ex.Message); }
        #endregion

    }
    public static DataTable Tabulate(string jsonContent)
    {

        var jsonLinq = JObject.Parse(jsonContent);

        // Find the first array using Linq
        var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First();
        var trgArray = new JArray();
        foreach (JObject row in srcArray.Children<JObject>())
        {
            var cleanRow = new JObject();
            foreach (JProperty column in row.Properties())
            {
                // Only include JValue types
                if (column.Value is JValue)
                {
                    cleanRow.Add(column.Name, column.Value);
                }
            }

            trgArray.Add(cleanRow);
        }

        return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("./");
    }
    protected void lbSignUp_Click(object sender, EventArgs e)
    {
        Response.Redirect("./Signup.aspx");
    }
    protected void lbForgot_Click(object sender, EventArgs e)
    {
        Response.Redirect("./ForgotLogon.aspx");
    }
    protected void lbResend_Click(object sender, EventArgs e)
    {
        if (txtEmail.Text == "") { Err(1058,"Email Address must be provided to resend Validation Email."); return; }
        Response.Redirect("./ResendValidation.aspx?t=validate&g=" + API.Session.GetGUID(txtEmail.Text));
    }
    protected bool ValidateData()
    {
        bool bReturn = true;
        if (txtEmail.Text == "") { Err(1059,"Email address cannot be blank."); bReturn = false; }
        if (txtPassword.Text == "") { Err(1060,"Password cannot be blank."); bReturn = false; }
        return bReturn;
    }
    private void Err(int num, string msg)
    {
        litError.Text += "Error " + num.ToString() + " - " + msg + "<br />";
    }



    protected void btnCareCloudLogon_Click(object sender, EventArgs e)
    {
        OpenLoginScreen();
       
    }

   

    protected void Encrypt_MasterUsersPassword()
    {
        try {
            DataTable dt = new DataTable();
            dt = API.Session.GetCareCloudMasterUserList();
            foreach (DataRow row in dt.Rows) 
            {
              string password=API.Session.Encrypt (row["password"].ToString().Trim());
                int id =int.Parse( row["id"].ToString());
               API.Session.UpdateCareCloudMasterUserPassword(password,id);
               
            }
        } catch (Exception ex) { }
    }
}