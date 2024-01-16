using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public static class Statics
{

    public const string EHPconnstring = "Server=SEES-DEV; Database=EHPortal; UID=RPPDatabaseUser; PWD=RPPDatabaseUser@UAT;Connect Timeout=200; pooling='true'; Max Pool Size=200";
    //public const string EHPconnstring = "Server=SEES-DEV; Database=EHPortal; UID=umad; PWD=EMRUser2003;Connect Timeout=200; pooling='true'; Max Pool Size=200";
    //public const string EHPconnstring = "Server=EHP-CTY-SQL02; Database=EHPortal; UID=RPPDatabaseUser; PWD=*$mkHijB863kHN0o7F$w;Connect Timeout=200; poo+ling='true'; Max Pool Size=200";
    public const string CPSALconnstring = "Server=SEES-DEV; Database=Birmingham; UID=umad; PWD=EMRUser2003;Connect Timeout=200; pooling='true'; Max Pool Size=200";
    public const string CPSTNconnstring = "Server=SEES-DEV; Database=Nashville; UID=umad; PWD=EMRUser2003;Connect Timeout=200; pooling='true'; Max Pool Size=200";
    public const string DEMOconnstring = "Server=SEES-DEV; Database=DEMO;UID=umad;PWD=EMRUser2003;Connect Timeout=200; pooling='true'; Max Pool Size=200";
    public const string FROMEMAIL = "rppdonotreply@theseesgroup.com";
    public const string EMAILUSERNAME = "rppdonotreply@theseesgroup.com";
    public const string EMAILPASSWORD = "Nuj39590";
    public const string EMAILDOMAIN = "SEESGROUP.com";
    public const string SMTPHOST = "smtp-legacy.office365.com";
    // Had to change SMTPPORT from port 587 to port 25 due to the email stopped working.
    // Guessing C Spire started blocking port 587 for some reason.
    public const int SMTPPORT = 25; 
    public const int MinPwdLength = 6;
    public static string[] States = new string[] { "AL", "TN" };
    public static string[] ConnStrings = new string[] { CPSALconnstring, CPSTNconnstring };
    public static int SWMinimumDaysOut= 5;
    public static int SWInitNumApptSlots = 10;
    public static int SWMoreNumApptSlots = 200;
    public static string NewPassword = "*G4n%H#m@D"; //This is the default new password that we put on user accounts.

    private const string C_DOUBLECHAR = "0123456789";
    private const string C_ALPHACHAR = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string C_UPPERALPHACHAR = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string C_LOWERALPHACHAR = "abcdefghijklmnopqrstuvwxyz";
    private const string C_SYMBOLCHAR = " `~!@#$%^&*()-_=+[{]}\\^;:'\",<.>?/";

    public static char[] caDouble = C_DOUBLECHAR.ToCharArray();
    public static char[] caAlpha = C_ALPHACHAR.ToCharArray();
    public static char[] caUpperAlpha = C_UPPERALPHACHAR.ToCharArray();
    public static char[] caLowerAlpha = C_LOWERALPHACHAR.ToCharArray();
    public static char[] caSymbol = C_SYMBOLCHAR.ToCharArray();

    public delegate void BasicErrorHandler(int ErrorNumber, string Msg);
    public delegate void Basic1ErrorHandler( string Msg);

    //API Login
    //public const string PracticeID = "af5eb21e-8e61-4368-825d-6e06cfb17270";
    public const string AuthorizationKey = "Bearer amxOSU5US2xxdVNzVHRUa0JuZkpWRnRiTDgwTFd1WFM6a2UxQ3FqTVdNMHRSVzdPRA==";
    public const string ClientID = "jlNINTKlquSsTtTkBnfJVFtbL80LWuXS";
    public const string RedirectUrl= "http://localhost:54513/ScheduleApptWithCareCloud.aspx";

    


    //API URL's
    public const string URL_GETLocation = "https://api.carecloud.com/v2/physicians/1063891216";//by NPI
    public const string URL_POSTCreateAppointment = "https://api.carecloud.com/v2/appointments";
    public const string URL_GETFAcility = "https://api.carecloud.com/v2/locations";
    public const string URL_GETAccessToken = "https://api.carecloud.com/oauth2/access_token";
    public const string URL_GETDoctors = "https://api.carecloud.com/v2/appointment_resources";
    public const string URL_GETApptType = "https://api.carecloud.com/v2/appointment_resources/";//https://api.carecloud.com/v2/appointment_templates";//"https://api.carecloud.com/v2/visit_reasons?request_types_only=true"; 
    public const string URL_GETApptSlots = "https://api.carecloud.com/v2/appointment_availability?";//"https://api.carecloud.com/v2/appointment_availability?start_date=2021-03-25&visit_reason_id=82266&location_ids=17811&resource_ids=25323&end_date=2021-03-25";
    public const string URL_CreatePatient = "https://api.carecloud.com/v2/patients";
    public const string URL_ShowPatient = "https://api.carecloud.com/v2/patients/";
    public const string URL_GETPatient = "";
    public const string URL_GETRefLocation = "https://api.carecloud.com/v2/referral_location/search?";
}