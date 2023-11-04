using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Accounts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.GetAccountsError += Err;
        API.Session.GetAccountDetailsError += Err;
        //API.Session.EHPFacility_GetDetailsError += Err;
        //API.Session.GetCPSFacilityDisplayNameError += Err;

        litError.Text = "";
        lbNew.Visible = true;
        if (!IsPostBack)
        {
            RefreshAccounts();
            //SetEHPFacility();
        }
    }

    #region " Event Handlers "
    protected void cboAccounts_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lbEdit.Visible = cboAccounts.SelectedIndex > 0;
        lbDelete.Visible = cboAccounts.SelectedIndex > 0;
        if (cboAccounts.SelectedIndex > 0)
        {
            API.Account acct = API.Session.GetAccountDetails(int.Parse(cboAccounts.SelectedItem.Value));
            if (acct != null)
            {
                pnlAccountDetails.Visible = true;
                txtName.Text = acct.Name;
                txtEmailAddress.Text = acct.EmailAddress;
                txtPassword.Text = acct.Password;
                lblLastLogon.Text = acct.LastLogon.ToString();
                chkActive.Checked = acct.Active;
                chkIsAdmin.Checked = acct.IsAdmin;
                chkIsSetupAdmin.Checked = acct.IsSetupAdmin;                
            }
        }
    }

    protected void lbNew_Click(object sender, EventArgs e)
    {

    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        if(int.Parse(cboAccounts.SelectedItem.Value) > 0)
        {
            API.Session.DeleteAccount(int.Parse(cboAccounts.SelectedItem.Value));
            ClearFields();
            pnlAccountDetails.Visible = false;
            RefreshAccounts();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        API.Account data = new API.Account();
        data.UserID = int.Parse(cboAccounts.SelectedItem.Value);
        data.EmailAddress = txtEmailAddress.Text;
        data.Password = txtPassword.Text;
        data.Name = txtName.Text;
        data.Active = chkActive.Checked;
        data.IsAdmin = chkIsAdmin.Checked;
        data.IsSetupAdmin = chkIsSetupAdmin.Checked;

        API.Session.UpdateAccount(data);
        ClearFields();
        pnlAccountDetails.Visible = false;
        RefreshAccounts();
    }
    #endregion

    #region " Functions "
    protected void Err(int ErrorNumber, string Msg)
    {
        litError.Text = "Error " + ErrorNumber.ToString() + " - " + Msg + "<br />";
    }
    private void RefreshAccounts()
    {
        cboAccounts.Items.Clear();
        cboAccounts.Items.Add(new ListItem("Select Account", "0"));

        API.Account[] data = API.Session.GetAccountList();
        if (data != null)
        {
            for (int a = 0; a < data.Length; a++)
            {
                cboAccounts.Items.Add(new ListItem(data[a].Name, data[a].UserID.ToString()));
            }
        }
    }
    private void ClearFields()
    {
        txtPassword.Text = "";
        txtName.Text = "";
        txtEmailAddress.Text = "";
        chkActive.Checked = false;
        chkIsAdmin.Checked = false;
        chkIsSetupAdmin.Checked = false;
        lblLastLogon.Text = "";
    }

    #endregion


    protected void lnkReset_Click(object sender, EventArgs e)
    {
        txtPassword.Text = Statics.NewPassword;
    }
}