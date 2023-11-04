using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

public class API
{
    //Private constructor 
    private API() { }
    public static API Session
    {
        get
        {
            API session = (API)HttpContext.Current.Session["__API__"];
            if (session == null)
            {
                session = new API();
                HttpContext.Current.Session["__API__"] = session;
            }
            return session;
        }

    }

    #region Enumerations
    public enum EmailType { Validate, PwdReset, UnlockAccount, VIPSend };
    public enum Eye { Left, Right, Both };
    #endregion

    #region Variables
    // ALL USERS
    private string email = "";
    public string Practicename = "";
    private Guid guid = Guid.Empty;
    private bool isadmin = false;
    private bool issetupadmin = false;
    private string isState = "";
    private string chState = "";
    private bool loggedon = false;
    private string npi = "";
    private int userid = 0;
    private bool middletn = false;
    private bool al = false;
    private bool easttn = false;

    // Schedule Wizard Use
    private string swapptslot = "";
    private string swappttype = "";
    private int swcpsapptslotid = 0;
    private int[] swcpsappttypeids = null;
    private int swcpsdoctorid = 0;
    private int swcpsfacilityid = 0;
    private string swdoctor = "";
    private int swehpappttypeid = 0;
    private int swehpdoctorid = 0;
    private int swehpfacilityid = 0;
    private CPSPatient swexistingpatient = null;
    private Eye sweye = Eye.Both;
    private string sweyetext = "Both";
    private string swfacility = "";
    private string swlocation = "";
    private int swlocationid = 0;
    private string swpatient = "";
    private int swpatientprofileid = 0;
    private string swstate = "";
    private string swreason = "";

    // Admin Section
    private string adminehpappttype = "";
    private int adminehpappttypeid = 0;
    private string adminehpcpsappttype = "";
    private int adminehpcpsappttypeid = 0;
    private string adminehpdoctor = "";
    private int adminehpdoctorid = 0;
    private string adminehpfacility = "";
    private int adminehpfacilitycontactid = 0;
    private int adminehpfacilityid = 0;
    private string admingecpsappttype = "";
    private int admingecpsappttypeid = 0;
    private int adminmasteraccountid = 0;
    private int adminchildaccountid = 0;
    private string adminstate = "";
    #endregion

    //API calling
    public string accessToken = "";
    public string autherisationCode = "";
    public string practiceid = "";
    public string username = "";
    public string password = "";
    public DataSet dataset = new DataSet();

    //Appointment Details
    public string facilityDetails = "";
    public string phone = "";
    public string fax = "";
    public string appointmentType = "";
    public string doctor = "";
    public string datetime = "";
    public string eye = "";
    public string referralDoctor = "";

    //Patient API
    public string patientId = "";
    public int appointmentTypeValue = 0;
    public int doctorValue = 0;
    public int sloatValue = 0;
    public int referralDoctorValue = 0;
    public int facilityValue = 0;
    public string phoneValue = "";
    public string refDocval = "";
    public string CityName = "";
    //For Email
    public string facilityContact = "";

    public int FacilityValue { get { return facilityValue; } set { facilityValue = value; } }
    public int ReferralDoctorValue { get { return referralDoctorValue; } set { referralDoctorValue = value; } }
    public int SloatValue { get { return sloatValue; } set { sloatValue = value; } }
    public int DoctorValue { get { return doctorValue; } set { doctorValue = value; } }
    public int AppointmentTypeValue { get { return appointmentTypeValue; } set { appointmentTypeValue = value; } }
    public string PatientPhone { get { return phoneValue; } set { phoneValue = value; } }

    #region Properties
    // ALL USERS
    public string Email { get { return email; } set { email = value; } }
    public string PracticeName { get { return Practicename; } set { Practicename = value; } }
    public Guid GUID { get { return guid; } set { guid = value; } }
    public bool IsAdmin { get { return isadmin; } set { isadmin = value; } }
    public bool IsSetupAdmin { get { return issetupadmin; } set { issetupadmin = value; } }
    public bool IsMiddleTN { get { return middletn; } set { middletn = value; } }
    public bool IsAL { get { return al; } set { al = value; } }
    public bool IsEastTN { get { return easttn; } set { easttn = value; } }
    public string IsState { get { return isState; } set { isState = value; } }
    public string ChState { get { return chState; } set { chState = value; } }
    public bool LoggedOn { get { return loggedon; } set { loggedon = value; } }
    public string NPI { get { return npi; } set { npi = value; } }
    public int UserID { get { return userid; } set { userid = value; } }
    // Schedule Wizard Use
    public string SWState { get { return swstate; } set { swstate = value; } }
    public int SWLocationID { get { return swlocationid; } set { swlocationid = value; } }
    public string SWLocation { get { return swlocation; } set { swlocation = value; } }
    public int SWEHPFacilityID { get { return swehpfacilityid; } set { swehpfacilityid = value; } }
    public int SWCPSFacilityID { get { return swcpsfacilityid; } set { swcpsfacilityid = value; } }
    public string SWFacility { get { return swfacility; } set { swfacility = value; } }
    public int SWEHPDoctorID { get { return swehpdoctorid; } set { swehpdoctorid = value; } }
    public int SWCPSDoctorID { get { return swcpsdoctorid; } set { swcpsdoctorid = value; } }
    public string SWDoctor { get { return swdoctor; } set { swdoctor = value; } }
    public int SWEHPApptTypeID { get { return swehpappttypeid; } set { swehpappttypeid = value; } }
    public int[] SWCPSApptTypeIDs { get { return swcpsappttypeids; } set { swcpsappttypeids = value; } }
    public string SWApptType { get { return swappttype; } set { swappttype = value; } }
    public int SWCPSApptSlotID { get { return swcpsapptslotid; } set { swcpsapptslotid = value; } }
    public string SWApptSlot { get { return swapptslot; } set { swapptslot = value; } }
    public int SWPatientProfileID { get { return swpatientprofileid; } set { swpatientprofileid = value; } }
    public string SWPatient { get { return swpatient; } set { swpatient = value; } }
    public Eye SWEye
    {
        get { return sweye; }
        set
        {
            sweye = value;
            if (value == Eye.Both) SWEyeText = "Both";
            if (value == Eye.Left) SWEyeText = "Left";
            if (value == Eye.Right) SWEyeText = "Right";
        }
    }
    public string SWEyeText { get { return sweyetext; } set { sweyetext = value; } }
    public CPSPatient SWExistingPatient { get { return swexistingpatient; } set { swexistingpatient = value; } }
    public string SWReason { get { return swreason; } set { swreason = value; } }

    // Admin section
    public string AdminState { get { return adminstate; } set { adminstate = value; } }
    public int AdminEHPFacilityID { get { return adminehpfacilityid; } set { adminehpfacilityid = value; } }
    public string AdminEHPFacility { get { return adminehpfacility; } set { adminehpfacility = value; } }
    public int AdminEHPDoctorID { get { return adminehpdoctorid; } set { adminehpdoctorid = value; } }
    public string AdminEHPDoctor { get { return adminehpdoctor; } set { adminehpdoctor = value; } }
    public int AdminEHPApptTypeID { get { return adminehpappttypeid; } set { adminehpappttypeid = value; } }
    public string AdminEHPApptType { get { return adminehpappttype; } set { adminehpappttype = value; } }
    public int AdminEHPCPSApptTypeID { get { return adminehpcpsappttypeid; } set { adminehpcpsappttypeid = value; } }
    public string AdminEHPCPSApptType { get { return adminehpcpsappttype; } set { adminehpcpsappttype = value; } }
    public int AdminGECPSApptTypeID { get { return admingecpsappttypeid; } set { admingecpsappttypeid = value; } }
    public string AdminGECPSApptType { get { return admingecpsappttype; } set { admingecpsappttype = value; } }
    public int AdminEHPFacilityContactID { get { return adminehpfacilitycontactid; } set { adminehpfacilitycontactid = value; } }
    public int AdminMasterAccountID { get { return adminmasteraccountid; } set { adminmasteraccountid = value; } }
    public int AdminChildAccountID { get { return adminchildaccountid; } set { adminchildaccountid = value; } }
    #endregion

    //API Call
    public string AccessToken { get { return accessToken; } set { accessToken = value; } }
    public string AutherisationCode { get { return autherisationCode; } set { autherisationCode = value; } }
    public string PracticeId { get { return practiceid; } set { practiceid = value; } }
    public string UserName { get { return username; } set { username = value; } }
    public string Password { get { return password; } set { password = value; } }
    public DataSet Data { get { return dataset; } set { dataset = value; } }

    //Patient API
    public string PatientId { get { return patientId; } set { patientId = value; } }
    //Appointment Details
    public string FacilityDetails { get { return facilityDetails; } set { facilityDetails = value; } }
    public string Phone { get { return phone; } set { phone = value; } }
    public string Fax { get { return fax; } set { fax = value; } }
    public string AppointmentType { get { return appointmentType; } set { appointmentType = value; } }
    public string Datetime { get { return datetime; } set { datetime = value; } }
    public string Doctor { get { return doctor; } set { doctor = value; } }
    public string EyeDetails { get { return eye; } set { eye = value; } }
    public string ReferralDoctor { get { return referralDoctor; } set { referralDoctor = value; } }
    public string FacilityContact { get { return facilityContact; } set { facilityContact = value; } }

    #region Events
    public event Statics.BasicErrorHandler AdminAccountExistsError;
    public event Statics.BasicErrorHandler AdminConvertToMasterError;
    public event Statics.BasicErrorHandler AdminCreateMasterAccountError;
    public event Statics.BasicErrorHandler AdminDeleteChildError;
    public event Statics.BasicErrorHandler AdminGetLockedAccountsError;
    public event Statics.BasicErrorHandler AdminGetMasterAccountsError;
    //Added by Uma D.
    public event Statics.BasicErrorHandler CareCloudMasterUserListError;
    public event Statics.BasicErrorHandler AdminGetMasterChildrenError;
    public event Statics.BasicErrorHandler AdminLoadChildError;
    public event Statics.BasicErrorHandler AdminSaveMasterChildError;
    public event Statics.BasicErrorHandler AdminUnlockUserAccountError;
    public event Statics.BasicErrorHandler DeleteAccountError;
    public event Statics.BasicErrorHandler DeleteNONVIPAccountError;
    public event Statics.BasicErrorHandler GetAccountDetailsError;
    public event Statics.BasicErrorHandler GetAccountsError;
    public event Statics.BasicErrorHandler GetEHPFacilityListError;
    public event Statics.BasicErrorHandler ValidateCurrentPasswordError;
    public event Statics.BasicErrorHandler GetCPSDoctorDisplayNameError;
    public event Statics.BasicErrorHandler GetCPSFacilityDisplayNameError;
    public event Statics.BasicErrorHandler GetCPSDoctorListError;
    public event Statics.BasicErrorHandler GetCPSFacilityListError;
    public event Statics.BasicErrorHandler GetCurrentPasswordError;
    public event Statics.BasicErrorHandler GetEHPApptTypeListError;
    public event Statics.BasicErrorHandler GetEHPCPSApptTypeListError;
    public event Statics.BasicErrorHandler GetEHPDoctorListError;
    public event Statics.BasicErrorHandler GetEmailAddressError;
    public event Statics.BasicErrorHandler GetGECPSApptTypesError;
    public event Statics.BasicErrorHandler GetGUIDError;
    public event Statics.BasicErrorHandler GetLocationsError;
    public event Statics.BasicErrorHandler GetNONVIPAccountsError;
    public event Statics.BasicErrorHandler GetNONVIPAccountByUserIDError;
    public event Statics.BasicErrorHandler GetNPIError;
    public event Statics.BasicErrorHandler InvalidLogonError;
    public event Statics.BasicErrorHandler IsAlreadyValidatedError;
    public event Statics.BasicErrorHandler IsDoctorInDBError;
    public event Statics.BasicErrorHandler LocateDoctorError;
    public event Statics.BasicErrorHandler RegisterUserError;
    public event Statics.BasicErrorHandler ResetPasswordError;
    public event Statics.BasicErrorHandler SendEmailError;
    public event Statics.BasicErrorHandler SetInitialPasswordError;
    public event Statics.BasicErrorHandler UpdateDatabaseError;
    public event Statics.BasicErrorHandler UpdateLastLogonDateError;
    public event Statics.BasicErrorHandler UserLogonError;
    public event Statics.BasicErrorHandler ValidateEmailError;
    public event Statics.BasicErrorHandler GetGECPSApptTypeDisplayError;
    public event Statics.BasicErrorHandler GetEHPCPSApptTypeCPSIDError;
    public event Statics.BasicErrorHandler GetFacilityContactListError;
    public event Statics.BasicErrorHandler AddUpdateFacilityContactError;
    public event Statics.BasicErrorHandler GetFacilityContactDetailsError;
    public event Statics.BasicErrorHandler DeleteFacilityContactError;
    public event Statics.BasicErrorHandler GetCPSApptTypeIDsError;
    public event Statics.BasicErrorHandler EHPApptType_DeleteError;
    public event Statics.BasicErrorHandler EHPApptType_GetDetailsError;
    public event Statics.BasicErrorHandler EHPApptType_SaveError;
    public event Statics.BasicErrorHandler EHPCPSApptType_DeleteError;
    public event Statics.BasicErrorHandler EHPCPSApptType_GetDetailsError;
    public event Statics.BasicErrorHandler EHPCPSApptType_SaveError;
    public event Statics.BasicErrorHandler EHPDoctor_GetDetailsError;
    public event Statics.BasicErrorHandler EHPDoctor_SaveError;
    public event Statics.BasicErrorHandler EHPDoctor_DeleteError;
    public event Statics.BasicErrorHandler EHPFacility_SaveError;
    public event Statics.BasicErrorHandler EHPFacility_GetDetailsError;
    public event Statics.BasicErrorHandler EHPFacility_DeleteError;
    public event Statics.BasicErrorHandler Location_RefreshDataError;
    public event Statics.BasicErrorHandler Location_SaveDataError;
    public event Statics.BasicErrorHandler GetPatientPhoneNumberError;
    public event Statics.BasicErrorHandler GetSignatureSourceMIDError;
    public event Statics.BasicErrorHandler GetCPSScheduleIDsError;
    public event Statics.BasicErrorHandler GetOpenApptSlotsError;
    public event Statics.BasicErrorHandler GetRefDrPatientsError;
    public event Statics.BasicErrorHandler CreateNewPatientError;
    public event Statics.Basic1ErrorHandler ValidateNewPatientError;
    public event Statics.BasicErrorHandler PatientExistsError;
    public event Statics.BasicErrorHandler TransferPatientReferringDoctorError;
    public event Statics.BasicErrorHandler GetCompanyIDError;
    public event Statics.BasicErrorHandler GetApptStartError;
    public event Statics.BasicErrorHandler GetApptStopError;
    public event Statics.BasicErrorHandler BookAppointmentError;
    public event Statics.BasicErrorHandler GetFacilityContactsError;
    public event Statics.BasicErrorHandler UpdateAccountError;
    public event Statics.BasicErrorHandler UpdatePatientPhoneNumberError;
    public event Statics.BasicErrorHandler UpdateNONVIPAccountError;
    public event Statics.BasicErrorHandler DeleteVIPAccountError;
    #endregion

