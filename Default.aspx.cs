using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{

    #region Objects / Variables
    #endregion

    #region Event Handlers
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        if (url.Contains("?"))
        {
              var result = url.Substring(url.LastIndexOf('=') + 1);
            API.Session.AutherisationCode= result;
        }
    }
    #endregion




    protected void BtnAppointment_Click(object sender, EventArgs e)
    {
        //GetAccesstoken();
        CreateAppointment();

    }
    private void GetAccesstoken()
    {

       

        //Get Access Token using Practice Id 

        string URI = Statics.URL_GETAccessToken;
        string myParameters = "code=" + API.Session.AutherisationCode + "&grant_type=authorization_code&redirect_uri=" + Statics.RedirectUrl;
        using (WebClient wc = new WebClient())
        {
            wc.Headers.Add("Authorization", Statics.AuthorizationKey);
            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            string HtmlResult = wc.UploadString(URI, myParameters);
            API.Session.LoggedOn = true;
            DataTable dt = JsonConvert.DeserializeObject<DataTable>("[" + HtmlResult + "]");

            API.Session.AccessToken = "Bearer " + dt.Rows[0][0].ToString();
        }

        //    Response.Redirect("./");


    }

    private void CreateAppointment()
    {
        var responseString = "";
        WebRequest PostRequest = WebRequest.Create(Statics.URL_POSTCreateAppointment);
        PostRequest.Method = "POST";
        PostRequest.ContentType = "application/json";
        PostRequest.Headers.Add("Authorization", API.Session.AccessToken);
        string postData = "{\"appointment\": {\"start_time\": \"2022-04-27\", \"end_time\": \"2022-04-27\", \"appointment_status_id\": \"1\", \"location_id\":17811, \"provider_id\":25673, \"visit_reason_id\":82266, \"resource_id\":25323, \"chief_complaint\": \"string\",\"comments\": \"string\", \"patient\": {\"id\": \"3d29e42e-97d4-40a4-a36a-a76111bdca9d\"}}}";
        //PostRequest.Credentials = new NetworkCredential("", "");
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
            Label1.Text= responseString;
            Label1.Visible= true;
        }
    }
}