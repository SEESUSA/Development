using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

public partial class Signup : System.Web.UI.Page
{

    /// <summary>
    /// This page is used by a referring provide to self-register
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    #region Event Handlers
    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.IsDoctorInDBError += Err;
        API.Session.RegisterUserError += Err;
        API.Session.LocateDoctorError += Err;
        API.Session.SendEmailError += Err;
        API.Session.GetGUIDError += Err;
        litError.Text = "";
        if (!IsPostBack)
        { }
        string guidparameter = Request.QueryString["PrivateId"];
        if (guidparameter != null)
        {
            Guid guid = new Guid(guidparameter);
            GetEmailAddress(guid);
        }
    }
    public void GetEmailAddress(Guid guid)
    {
        SqlConnection cn = new SqlConnection(Statics.EHPconnstring);
        SqlCommand sc = new SqlCommand("GetEmailByGUID", cn);
        sc.CommandType = CommandType.StoredProcedure;
        sc.Parameters.Add("@GUID", SqlDbType.UniqueIdentifier).Value = guid;
        SqlDataAdapter da = new SqlDataAdapter(sc);
        DataSet ds = new DataSet();
        da.Fill(ds);
        DataTable dt = ds.Tables[0];
        cn.Close();
        //Binding TextBox From dataTable    
        txtEmailAddress.Text = dt.Rows[0]["EmailAddress"].ToString();
        txtEmailAddress.Enabled = false;
    }
    protected void btnSignup_Click(object sender, EventArgs e)
    {
        if (ValidateData())
        {
            if (API.Session.NPIAlreadyRegistered(txtNPI.Text))
            {
                // The NPI number provided was already registered in the Portal database.
                string msg = "Unfortunately, we could not register your account at this time because the NPI number " +
                    "you provided (" + txtNPI.Text + ") is already registered in our system.<br /><br />" +
                    "You can try logging in with the email address that you previously registered under.<br /><br />";
                Err(1161, msg);
            }
            else
            {
                if (API.Session.LocateDoctor(txtFirstName.Text, txtLastName.Text, txtZipCode.Text, txtPhoneNumber.Text, txtNPI.Text, txtEmailAddress.Text))
                {
                    if (API.Session.RegisterUser(txtNPI.Text, txtEmailAddress.Text))
                    {
                        API.Session.SendEmail(API.EmailType.Validate, API.Session.GetGUID(txtEmailAddress.Text));
                        pnlSignup.Visible = false;
                        pnlComplete.Visible = true;
                    }
                    else
                    {
                        // No doctor records found in any CPS database that match the user's specified data
                        string msg = "Unfortunately, we could not find any records that match your current information.  Please be sure your names are " +
                            "your legal names and not nicknames, ensure your phone number is only numbers - no spaces or symbols. <br /><br />" +
                            "If you continue to have problems signing up, please contact one of our offices for assistance.";
                        Err(1067, msg);
                    }
                }
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("./");
    }
    protected void Err(int num, string msg)
    {
        litError.Text += "Error "  + msg + "<br />";//+ num.ToString() + " - "
    }
    #endregion

    #region Functions
    protected bool ValidateData()
    {
        if (txtFirstName.Text == "") { Err(1068, "First Name cannot be blank."); return false; }
        if (txtLastName.Text == "") { Err(1069, "Last Name cannot be blank."); return false; }
        if (txtPhoneNumber.Text == "") { Err(1070,"Phone Number cannot be blank."); return false; }
        if (txtZipCode.Text == "") { Err(1071,"Zip Code cannot be blank."); return false; }
        if (txtNPI.Text == "") { Err(1072,"NPI cannot be blank."); return false; }
        if (txtEmailAddress.Text == "") { Err(1073,"Email Address cannot be blank."); return false; }
        return true;
    }

    #endregion

}