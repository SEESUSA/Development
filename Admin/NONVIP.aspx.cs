using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_NONVIP : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        btnDelete.Click += BtnDelete_Click;
        btnSave.Click += BtnSave_Click;
        btnYes.Click += BtnYes_Click;
        btnNo.Click += BtnNo_Click;
        cboNonVIPAccount.SelectedIndexChanged += CboNonVIPAccount_SelectedIndexChanged;
        litError.Text = "";
        if (!IsPostBack)
        {
            RefreshNONVIPAccounts();
        }
    }

    private void BtnNo_Click(object sender, EventArgs e)
    {
        pnlConfirmDelete.Visible = false;
    }

    private void BtnYes_Click(object sender, EventArgs e)
    {
        API.Session.DeleteNONVIPAccount(int.Parse(lblUserID.Text));
        pnlConfirmDelete.Visible = false;
        pnlEdit.Visible = false;
        RefreshNONVIPAccounts();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        pnlConfirmDelete.Visible = true;
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (ErrorCheck())
        {
            API.Account Acct = new API.Account();
            Acct.UserID = int.Parse(lblUserID.Text);
            Acct.NPINumber = txtNPINumber.Text;
            Acct.EmailAddress = txtEmailAddress.Text;
            Acct.Password = txtPassword.Text;
            API.Session.UpdateNONVIPAccount(Acct);
            pnlEdit.Visible = false;
            RefreshNONVIPAccounts();
        }
    }

    private void CboNonVIPAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboNonVIPAccount.SelectedIndex == 0)
        {
            pnlEdit.Visible = false;
        }
        else if (cboNonVIPAccount.SelectedIndex == 1)
        {
            pnlEdit.Visible = true;
            // Clear form for new entry
            lblUserID.Text = "0";
            txtNPINumber.Text = "";
            API.Session.NPI = "";
            txtEmailAddress.Text = "";
            txtPassword.Text = "";
            lblLastLogon.Text = "";
        }
        else
        {
            pnlEdit.Visible = true;
            int i = int.Parse(cboNonVIPAccount.SelectedValue.ToString());
            API.Account Account = API.Session.GetNONVIPAccountByUserID(i);
            if (Account != null)
            {
                lblUserID.Text = Account.UserID.ToString();
                txtNPINumber.Text = Account.NPINumber;
                API.Session.NPI = Account.NPINumber;
                txtEmailAddress.Text = Account.EmailAddress;
                txtPassword.Text = "";
                lblLastLogon.Text = Account.LastLogon.ToString();
            }

        }
    }
    private void RefreshNONVIPAccounts()
    {
        cboNonVIPAccount.Items.Clear();
        cboNonVIPAccount.Items.Add(new ListItem("-Select NON-VIP Account-", ""));
        cboNonVIPAccount.Items.Add(new ListItem("New NON-VIP Account", "0"));
        API.Account[] aAccts = API.Session.GetNONVIPAccounts();
        if (aAccts != null)
        {
            for(int a = 0; a < aAccts.Length; a++)
            {
                cboNonVIPAccount.Items.Add(new ListItem(aAccts[a].EmailAddress, aAccts[a].UserID.ToString()));
            }
        }
        cboNonVIPAccount.SelectedIndex = 0;
    }
    private bool ErrorCheck()
    {
        bool bReturn = true;
        if (txtNPINumber.Text == "") { litError.Text += "<BR>You must provide the NPI Number."; return false; }
        if (txtEmailAddress.Text == "") { litError.Text += "<BR>You must provide the Email Address."; return false; }
        if (lblUserID.Text == "0" && txtPassword.Text == "") { litError.Text += "<BR>You must provide the password for a new account."; return false; }
        if (API.Session.NPI != txtNPINumber.Text)
        {
            if (API.Session.NPIAlreadyRegistered(txtNPINumber.Text)) { litError.Text += "<BR>That NPI Number is already registered in the system."; return false; }
        }
        return bReturn;
    }
}