    #region Functions
    //Added by uma D.
    public DataTable GetCareCloudMasterUserList()
    {
        /*
         * This function returns all of the Care Cloud master user details from the
         * database.  The web site uses this information to list out all of the
         * master user.
         */
        // UserList[] aReturn = null;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("CareCloudMasterUserList", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        SqlDataReader dr = null;
        DataTable dt = new DataTable();
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                dt.Load(dr);
            }
        }
        catch (Exception ex)
        {
            CareCloudMasterUserListError(1144, ex.ToString());
        }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return dt;
    }
    public DataTable GetLocationwisePatientList(string PVId)
    {
        /*
         * This function returns all of the Care Cloud master user details from the
         * database.  The web site uses this information to list out all of the
         * master user.
         */
        // UserList[] aReturn = null;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("GetLocationwisePatientList", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();

        cm.Parameters.Add("@PVId", SqlDbType.VarChar).Value = PVId;
        cm.Parameters.Add("@Location", SqlDbType.VarChar).Value = API.Session.ChState.ToString();
        SqlDataReader dr = null;
        DataTable dt = new DataTable();
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                dt.Load(dr);
            }
        }
        catch (Exception ex)
        {
            CareCloudMasterUserListError(1144, ex.ToString());
        }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return dt;
    }
    public void UpdateCareCloudMasterUserPassword(string password, int id)
    {

        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("UpdateMasterUserPassword", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
        cm.Parameters.Add("@Id", SqlDbType.Int).Value = id;

        try
        {
            cn.Open();
            cm.ExecuteNonQuery();
        }
        catch (Exception ex) { ex.ToString(); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
    }
    public string Encrypt(string password)
    {
        const string AesIV = @"!QAZ2WSX#EDC4RFV";
        const string AesKey = @"NDsVwQwRbwbuYDcX2PRGwNewMediaCod";
        try
        {

            string result = "";
            if (password != null)
            {
                using (Aes aes = Aes.Create())
                {

                    aes.BlockSize = 128;
                    aes.KeySize = 128;
                    aes.IV = Encoding.UTF8.GetBytes(AesIV);
                    aes.Key = Encoding.UTF8.GetBytes(AesKey);
                    byte[] encryptedByte = Encrypt(password, aes.Key, aes.IV);
                    return Convert.ToBase64String(encryptedByte);
                }
            }
            return result;
        }
        catch (Exception ex) { return ex.Message; }
    }
    public static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
    {
        byte[] encrypted;
        // Create a new AesManaged.    
        using (Aes aes = Aes.Create())
        {
            // Create encryptor    
            ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
            // Create MemoryStream    
            using (MemoryStream ms = new MemoryStream())
            {
                // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                // to encrypt    
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    // Create StreamWriter and write data to a stream    
                    using (StreamWriter sw = new StreamWriter(cs))
                        sw.Write(plainText);
                    encrypted = ms.ToArray();
                }
            }
        }
        // Return encrypted data    
        return encrypted;
    }

    public string Decrypt(string value)
    {
        try
        {
            const string AesIV = @"!QAZ2WSX#EDC4RFV";
            const string AesKey = @"NDsVwQwRbwbuYDcX2PRGwNewMediaCod";
            using (Aes aes = Aes.Create())
            {
                aes.BlockSize = 128;
                aes.KeySize = 128;
                aes.IV = Encoding.UTF8.GetBytes(AesIV);
                aes.Key = Encoding.UTF8.GetBytes(AesKey);
                byte[] getEncrypted = Convert.FromBase64String(value);
                string decrypttoken = Decrypt(getEncrypted, aes.Key, aes.IV);
                return decrypttoken;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
    {
        string plaintext = null;
        // Create AesManaged    
        using (Aes aes = Aes.Create())
        {
            // Create a decryptor    
            ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
            // Create the streams used for decryption.    
            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                // Create crypto stream 
                CryptoStream cs = null;
                try
                {
                    cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                    // Read crypto stream    
                    using (StreamReader reader = new StreamReader(cs))
                    {
                        plaintext = reader.ReadToEnd();
                    }

                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
        return plaintext;
    }
    //End
    public bool AdminAccountExists(string Email)
    {
        /* This function checks to see if the email address provided already exists in the database
           as a non-VIP account (Master Account).  The return value will determine if we create/update
           the master account or convert a regular account to a master account */

        bool bReturn = false;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("AccountExists", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 255).Value = Email;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    bReturn = true;
                }
            }
        }
        catch (Exception ex)
        {
            AdminAccountExistsError(1143, ex.ToString());
        }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return bReturn;
    }
    public int AdminCovertToMaster(string Email)
    {
        /* 
         * The purpose of this is to take a regular (non-VIP) account and convert it into 
         * a master (VIP) account.  This is normally processed by the PDM user.
         */
        int iReturn = 0;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("ConvertToMaster", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 255).Value = Email;
        try
        {
            cn.Open();
            iReturn = int.Parse(cm.ExecuteScalar().ToString());
        }
        catch (Exception ex) { API.Session.AdminConvertToMasterError(1152, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return iReturn;
    }
    public void AdminDeleteChild(int ChildID)
    {
        /*
         * The purpose of this is to remove an existing child account
         * from an existing master account.
         */
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("DeleteChild", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@ChildID", SqlDbType.Int).Value = ChildID;
        try
        {
            cn.Open();
            cm.ExecuteNonQuery();
        }
        catch (Exception ex) { API.Session.AdminDeleteChildError(1150, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
    }
    public ChildAcct AdminLoadChild()
    {
        /*
         * This function returns a ChildAcct type.  This data type holds the information
         * for the child account.
         */
        ChildAcct oReturn = new ChildAcct();
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("LoadChild", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@ChildID", SqlDbType.Int).Value = API.Session.AdminChildAccountID;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    oReturn.Name = dr["Name"].ToString();
                    oReturn.NPI = dr["NPI"].ToString();
                    oReturn.EmailAddress = dr["EmailAddress"].ToString();
                }
            }
        }
        catch (Exception ex) { AdminLoadChildError(1151, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return oReturn;
    }
    public void AdminSaveMasterChild(int ParentID, int ChildID, string Name, string NPI, string EmailAddress)
    {
        /*
         * This method does 1 of 2 things...  (1) If the child account does not 
         * already exist, this method creates the child account.  (2) if the child
         * account does already exist, it updates the child account.
         */
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("AddUpdateChild", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@ParentID", SqlDbType.Int).Value = ParentID;
        cm.Parameters.Add("@ChildID", SqlDbType.Int).Value = ChildID;
        cm.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = Name;
        cm.Parameters.Add("@NPI", SqlDbType.VarChar, 80).Value = NPI;
        cm.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 255).Value = EmailAddress;
        try
        {
            cn.Open();
            API.Session.AdminChildAccountID = int.Parse(cm.ExecuteScalar().ToString());
        }
        catch (Exception ex) { API.Session.AdminSaveMasterChildError(1149, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
    }
    public int AdminSaveMasterAccount(int UserID, string EmailAddress)
    {
        /*
         * This function will create / update the master account.  the return value
         * (int) is the unique ID for the master account in the database.
         */
        int iReturn = 0;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("AdminSaveMasterAccount", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 255).Value = EmailAddress;
        cm.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = Statics.NewPassword;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    iReturn = int.Parse(dr["UserID"].ToString());
                }
            }
        }
        catch (Exception ex) { AdminCreateMasterAccountError(1142, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return iReturn;
    }
    public UserList[] AdminGetLockedAccounts()
    {
        /*
         * This function returns an array of all locked out accounts.  The web
         * site then adds each one of these accounts to a drop down list for the 
         * user (normally a PDM) to select and unlock the account of their choice.
         */
        UserList[] aReturn = null;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("AdminGetLockedAccounts", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    // increase array +1
                    if (aReturn == null) aReturn = new UserList[1];
                    else
                    {
                        UserList[] aTemp = new UserList[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (UserList[])aTemp.Clone();
                    }
                    int a = aReturn.Length - 1;
                    aReturn[a] = new UserList();
                    aReturn[a].ID = int.Parse(dr["UserID"].ToString());
                    aReturn[a].EmailAddress = dr["EmailAddress"].ToString();
                }
            }
        }
        catch (Exception ex) { AdminGetLockedAccountsError(1140, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return aReturn;
    }
    public UserList[] AdminGetMasterAccounts()
    {
        /*
         * This function returns an array of all of the master (VIP) accounts in the
         * database.  The web site uses this information to list out all of the
         * master accounts.
         */
        UserList[] aReturn = null;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("AdminGetMasterAccounts", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    //increase array +1
                    if (aReturn == null) aReturn = new UserList[1];
                    else
                    {
                        UserList[] aTemp = new UserList[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (UserList[])aTemp.Clone();
                    }
                    int a = aReturn.Length - 1;
                    aReturn[a] = new UserList();
                    aReturn[a].ID = int.Parse(dr["UserID"].ToString());
                    aReturn[a].EmailAddress = dr["EmailAddress"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            AdminGetMasterAccountsError(1144, ex.ToString());
        }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return aReturn;
    }
    public ChildAcct[] AdminGetMasterChildren()
    {
        /*
         * This function returns an array of all of the child accounts for
         * the selected master account
         */
        ChildAcct[] aReturn = null;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("GetMasterChildren", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@MasterID", SqlDbType.Int).Value = API.Session.AdminMasterAccountID;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    //increase array +1
                    if (aReturn == null)
                        aReturn = new ChildAcct[1];
                    else
                    {
                        ChildAcct[] aTemp = new ChildAcct[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (ChildAcct[])aTemp.Clone();
                    }
                    int a = aReturn.Length - 1;
                    aReturn[a] = new ChildAcct();
                    aReturn[a].Name = dr["Name"].ToString();
                    aReturn[a].NPI = dr["NPI"].ToString();
                    aReturn[a].ParentID = API.Session.AdminMasterAccountID;
                    aReturn[a].UserID = int.Parse(dr["UserID"].ToString());
                }
            }
        }
        catch (Exception ex) { API.Session.AdminGetMasterChildrenError(1145, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) { cn.Close(); cn.Dispose(); } }
        }
        return aReturn;
    }
    public void AdminUnlockUserAccount(int UserID)
    {
        /*
         * This method unlocks the selected account in the database.  The account
         * was locked because the account's credentials were not supplied on the
         * logon screen properly.
         */
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("AdminUnlockAccount", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
        cm.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = Statics.NewPassword;
        try
        {
            cn.Open();
            cm.ExecuteNonQuery();
        }
        catch (Exception ex) { AdminUnlockUserAccountError(1141, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
    }
    public void DeleteAccount(int UserID)
    {
        /*
         * This method deletes the selected account from the database.  The selected
         * account is an admin.  To delete a master account - see the DeleteVIPAccount() method.
         * To delete a non-admin provider account, see the DeleteNONVIPAccount() method.
         */
        SqlConnection cn = null;
        SqlCommand cm = null;

        try
        {
            cn = new SqlConnection(Statics.EHPconnstring);
            cm = new SqlCommand("DeleteAccount", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Clear();
            cm.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

            cn.Open();
            cm.ExecuteNonQuery();
        }
        catch (Exception ex) { DeleteAccountError(1159, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
    }
    public void DeleteVIPAccount(int UserID)
    {
        /*
         * This method deletes a master (VIP) account.  To delete an admin
         * account, see the DeleteAccount() method.  To delete a non-VIP provider
         * account, see the DeleteNONVIPAccount() method.
         */
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("DeleteVIPAccount", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
        try
        {
            cn.Open();
            cm.ExecuteNonQuery();
        }
        catch (Exception ex) { DeleteVIPAccountError(1168, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
    }
    public void DeleteNONVIPAccount(int UserID)
    {
        /*
         * This method deletes a non-master (VIP) account.  To delete
         * an admin account, see the DeleteAccount() method.  To delete
         * a master account, see the DeleteVIPAccount() method.
         */
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("DeleteNONVIPAccount", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
        try
        {
            cn.Open();
            cm.ExecuteNonQuery();
        }
        catch (Exception ex) { DeleteNONVIPAccountError(1167, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
    }
    public Account GetAccountDetails(int UserID)
    {
        /*
         * This function pulls the account details from the database and
         * returns the data to the web site.
         */
        Account data = null;
        SqlConnection cn = null;
        SqlCommand cm = null;
        SqlDataReader dr = null;

        cn = new SqlConnection(Statics.EHPconnstring);
        cm = new SqlCommand("GetAccountDetails", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr.Read())
            {
                data = new Account();
                data.EmailAddress = dr["EmailAddress"].ToString();
                data.Password = dr["Password"].ToString();
                data.LastLogon = DateTime.Parse(dr["LastLogon"].ToString());
                data.Name = dr["Name"].ToString();
                data.Active = bool.Parse(dr["Active"].ToString());
                data.IsAdmin = bool.Parse(dr["IsAdmin"].ToString());
                data.IsSetupAdmin = bool.Parse(dr["IsSetupAdmin"].ToString());
                data.UserID = UserID;
            }
        }
        catch (Exception ex) { GetAccountDetailsError(1157, ex.ToString()); }
        finally
        {
            if (dr != null) if (!dr.IsClosed) dr.Close();
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return data;
    }
    public Account[] GetAccountList()
    {
        /* 
         * This function gathers the admin accounts from the database
         * and returns them to the web site in an array.
         *
         * This is for Admin accounts only 
         */

        Account[] aReturn = null;
        SqlConnection cn = null;
        SqlCommand cm = null;
        SqlDataReader dr = null;

        cn = new SqlConnection(Statics.EHPconnstring);
        cm = new SqlCommand("GetAccountList", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();

        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    // increase array +1
                    if (aReturn == null) aReturn = new Account[1];
                    else
                    {
                        Account[] aTemp = new Account[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (Account[])aTemp.Clone();
                    }
                    int a = aReturn.Length - 1;
                    aReturn[a] = new Account();
                    aReturn[a].Name = dr["Name"].ToString();
                    aReturn[a].UserID = int.Parse(dr["UserID"].ToString());
                }
            }
        }
        catch (Exception ex) { GetAccountsError(1156, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return aReturn;
    }
    public int GetEHPCPSApptTypeCPSID(int ehpcpsappttypeid)
    {
        /*
         * This function returns the CPSID value from the
         * CPSApptType table.  This is used on the web site
         * when configuring which appointment types to allow
         * appointments to be booked for.
         */
        int iReturn = 0;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("GetEHPCPSApptTypeCPSID", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@EHPCPSApptTypeID", SqlDbType.Int).Value = ehpcpsappttypeid;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    iReturn = int.Parse(dr["CPSID"].ToString());
                }
            }
        }
        catch (Exception ex) { GetEHPCPSApptTypeCPSIDError(1001, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return iReturn;
    }
    public int[] GetCPSApptTypeIDs(int ehpappttypeid)
    {
        /*
         * This function returns an int array of all the appointment types
         * in centricity to relate to the friendly name appointment type.
         */
        int[] aReturn = null;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("GetCPSApptTypeIDs", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@EHPApptTypeID", SqlDbType.Int).Value = ehpappttypeid;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    // increase array +1
                    if (aReturn == null) aReturn = new int[1];
                    else
                    {
                        int[] aTemp = new int[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (int[])aTemp.Clone();
                    }
                    int a = aReturn.Length - 1;
                    aReturn[a] = int.Parse(dr["CPSID"].ToString());
                }
            }
        }
        catch (Exception ex) { GetCPSApptTypeIDsError(1095, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return aReturn;
    }
    public string GetCPSDoctorDisplayName(string state, int cpsid)
    {
        /*
         * This function returns the ListName from the Centricity database 
         * based on the doctor's ID number.  It uses the AL | TN values to 
         * determine which Centricity database to pull this from.
         */
        string sReturn = "";
        SqlConnection cn = null;
        SqlCommand cm = null;
        string q = "SELECT ListName FROM DoctorFacility WHERE DoctorFacilityID = @DoctorFacilityID ";
        if (state == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (state == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@DoctorFacilityID", SqlDbType.Int).Value = cpsid;
        try
        {
            cn.Open();
            sReturn = cm.ExecuteScalar().ToString();
        }
        catch (Exception ex) { GetCPSDoctorDisplayNameError(1002, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return sReturn;
    }
    public string GetCPSFacilityDisplayName(string state, int cpsid)
    {
        /*
         * This function returns the ListName of the Facility from the centricity
         * database.  It uses AL|TN values to determine which centricity database
         * to pull this info from.
         */
        if (state != "AL" && state != "TN") { return state; }
        string sReturn = "";
        SqlConnection cn = null;
        SqlCommand cm = null;
        string q = "SELECT ListName FROM DoctorFacility WHERE DoctorFacilityID = @DoctorFacilityID ";
        if (state == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (state == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@DoctorFacilityID", SqlDbType.Int).Value = cpsid;
        try
        {
            cn.Open();
            sReturn = cm.ExecuteScalar().ToString();
        }
        catch (Exception ex) { GetCPSFacilityDisplayNameError(1003, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return sReturn;
    }
    public CPSDoctor[] GetCPSDoctorList(string state)
    {
        /*
         * This function returns an array of information from the centricity
         * database.  The list includes the doctor ID and ListName.
         * The AL|TN values are used to determine which database to pull from.
         * The "Type IN (1,7)" part of the query...  1 is provider, 7 is a
         * resource.  Only pulls active records.
         */
        CPSDoctor[] aReturn = null;
        SqlConnection cn = null;
        SqlCommand cm = null;
        SqlDataReader dr = null;
        string q = "SELECT ID=DoctorFacilityID, ListName = ISNULL(ListName,'') ";
        q += "FROM DoctorFacility ";
        q += "WHERE Type IN (1,7) AND ISNULL(Inactive,0)=0 ";
        q += "ORDER BY ListName ";

        if (state == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (state == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    //increase array +1
                    if (aReturn == null) aReturn = new CPSDoctor[1];
                    else
                    {
                        CPSDoctor[] aTemp = new CPSDoctor[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (CPSDoctor[])aTemp.Clone();
                    }
                    int a = aReturn.Length - 1;
                    aReturn[a] = new CPSDoctor();
                    aReturn[a].ID = int.Parse(dr["ID"].ToString());
                    aReturn[a].ListName = dr["ListName"].ToString();
                    if (cn.Database.ToString() == "Birmingham") aReturn[a].State = "AL";
                    if (cn.Database.ToString() == "Nashville") aReturn[a].State = "TN";
                }
            }
        }
        catch (Exception ex) { GetCPSDoctorListError(1004, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return aReturn;
    }
    public CPSFacility[] GetCPSFacilityList()
    {
        /*
         * This function returns an array of facilities from the centricity database.
         * It pulls from both the Birmingham and Nashville databases and merges all the
         * data together.  It is separated by the aReturn[a].State variable.
         * Type=2 means facility in Centricity.  Only pulls active records.
         */
        CPSFacility[] aReturn = null;
        SqlConnection cn = null;
        SqlCommand cm = null;
        SqlDataReader dr = null;
        string q = "SELECT ID = DoctorFacilityID, ListName = ISNULL(ListName,'') ";
        q += "FROM DoctorFacility ";
        q += "WHERE Type = 2 AND ListName IS NOT NULL AND ISNULL(Inactive,0) = 0 ";
        q += "ORDER BY ListName ";

        foreach (string connstring in Statics.ConnStrings)
        {
            cn = new SqlConnection(connstring);
            cm = new SqlCommand(q, cn);
            cm.CommandType = CommandType.Text;
            cm.Parameters.Clear();
            try
            {
                cn.Open();
                dr = cm.ExecuteReader();
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        // increase array + 1
                        if (aReturn == null) aReturn = new CPSFacility[1];
                        else
                        {
                            CPSFacility[] aTemp = new CPSFacility[aReturn.Length + 1];
                            Array.Copy(aReturn, aTemp, aReturn.Length);
                            aReturn = (CPSFacility[])aTemp.Clone();
                        }
                        int a = aReturn.Length - 1;
                        aReturn[a] = new CPSFacility();
                        aReturn[a].ID = int.Parse(dr["ID"].ToString());
                        if (cn.Database == "Birmingham") aReturn[a].State = "AL";
                        if (cn.Database == "Nashville") aReturn[a].State = "TN";
                        aReturn[a].ListName = dr["ListName"].ToString();
                    }
                }
            }
            catch (Exception ex) { GetCPSFacilityListError(1005, ex.ToString()); }
            finally
            {
                if (dr != null) { if (!dr.IsClosed) dr.Close(); }
                if (cm != null) cm.Dispose();
                if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
            }
        }
        return aReturn;
    }
    public EHPApptType[] GetEHPApptTypeList(int doctorid)
    {
        /*
         * This function returns an array that contains a list of appointment
         * types based on the selected provider.  This list comes from the EHPortal
         * database - not centricity.
         */
        EHPApptType[] aReturn = null;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("GetEHPApptTypeList", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@DoctorID", SqlDbType.Int).Value = doctorid;
        SqlDataReader dr = null;

        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    //increase array +1
                    if (aReturn == null) aReturn = new EHPApptType[1];
                    else
                    {
                        EHPApptType[] aTemp = new EHPApptType[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (EHPApptType[])aTemp.Clone();
                    }
                    int a = aReturn.Length - 1;
                    aReturn[a] = new EHPApptType();
                    aReturn[a].ID = int.Parse(dr["ID"].ToString());
                    aReturn[a].Name = dr["Name"].ToString();
                    aReturn[a].DoctorID = doctorid;
                    aReturn[a].Enabled = bool.Parse(dr["Enabled"].ToString());
                }
            }
        }
        catch (Exception ex) { GetEHPApptTypeListError(1006, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return aReturn;
    }
    public EHPCPSApptType[] GetEHPCPSApptTypeList(int ehpappttypeid)
    {
        /*
         * This function uses the appttype id from the EHPortal database
         * to determine what the appt type id is in centricity.
         */
        EHPCPSApptType[] aReturn = null;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("GetCPSApptTypeList", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@EHPApptTypeID", SqlDbType.Int).Value = ehpappttypeid;
        SqlDataReader dr = null;

        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    //increase array +1
                    if (aReturn == null) aReturn = new EHPCPSApptType[1];
                    else
                    {
                        EHPCPSApptType[] aTemp = new EHPCPSApptType[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (EHPCPSApptType[])aTemp.Clone();
                    }
                    int a = aReturn.Length - 1;
                    aReturn[a] = new EHPCPSApptType();
                    aReturn[a].CPSID = int.Parse(dr["CPSID"].ToString());
                    aReturn[a].EHPApptTypeID = ehpappttypeid;
                    aReturn[a].ID = int.Parse(dr["ID"].ToString());
                    aReturn[a].State = dr["State"].ToString();
                    aReturn[a].Display = API.Session.GetGECPSApptTypeDisplay(dr["State"].ToString(), int.Parse(dr["CPSID"].ToString()));
                }
            }
        }
        catch (Exception ex) { GetEHPCPSApptTypeListError(1007, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return aReturn;
    }
    public EHPDoctor[] GetEHPDoctorList(int facilityid)
    {
        /*
         * This function returns an array of doctor records based on the facility.
         */
        EHPDoctor[] aReturn = null;
        SqlConnection cn = null;
        SqlCommand cm = null;
        SqlDataReader dr = null;

        cn = new SqlConnection(Statics.EHPconnstring);
        cm = new SqlCommand("GetEHPDoctorList", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@FacilityID", SqlDbType.Int).Value = facilityid;

        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    // increase array +
                    if (aReturn == null) aReturn = new EHPDoctor[1];
                    else
                    {
                        EHPDoctor[] aTemp = new EHPDoctor[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (EHPDoctor[])aTemp.Clone();
                    }
                    int a = aReturn.Length - 1;
                    aReturn[a] = new EHPDoctor();
                    aReturn[a].ID = int.Parse(dr["DoctorID"].ToString());
                    aReturn[a].State = dr["State"].ToString();
                    aReturn[a].CPSID = int.Parse(dr["CPSID"].ToString());
                    aReturn[a].Name = dr["Name"].ToString();
                    aReturn[a].Enabled = bool.Parse(dr["Enabled"].ToString());
                }
            }
        }
        catch (Exception ex) { GetEHPDoctorListError(1008, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return aReturn;
    }
    public EHPFacility[] GetEHPFacilityList()
    {
        /*
         * This function returns an array of facilities.
         */
        EHPFacility[] aReturn = null;
        SqlConnection cn = null;
        SqlCommand cm = null;
        SqlDataReader dr = null;

        cn = new SqlConnection(Statics.EHPconnstring);
        cm = new SqlCommand("GetEHPFacilityList", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();

        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    // increase array +1
                    if (aReturn == null) aReturn = new EHPFacility[1];
                    else
                    {
                        EHPFacility[] aTemp = new EHPFacility[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (EHPFacility[])aTemp.Clone();
                    }
                    int a = aReturn.Length - 1;
                    aReturn[a] = new EHPFacility();
                    aReturn[a].FacilityID = int.Parse(dr["FacilityID"].ToString());
                    aReturn[a].CPSID = int.Parse(dr["CPSID"].ToString());
                    aReturn[a].Enabled = bool.Parse(dr["Enabled"].ToString());
                    aReturn[a].Name = dr["Name"].ToString();
                    aReturn[a].State = dr["State"].ToString();
                }
            }
        }
        catch (Exception ex) { GetEHPFacilityListError(1009, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return aReturn;
    }
    public string GetEmailAddress(Guid guid)
    {
        /*
         * This function returns the Email Address of the user account based on the GUID value.
         */
        string sReturn = "";
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("GetEmailByGUID", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@GUID", SqlDbType.UniqueIdentifier).Value = guid;
        try
        {
            cn.Open();
            sReturn = cm.ExecuteScalar().ToString();
        }
        catch (Exception ex) { GetEmailAddressError(1010, ex.ToString()); sReturn = ""; }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return sReturn;
    }
    public string GetEmailAddress(int UserID)
    {
        /*
         * This function returns the email address of the user account
         * based on the userid value.
         */
        string sReturn = "";
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("GetEmailByUserID", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
        try
        {
            cn.Open();
            sReturn = cm.ExecuteScalar().ToString();
        }
        catch (Exception ex) { GetEmailAddressError(1010, ex.ToString()); sReturn = ""; }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return sReturn;
    }
    public string GetEmailAddressByNPI(string NPI)
    {
        /*
         * This function returns the email address of the user
         * account based on the NPI number.
         */
        string sReturn = "";
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("GetEmailByNPI", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@NPI", SqlDbType.VarChar, 80).Value = NPI;
        try
        {
            cn.Open();
            sReturn = cm.ExecuteScalar().ToString();
        }
        catch (Exception ex) { GetEmailAddressError(1163, ex.ToString()); sReturn = ""; }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return sReturn;
    }
    public GECPSApptType[] GetGECPSApptTypes(string state)
    {
        /*
         * This function retuns appointment types from Centricity 
         * based on the state value.  The state value is used to 
         * know which centricity database to pull data from.
         */
        GECPSApptType[] aReturn = null;
        SqlConnection cn = null;
        SqlCommand cm = null;
        SqlDataReader dr = null;
        string q = "SELECT ApptTypeID, Name, Duration ";
        q += "FROM ApptType WHERE ISNULL(Inactive,0)=0 ";
        q += "ORDER BY Name";

        if (state == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (state == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    //increase array +1
                    if (aReturn == null) aReturn = new GECPSApptType[1];
                    else
                    {
                        GECPSApptType[] aTemp = new GECPSApptType[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (GECPSApptType[])aTemp.Clone();
                    }
                    int a = aReturn.Length - 1;
                    aReturn[a] = new GECPSApptType();
                    aReturn[a].Duration = int.Parse(dr["Duration"].ToString());
                    aReturn[a].ID = int.Parse(dr["ApptTypeID"].ToString());
                    aReturn[a].Name = dr["Name"].ToString();
                }
            }
        }
        catch (Exception ex) { GetGECPSApptTypesError(1011, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return aReturn;
    }
    public string GetGECPSApptTypeDisplay(string state, int geappttypeid)
    {
        /*
         * this function returns the selected appointment type information
         * from the centricity database.
         */
        string sReturn = "";
        SqlConnection cn = null;
        SqlCommand cm = null;
        SqlDataReader dr = null;
        string q = "SELECT Name, Duration FROM ApptType WHERE ApptTypeID = @ApptTypeID ";

        if (state == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (state == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@ApptTypeID", SqlDbType.Int).Value = geappttypeid;

        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    sReturn = dr["Name"].ToString() + " [" + dr["Duration"].ToString() + "]";
                }
            }
        }
        catch (Exception ex) { GetGECPSApptTypeDisplayError(1012, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return sReturn;
    }
    public Guid GetGUID(string email)
    {
        /*
         * This function returns the GUID value of the user account based on the
         * provided email address.
         */
        Guid gReturn = Guid.Empty;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("GetUserGUIDByEmailAddress", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 255).Value = email;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    gReturn = new Guid(dr["GUID"].ToString());
                }
            }
        }
        catch (Exception ex) { GetGUIDError(1013, ex.ToString()); gReturn = Guid.Empty; }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return gReturn;
    }
    public Location[] GetLocations(Guid guid)
    {
        /*
         * this function returns an array of locations based on the GUID.
         * It first pulls the NPI number by the GUID number.
         * Then pulls referring provider locations from Centricity database
         * based on the NPI number.
         */
        Location[] aReturn = null;
        SqlConnection cn = null;
        SqlCommand cm = null;
        SqlDataReader dr = null;
        //string npi = GetNPI(guid);
        string[] npi = GetNPI(API.Session.Email);
        if (npi == null) return null;
        if (npi.Length == 0) return null;
        string sNPI = "";
        for (int a = 0; a < npi.Length; a++)
        {
            sNPI += "'" + npi[a].ToString() + "'";
            if (a < npi.Length - 1) sNPI += ",";
        }
        string q = "SELECT ID=DoctorFacilityID, Display = ISNULL(First,'') + ISNULL(' ' + Middle,'') + ISNULL(' '+Last,'') + ISNULL(' (' + OrgName + ') ','') ";
        q += "+ ISNULL(' ' + Address1,'') + ISNULL(', ' + Address2,'') + ISNULL(', ' + City,'') + ISNULL(' ' + State,'') + ISNULL(', ' + Zip,'') ";
        q += "FROM DoctorFacility WHERE NPI IN (";
        q += sNPI;
        q += ") AND ISNULL(Inactive,0) = 0 AND Type = 3  ";


        // Pull locations from all databases
        foreach (string connstring in Statics.ConnStrings)
        {

            cn = new SqlConnection(connstring);
            cm = new SqlCommand(q, cn);
            cm.CommandType = CommandType.Text;
            cm.Parameters.Clear();
            try
            {
                cn.Open();
                dr = cm.ExecuteReader();
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        // increase array +1
                        if (aReturn == null) aReturn = new Location[1];
                        else
                        {
                            Location[] aTemp = new Location[aReturn.Length + 1];
                            Array.Copy(aReturn, aTemp, aReturn.Length);
                            aReturn = (Location[])aTemp.Clone();
                        }
                        int a = aReturn.Length - 1;
                        aReturn[a] = new Location();
                        aReturn[a].Database = cn.Database.ToString();
                        aReturn[a].ID = int.Parse(dr["ID"].ToString());
                        aReturn[a].Display = dr["Display"].ToString();
                    }
                }
            }
            catch (Exception ex) { GetLocationsError(1014, ex.ToString()); }
            finally
            {
                if (dr != null) { if (!dr.IsClosed) dr.Close(); }
                if (cm != null) cm.Dispose();
                if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
            }




        }


        return aReturn;
    }

    public Location[] GetLocationsforTN(string Email)//Guid guid
    {
        /*
         * this function returns an array of locations based on the GUID.
         * It first pulls the NPI number by the GUID number.
         * Then pulls referring provider locations from Centricity database
         * based on the NPI number.
         */
        Location[] aReturn = null;
        SqlConnection cn = null;
        SqlCommand cm = null;
        SqlDataReader dr1 = null;
        //string npi = GetNPI(guid);
       // string[] npi = GetNPI(Email);
        //if (npi == null) return null;
        //if (npi.Length == 0) return null;
        string sNPI = "";
        //if(npi != null)
        //{
        //    for (int a = 0; a < npi.Length; a++)
        //    {
        //        sNPI += "'" + npi[a].ToString() + "'";
        //        if (a < npi.Length - 1) sNPI += ",";
        //    }
        //}

        //PVId
        string qr = " SELECT ID1=ReferringPhysiciansID, Display1= ISNULL(First,'') + ' ' + ISNULL(Last,'') + ' | ' + ISNULL(OrgName+' | ','') + ISNULL(' '+Address1,'') + ISNULL(', '+City,'')+ ISNULL(', '+ State ,'')+ ISNULL(' '+Zip,''), States=State , OrgName1=ISNULL(' ('+OrgName+') ',''),Location1=ISNULL(Location,'')  FROM ReferringPhysicians_Master WHERE ";
        //qr += sNPI!="" ?"( NPI IN ("+ sNPI+ ") OR   emailaddress='" + API.Session.email + "' ) " : "  emailaddress='" + API.Session.email + "' ";
        qr += "  emailaddress='" + API.Session.email + "' ";
        //qr += sNPI;
        qr += " AND ISNULL(Inactive,0) = 0  ";
        // Pull locations from all databases

        cn = new SqlConnection(Statics.EHPconnstring);
        cm = new SqlCommand(qr, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();

        try
        {
            cn.Open();

            dr1 = cm.ExecuteReader();
            if (dr1 != null)
            {
                while (dr1.Read())
                {
                    // increase array +1
                    if (aReturn == null) aReturn = new Location[1];
                    else
                    {
                        Location[] aTemp = new Location[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (Location[])aTemp.Clone();
                    }
                    int a = aReturn.Length - 1;
                    aReturn[a] = new Location();
                    aReturn[a].Database = cn.Database.ToString();
                    aReturn[a].Address1 = dr1["ID1"].ToString();
                    aReturn[a].Display = dr1["Display1"].ToString();
                    aReturn[a].State = dr1["States"].ToString();
                    aReturn[a].OrgName = dr1["OrgName1"].ToString();
                    aReturn[a].Location1 = dr1["Location1"].ToString();
                }
            }
        }
        catch (Exception ex) { GetLocationsError(1014, ex.ToString()); }
        finally
        {
            if (dr1 != null) { if (!dr1.IsClosed) dr1.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }

        return aReturn;
    }
    public Account[] GetNONVIPAccounts()
    {
        /*
         * This function returns an array of non-master/vip accounts
         */
        Account[] aReturn = null;
        SqlConnection cn = null;
        SqlCommand cm = null;
        SqlDataReader dr = null;

        cn = new SqlConnection(Statics.EHPconnstring);
        cm = new SqlCommand("GetNONVIPAccounts", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();

        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    // increase array by +1
                    if (aReturn == null) aReturn = new Account[1];
                    else
                    {
                        Account[] aTemp = new Account[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (Account[])aTemp.Clone();
                    }
                    int a = aReturn.Length - 1;
                    aReturn[a] = new Account();
                    aReturn[a].UserID = int.Parse(dr["UserID"].ToString());
                    aReturn[a].NPINumber = dr["NPI"].ToString();
                    aReturn[a].EmailAddress = dr["EmailAddress"].ToString();
                    aReturn[a].Password = dr["Password"].ToString();
                    if (dr["LastLogon"].ToString() != "")
                        aReturn[a].LastLogon = DateTime.Parse(dr["LastLogon"].ToString());
                }
            }
        }
        catch (Exception ex) { GetNONVIPAccountsError(1164, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return aReturn;
    }
    public Account GetNONVIPAccountByUserID(int UserID)
    {
        /*
         * this function returns a single account (non-master/vip)
         */
        Account oReturn = new Account();
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("GetNONVIPAccountByUserID", cn);
        SqlDataReader dr = null;
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    oReturn.UserID = UserID;
                    oReturn.NPINumber = dr["NPI"].ToString();
                    oReturn.EmailAddress = dr["EmailAddress"].ToString();
                    oReturn.Password = dr["Password"].ToString();
                    if (dr["LastLogon"].ToString() != "")
                        oReturn.LastLogon = DateTime.Parse(dr["LastLogon"].ToString());
                }
            }
        }
        catch (Exception ex) { GetNONVIPAccountByUserIDError(1165, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return oReturn;
    }
    public string[] GetNPI(string Email)//Guid guid
    {
        /*
         * This function returns the NPI number of a user account based on the provided GUID
         */
        string[] aReturn = null;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("GetNPIByGUID", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@Email", SqlDbType.VarChar,200).Value = Email;
        //cm.Parameters.Add("@GUID", SqlDbType.UniqueIdentifier).Value = guid;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    // Increase Array +1
                    if (aReturn == null) aReturn = new string[1];
                    else
                    {
                        string[] aTemp = new string[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (string[])aTemp.Clone();
                    }
                    int a = aReturn.Length - 1;
                    aReturn[a] = dr["NPI"].ToString();
                }
            }

        }
        catch (Exception ex) { GetNPIError(1015, ex.ToString()); return null; }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return aReturn;
    }
    public string GetPatientPhoneNumber(string state, int patientprofileid)
    {
        /*
         * This function returns the Phone1 value from the centricity database,
         * patientprofile table
         */
        string sReturn = "";
        string q = "SELECT Phone1 FROM PatientProfile WHERE PatientProfileID = @PatientProfileID ";
        SqlConnection cn = null;
        if (Session.SWState == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (Session.SWState == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@PatientProfileID", SqlDbType.Int).Value = patientprofileid;
        try
        {
            cn.Open();
            sReturn = cm.ExecuteScalar().ToString();
        }
        catch (Exception ex) { GetPatientPhoneNumberError(1138, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return sReturn;
    }
    public int GetSignatureSourceMID(string state, string Desc)
    {
        /*
         * This function returns the signaturesourceID (medlistsid) from the centricity
         * database based on the desc value provided.
         */
        int iReturn = 0;
        string q = "SELECT MedListsID FROM MedLists WHERE TableName = 'SignatureSources' AND Description = @Desc ";
        SqlConnection cn = null;
        if (state == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (state == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@Desc", SqlDbType.VarChar, 200).Value = Desc;
        try
        {
            cn.Open();
            iReturn = int.Parse(cm.ExecuteScalar().ToString());
        }
        catch (Exception ex) { GetSignatureSourceMIDError(1154, ex.ToString()); return 0; }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return iReturn;
    }
    public void InvalidLogon(string emailaddress)
    {
        /*
         * This method updates the InvalidLogonAttemps to +1 for the provided
         * email address.  Once the value reaches a certain number, the account
         * will become locked.
         */
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("SetInvalidLogonAttempts", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 255).Value = emailaddress;
        try
        {
            cn.Open();
            cm.ExecuteNonQuery();
        }
        catch (Exception ex) { InvalidLogonError(1016, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        InvalidLogonError(1017, "Invalid email address / password combination.  Please try again.");
    }
    public bool IsAlreadyValidated(Guid guid)
    {
        /*
         * This function determines if the account has already been validated.
         */
        bool bReturn = false;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("IsAlreadyValidated", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@GUID", SqlDbType.UniqueIdentifier).Value = guid;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    bool bValidated = bool.Parse(dr["Validated"].ToString());
                    bReturn = bValidated;
                }
            }
        }
        catch (Exception ex) { IsAlreadyValidatedError(1018, ex.ToString()); return false; }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return bReturn;
    }
    public bool IsDoctorInDB(string connstring, string fname, string lname, string zipcode, string phone, string npi, string email)
    {
        /*
         * this function determines if the provider record exists in the centricity database or not.  This
         * is used by the referring provider when they attempt to self-register on the website.  This is a
         * validation process to determine that they are in fact one of our referring doctors.
         */
        bool bReturn = false;
        string q = "SELECT TOP 1 DoctorFacilityID FROM DoctorFacility WHERE NPI = @NPI AND First LIKE @FirstName ";
        q += "AND Last LIKE @LastName AND (Phone1 LIKE @Phone OR Phone1 IS NULL) ";
        q += "AND (Zip LIKE @ZipCode OR Zip IS NULL) ORDER BY DoctorFacilityID  DESC ";
        SqlConnection cn = new SqlConnection(connstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@FirstName", SqlDbType.VarChar, 35).Value = fname + "%";
        cm.Parameters.Add("@LastName", SqlDbType.VarChar, 60).Value = lname + "%";
        cm.Parameters.Add("@ZipCode", SqlDbType.VarChar, 10).Value = zipcode + "%";
        cm.Parameters.Add("@NPI", SqlDbType.VarChar, 80).Value = npi;
        cm.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 255).Value = email;
        cm.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = phone + "%";
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    bReturn = true;
                }
            }
        }
        catch (Exception ex) { IsDoctorInDBError(1019, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return bReturn;
    }
    public bool LocateDoctor(string fname, string lname, string zipcode, string phone, string npi, string email)
    {
        bool bReturn = false;
        //if (IsDoctorInDB(Statics.DEMOconnstring, fname, lname, zipcode, phone, npi, email))
        //{
        //    bReturn = true;
        //}
        if (IsDoctorInDB(Statics.CPSALconnstring, fname, lname, zipcode, phone, npi, email)) bReturn = true;
        if (IsDoctorInDB(Statics.CPSTNconnstring, fname, lname, zipcode, phone, npi, email)) bReturn = true;
        return bReturn;
    }
    public bool NPIAlreadyRegistered(string npi)
    {
        /*
         * this function determines if the provided NPI number has already been registered in the EHPortal database
         * it prevents the same NPI number from being used on two separate provider records.
         */
        bool bReturn = false;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("GetUserByNPI", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@NPI", SqlDbType.VarChar, 80).Value = npi;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    bReturn = true;
                }
            }
        }
        catch (Exception ex) { RegisterUserError(1160, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return bReturn;
    }
    public bool RegisterUser(string npi, string email)
    {
        /*
         * This function sets up the user account in the EHPortal database
         * and returns a bool success value.
         */
        bool bReturn = false;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("RegisterUser", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@NPI", SqlDbType.VarChar, 80).Value = npi;
        cm.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 255).Value = email;
        cm.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = "R3g!5tered_0n1y";
        cm.Parameters.Add("@Validated", SqlDbType.Bit).Value = false;
        cm.Parameters.Add("@Active", SqlDbType.Bit).Value = true;
        cm.Parameters.Add("@RegisteredDate", SqlDbType.DateTime).Value = DateTime.Now;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    bReturn = true;
                }
            }
        }
        catch (Exception ex) { RegisterUserError(1020, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return bReturn;
    }
    public bool ResetPasswordForgot(string password, string email)//Guid guid,
    {
        /*
         * this function resets a password on a user account.
         * returns a bool success value.
         */
        bool bReturn = false;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("ResetPassword", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        //  cm.Parameters.Add("@GUID", SqlDbType.UniqueIdentifier).Value = guid;//API.Session.Email.ToString();
        cm.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = email.ToString();
        cm.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = password;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    if (int.Parse(dr["Result"].ToString()) == 1) bReturn = true;
                    else bReturn = false;
                }
            }
        }
        catch (Exception ex) { ResetPasswordError(1021, ex.ToString()); bReturn = false; }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return bReturn;
    }
    public bool ResetPassword( string password)//Guid guid,
    {
        /*
         * this function resets a password on a user account.
         * returns a bool success value.
         */
        bool bReturn = false;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("ResetPassword", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        //  cm.Parameters.Add("@GUID", SqlDbType.UniqueIdentifier).Value = guid;//API.Session.Email.ToString();
        cm.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = API.Session.Email.ToString();
        cm.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = password;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    if (int.Parse(dr["Result"].ToString()) == 1) bReturn = true;
                    else bReturn = false;
                }
            }
        }
        catch (Exception ex) { ResetPasswordError(1021, ex.ToString()); bReturn = false; }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return bReturn;
    }
    public void SendEmail(string name, string emailaddress, string subject, string msg)
    {
        /*
         * this method is used to send out an email.  credentials to be used
         * and connectivity information are stored in the statics.cs file.
         */
        SmtpClient oMail = null;
        // MailAddress From = null;
        // MailAddress SendTo = null;
        MailMessage Msg = null;

        try
        {
            System.Net.NetworkCredential basicCredential = new System.Net.NetworkCredential(Statics.EMAILUSERNAME, Statics.EMAILPASSWORD, Statics.EMAILDOMAIN);
            //System.Net.NetworkCredential basicCredential = new System.Net.NetworkCredential(Statics.EMAILUSERNAME, Statics.EMAILPASSWORD);
            oMail = new SmtpClient(Statics.SMTPHOST);
            oMail.Port = Statics.SMTPPORT;
            oMail.EnableSsl = true;
            oMail.DeliveryMethod = SmtpDeliveryMethod.Network;
            oMail.UseDefaultCredentials = false;
            oMail.Credentials = basicCredential;
            oMail.Timeout = 600000;
            Msg = new MailMessage();
            Msg.From = new MailAddress(Statics.FROMEMAIL);
            //Msg.To.Add(new MailAddress(emailaddress, name));
            //Msg.To.Add(new MailAddress("aniket.newale@theseesgroup.com", "Aniket Newale"));
            //Msg.To.Add(new MailAddress("naveed.javaid@theseesgroup.com", "Naveed Javaid"));
            Msg.To.Add(new MailAddress("rppsupport@theseesgroup.com", "RPP Support"));
            Msg.BodyEncoding = System.Text.Encoding.UTF8;
            Msg.IsBodyHtml = true;
            Msg.Subject = subject;
            Msg.Body = msg;
            oMail.Send(Msg);
            Msg.Dispose();
        }
        catch (Exception ex) { SendEmailError(1131, ex.ToString()); }
        finally
        {
            if (Msg != null) Msg.Dispose();
        }
    }
    public void SendEmailForgotPassword(EmailType type, Guid guid,string email)
    {
        /*
         * this method is used to send out an email.  credentials to be used
         * and connectivity information are stored in the statics.cs file.
         */
        SmtpClient oMail = null;
        // MailAddress from = null;
        MailAddress sendto = null;
        MailMessage Msg = null;
        try
        {
            //sendto = new MailAddress(GetEmailAddress(guid));
            sendto = new MailAddress(email);
            System.Net.NetworkCredential basicCredential = new System.Net.NetworkCredential(Statics.EMAILUSERNAME, Statics.EMAILPASSWORD, Statics.EMAILDOMAIN);
            //System.Net.NetworkCredential basicCredential = new System.Net.NetworkCredential(Statics.EMAILUSERNAME, Statics.EMAILPASSWORD);
            oMail = new SmtpClient(Statics.SMTPHOST);
            oMail.Port = Statics.SMTPPORT;
            oMail.EnableSsl = true;
            oMail.DeliveryMethod = SmtpDeliveryMethod.Network;
            oMail.UseDefaultCredentials = false;
            oMail.Timeout = 600000;
            oMail.Credentials = basicCredential;
            Msg = new MailMessage();
            Msg.From = new MailAddress(Statics.FROMEMAIL);
            //Msg.To.Add(new MailAddress(GetEmailAddress(guid)));
            Msg.To.Add(sendto);
            Msg.BodyEncoding = System.Text.Encoding.UTF8;
            Msg.IsBodyHtml = true;

            string pagetitle = HttpContext.Current.Application.Get("PageTitle").ToString();
            switch (type)
            {
                case EmailType.PwdReset:
                    Msg.Subject = "SEES & Vision America Referring Physician Portal" + " - Password Reset";
                    Msg.Body = "You are receiving this email because you requested a password reset on our portal.  If you did not ";
                    Msg.Body += "request this password reset, please delete this email and no further action will be necessary on your part.<BR><BR>";
                    Msg.Body += "If you did request the password reset, please click the link below:<BR><BR>";
                    Msg.Body += "<a href=\"http://extranet.eyehealthpartners.com/ResetPassword.aspx?email=" + API.Session.Encrypt(email.ToString().Trim()); //email.ToString();
                    Msg.Body += "\">Password Reset</a><BR><BR>";
                    Msg.Body += "If the above link doesn't work, copy and paste the following URL into your browser.<BR><BR>";
                    Msg.Body += "http://extranet.eyehealthpartners.com/resetpassword.aspx?email=" + API.Session.Encrypt(email.ToString().Trim());// email.ToString();
                    Msg.Body += "<BR><BR><BR> RPP Support Team <BR>";
                    Msg.Body += "RPPSupport@theseesgroup.com <BR>";
                    Msg.Body += "SEES Group, LLC <BR>";
                    Msg.Body += "[theseesgroup.com]TheSEESGroup.com <BR>";

                    // Load the image from a local file
                    //string imagePath = @"E:\Refferal Physician Portal\EHPortal\img\Signature.png";
                    //byte[] imageBytes = File.ReadAllBytes(imagePath);
                    //string base64Image = Convert.ToBase64String(imageBytes);

                    //// Add the base64-encoded image signature
                    //Msg.Body += "<img src='data:image/png;base64," + base64Image + "' alt='Email Signature' style='margin-top:15px;'>";
                    break;
                default:
                    break;
            }
            oMail.Send(Msg);
            Msg.Dispose();
        }
        catch (Exception ex) { SendEmailError(1022, ex.ToString()); }
        finally
        {
            if (Msg != null) Msg.Dispose();
        }
    }
    public void SendEmail(EmailType type, Guid guid)
    {
        /*
         * this method is used to send out an email.  credentials to be used
         * and connectivity information are stored in the statics.cs file.
         */
        SmtpClient oMail = null;
        // MailAddress from = null;
        MailAddress sendto = null;
        MailMessage Msg = null;
        try
        {
            sendto = new MailAddress(GetEmailAddress(guid));
            System.Net.NetworkCredential basicCredential = new System.Net.NetworkCredential(Statics.EMAILUSERNAME, Statics.EMAILPASSWORD, Statics.EMAILDOMAIN);
            //System.Net.NetworkCredential basicCredential = new System.Net.NetworkCredential(Statics.EMAILUSERNAME, Statics.EMAILPASSWORD);
            oMail = new SmtpClient(Statics.SMTPHOST);
            oMail.Port = Statics.SMTPPORT;
            oMail.EnableSsl = true;
            oMail.DeliveryMethod = SmtpDeliveryMethod.Network;
            oMail.UseDefaultCredentials = false;
            oMail.Timeout = 600000;
            oMail.Credentials = basicCredential;
            Msg = new MailMessage();
            Msg.From = new MailAddress(Statics.FROMEMAIL);
            //Msg.To.Add(new MailAddress(GetEmailAddress(guid)));
            Msg.To.Add(sendto);
            Msg.BodyEncoding = System.Text.Encoding.UTF8;
            Msg.IsBodyHtml = true;

            string pagetitle = HttpContext.Current.Application.Get("PageTitle").ToString();
            switch (type)
            {
                case EmailType.Validate:
                    Msg.Subject = pagetitle + " - Email Validation";
                    Msg.Body = "Thank you for registering eith the " + pagetitle + ".  Before you can logon to the web site, ";
                    Msg.Body += "you will need to validate your email address by clicking on the validation link below.<BR><BR>";
                    Msg.Body += "<a href=\"https://extranet.eyehealthpartners.com/ehportal/validate.aspx?g=" + guid.ToString();
                    Msg.Body += "\">Validation Link</a><BR><BR>";
                    Msg.Body += "If you have problems clicking on the link above, copy and past the following line into your web browser manually:<BR><BR>";
                    Msg.Body += "https://extranet.eyehealthpartners.com/ehportal/validate.aspx?g=" + guid.ToString();
                    break;
                case EmailType.PwdReset:
                    Msg.Subject = pagetitle + " - Password Reset";
                    Msg.Body = "You are receiving this email because you requested a password reset on our portal web site.  If you did not ";
                    Msg.Body += "request this password reset, please delete this email and no further action will be necessary on your part.<BR><BR>";
                    Msg.Body += "If you did request this password reset, please click the 'Reset Password' link below.<BR><BR>";
                    Msg.Body += "<a href=\"https://extranet.eyehealthpartners.com/ehportal/ResetPassword.aspx?g=" + guid.ToString();
                    Msg.Body += "\">Password Reset</a><BR><BR>";
                    Msg.Body += "If you have problems clicking on the link above, copy and paste the following line into your web browser manually:<BR><BR>";
                    Msg.Body += "https://extranet.eyehealthpartners.com/ehportal/resetpassword.aspx?g=" + guid.ToString();
                    break;
                case EmailType.UnlockAccount:
                    Msg.Subject = pagetitle + " - Account Unlock";
                    Msg.CC.Add(new MailAddress(API.Session.Email));
                    Msg.Body = "Your account has now been unlocked.  <BR>";
                    Msg.Body += "Your username is " + sendto + ".<BR>";
                    Msg.Body += "Your new password is now " + Statics.NewPassword + "<BR><BR>";
                    Msg.Body += "Your password is case-sensitive so please be sure to enter it correctly. ";
                    Msg.Body += "When you logon again, please be sure to change your password immediately to something you will ";
                    Msg.Body += "easily remember.<BR><BR>";
                    Msg.Body += "Please Note - This email was sent from an unmonitored mailbox.  Please do not reply to this email. ";
                    Msg.Body += "If you have additional issues / questions, please contact one of our clinics for assistance.<BR>";
                    break;
                case EmailType.VIPSend:
                    Msg.Subject = pagetitle + " - Logon Credentials";
                    Msg.CC.Add(new MailAddress(API.Session.Email));
                    Msg.Body = "We are pleased to inform you that we have setup your accounts on our Patient Appointment Portal. <BR><BR>";
                    Msg.Body += "Please click <a href=\"https://extranet.eyehealthpartners.com/ehportal\">here</a> to logon.<BR><BR>";
                    Msg.Body += "Your username is: " + sendto + "<BR>";
                    Msg.Body += "Your current password is: " + API.Session.GetCurrentPassword(guid) + "<BR><BR>";
                    break;
                default:
                    break;
            }
            oMail.Send(Msg);
            Msg.Dispose();
        }
        catch (Exception ex) { SendEmailError(1022, ex.ToString()); }
        finally
        {
            if (Msg != null) Msg.Dispose();
        }
    }
    public string GetCurrentPassword(Guid guid)
    {
        /*
         * This function returns the current password of a user account
         * based on the provided GUID.
         */
        string pwd = "";
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("GetCurrentPassword", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@GUID", SqlDbType.UniqueIdentifier).Value = guid;
        try
        {
            cn.Open();
            pwd = cm.ExecuteScalar().ToString();
        }
        catch (Exception ex) { GetCurrentPasswordError(1153, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return pwd;
    }
    public bool SetInitialPassword(Guid guid, string password)
    {
        /*
         * this function changes a password for the account
         * specified by the GUID.
         */
        bool bReturn = false;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("ChangePassword", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@GUID", SqlDbType.UniqueIdentifier).Value = guid;
        cm.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = password;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    bReturn = true;
                }
            }
        }
        catch (Exception ex) { SetInitialPasswordError(1023, ex.ToString()); bReturn = false; }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return bReturn;
    }
    public void UpdateAccount(Account data)
    {
        /*
         * this method adds a new user account or updates an existing user account.
         */
        SqlConnection cn = null;
        SqlCommand cm = null;

        try
        {
            cn = new SqlConnection(Statics.EHPconnstring);
            cm = new SqlCommand("AddUpdateAccount", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Clear();
            cm.Parameters.Add("@UserID", SqlDbType.Int).Value = data.UserID;
            cm.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 255).Value = data.EmailAddress;
            cm.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = data.Password;
            cm.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = data.Name;
            cm.Parameters.Add("@Active", SqlDbType.Bit).Value = data.Active;
            cm.Parameters.Add("@IsAdmin", SqlDbType.Bit).Value = data.IsAdmin;
            cm.Parameters.Add("@IsSetupAdmin", SqlDbType.Bit).Value = data.IsSetupAdmin;

            cn.Open();
            cm.ExecuteNonQuery();

        }
        catch (Exception ex) { UpdateAccountError(1158, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
    }
    public void UpdateDatabase(string connstring, string email, string npi)
    {
        /*
         * This method updates the email address for the referring doctor in 
         * the centricity database.  Many of the referring doctor records in 
         * centricity did not have email addresses at the time of this code
         * writing.  The purpose of this was to collect that info through this
         * web site.
         */
        string q = "UPDATE DoctorFacility SET EmailAddress = @EmailAddress WHERE NPI = @NPI AND Type = 3 ";

        SqlConnection cn = new SqlConnection(connstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 255).Value = email;
        cm.Parameters.Add("@NPI", SqlDbType.VarChar, 80).Value = npi;
        try
        {
            cn.Open();
            cm.ExecuteNonQuery();
        }
        catch (Exception ex) { UpdateDatabaseError(1024, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
    }
    public void UpdateLastLogonDate(int userid)
    {
        /*
         * Each time a user logs onto the site, we update the last date/time. 
         * This can be used later in a future version to track basic usage.
         */
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("UpdateLastLogonDate", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Add("@UserID", SqlDbType.Int).Value = userid;
        cm.Parameters.Add("@LastLogon", SqlDbType.DateTime).Value = DateTime.Now;
        try
        {
            cn.Open();
            cm.ExecuteNonQuery();
        }
        catch (Exception ex) { UpdateLastLogonDateError(1025, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
    }
    public void UpdateNONVIPAccount(Account acct)
    {
        /*
         * This method updates the user account info for a non-master account
         */
        if (acct != null)
        {
            SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
            SqlCommand cm = new SqlCommand("SaveNONVIPAccount", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Clear();
            cm.Parameters.Add("@UserID", SqlDbType.Int).Value = acct.UserID;
            cm.Parameters.Add("@NPI", SqlDbType.VarChar, 80).Value = acct.NPINumber;
            cm.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 255).Value = acct.EmailAddress;
            cm.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = acct.Password;

            try
            {
                cn.Open();
                cm.ExecuteNonQuery();
            }
            catch (Exception ex) { UpdateNONVIPAccountError(1166, ex.ToString()); }
            finally
            {
                if (cm != null) cm.Dispose();
                if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
            }
        }
    }
    public void UpdatePatientPhoneNumber(int patientprofileid, string phonenumber)
    {
        /*
         * This method updates the patient's phone number in Centricity.  The referring doctor
         * provides this information on the web site in the course of booking an appointment or 
         * adding / updating a patient.  We collect that data here and send it to centricity
         * to keep our patient information updated.
         */
        if (Session.SWState == "") { UpdatePatientPhoneNumberError(1136, "API.Session.AdminState not defined."); return; }

        string q = "UPDATE PatientProfile SET Phone1 = @Phone WHERE PatientProfileID = @PatientProfileID ";
        SqlConnection cn = null;
        if (Session.SWState == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (Session.SWState == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = phonenumber;
        cm.Parameters.Add("@PatientProfileID", SqlDbType.Int).Value = patientprofileid;
        try
        {
            cn.Open();
            cm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            UpdatePatientPhoneNumberError(1137, ex.ToString());
        }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
    }

    public bool UserLogon(string email, string password)
    {
        /*
         * This function validates a user logon.  It also returns error messages
         * when it cannot be validated.
         */
        bool bReturn = false;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("UserLogon", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 255).Value = email;
        cm.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = password;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    if (!bool.Parse(dr["Active"].ToString()))
                    {
                        // Account is not active
                        UserLogonError(1026, "The portal is under maintenance. As soon as the portal is up and running, you will be informed.");
                        //UserLogonError(1026, "Your account is not currently enabled.  Please contact one of our offices for more information.");
                        return false;
                    }
                    if (int.Parse(dr["InvalidLogonAttempts"].ToString()) >= 3)
                    {
                        // too many attempts
                        UserLogonError(1027, "Your account is locked due to too many unsuccessful logon attempts.");
                        return false;
                    }
                    if (!bool.Parse(dr["Validated"].ToString()))
                    {
                        // Account not validated yet
                        UserLogonError(1028, "Your account has not been validated yet.  Please check your email for the validation link or click the 'Resend Validation' link.");
                        return false;
                    }
                    if (dr["Password"].ToString() != password)
                    {
                        // invalid logon
                        InvalidLogon(email);
                        return false;
                    }
                    UserID = int.Parse(dr["UserID"].ToString());
                    Email = email;
                  //  GUID = GetGUID(email);
                    IsAdmin = bool.Parse(dr["IsAdmin"].ToString());
                    IsSetupAdmin = bool.Parse(dr["IsSetupAdmin"].ToString());
                    IsMiddleTN = bool.Parse(dr["MiddleTN"].ToString());
                    IsAL = bool.Parse(dr["AL"].ToString());
                    IsEastTN = bool.Parse(dr["EastTN"].ToString());
                   // IsState = dr["State"].ToString();
                    //NPI = GetNPI(GUID);
                    loggedon = true;
                    int countTN = 0;
                    int countAL = 0;
                    int countEH = 0;
                    //API.Location[] data = API.Session.GetLocations(GUID);
                    //if (data != null)
                    //{
                    //    for (int a = 0; a < data.Length; a++)
                    //    {
                    //        string state = "";
                    //        if (data[a].Database == "Nashville")
                    //        {
                    //            state = "TN";
                    //            countTN = countTN + 1;
                    //        }
                    //        else if (data[a].Database == "Birmingham")
                    //        {
                    //            state = "AL";
                    //            countAL = countAL + 1;
                    //        }
                    //    }
                    //    // if(countTN == data.Length)
                    //    // {
                    //    //     bReturn = true;
                    //    // }
                    //    //else if (countAL == data.Length)
                    //    // {
                    //    //     bReturn = false;
                    //    //     API.Session.LoggedOn = false;
                    //    //     UserLogonError(1029, "This application is under maintenance for Alabama locations. For scheduling assistance, please call a VisionAmerica clinic.");
                    //    //     return false;
                    //    // }
                    //    ////else if((countAL+ countTN) == data.Length)
                    //    //// {
                    //    ////     bReturn = false;
                    //    ////     API.Session.LoggedOn = false;
                    //    ////     UserLogonError(1029, "This application is under maintenance for Alabama locations. For scheduling assistance, please call a VisionAmerica clinic.");
                    //    ////     return false;
                    //    //// }
                    //    // else
                    //    // {
                    //    //     bReturn = true;
                    //    // }
                    //}
                    //else
                    //{
                    //    bReturn = true;
                    //}
                    //API.Location[] data1 = API.Session.GetLocationsforTN(GUID);
                    //if (data1 != null)
                    //{
                    //    for (int a = 0; a < data1.Length; a++)
                    //    {
                    //        if (data1[a].State == "TN")
                    //        {
                    //            countEH = countEH + 1;
                    //        }
                    //    }
                    //}
                    //if (data1 != null)
                    //{
                    //    //if (countTN == data.Length || countEH > 0)
                    //    //{
                    //    //    bReturn = true;
                    //    //}
                    //    //else if (countAL == data.Length)
                    //    //{
                    //    //    bReturn = false;
                    //    //    API.Session.LoggedOn = false;
                    //    //    UserLogonError(1029, "This application is under maintenance for Alabama locations. For scheduling assistance, please call a VisionAmerica clinic.");
                    //    //    return false;
                    //    //}
                    //    //else if((countAL+ countTN) == data.Length)
                    //    // {
                    //    //     bReturn = false;
                    //    //     API.Session.LoggedOn = false;
                    //    //     UserLogonError(1029, "This application is under maintenance for Alabama locations. For scheduling assistance, please call a VisionAmerica clinic.");
                    //    //     return false;
                    //    // }
                    //    //else
                    //    //{
                    //      //  bReturn = true;
                    //    //}
                    //}
                    //else
                    //{
                        //if ( countEH > 0)
                        //{
                        bReturn = true;
                        //   }
                  //  }

                }
                else
                {
                    UserLogonError(1029, "Invalid email address / password combination.<BR />Please try again.");
                    return false;
                }
            }
        }
        catch (Exception ex) { UserLogonError(1030, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return bReturn;
    }
    //public bool UserLogon(string email, string password)
    //{
    //    /*
    //     * This function validates a user logon.  It also returns error messages
    //     * when it cannot be validated.
    //     */
    //    bool bReturn = false;
    //    SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
    //    SqlCommand cm = new SqlCommand("UserLogon", cn);
    //    cm.CommandType = CommandType.StoredProcedure;
    //    cm.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 255).Value = email;
    //    cm.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = password;
    //    SqlDataReader dr = null;
    //    try
    //    {
    //        cn.Open();
    //        dr = cm.ExecuteReader();
    //        if (dr != null)
    //        {
    //            if (dr.Read())
    //            {
    //                if (!bool.Parse(dr["Active"].ToString()))
    //                {
    //                    // Account is not active
    //                    UserLogonError(1026, "Your account is not currently enabled.  Please contact one of our offices for more information.");
    //                    return false;
    //                }
    //                if (int.Parse(dr["InvalidLogonAttempts"].ToString()) >= 3)
    //                {
    //                    // too many attempts
    //                    UserLogonError(1027, "Your account is locked due to too many unsuccessful logon attempts.");
    //                    return false;
    //                }
    //                if (!bool.Parse(dr["Validated"].ToString()))
    //                {
    //                    // Account not validated yet
    //                    UserLogonError(1028, "Your account has not been validated yet.  Please check your email for the validation link or click the 'Resend Validation' link.");
    //                    return false;
    //                }
    //                if (dr["Password"].ToString() != password)
    //                {
    //                    // invalid logon
    //                    InvalidLogon(email);
    //                    return false;
    //                }
    //                UserID = int.Parse(dr["UserID"].ToString());
    //                Email = email;
    //                GUID = GetGUID(email);
    //                IsAdmin = bool.Parse(dr["IsAdmin"].ToString());
    //                IsSetupAdmin = bool.Parse(dr["IsSetupAdmin"].ToString());
    //                //NPI = GetNPI(GUID);
    //                loggedon = true;                   
    //                bReturn = true;
    //            }
    //            else
    //            {
    //                UserLogonError(1029, "Invalid email address / password combination.<BR />Please try again.");
    //                return false;
    //            }
    //        }
    //    }
    //    catch (Exception ex) { UserLogonError(1030, ex.ToString()); }
    //    finally
    //    {
    //        if (dr != null) { if (!dr.IsClosed) dr.Close(); }
    //        if (cm != null) cm.Dispose();
    //        if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
    //    }
    //    return bReturn;
    //}
    public bool ValidateEmail(Guid guid)
    {
        /*
         * this function marks a user's email address as validated in the 
         * EHPortal database.  when a referring provider self registers, they are
         * sent an email with a validation link.  When they click that link, 
         * it brings them back to the website and validates their email address here 
         * in this function.
         */
        bool bReturn = false;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("ValidateEmail", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@GUID", SqlDbType.UniqueIdentifier).Value = guid;

        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    API.Session.UserID = int.Parse(dr["UserID"].ToString());
                    API.Session.GUID = guid;
                    UpdateDatabase(Statics.DEMOconnstring, dr["EmailAddress"].ToString(), dr["NPI"].ToString());
                    UpdateDatabase(Statics.CPSALconnstring, dr["EmailAddress"].ToString(), dr["NPI"].ToString());
                    UpdateDatabase(Statics.CPSTNconnstring, dr["EmailAddress"].ToString(), dr["NPI"].ToString());
                    bReturn = true;
                }
                else
                    bReturn = false;
            }
        }
        catch (Exception ex) { ValidateEmailError(1031, ex.ToString()); bReturn = false; }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return bReturn;
    }
    public bool ValidateCurrentPassword(string password)
    {
        /*
         * this function validates the password.
         */
        bool bReturn = false;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("ValidateCurrentPassword", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        //        cm.Parameters.Add("@GUID", SqlDbType.UniqueIdentifier).Value = Session.GUID;// API.Session.Email 
        cm.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = API.Session.Email.ToString();
        cm.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = password;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    if (int.Parse(dr["UserID"].ToString()) > 0) bReturn = true;
                }
            }
        }
        catch (Exception ex) { Session.ValidateCurrentPasswordError(1081, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return bReturn;
    }
    public string[] GetFacilityContactList(int facilityid)
    {
        /*
         * this function returns a list of contacts for a given
         * facility.
         */
        string[] aReturn = null;
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("GetFacilityContactList", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@FacilityID", SqlDbType.Int).Value = facilityid;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    // increase array +1 
                    if (aReturn == null) aReturn = new string[1];
                    else
                    {
                        string[] aTemp = new string[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (string[])aTemp.Clone();
                    }
                    int a = aReturn.Length - 1;
                    aReturn[a] = dr["FacilityContactID"].ToString() + "^" + dr["Name"].ToString() + "^" + dr["EmailAddress"].ToString();
                }
            }
        }
        catch (Exception ex) { GetFacilityContactListError(1089, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return aReturn;
    }
    public void AddUpdateFacilityContact(int facilitycontactid, int facilityid, string name, string emailaddress)
    {
        /*
         * this method adds or updates a contact for a facility.
         */
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("AddUpdateFacilityContact", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@FacilityContactID", SqlDbType.Int).Value = facilitycontactid;
        cm.Parameters.Add("@FacilityID", SqlDbType.Int).Value = facilityid;
        cm.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = name;
        cm.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 255).Value = emailaddress;
        try
        {
            cn.Open();
            cm.ExecuteNonQuery();
        }
        catch (Exception ex) { AddUpdateFacilityContactError(1092, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
    }
    public string GetFacilityContactDetails(int facilitycontactid)
    {
        /*
         * this function returns the name and email address of a contact
         * record for a given facility.  It separates the two fields with the ^ character.
         */
        string sReturn = "";
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("GetFacilityContactDetails", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@FacilityContactID", SqlDbType.Int).Value = facilitycontactid;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    sReturn = dr["Name"].ToString() + "^" + dr["EmailAddress"].ToString();
                }
            }
        }
        catch (Exception ex) { GetFacilityContactDetailsError(1093, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return sReturn;
    }
    public void DeleteFacilityContact(int facilitycontactid)
    {
        /*
         * this method deletes a contact from the EHPortal database.
         */
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("DeleteFacilityContact", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@FacilityContactID", SqlDbType.Int).Value = facilitycontactid;
        try
        {
            cn.Open();
            cm.ExecuteNonQuery();
        }
        catch (Exception ex) { DeleteFacilityContactError(1094, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
    }
    public int[] GetCPSScheduleIDs(string state, int CPSDoctorID, int CPSFacilityID, DateTime startdate, DateTime stopdate)
    {
        /*
         * this function returns a list of schedule IDs from the centricity database for a given date range.
         */
        int[] aReturn = null;

        /* 
         * THIS QUERY WAS REMARKED OUT ON 3/24/2016 BY DAVE COOPER - SEE UPDATED QUERY BELOW THIS ONE.
         */
        /*        string q = "SELECT Schd.ScheduleId As ScheduleId ";
                q += "FROM Schedule As Schd ";
                q += "Inner Join ScheduleResource As SchdResource On Schd.ScheduleId = SchdResource.ScheduleId ";
                q += "Inner Join ApptSlot On Schd.ScheduleId = ApptSlot.ScheduleId ";
                q += "Where Schd.DoctorResourceId = @DoctorID ";
                q += "And IsNull (Schd.Inactive, 0) = 0 ";
                q += "And SchdResource.ScheduleDate <= @startdate ";
                q += "And ApptSlot.Start<@stopdate And ApptSlot.Stop> @startdate ";
                q += "AND ApptSlot.FacilityID = @FacilityID ";
                q += "Group By Schd.[Description], Schd.DoctorResourceId, Schd.LongTickMark, ";
                q += "Schd.ShortTickMark, Schd.Inactive, Schd.ScheduleId, ApptSlot.FacilityId ";
        */
        string q = "SELECT DISTINCT ScheduleID = s.ScheduleId ";
        q += "FROM Schedule s ";
        //q += "Inner Join ScheduleResource As SchdResource On Schd.ScheduleId = SchdResource.ScheduleId ";
        q += "Inner Join ApptSlot slot On s.ScheduleId = slot.ScheduleId ";
        q += "WHERE ISNULL(s.Inactive,0) = 0 ";
        q += "AND s.DoctorResourceId = @DoctorID ";
        q += "AND slot.FacilityID = @FacilityID ";
        q += "AND slot.Start >= @startdate ";
        SqlConnection cn = null;
        if (state == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (state == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        SqlDataReader dr = null;
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@DoctorID", SqlDbType.Int).Value = CPSDoctorID;
        cm.Parameters.Add("@FacilityID", SqlDbType.Int).Value = CPSFacilityID;
        cm.Parameters.Add("@startdate", SqlDbType.DateTime).Value = startdate;
        //cm.Parameters.Add("@stopdate", SqlDbType.DateTime).Value = stopdate;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    // increase array +1
                    if (aReturn == null) aReturn = new int[1];
                    else
                    {
                        int[] aTemp = new int[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (int[])aTemp.Clone();
                    }
                    int a = aReturn.Length - 1;
                    aReturn[a] = int.Parse(dr["ScheduleID"].ToString());
                }
            }
        }
        catch (Exception ex) { GetCPSScheduleIDsError(1096, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return aReturn;
    }
    public ApptSlot[] GetOpenApptSlots(int recordlimit, DateTime mindate, DateTime maxdate, string state, int cpsdoctorid, int cpsfacilityid, int[] cpsappttypeids)
    {
        /*
         * this function returns a list of open appointment slots from the 
         * centricity database for the given date range.
         */
        ApptSlot[] aReturn = null;
        int[] scheduleid = GetCPSScheduleIDs(state, cpsdoctorid, cpsfacilityid, mindate, maxdate);
        string scheduleids = "";
        if (scheduleid != null)
        {
            if (scheduleid.Length > 0)
            {
                for (int a = 0; a < scheduleid.Length; a++)
                {
                    scheduleids += scheduleid[a].ToString();
                    if ((scheduleid.Length - (a + 1)) > 0)
                        scheduleids += ",";
                }
            }
        }
        if (scheduleids == "")
        {
            GetOpenApptSlotsError(1139, "Selected Doctor does not have any Schedules at the selected Facility."); return null;
        }

        string appttypeids = "";
        for (int b = 0; b < cpsappttypeids.Length; b++)
        {
            appttypeids += cpsappttypeids[b].ToString();
            if ((cpsappttypeids.Length - (b + 1)) > 0)
                appttypeids += ",";
        }
        /* 
         * THIS IS THE OLD VERSION OF THE SCRIPT
         * REMARKED OUT ON 3/24/2016 BY DAVE COOPER
         * REASON: CHANGE OF QUERY
         
        string q = "SELECT ";
        if (recordlimit > 0) q += "TOP " + recordlimit.ToString() + " ";
        q += "aa.ApptSlotID, aa.ScheduleID, slot.Start, DATEADD(mi,atype.duration,slot.start) as Stop ";
        q += "FROM AppointmentsAlloc aa ";
        q += "LEFT JOIN ApptType atype ON aa.ApptTypeID = atype.ApptTypeID ";
        q += "LEFT JOIN ApptSlot slot ON aa.ApptSlotID = slot.ApptSlotID ";
        q += "LEFT JOIN AppointmentsAlloc aa2 ON aa2.ApptSlotID = aa.ApptSlotID ";
        q += "LEFT JOIN ApptType aa2type ON aa2.ApptTypeID = aa2type.ApptTypeID ";
        q += "WHERE aa.ScheduleID IN (" + scheduleids + ") ";
        q += "AND aa.ApptTypeId IN (" + appttypeids + ") ";
        q += "AND NOT (slot.Stop < @StartDate OR slot.Start > @StopDate) ";
        q += "AND 0=ISNULL(slot.Status,0) ";
        q += "GROUP BY aa.ApptSlotID, aa.ScheduleID, slot.Start, atype.Duration ";
        q += "ORDER BY slot.Start ";
         */
        /* 
         * THIS IS THE NEW VERSION, PUT IN PLACE ON 3/24/2016 BY DAVE COOPER
         */
        string q = "SELECT ";
        if (recordlimit > 0) q += "TOP " + recordlimit.ToString() + " ";
        q += "aa.ApptSlotID, aa.ScheduleID, slot.Start, [Stop]=DateAdd(mi, aType.duration, slot.Start) ";
        q += "FROM AppointmentsAlloc aa ";
        q += "INNER JOIN ApptType aType ON aa.ApptTypeID = aType.ApptTypeID ";
        q += "LEFT OUTER JOIN ApptSlot slot ON aa.ApptSlotID = slot.ApptSlotID ";
        q += "WHERE aa.ScheduleID IN (" + scheduleids + ") ";
        q += "AND aa.ApptTypeID IN (" + appttypeids + ") ";
        q += "AND slot.FacilityID = @FacilityID ";
        q += "AND NOT (slot.Stop < @StartDate OR slot.Start > @StopDate) ";
        q += "AND ISNULL(slot.Status,0) = 0 ";
        q += "AND slot.ListOrder > 0 ";
        q += "ORDER BY slot.Start ";

        SqlConnection cn = null;
        if (state == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (state == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = mindate;
        cm.Parameters.Add("@StopDate", SqlDbType.DateTime).Value = maxdate;
        cm.Parameters.Add("@FacilityID", SqlDbType.Int).Value = cpsfacilityid;

        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    // increase array +1
                    if (aReturn == null) aReturn = new ApptSlot[1];
                    else
                    {
                        ApptSlot[] aTemp = new ApptSlot[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (ApptSlot[])aTemp.Clone();
                    }
                    int c = aReturn.Length - 1;
                    aReturn[c] = new ApptSlot();
                    aReturn[c].ApptSlotID = int.Parse(dr["ApptSlotID"].ToString());
                    aReturn[c].ScheduleID = int.Parse(dr["ScheduleID"].ToString());
                    aReturn[c].Start = DateTime.Parse(dr["Start"].ToString());
                    aReturn[c].Stop = DateTime.Parse(dr["Stop"].ToString());
                }
            }
        }
        catch (Exception ex) { GetOpenApptSlotsError(1097, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return aReturn;
    }
    public CPSPatient[] GetRefDrPatients(string state, int refdrid)
    {
        /*
         * this function returns a list of the referring providers patients.
         */
        CPSPatient[] aReturn = null;
        string q = "SELECT PatientProfileID, First = ISNULL(First, ''), Middle = ISNULL(Middle, ''), ";
        q += "Last = ISNULL(Last, ''), Sex = ISNULL(Sex, ''), BirthDate = ISNULL(Birthdate, '1/1/1900'), ";
        q += "Phone1 = ISNULL(Phone1, ''), EMailAddress = ISNULL(EmailAddress, ''), SSN = ISNULL(SSN, '') ";
        q += "FROM PatientProfile WHERE(RefDoctorId = @LocationID OR PrimaryCareDoctorId = @LocationID) ";
        q += "ORDER BY Last, First, Middle ";

        SqlConnection cn = null;
        if (state == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (state == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        SqlDataReader dr = null;
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@LocationID", SqlDbType.Int).Value = refdrid;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    // increase array +1
                    if (aReturn == null) aReturn = new CPSPatient[1];
                    else
                    {
                        CPSPatient[] aTemp = new CPSPatient[aReturn.Length + 1];
                        Array.Copy(aReturn, aTemp, aReturn.Length);
                        aReturn = (CPSPatient[])aTemp.Clone();
                    }
                    int a = aReturn.Length - 1;
                    aReturn[a] = new CPSPatient();
                    aReturn[a].DOB = DateTime.Parse(dr["BirthDate"].ToString());
                    aReturn[a].EmailAddress = dr["EmailAddress"].ToString();
                    aReturn[a].First = dr["First"].ToString();
                    aReturn[a].Gender = dr["Sex"].ToString();
                    aReturn[a].Last = dr["Last"].ToString();
                    aReturn[a].Middle = dr["Middle"].ToString();
                    aReturn[a].PatientProfileID = int.Parse(dr["PatientProfileID"].ToString());
                    aReturn[a].Phone = dr["Phone1"].ToString();
                    if (dr["SSN"].ToString() != "")
                    {
                        if (dr["SSN"].ToString().Length == 9)
                        {
                            aReturn[a].SSN4 = dr["SSN"].ToString().Substring(5);
                            aReturn[a].SSN = dr["SSN"].ToString();
                        }
                    }
                }
            }
        }
        catch (Exception ex) { GetRefDrPatientsError(1098, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return aReturn;
    }
    public CPSPatient PatientExists(string state, CPSPatient data)
    {
        /*
         * This function verifies the patient exists int he centricity database
         * and retuns the data to the web site.
         */
        CPSPatient ExistingPatient = new CPSPatient();
        string q = "SELECT PatientProfileID, Middle=ISNULL(Middle,''), SSN=ISNULL(SSN,''), EmailAddress=ISNULL(EmailAddress,''), Phone = ISNULL(Phone1,'') ";
        q += "FROM PatientProfile ";
        q += "WHERE First = @First AND Last = @Last AND Birthdate = @DOB AND Sex = @Gender ";
        SqlConnection cn = null;
        if (state == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (state == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        SqlDataReader dr = null;
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@First", SqlDbType.VarChar, 35).Value = data.First;
        cm.Parameters.Add("@Last", SqlDbType.VarChar, 60).Value = data.Last;
        cm.Parameters.Add("@DOB", SqlDbType.DateTime).Value = data.DOB;
        cm.Parameters.Add("@Gender", SqlDbType.VarChar, 1).Value = data.Gender;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    ExistingPatient.First = data.First;
                    ExistingPatient.Last = data.Last;
                    ExistingPatient.DOB = data.DOB;
                    ExistingPatient.Gender = data.Gender;
                    ExistingPatient.EmailAddress = dr["EmailAddress"].ToString();
                    ExistingPatient.PatientProfileID = int.Parse(dr["PatientProfileID"].ToString());
                    ExistingPatient.Phone = dr["Phone"].ToString();
                    if (dr["SSN"].ToString() != "")
                    {
                        if (dr["SSN"].ToString().Length == 9)
                        {
                            ExistingPatient.SSN = dr["SSN"].ToString();
                            ExistingPatient.SSN4 = dr["SSN"].ToString().Substring(5);
                        }
                    }
                    ExistingPatient.Middle = dr["Middle"].ToString();
                }
            }
        }
        catch (Exception ex) { PatientExistsError(1118, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return ExistingPatient;
    }

    public DataTable GetPatientList()
    {
        string NPIValue = API.Session.patientId;
        //string q = "SELECT *  from PatientMaster ";
        string q = "";
        if (API.Session.ChState.ToString() == "AL")
        {
             q = "SELECT * from PatientMaster_AL ";
        }
        else if (API.Session.ChState.ToString() == "ETN")
        {
            q = "SELECT * from PatientMaster_EastTN ";
        }
        else
        {
             q = "SELECT * from PatientMaster_MiddleTN ";
        }
        
        SqlConnection cn = null;
        cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;

        SqlDataReader dr = null;
        DataTable dt = new DataTable();
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                dt.Load(dr);
            }
        }
        catch (Exception ex)
        {
            CareCloudMasterUserListError(1144, ex.ToString());
        }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return dt;
    }
    public void CreateNewPatient(string state, CPSPatient data, int RefDrID)
    {
        /*
         * in the course of booking an appointment, we decided to allow the referring 
         * provider to actually create a patient record in centricity once we verified
         * that the patient did NOT already exist.  There is still a change of this
         * creating a duplicate record though.
         */
        string q = "INSERT INTO PatientProfile (First, Last, Sex, Birthdate, Phone1, RefDoctorID, SignatureOnFile, SignatureSourceMID, Created, CreatedBy, LastModified, LastModifiedBy) ";
        q += "VALUES(@First, @Last, @Sex, @Birthdate, @Phone1, @RefDrID, @SignatureOnFile, @SignatureSourceMID, @Created, @CreatedBy, @LastModified, @LastModifiedBy) ";

        SqlConnection cn = null;
        if (state == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (state == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@First", SqlDbType.VarChar, 35).Value = data.First;
        cm.Parameters.Add("@Last", SqlDbType.VarChar, 60).Value = data.Last;
        cm.Parameters.Add("@Sex", SqlDbType.VarChar, 1).Value = data.Gender;
        cm.Parameters.Add("@Birthdate", SqlDbType.DateTime).Value = data.DOB;
        cm.Parameters.Add("@Phone1", SqlDbType.VarChar, 15).Value = data.Phone;
        cm.Parameters.Add("@RefDrID", SqlDbType.Int).Value = RefDrID;
        cm.Parameters.Add("@SignatureOnFile", SqlDbType.SmallInt).Value = 1;
        cm.Parameters.Add("@SignatureSourceMID", SqlDbType.Int).Value = Session.GetSignatureSourceMID(state, "Signature On File");
        cm.Parameters.Add("@Created", SqlDbType.DateTime).Value = DateTime.Now;
        cm.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 30).Value = "EHPortal";
        cm.Parameters.Add("@LastModified", SqlDbType.DateTime).Value = DateTime.Now;
        cm.Parameters.Add("@LastModifiedBy", SqlDbType.VarChar, 30).Value = "EHPortal";

        try
        {
            cn.Open();
            cm.ExecuteReader();
        }
        catch (Exception ex) { CreateNewPatientError(1117, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
    }

    public DataTable GetPatient(string first_name, string last_name, string gender_code, DateTime date_of_birth, string phone_number, string PatientSSN, string City, string location)
    {
        string result = "";
        DataTable dt = new DataTable();
        SqlDataReader Dr = null;
        string q = "";
        if (location == "AL")
        {
            q = "If(@PatientSSN!='') begin  Select PID,FirstName,LastName,PatientName,PhoneNumber,Gender,PatientSSN,Convert(Varchar(10),DateOfBirth,126) as DateOfBirth   from  PatientMaster_AL where PatientSSN=@PatientSSN ";
            q += "  end ";
            q += "else begin Select PID,FirstName,LastName,PatientName,PhoneNumber, Gender,PatientSSN,Convert(Varchar(10),DateOfBirth,126) as DateOfBirth from  PatientMaster_AL where FirstName=@first_name AND LastName= @last_name  and DateOfBirth=@date_of_birth ";
            q += "  end ";
        }
        else if (location == "ETN")
        {
            q = "If(@PatientSSN!='') begin  Select PID,FirstName,LastName,PatientName,PhoneNumber,Gender,PatientSSN,Convert(Varchar(10),DateOfBirth,126) as DateOfBirth   from  PatientMaster_EastTN where PatientSSN=@PatientSSN ";
            q += "  end ";
            q += "else begin Select PID,FirstName,LastName,PatientName,PhoneNumber, Gender,PatientSSN,Convert(Varchar(10),DateOfBirth,126) as DateOfBirth from  PatientMaster_EastTN where FirstName=@first_name AND LastName= @last_name  and DateOfBirth=@date_of_birth ";
            q += "  end ";
        }
        else
        {
            q = "If(@PatientSSN!='') begin  Select PID,FirstName,LastName,PatientName,PhoneNumber,Gender,PatientSSN,Convert(Varchar(10),DateOfBirth,126) as DateOfBirth   from  PatientMaster_MiddleTN where PatientSSN=@PatientSSN ";
            q += "  end ";
            q += "else begin Select PID,FirstName,LastName,PatientName,PhoneNumber, Gender,PatientSSN,Convert(Varchar(10),DateOfBirth,126) as DateOfBirth from  PatientMaster_MiddleTN where FirstName=@first_name AND LastName= @last_name  and DateOfBirth=@date_of_birth ";
            q += "  end ";
        }
        //string q = "If(@PatientSSN!='') begin  Select PID,FirstName,LastName,PatientName,PhoneNumber,Gender,PatientSSN,Convert(Varchar(10),DateOfBirth,126) as DateOfBirth   from  PatientMaster where PatientSSN=@PatientSSN ";
        //q += "  end ";
        //q += "else begin Select PID,FirstName,LastName,PatientName,PhoneNumber, Gender,PatientSSN,Convert(Varchar(10),DateOfBirth,126) as DateOfBirth from  PatientMaster where PatientName=(@first_name+' '+@last_name)  and DateOfBirth=@date_of_birth ";
        //q += "  end ";
        SqlConnection cn = null;
        cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@first_name", SqlDbType.VarChar, 35).Value = first_name;
        cm.Parameters.Add("@last_name", SqlDbType.VarChar, 60).Value = last_name;
        cm.Parameters.Add("@gender_code", SqlDbType.VarChar, 1).Value = gender_code;
        cm.Parameters.Add("@date_of_birth", SqlDbType.DateTime).Value = date_of_birth;
        cm.Parameters.Add("@phone_number", SqlDbType.VarChar, 30).Value = phone_number;
        cm.Parameters.Add("@PatientSSN", SqlDbType.VarChar, 100).Value = PatientSSN;
        cm.Parameters.Add("@city", SqlDbType.VarChar, 100).Value = City;
     //   cm.Parameters.Add("@location", SqlDbType.VarChar, 100).Value = location;
        try
        {
            cn.Open();
            // cm.ExecuteReader();
            Dr = cm.ExecuteReader();
            if (Dr != null)
            {
                dt.Load(Dr);
            }

        }
        catch (Exception ex) { CreateNewPatientError(1117, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return dt;
    }
    public string ValidatePatient(string first_name, string last_name, string gender_code, DateTime date_of_birth, string phone_number, string PatientSSN, string City ,string location)
    {
        string result = "";
        SqlDataReader Dr = null;
        string q = "";
        if (location == "AL")
        {
            q = @"IF(@PatientSSN='') 
                        BEGIN
                        IF EXISTS (SELECT PID FROM  PatientMaster_AL WHERE FirstName=@first_name AND LastName= @last_name AND DateOfBirth=@date_of_birth )
                        BEGIN
                        Select  1 as Count, 0 as SSNExists  
                        END
                        ELSE
                        BEGIN
                        Select  0 as Count, 0 as SSNExists 
                        END
                        END
                        ELSE
                        BEGIN
                        IF EXISTS (SELECT PID FROM  PatientMaster_AL WHERE  FirstName=@first_name AND LastName= @last_name  AND DateOfBirth=@date_of_birth  AND  PatientSSN=@PatientSSN)
                        BEGIN
                        Select  1 as Count , 0 as SSNExists 
                        END
                        ELSE IF EXISTS (SELECT PID FROM  PatientMaster_AL WHERE  FirstName=@first_name AND LastName= @last_name  AND DateOfBirth=@date_of_birth  AND  PatientSSN!=@PatientSSN)
                        BEGIN
                        Select  0 as Count , 0 as SSNExists 
                        END
                        ELSE IF EXISTS (SELECT PID FROM  PatientMaster_AL WHERE  PatientSSN=@PatientSSN)
                        BEGIN
                        Select  1 as Count , 1 as SSNExists
                        END
                        ELSE
                        BEGIN
                        Select  0 as Count , 0 as SSNExists 
                        END
                        END";
        }
        else if (location == "ETN")
        {
            q = @"IF(@PatientSSN='') 
                        BEGIN
                        IF EXISTS (SELECT PID FROM  PatientMaster_EastTN WHERE FirstName=@first_name AND LastName= @last_name AND DateOfBirth=@date_of_birth )
                        BEGIN
                        Select  1 as Count, 0 as SSNExists  
                        END
                        ELSE
                        BEGIN
                        Select  0 as Count, 0 as SSNExists 
                        END
                        END
                        ELSE
                        BEGIN
                        IF EXISTS (SELECT PID FROM  PatientMaster_EastTN WHERE  FirstName=@first_name AND LastName= @last_name  AND DateOfBirth=@date_of_birth  AND  PatientSSN=@PatientSSN)
                        BEGIN
                        Select  1 as Count , 0 as SSNExists 
                        END
                        ELSE IF EXISTS (SELECT PID FROM  PatientMaster_EastTN WHERE  FirstName=@first_name AND LastName= @last_name  AND DateOfBirth=@date_of_birth  AND  PatientSSN!=@PatientSSN)
                        BEGIN
                        Select  0 as Count , 0 as SSNExists 
                        END
                        ELSE IF EXISTS (SELECT PID FROM  PatientMaster_EastTN WHERE  PatientSSN=@PatientSSN)
                        BEGIN
                        Select  1 as Count , 1 as SSNExists
                        END
                        ELSE
                        BEGIN
                        Select  0 as Count , 0 as SSNExists 
                        END
                        END";
        }
        else
        {
            q = @"IF(@PatientSSN='') 
                        BEGIN
                        IF EXISTS (SELECT PID FROM  PatientMaster_MiddleTN WHERE FirstName=@first_name AND LastName= @last_name AND DateOfBirth=@date_of_birth )
                        BEGIN
                        Select  1 as Count, 0 as SSNExists  
                        END
                        ELSE
                        BEGIN
                        Select  0 as Count, 0 as SSNExists 
                        END
                        END
                        ELSE
                        BEGIN
                        IF EXISTS (SELECT PID FROM  PatientMaster_MiddleTN WHERE  FirstName=@first_name AND LastName= @last_name  AND DateOfBirth=@date_of_birth  AND  PatientSSN=@PatientSSN)
                        BEGIN
                        Select  1 as Count , 0 as SSNExists 
                        END
                        ELSE IF EXISTS (SELECT PID FROM  PatientMaster_MiddleTN WHERE  FirstName=@first_name AND LastName= @last_name  AND DateOfBirth=@date_of_birth  AND  PatientSSN!=@PatientSSN)
                        BEGIN
                        Select  0 as Count , 0 as SSNExists 
                        END
                        ELSE IF EXISTS (SELECT PID FROM  PatientMaster_MiddleTN WHERE  PatientSSN=@PatientSSN)
                        BEGIN
                        Select  1 as Count , 1 as SSNExists
                        END
                        ELSE
                        BEGIN
                        Select  0 as Count , 0 as SSNExists 
                        END
                        END";
        }
       
        //string q = "If(@PatientSSN!='') begin If exists (Select PID from  PatientMaster where  PatientName=(@first_name+' '+@last_name)  and DateOfBirth=@date_of_birth and  PatientSSN=@PatientSSN) begin Select  1 as Count , 0 as SSNExists end  else If exists (Select PID from  PatientMaster where  PatientSSN=@PatientSSN) begin ";
        //q += "Select  1 as Count , 1 as SSNExists end else begin Select 0 as Count,0 as SSNExists end end ";
        //q += "else begin  If exists (Select PID from  PatientMaster where PatientName=(@first_name+' '+@last_name)  and DateOfBirth=@date_of_birth) begin ";
        //q += " Select  1 as Count, 0 as SSNExists  end else begin Select 0 as Count , 0 as SSNExists   end end ";
        SqlConnection cn = null;
        cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@first_name", SqlDbType.VarChar, 35).Value = first_name;
        cm.Parameters.Add("@last_name", SqlDbType.VarChar, 60).Value = last_name;
        cm.Parameters.Add("@gender_code", SqlDbType.VarChar, 1).Value = gender_code;
        cm.Parameters.Add("@date_of_birth", SqlDbType.DateTime).Value = date_of_birth;
        cm.Parameters.Add("@phone_number", SqlDbType.VarChar, 30).Value = phone_number;
        cm.Parameters.Add("@PatientSSN", SqlDbType.VarChar, 100).Value = PatientSSN;
        cm.Parameters.Add("@city", SqlDbType.VarChar, 100).Value = City;
        try
        {
            cn.Open();
            // cm.ExecuteReader();
            Dr = cm.ExecuteReader();
            if (Dr != null)
            {
                while (Dr.Read())
                {
                    String Count = Dr["Count"].ToString();
                    if (Count == "1")
                    {
                        if (Dr["SSNExists"].ToString() == "1")
                        {
                            CreateNewPatientError(1117, "SSN already exists, please leave it blank and proceed");
                            result = "SSNExists";
                        }
                        else
                        {
                            ValidateNewPatientError("The information you entered matches the following existing patient(s). Please select from this list:");
                            result = "false";
                        }

                        //if (PatientSSN != "")
                        //{
                        //    API.Session.patientId = Dr["PID"].ToString();
                        //}


                    }
                    else
                    {

                        result = "true";


                    }

                }
            }

        }
        catch (Exception ex) { CreateNewPatientError(1117, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return result;
    }

    public string CreateSignupUser(string name,string email,string guid)
    {
        /*
         * in the course of booking an appointment, we decided to allow the referring 
         * provider to actually create a patient record in centricity once we verified
         * that the patient did NOT already exist.  There is still a change of this
         * creating a duplicate record though.
         */
        string result = "";
        SqlDataReader Dr = null;
        string q = "INSERT INTO [dbo].[User] ([Name], [EmailAddress],[GUID]) ";
        q += "VALUES( @name,@email_id,@guid) ";
        q += " Select  1 as Count";
        SqlConnection cn = null;
        cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = name;
        cm.Parameters.Add("@email_id", SqlDbType.NVarChar, 50).Value = email;
        cm.Parameters.Add("@guid", SqlDbType.NVarChar, 100).Value = guid;
        try
        {
            cn.Open();
            // cm.ExecuteReader();
            Dr = cm.ExecuteReader();
            if (Dr != null)
            {
                while (Dr.Read())
                {
                    String Count = Dr["Count"].ToString();
                    if (Count == "0")
                    {
                        CreateNewPatientError(1017, "Signup Failed.");
                        result = "false";
                    }
                    else
                    {
                        result = "true";
                    }

                }
            }
        }
        catch (Exception ex) { CreateNewPatientError(1117, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return result;
    }

    public void SendEmailCreateSignupUser(Guid guid)
    {
        /*
         * this method is used to send out an email.  credentials to be used
         * and connectivity information are stored in the statics.cs file.
         */
        SmtpClient oMail = null;
        // MailAddress from = null;
        MailAddress sendto = null;
        MailMessage Msg = null;
        try
        {
            sendto = new MailAddress(GetEmailAddress(guid));
            System.Net.NetworkCredential basicCredential = new System.Net.NetworkCredential(Statics.EMAILUSERNAME, Statics.EMAILPASSWORD, Statics.EMAILDOMAIN);
            //System.Net.NetworkCredential basicCredential = new System.Net.NetworkCredential(Statics.EMAILUSERNAME, Statics.EMAILPASSWORD);
            oMail = new SmtpClient(Statics.SMTPHOST);
            oMail.Port = Statics.SMTPPORT;
            oMail.EnableSsl = true;
            oMail.DeliveryMethod = SmtpDeliveryMethod.Network;
            oMail.UseDefaultCredentials = false;
            oMail.Timeout = 600000;
            oMail.Credentials = basicCredential;
            Msg = new MailMessage();
            Msg.From = new MailAddress(Statics.FROMEMAIL);
            //Msg.To.Add(new MailAddress(GetEmailAddress(guid)));
            Msg.To.Add(sendto);
            Msg.BodyEncoding = System.Text.Encoding.UTF8;
            Msg.IsBodyHtml = true;
            Msg.Subject = "Signup Link";
            Msg.Body = "http://localhost:54513/Signup.aspx?PrivateId=" + guid;
            oMail.Send(Msg);
            Msg.Dispose();
        }
        catch (Exception ex) { SendEmailError(1022, ex.ToString()); }
        finally
        {
            if (Msg != null) Msg.Dispose();
        }
    }


    public string CreatePatient(string PatientId, string first_name, string last_name, string gender_code, DateTime date_of_birth, string referring_physician_npi, string line1, string city, string state, string zip_code, string country_name, string phone_number, string PatientSSN)
    {
        /*
         * in the course of booking an appointment, we decided to allow the referring 
         * provider to actually create a patient record in centricity once we verified
         * that the patient did NOT already exist.  There is still a change of this
         * creating a duplicate record though.
         */
        string result = "";
        SqlDataReader Dr = null;
        //string q = " If Not exists (Select PID from  PatientMaster where PatientName=(@first_name+' '+@last_name) and City=@city and PhoneNumber=@phone_number and PatientSSN=@PatientSSN) begin ";
        //q += "INSERT INTO PatientMaster ([PID],[FirstName], [LastName],[PatientName], [Gender], [DateOfBirth], [AddressLine1], [City], [State], [Zip], [Country], [PhoneNumber],CreatedOn,UpdatedOn,RefPhysicianNPI,PatientSSN) ";
        //q += "VALUES(@PatientId, @first_name, @last_name,@first_name+' '+@last_name, @gender_code, @date_of_birth, @line1, @city, @state, @zip_code, @country_name, @phone_number,@CreatedOn,@UpdatedOn,@RefPhysicianNPI,@PatientSSN) ";
        //q += " Select  1 as Count end else begin Select 0 as Count end ";
        SqlConnection cn = null;
        cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("AddPatientMasterDetails", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@PatientId", SqlDbType.VarChar, 100).Value = PatientId;
        cm.Parameters.Add("@first_name", SqlDbType.VarChar, 35).Value = first_name;
        cm.Parameters.Add("@last_name", SqlDbType.VarChar, 60).Value = last_name;
        cm.Parameters.Add("@gender_code", SqlDbType.VarChar, 1).Value = gender_code;
        cm.Parameters.Add("@date_of_birth", SqlDbType.DateTime).Value = date_of_birth;
        cm.Parameters.Add("@line1", SqlDbType.VarChar, 30).Value = line1;
        cm.Parameters.Add("@city", SqlDbType.VarChar, 30).Value = city;
        cm.Parameters.Add("@state", SqlDbType.VarChar, 30).Value = state;
        cm.Parameters.Add("@zip_code", SqlDbType.VarChar, 30).Value = zip_code;
        cm.Parameters.Add("@country_name", SqlDbType.VarChar, 30).Value = country_name;
        cm.Parameters.Add("@phone_number", SqlDbType.VarChar, 30).Value = phone_number;
        cm.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = DateTime.Now;
        cm.Parameters.Add("@UpdatedOn", SqlDbType.DateTime).Value = DateTime.Now;
        cm.Parameters.Add("@RefPhysicianNPI", SqlDbType.VarChar, 100).Value = referring_physician_npi;
        cm.Parameters.Add("@PatientSSN", SqlDbType.VarChar, 100).Value = PatientSSN;//
        try
        {
            cn.Open();
            // cm.ExecuteReader();
            Dr = cm.ExecuteReader();
            if (Dr != null)
            {
                while (Dr.Read())
                {
                    String Count = Dr["Count"].ToString();
                    if (Count == "0")
                    {
                        CreateNewPatientError(1017, "Patient already exists.");
                        result = "false";
                    }
                    else
                    {
                        result = "true";
                    }

                }
            }

        }
        catch (Exception ex) { CreateNewPatientError(1117, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return result;
    }

    public string CreateAppointmentLog(string PracticeEmail,  int ReferrigPhysicianID, string FacilityName,  string DoctorDisplayName,  string ApptDisplayName, DateTime AppointmentSlot, string AppointmentTimeZone, string PatientID, string PhoneNumber, string EyeType,string ReasonForVisit,string AppointmentStatus, string SEESEntity,string Location)
    {
        string result = "";
        SqlDataReader Dr = null;
        SqlConnection cn = null;
        cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand("Create_ApointmentLog", cn);
        cm.CommandType = CommandType.StoredProcedure;
        cm.Parameters.Clear();
        cm.Parameters.Add("@PracticeEmail", SqlDbType.VarChar, 100).Value = PracticeEmail;
        cm.Parameters.Add("@ReferrigPhysicianID", SqlDbType.Int).Value = ReferrigPhysicianID;
        cm.Parameters.Add("@FacilityName", SqlDbType.VarChar, 50).Value = FacilityName;
        cm.Parameters.Add("@DoctorDisplayName", SqlDbType.VarChar, 200).Value = DoctorDisplayName;
        cm.Parameters.Add("@ApptDisplayName", SqlDbType.VarChar, 100).Value = ApptDisplayName;
        cm.Parameters.Add("@AppointmentSlot", SqlDbType.DateTime).Value = AppointmentSlot;
        cm.Parameters.Add("@AppointmentTimeZone", SqlDbType.VarChar, 200).Value = AppointmentTimeZone;
        cm.Parameters.Add("@PatientID", SqlDbType.VarChar, 200).Value = PatientID;
        cm.Parameters.Add("@PhoneNumber", SqlDbType.VarChar,20).Value = PhoneNumber;
        cm.Parameters.Add("@EyeType", SqlDbType.VarChar,10).Value = EyeType;
        cm.Parameters.Add("@ReasonForVisit", SqlDbType.VarChar, 500).Value = ReasonForVisit;
        cm.Parameters.Add("@AppointmentStatus", SqlDbType.VarChar, 20).Value = AppointmentStatus;
        cm.Parameters.Add("@SEESEntity", SqlDbType.VarChar, 500).Value = SEESEntity;
        cm.Parameters.Add("@Location", SqlDbType.VarChar, 500).Value = Location;
        try
        {
            cn.Open();
            Dr = cm.ExecuteReader();
            if (Dr != null)
            {
                while (Dr.Read())
                {                   
                     result = "true";                 

                }
            }

        }
        catch (Exception ex) { CreateNewPatientError(1117, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return result;
    }

    public string CreateAppointmentType(string name, string id)
    {
        /*
         * in the course of booking an appointment, we decided to allow the referring 
         * provider to actually create a patient record in centricity once we verified
         * that the patient did NOT already exist.  There is still a change of this
         * creating a duplicate record though.
         */
        string result = "";
        SqlDataReader Dr = null;
        string q = " If Not exists (Select ApptTypeId from  AppointmentTypeMaster where ApptType=@name and ApptTypeId=@id) begin ";
        q += "INSERT INTO AppointmentTypeMaster ([ApptType],[ApptTypeId]) ";
        q += "VALUES(@name, @id) ";
        q += " Select  1 as Count end else begin Select 0 as Count end ";
        SqlConnection cn = null;
        cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = name;
        cm.Parameters.Add("@id", SqlDbType.VarChar, 100).Value = id;

        try
        {
            cn.Open();
            // cm.ExecuteReader();
            Dr = cm.ExecuteReader();
            if (Dr != null)
            {
                while (Dr.Read())
                {
                    result = "true";

                }
            }

        }
        catch (Exception ex) { CreateNewPatientError(1117, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return result;
    }

    public DataTable GetDoctors(string LocationId)
    {
        /*
         * in the course of booking an appointment, we decided to allow the referring 
         * provider to actually create a patient record in centricity once we verified
         * that the patient did NOT already exist.  There is still a change of this
         * creating a duplicate record though.
         */
        string result = "";
        SqlDataReader Dr = null;
        DataTable dt = new DataTable();
        string q = " Select * from  Appointment_Doctors where LocationId=@LocationId ";

        SqlConnection cn = null;
        cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@LocationId", SqlDbType.VarChar, 100).Value = LocationId;
        try
        {
            cn.Open();
            // cm.ExecuteReader();
            Dr = cm.ExecuteReader();
            if (Dr != null)
            {

                dt.Load(Dr);

            }

        }
        catch (Exception ex) { CreateNewPatientError(1117, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return dt;
    }
    public DataTable GetAppointmentType(string DoctorId)
    {
        /*
         * in the course of booking an appointment, we decided to allow the referring 
         * provider to actually create a patient record in centricity once we verified
         * that the patient did NOT already exist.  There is still a change of this
         * creating a duplicate record though.
         */
        string result = "";
        SqlDataReader Dr = null;
        DataTable dt = new DataTable();
        string q = " Select * from  AppointmentTypeMaster where DoctorId=@DoctorId ";

        SqlConnection cn = null;
        cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@DoctorId", SqlDbType.VarChar, 100).Value = DoctorId;
        try
        {
            cn.Open();
            // cm.ExecuteReader();
            Dr = cm.ExecuteReader();
            if (Dr != null)
            {

                dt.Load(Dr);

            }

        }
        catch (Exception ex) { CreateNewPatientError(1117, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return dt;
    }
    public void TransferPatientReferringDoctor(string state, CPSPatient data, int refdrid)
    {
        /*
         * Patients do tend to move from one referring doctor to another referring doctor.  This
         * method updates which referring doctor the patient is being seen by in centricity.
         * To do this, the web site asks the referring provider a series of questions to 
         * validate the patient's data first to ensure that the patient really is theirs.
         */
        string q = "UPDATE PatientProfile SET RefDoctorID = @RefDrID WHERE PatientProfileID = @PatientProfileID ";
        SqlConnection cn = null;
        if (state == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (state == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@RefDrID", SqlDbType.Int).Value = refdrid;
        cm.Parameters.Add("@PatientProfileID", SqlDbType.Int).Value = data.PatientProfileID;
        try
        {
            cn.Open();
            cm.ExecuteNonQuery();
        }
        catch (Exception ex) { TransferPatientReferringDoctorError(1119, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
    }
    public int GetCompanyID(string state, int facilityid)
    {
        /*
         * gets the companyid (GroupID) from centricity based on the provided facility.
         */
        int iReturn = 0;
        string q = "SELECT GroupID FROM DoctorFacility WHERE DoctorFacilityId = @FacilityID ";

        SqlConnection cn = null;
        if (state == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (state == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@FacilityID", SqlDbType.Int).Value = facilityid;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    iReturn = int.Parse(dr["GroupID"].ToString());
                }
            }
        }
        catch (Exception ex) { GetCompanyIDError(1126, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return iReturn;
    }
    public void BookAppointment()
    {
        /*
         * this method books the appointment in Centricity.
         * 
         */
        // q1 is query to insert the actual appointment record
        string q1 = "INSERT INTO Appointments (FacilityID, ApptKind, OwnerID, ApptStart, ApptStop, EMRApptStart, ";
        q1 += "Duration, Status, Notes, DoctorID, ResourceID, ApptTypeID, CompanyID, Created, CreatedBy, ";
        q1 += "LastModified, LastModifiedBy) VALUES(@FacilityID, 1, @PatientProfileID, @ApptStart, @ApptStop, ";
        q1 += "@EMRApptStart, @Duration, 'Scheduled', @Notes, @DoctorID, @DoctorID, @ApptTypeID, @CompanyID, ";
        q1 += "GetDate(), 'EHPortal', GetDate(), 'EHPortal') ";
        q1 += "SELECT ApptID = SCOPE_IDENTITY() ";

        //q2 is query to update the existing ApptSlot record
        string q2 = "UPDATE ApptSlot SET ApptID = @ApptID, Status = 1, LastModified = GetDate(), ";
        q2 += "LastModifiedBy = 'EHPortal' WHERE ApptSlotID = @ApptSlotID ";

        // apptid is the result of query #1
        int apptid = 0;

        SqlConnection cn = null;
        if (Session.SWState == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (Session.SWState == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        SqlCommand cm = new SqlCommand(q1, cn);
        cm.CommandType = CommandType.Text;
        SqlDataReader dr = null;

        DateTime apptstart = Session.GetApptStart(Session.SWState, Session.SWCPSApptSlotID);
        DateTime apptstop = Session.GetApptStop(Session.SWState, Session.SWCPSApptSlotID);
        TimeSpan duration = apptstop.Subtract(apptstart);
        string eye = "Eye: ";
        if (Session.SWEye == Eye.Both) eye += "Both";
        if (Session.SWEye == Eye.Left) eye += "Left";
        if (Session.SWEye == Eye.Right) eye += "Right";

        cm.Parameters.Clear();
        cm.Parameters.Add("@FacilityID", SqlDbType.Int).Value = Session.SWCPSFacilityID;
        cm.Parameters.Add("@PatientProfileID", SqlDbType.Int).Value = Session.SWPatientProfileID;
        cm.Parameters.Add("@ApptStart", SqlDbType.DateTime).Value = apptstart;
        cm.Parameters.Add("@ApptStop", SqlDbType.DateTime).Value = apptstop;
        cm.Parameters.Add("@EMRApptStart", SqlDbType.DateTime).Value = DateTime.Parse(apptstart.ToShortDateString());
        cm.Parameters.Add("@Duration", SqlDbType.Int).Value = duration.Minutes;
        cm.Parameters.Add("@Notes", SqlDbType.Text).Value = eye + " // Reason: " + Session.SWReason;
        cm.Parameters.Add("@DoctorID", SqlDbType.Int).Value = Session.SWCPSDoctorID;
        cm.Parameters.Add("@ApptTypeID", SqlDbType.Int).Value = Session.SWCPSApptTypeIDs[0];
        cm.Parameters.Add("@CompanyID", SqlDbType.Int).Value = Session.GetCompanyID(Session.SWState, Session.SWCPSFacilityID);
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    apptid = int.Parse(dr["ApptID"].ToString());
                }
            }
        }
        catch (Exception ex) { BookAppointmentError(1129, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        if (Session.SWState == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (Session.SWState == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        cm = new SqlCommand(q2, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@ApptID", SqlDbType.Int).Value = apptid;
        cm.Parameters.Add("@ApptSlotID", SqlDbType.Int).Value = Session.SWCPSApptSlotID;
        try
        {
            cn.Open();
            cm.ExecuteNonQuery();
        }
        catch (Exception ex) { BookAppointmentError(1130, ex.ToString()); }
        finally
        {
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
    }
    public DateTime GetApptStart(string state, int apptslotid)
    {
        /*
         * This function returns the start time of the selected appointment slot.
         */
        DateTime dtReturn = DateTime.Parse("1/1/1900");
        string q = "SELECT Start FROM ApptSlot WHERE ApptSlotID = @ApptSlotID ";
        SqlConnection cn = null;
        if (state == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (state == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@ApptSlotID", SqlDbType.Int).Value = apptslotid;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    dtReturn = DateTime.Parse(dr["Start"].ToString());
                }
            }
        }
        catch (Exception ex) { GetApptStartError(1127, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return dtReturn;
    }
    public DateTime GetApptStop(string state, int apptslotid)
    {
        /*
         * this function returns the stop time of the selected appointment slot
         */
        DateTime dtReturn = DateTime.Parse("1/1/1900");
        string q = "SELECT Stop FROM ApptSlot WHERE ApptSlotID = @ApptSlotID ";
        SqlConnection cn = null;
        if (state == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (state == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@ApptSlotID", SqlDbType.Int).Value = apptslotid;
        SqlDataReader dr = null;
        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    dtReturn = DateTime.Parse(dr["Stop"].ToString());
                }
            }
        }
        catch (Exception ex) { GetApptStopError(1128, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return dtReturn;
    }
    public string GetFacilityContactDetails(string state, int cpsfacilityid)
    {
        /*
         * this function returns the contact details for the given facility.
         * this info is pulled out of centricity.  It is used to print out the 
         * appointment reminder page that the referring provider gives to the
         * patient at the time of the appointment booking.
         */
        string sReturn = "";
        string q = "SELECT Address = ISNULL(Address1,'') + ISNULL(', ' + Address2,''), ";
        q += "City=ISNULL(City,''), State=ISNULL(State,''), Zip=ISNULL(Zip,''), ";
        q += "Phone1 =ISNULL(Phone1,''), Phone2=ISNULL(Phone2,'') ";
        q += "FROM DoctorFacility WHERE DoctorFacilityID = @FacilityID ";

        SqlConnection cn = null;
        if (state == "AL") cn = new SqlConnection(Statics.CPSALconnstring);
        if (state == "TN") cn = new SqlConnection(Statics.CPSTNconnstring);
        SqlCommand cm = new SqlCommand(q, cn);
        cm.CommandType = CommandType.Text;
        cm.Parameters.Clear();
        cm.Parameters.Add("@FacilityID", SqlDbType.Int).Value = cpsfacilityid;
        SqlDataReader dr = null;

        try
        {
            cn.Open();
            dr = cm.ExecuteReader();
            if (dr != null)
            {
                if (dr.Read())
                {
                    sReturn = dr["Address"].ToString() + "^";
                    sReturn += dr["City"].ToString() + "^";
                    sReturn += dr["State"].ToString() + "^";
                    sReturn += dr["Zip"].ToString() + "^";
                    sReturn += dr["Phone1"].ToString() + "^";
                    sReturn += dr["Phone2"].ToString();
                }
            }
        }
        catch (Exception ex) { GetFacilityContactDetailsError(1132, ex.ToString()); }
        finally
        {
            if (dr != null) { if (!dr.IsClosed) dr.Close(); }
            if (cm != null) cm.Dispose();
            if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
        }
        return sReturn;
    }

    #endregion

    #region Test Functions

    public void TestSendEmail(string EmailAddress)
    {
        SmtpClient oMail = null;
        MailMessage Msg = null;

        try
        {
            System.Net.NetworkCredential basicCredential = new System.Net.NetworkCredential(Statics.EMAILUSERNAME, Statics.EMAILPASSWORD, Statics.EMAILDOMAIN);
            oMail = new SmtpClient(Statics.SMTPHOST);
            oMail.Port = Statics.SMTPPORT;
            oMail.EnableSsl = true;
            //oMail.EnableSsl = false;
            oMail.DeliveryMethod = SmtpDeliveryMethod.Network;
            oMail.UseDefaultCredentials = false;
            oMail.Credentials = basicCredential;
            oMail.Timeout = 600000;
            Msg = new MailMessage();
            Msg.From = new MailAddress(Statics.FROMEMAIL);
            Msg.To.Add(new MailAddress(EmailAddress));
            Msg.BodyEncoding = System.Text.Encoding.UTF8;
            Msg.IsBodyHtml = true;
            Msg.Subject = "Patient Portal - Test Email";
            Msg.Body = "This is a test email message from the EHP Patient Portal.  Looks like it is working if you are reading this.";
            oMail.Send(Msg);
            Msg.Dispose();
        }
        catch (Exception ex) { SendEmailError(1131, ex.ToString()); }
        finally
        {
            if (Msg != null) Msg.Dispose();
        }
    }

    #endregion

    public class UserList
    {
        public UserList() { }
        private int id = 0;
        private string emailaddress = "";

        public int ID { get { return id; } set { id = value; } }
        public string EmailAddress { get { return emailaddress; } set { emailaddress = value; } }

    }
    public class GECPSApptType
    {
        public GECPSApptType() { }

        private int id = 0;
        private string name = "";
        private int duration = 0;

        public int ID { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int Duration { get { return duration; } set { duration = value; } }
    }
    public class CPSDoctor
    {
        /// <summary>
        /// This class deals with the Doctor records in the CPS databases
        /// </summary>
        public CPSDoctor() { }

        private int id = 0;
        private string listname = "";
        private string state = "";

        public int ID { get { return id; } set { id = value; } }
        public string ListName { get { return listname; } set { listname = value; } }
        public string State { get { return state; } set { state = value; } }

    }
    public class CPSFacility
    {
        /// <summary>
        /// This class deals with the Facility records in the CPS Databases
        /// </summary>
        public CPSFacility() { }

        private int id = 0;
        private string listname = "";
        private string state = "";

        public int ID { get { return id; } set { id = value; } }
        public string ListName { get { return listname; } set { listname = value; } }
        public string State { get { return state; } set { state = value; } }

    }
    public class EHPApptType
    {
        public EHPApptType() { }
        public EHPApptType(int id)
        {
            ID = id;
            GetDetails();
        }

        private int id = 0;
        private string name = "";
        private bool enabled = false;
        private int doctorid = 0;

        public int ID { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public bool Enabled { get { return enabled; } set { enabled = value; } }
        public int DoctorID { get { return doctorid; } set { doctorid = value; } }

        public void Delete()
        {
            SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
            SqlCommand cm = new SqlCommand("DeleteEHPApptType", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Clear();
            cm.Parameters.Add("@EHPApptTypeID", SqlDbType.Int).Value = ID;
            try
            {
                cn.Open();
                cm.ExecuteNonQuery();
            }
            catch (Exception ex) { Session.EHPApptType_DeleteError(1032, ex.ToString()); }
            finally
            {
                if (cm != null) cm.Dispose();
                if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
            }
        }
        public void GetDetails()
        {
            SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
            SqlCommand cm = new SqlCommand("GetEHPApptTypeDetails", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Clear();
            cm.Parameters.Add("@EHPApptTypeID", SqlDbType.Int).Value = ID;
            SqlDataReader dr = null;

            try
            {
                cn.Open();
                dr = cm.ExecuteReader();
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        Name = dr["Name"].ToString();
                        Enabled = bool.Parse(dr["Enabled"].ToString());
                        DoctorID = int.Parse(dr["DoctorID"].ToString());
                    }
                }
            }
            catch (Exception ex) { Session.EHPApptType_GetDetailsError(1033, ex.ToString()); }
            finally
            {
                if (dr != null) { if (!dr.IsClosed) dr.Close(); }
                if (cm != null) cm.Dispose();
                if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
            }
        }
        public void Save()
        {
            SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
            SqlCommand cm = new SqlCommand("AddUpdateEHPApptType", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Clear();
            cm.Parameters.Add("@EHPApptTypeID", SqlDbType.Int).Value = ID;
            cm.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = Name;
            cm.Parameters.Add("@DoctorID", SqlDbType.Int).Value = DoctorID;
            cm.Parameters.Add("@Enabled", SqlDbType.Bit).Value = Enabled;
            try
            {
                cn.Open();
                cm.ExecuteNonQuery();
            }
            catch (Exception ex) { Session.EHPApptType_SaveError(1034, ex.ToString()); }
            finally
            {
                if (cm != null) cm.Dispose();
                if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
            }
        }
    }
    public class EHPCPSApptType
    {
        public EHPCPSApptType() { }
        public EHPCPSApptType(int id)
        {
            ID = id;
            GetDetails();
        }

        private int id = 0;
        private int ehpappttypeid = 0;
        private int cpsid = 0;
        private string state = "";
        private string display = "";

        public int ID { get { return id; } set { id = value; } }
        public int EHPApptTypeID { get { return ehpappttypeid; } set { ehpappttypeid = value; } }
        public int CPSID { get { return cpsid; } set { cpsid = value; } }
        public string State { get { return state; } set { state = value; } }
        public string Display { get { return display; } set { display = value; } }

        public void GetDetails()
        {
            if (ID > 0)
            {
                SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
                SqlCommand cm = new SqlCommand("GetEHPCPSApptTypeDetails", cn);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Clear();
                cm.Parameters.Add("@CPSApptTypeID", SqlDbType.Int).Value = ID;
                SqlDataReader dr = null;
                try
                {
                    cn.Open();
                    dr = cm.ExecuteReader();
                }
                catch (Exception ex) { Session.EHPCPSApptType_GetDetailsError(1035, ex.ToString()); }
                finally
                {
                    if (dr != null) { if (!dr.IsClosed) dr.Close(); }
                    if (cm != null) cm.Dispose();
                    if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
                }
            }
        }
        public void Save()
        {
            SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
            SqlCommand cm = new SqlCommand("AddUpdateCPSApptType", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Clear();
            cm.Parameters.Add("@CPSApptTypeID", SqlDbType.Int).Value = ID;
            cm.Parameters.Add("@EHPApptTypeID", SqlDbType.Int).Value = EHPApptTypeID;
            cm.Parameters.Add("@CPSID", SqlDbType.Int).Value = CPSID;
            cm.Parameters.Add("@State", SqlDbType.VarChar, 2).Value = State;
            try
            {
                cn.Open();
                cm.ExecuteNonQuery();
            }
            catch (Exception ex) { Session.EHPCPSApptType_SaveError(1036, ex.ToString()); }
            finally
            {
                if (cm != null) cm.Dispose();
                if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
            }
        }
        public void Delete()
        {
            SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
            SqlCommand cm = new SqlCommand("DeleteCPSApptType", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Clear();
            cm.Parameters.Add("@CPSApptTypeID", SqlDbType.Int).Value = ID;
            try
            {
                cn.Open();
                cm.ExecuteNonQuery();
            }
            catch (Exception ex) { API.Session.EHPCPSApptType_DeleteError(1037, ex.ToString()); }
            finally
            {
                if (cm != null) cm.Dispose();
                if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
            }
        }
    }
    public class EHPDoctor
    {
        /// <summary>
        /// This class deals with the Doctor records in the EHPortal database.
        /// </summary>
        public EHPDoctor() { }
        public EHPDoctor(int id)
        {
            ID = id;
            State = state;
            GetDetails();
        }

        private int id = 0;
        private int facilityid = 0;
        private string state = "";
        private int cpsid = 0;
        private string name = "";
        private bool enabled = false;

        public int ID { get { return id; } set { id = value; } }
        public int FacilityID { get { return facilityid; } set { facilityid = value; } }
        public string State { get { return state; } set { state = value; } }
        public int CPSID { get { return cpsid; } set { cpsid = value; } }
        public string Name { get { return name; } set { name = value; } }
        public bool Enabled { get { return enabled; } set { enabled = value; } }



        public void GetDetails()
        {
            SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
            SqlCommand cm = new SqlCommand("GetEHPDoctorDetails", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Clear();
            cm.Parameters.Add("@DoctorID", SqlDbType.Int).Value = ID;
            SqlDataReader dr = null;

            try
            {
                cn.Open();
                dr = cm.ExecuteReader();
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        CPSID = int.Parse(dr["CPSID"].ToString());
                        Enabled = bool.Parse(dr["Enabled"].ToString());
                        FacilityID = int.Parse(dr["FacilityID"].ToString());
                        Name = dr["Name"].ToString();
                        State = dr["State"].ToString();
                    }
                }
            }
            catch (Exception ex) { Session.EHPDoctor_GetDetailsError(1038, ex.ToString()); }
            finally
            {
                if (dr != null) { if (!dr.IsClosed) dr.Close(); }
                if (cm != null) cm.Dispose();
                if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
            }
        }
        public void Save()
        {
            SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
            SqlCommand cm = new SqlCommand("AddUpdateEHPDoctor", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Clear();
            cm.Parameters.Add("@DoctorID", SqlDbType.Int).Value = ID;
            cm.Parameters.Add("@State", SqlDbType.VarChar, 2).Value = State;
            cm.Parameters.Add("@CPSID", SqlDbType.Int).Value = CPSID;
            cm.Parameters.Add("@Enabled", SqlDbType.Bit).Value = Enabled;
            cm.Parameters.Add("@FacilityID", SqlDbType.Int).Value = FacilityID;
            cm.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = Name;

            try
            {
                cn.Open();
                cm.ExecuteNonQuery();
            }
            catch (Exception ex) { Session.EHPDoctor_SaveError(1039, ex.ToString()); }
            finally
            {
                if (cm != null) cm.Dispose();
                if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
            }
        }
        public void Delete()
        {
            SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
            SqlCommand cm = new SqlCommand("DeleteDoctor", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Clear();
            cm.Parameters.Add("@EHPDoctorID", SqlDbType.Int).Value = ID;
            try
            {
                cn.Open();
                cm.ExecuteNonQuery();
            }
            catch (Exception ex) { Session.EHPDoctor_DeleteError(1040, ex.ToString()); }
            finally
            {
                if (cm != null) cm.Dispose();
                if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
            }
        }
    }
    public class EHPFacility
    {
        /// <summary>
        /// This class deals with the Facility records in the EHPortal database
        /// </summary>
        public EHPFacility() { }
        public EHPFacility(int id)
        {
            FacilityID = id;
            State = state;
            GetDetails();
        }

        private int facilityid = 0;
        private string state = "";
        private int cpsid = 0;
        private string name = "";
        private bool enabled = false;

        public int FacilityID { get { return facilityid; } set { facilityid = value; } }
        public string State { get { return state; } set { state = value; } }
        public int CPSID { get { return cpsid; } set { cpsid = value; } }
        public string Name { get { return name; } set { name = value; } }
        public bool Enabled { get { return enabled; } set { enabled = value; } }


        public void Delete()
        {
            SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
            SqlCommand cm = new SqlCommand("DeleteEHPFacility", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Clear();
            cm.Parameters.Add("@EHPFacilityID", SqlDbType.Int).Value = FacilityID;
            try
            {
                cn.Open();
                cm.ExecuteNonQuery();
            }
            catch (Exception ex) { API.Session.EHPFacility_DeleteError(1041, ex.ToString()); }
            finally
            {
                if (cm != null) cm.Dispose();
                if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
            }
        }
        public void GetDetails()
        {
            SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
            SqlCommand cm = new SqlCommand("GetEHPFacilityDetails", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Clear();
            cm.Parameters.Add("@FacilityID", SqlDbType.Int).Value = FacilityID;
            SqlDataReader dr = null;

            try
            {
                cn.Open();
                dr = cm.ExecuteReader();
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        CPSID = int.Parse(dr["CPSID"].ToString());
                        Name = dr["Name"].ToString();
                        Enabled = bool.Parse(dr["Enabled"].ToString());
                        State = dr["State"].ToString();
                    }
                }
            }
            catch (Exception ex) { API.Session.EHPFacility_GetDetailsError(1042, ex.ToString()); }
            finally
            {
                if (dr != null) { if (!dr.IsClosed) dr.Close(); }
                if (cm != null) cm.Dispose();
                if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
            }
        }
        public void Save()
        {
            SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
            SqlCommand cm = new SqlCommand("AddUpdateEHPFacility", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@FacilityID", SqlDbType.Int).Value = FacilityID;
            cm.Parameters.Add("@CPSID", SqlDbType.Int).Value = CPSID;
            cm.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = Name;
            cm.Parameters.Add("@Enabled", SqlDbType.Bit).Value = Enabled;
            cm.Parameters.Add("@State", SqlDbType.VarChar, 2).Value = State;
            try
            {
                cn.Open();
                cm.ExecuteNonQuery();
            }
            catch (Exception ex) { API.Session.EHPFacility_SaveError(1043, ex.ToString()); }
            finally
            {
                if (cm != null) cm.Dispose();
                if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
            }
        }
    }
    public class Location
    {
        /// <summary>
        /// This class deals with the referring doctor records in the CPS databases
        /// </summary>
        #region Constructors
        public Location() { }
        public Location(string database, int id)
        {
            Database = database;
            ID = id;
            RefreshData();
        }
        #endregion

        #region Variables
        private string database = "";
        private string location1 = "";
        private int id = 0;
        private string npi = "";
        private string display = "";
        private string first = "";
        private string middle = "";
        private string last = "";
        private string orgname = "";
        private string address1 = "";
        private string address2 = "";
        private string city = "";
        private string state = "";
        private string zip = "";
        private string phone = "";
        private string fax = "";
        #endregion

        #region Properties
        public string Database { get { return database; } set { database = value; } }
        public string Location1 { get { return location1; } set { location1 = value; } }
        public int ID { get { return id; } set { id = value; } }
        public string NPI { get { return npi; } set { npi = value; } }
        public string Display { get { return display; } set { display = value; } }
        public string First { get { return first; } set { first = value; } }
        public string Middle { get { return middle; } set { middle = value; } }
        public string Last { get { return last; } set { last = value; } }
        public string OrgName { get { return orgname; } set { orgname = value; } }
        public string Address1 { get { return address1; } set { address1 = value; } }
        public string Address2 { get { return address2; } set { address2 = value; } }
        public string City { get { return city; } set { city = value; } }
        public string State { get { return state; } set { state = value; } }
        public string Zip { get { return zip; } set { zip = value; } }
        public string Phone { get { return phone; } set { phone = value; } }
        public string Fax { get { return fax; } set { fax = value; } }
        #endregion

        #region Functions
        private void RefreshData()
        {
            if (ID > 0 && Database != "")
            {
                string connstring = "";
                if (Database == "AL") connstring = Statics.CPSALconnstring;
                if (Database == "TN") connstring = Statics.CPSTNconnstring;
                string q = "SELECT First = ISNULL(First,''), Middle = ISNULL(Middle,''), Last = ISNULL(Last,''), OrgName = ISNULL(OrgName,''), Address1 = ISNULL(Address1,''), ";
                q += "Address2 = ISNULL(Address2,''), City = ISNULL(City,''), State = ISNULL(State,''), Zip = ISNULL(Zip,''), Phone = ISNULL(Phone1,''), Fax = ISNULL(Phone2,'') ";
                q += "FROM DoctorFacility WHERE DoctorFacilityID = @ID ";

                SqlConnection cn = new SqlConnection(connstring);
                SqlCommand cm = new SqlCommand(q, cn);
                cm.CommandType = CommandType.Text;
                cm.Parameters.Clear();
                cm.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                SqlDataReader dr = null;

                try
                {
                    cn.Open();
                    dr = cm.ExecuteReader();
                    if (dr != null)
                    {
                        if (dr.Read())
                        {
                            First = dr["First"].ToString();
                            Middle = dr["Middle"].ToString();
                            Last = dr["Last"].ToString();
                            OrgName = dr["OrgName"].ToString();
                            Address1 = dr["Address1"].ToString();
                            Address2 = dr["Address2"].ToString();
                            City = dr["City"].ToString();
                            State = dr["State"].ToString();
                            Zip = dr["Zip"].ToString();
                            Phone = dr["Phone"].ToString();
                            Fax = dr["Fax"].ToString();
                        }
                    }
                }
                catch (Exception ex) { API.Session.Location_RefreshDataError(1044, ex.ToString()); }
                finally
                {
                    if (dr != null) { if (!dr.IsClosed) dr.Close(); }
                    if (cm != null) cm.Dispose();
                    if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
                }
            }
        }
        public void SaveData()
        {
            string q = "UPDATE DoctorFacility SET ";
            q += "Phone1 = @Phone, ";
            q += "Phone2 = @Fax ";
            q += "WHERE DoctorFacilityID = @ID ";
            string connstring = "";
            if (Database == "AL") connstring = Statics.CPSALconnstring;
            if (Database == "TN") connstring = Statics.CPSTNconnstring;

            SqlConnection cn = new SqlConnection(connstring);
            SqlCommand cm = new SqlCommand(q, cn);
            cm.CommandType = CommandType.Text;
            cm.Parameters.Clear();
            cm.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = Phone;
            cm.Parameters.Add("@Fax", SqlDbType.VarChar, 15).Value = Fax;
            cm.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
            //cm.Parameters.Add("@NPI", SqlDbType.VarChar, 80).Value = API.Session.GetNPI(API.Session.GUID);

            try
            {
                cn.Open();
                cm.ExecuteNonQuery();
            }
            catch (Exception ex) { API.Session.Location_SaveDataError(1045, ex.ToString()); }
            finally
            {
                if (cm != null) cm.Dispose();
                if (cn != null) { if (cn.State != ConnectionState.Closed) cn.Close(); cn.Dispose(); }
            }
        }
        #endregion

    }
    public class ApptSlot
    {
        public ApptSlot() { }

        private int apptslotid = 0;
        private int scheduleid = 0;
        private int listorder = 0;
        private DateTime start = DateTime.Parse("1/1/1900");
        private DateTime stop = DateTime.Parse("1/1/1900");

        public int ApptSlotID { get { return apptslotid; } set { apptslotid = value; } }
        public int ScheduleID { get { return scheduleid; } set { scheduleid = value; } }
        public int ListOrder { get { return listorder; } set { listorder = value; } }
        public DateTime Start { get { return start; } set { start = value; } }
        public DateTime Stop { get { return stop; } set { stop = value; } }

    }
    public class CPSPatient
    {
        public CPSPatient() { }

        private int patientprofileid = 0;
        private string first = "";
        private string middle = "";
        private string last = "";
        private string gender = "";
        private DateTime dob = DateTime.Parse("1/1/1900");
        private string phone = "";
        private string emailaddress = "";
        private string ssn4 = "0000";
        private string ssn = "000000000";

        public int PatientProfileID { get { return patientprofileid; } set { patientprofileid = value; } }
        public string First { get { return first; } set { first = value; } }
        public string Middle { get { return middle; } set { middle = value; } }
        public string Last { get { return last; } set { last = value; } }
        public string Gender { get { return gender; } set { gender = value; } }
        public DateTime DOB { get { return dob; } set { dob = value; } }
        public string Phone { get { return phone; } set { phone = value; } }
        public string EmailAddress { get { return emailaddress; } set { emailaddress = value; } }
        public string SSN4 { get { return ssn4; } set { ssn4 = value; } }
        public string SSN { get { return ssn; } set { ssn = value; } }

    }
    public class ChildAcct
    {
        public ChildAcct() { }
        private int userid = 0;
        private string name = "";
        private int parentid = 0;
        private string npi = "";
        private string emailaddress = "";

        public int UserID { get { return userid; } set { userid = value; } }
        public int ParentID { get { return parentid; } set { parentid = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string NPI { get { return npi; } set { npi = value; } }
        public string EmailAddress { get { return emailaddress; } set { emailaddress = value; } }
    }
    public class Account
    {
        public Account() { }
        private int userid = 0;
        private string npinumber = "";
        private string emailaddress = "";
        private string password = "";
        private bool validated = false;
        private bool active = false;
        private DateTime lastlogon = DateTime.Parse("1/1/1900");
        private string guid = "";
        private string name = "";
        private int invalidlogonattempts = 0;
        private int passwordage = 0;
        private bool isadmin = false;
        private bool issetupadmin = false;

        public int UserID { get { return userid; } set { userid = value; } }
        public string NPINumber { get { return npinumber; } set { npinumber = value; } }
        public string EmailAddress { get { return emailaddress; } set { emailaddress = value; } }
        public string Password { get { return password; } set { password = value; } }
        public bool Validated { get { return validated; } set { validated = value; } }
        public bool Active { get { return active; } set { active = value; } }
        public DateTime LastLogon { get { return lastlogon; } set { lastlogon = value; } }
        public string GUID { get { return guid; } set { guid = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int InvalidLogonAttempts { get { return invalidlogonattempts; } set { invalidlogonattempts = value; } }
        public int PasswordAge { get { return passwordage; } set { passwordage = value; } }
        public bool IsAdmin { get { return isadmin; } set { isadmin = value; } }
        public bool IsSetupAdmin { get { return issetupadmin; } set { issetupadmin = value; } }

    }
    public static string Method(string path)
    {
        //using (var client = new HttpClient())
        //{
        //    var response = client.GetAsync(path).GetAwaiter().GetResult();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var responseContent = response.Content;
        //        return responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
        //    }
        //}
        return null;
    }
}
