using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CareCloudLogin : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
       // Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (!IsPostBack)
        {
            string password = API.Session.Encrypt(("RppETN2023@").ToString().Trim());
            RefreshLoginMode();
            
        }
        
    }

    

    protected void ddlLoginMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        string practiceId = "";
        API.Session.PracticeId = "";
        try
        {
            practiceId=ddlLoginMode.SelectedValue;
            DataTable dt = new DataTable();
            dt = API.Session.GetCareCloudMasterUserList();
            foreach (DataRow row in dt.Rows)
            {
                if (row["PracticeId"].ToString() == practiceId)
                {
                    API.Session.PracticeId = row["PracticeId"].ToString();
                    API.Session.UserName = row["email"].ToString();
                    string password=API.Session.Decrypt(row["password"].ToString());
                    API.Session.Password = password;
                    API.Session.IsState = row["state"].ToString();
                    API.Session.PracticeName = row["PracticeName"].ToString();
                    API.Session.ReferralDoctorValue = 0;
                    API.Session.FacilityValue = 0;
                    API.Session.DoctorValue = 0;
                    API.Session.AppointmentTypeValue = 0;
                    API.Session.SloatValue = 0;
                }
                    
            }
            if (API.Session.PracticeId != "" || API.Session.PracticeId != "0")
            {
                GetAccesstoken();
            }

        }
        catch (Exception ex){ }        
    }

    private void RefreshLoginMode()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = API.Session.GetCareCloudMasterUserList();
            string state = "";
            bool IsStateAvailable = false;
            API.Location[] data = API.Session.GetLocationsforTN(API.Session.Email);//API.Session.GUID
            ddlLoginMode.Items.Clear();
            ddlLoginMode.Items.Add(new ListItem("Select Your Location", "0"));
            if (data != null)
            {
                //for (int a = 0; a < data.Length; a++)
                //{

                // if (data[a].Database == "Birmingham") state = "AL";
                // if (data[a].Database == "Nashville") state = "IT";
                //if (data[a].Database == "Nashville") state = "TN";
                //if (data[a].Database == "EHPortal") state = "TN";
                //if (data[a].Database == "EHPortal") state = "AL";
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        //if (data[a].Database == "EHPortal")
                        //{
                        if (API.Session.IsAL == true && API.Session.IsMiddleTN == true && API.Session.IsEastTN == true)// && API.Session.IsEastTN == true
                        {
                            if (dr["state"].ToString() == "TN" || dr["state"].ToString() == "AL" || dr["state"].ToString() == "ETN")
                            {
                                IsStateAvailable = true;

                                ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                            }
                        }
                        else if (API.Session.IsAL == true && API.Session.IsMiddleTN == true && API.Session.IsEastTN != true)// && API.Session.IsEastTN == true
                        {
                            if (dr["state"].ToString() == "TN" || dr["state"].ToString() == "AL")
                            {
                                IsStateAvailable = true;

                                ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                            }
                        }

                        else if (API.Session.IsAL != true && API.Session.IsMiddleTN == true && API.Session.IsEastTN == true)// (API.Session.IsMiddleTN == true || API.Session.IsEastTN == true)
                        {
                            if (dr["state"].ToString() == "TN" || dr["state"].ToString() == "ETN")
                            {
                                IsStateAvailable = true;

                                ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                            }
                        }
                        else if (API.Session.IsAL != true && API.Session.IsMiddleTN == true && API.Session.IsEastTN != true)// (API.Session.IsMiddleTN == true || API.Session.IsEastTN == true)
                        {
                            if (dr["state"].ToString() == "TN")
                            {
                                IsStateAvailable = true;

                                ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                            }
                        }
                        else if (API.Session.IsAL != true && API.Session.IsMiddleTN != true && API.Session.IsEastTN == true)// (API.Session.IsMiddleTN == true || API.Session.IsEastTN == true)
                        {
                            if (dr["state"].ToString() == "ETN")
                            {
                                IsStateAvailable = true;

                                ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                            }
                        }
                        else if (API.Session.IsAL == true && API.Session.IsMiddleTN != true && API.Session.IsEastTN == true)// && (API.Session.IsMiddleTN != true || API.Session.IsEastTN != true))
                        {
                            if (dr["state"].ToString() == "AL" || dr["state"].ToString() == "ETN")
                            {
                                IsStateAvailable = true;

                                ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                            }
                        }
                        else if (API.Session.IsAL == true && API.Session.IsMiddleTN != true && API.Session.IsEastTN != true)// && (API.Session.IsMiddleTN != true || API.Session.IsEastTN != true))
                        {
                            if (dr["state"].ToString() == "AL")
                            {
                                IsStateAvailable = true;

                                ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                            }
                        }
                        //if (dr["state"].ToString() == "TN" || dr["state"].ToString() == "AL")
                        //        {
                        //            if(API.Session.IsMiddleTN==true || API.Session.IsAL==true || API.Session.IsEastTN == true)
                        //           {
                        //            IsStateAvailable = true;

                        //            ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                        //           }
                        //    //if (API.Session.IsState == dr["state"].ToString())
                        //    //{
                        //    //    IsStateAvailable = true;

                        //    //    ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                        //    //}
                        //    //else if (API.Session.IsState == "" || API.Session.IsState==null)
                        //    //{
                        //    //    IsStateAvailable = true;

                        //    //    ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                        //    //}

                        //}
                        // }


                    }
                }
                // }
            }
            if (IsStateAvailable == false)
            { Response.Redirect("~/ScheduleAppt.aspx"); }
        }
        catch (Exception ex) { }
    }

    //protected void RefreshLoginMode()
    //{
    //    ddlLoginMode.Items.Clear();
    //    ddlLoginMode.Items.Add(new ListItem("Select Your Location", "0"));

    //    DataTable dt1 = new DataTable();
    //    dt1 = API.Session.GetLoginModes();

    //    if (dt1.Rows.Count > 0)
    //    {

    //        foreach (DataRow dr1 in dt1.Rows)
    //        {
    //            if (dr1["SEESEntityID"].ToString() != "")
    //            {
    //                ddlLoginMode.Items.Add(new ListItem(dr1["SEESEntityName"].ToString(), dr1["SEESEntityID"].ToString()));
    //            }
    //        }
    //    }
    //}

    private void GetAccesstoken()
    {
         //Get Access Token using Practice Id 
        API.Session.AccessToken = "";
        string URI = Statics.URL_GETAccessToken;
        string myParameters = "practice_id=" + API.Session.PracticeId + "&grant_type=password&user_name=" + API.Session.UserName + "&password=" + API.Session.Password;
        using (WebClient wc = new WebClient())
        {
            wc.Headers.Add("Authorization", Statics.AuthorizationKey);
            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            string HtmlResult = wc.UploadString(URI, myParameters);
            API.Session.LoggedOn = true;
            DataTable dt = JsonConvert.DeserializeObject<DataTable>("[" + HtmlResult + "]");

            API.Session.AccessToken = "Bearer " + dt.Rows[0][0].ToString();
        }
            Response.Redirect("~/ScheduleApptWithCareCloud.aspx");


    }

    




    protected void LnkBtnCareCloudLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://platform.carecloud.com/oauth2/authorize?client_id=" + Statics.ClientID + "&response_type=code&redirect_uri=" + Statics.RedirectUrl);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/");
    }
}