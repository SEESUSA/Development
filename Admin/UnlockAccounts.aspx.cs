using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UnlockAccounts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.AdminGetLockedAccountsError += Err;
        btnUnlock.Click += BtnUnlock_Click;
        litError.Text = "";
        
        if (!IsPostBack)
        {
            // Refresh UserList dropdown
            RefreshUserList();
        }

    }

    private void BtnUnlock_Click(object sender, EventArgs e)
    {
        if(int.Parse(cboUserList.SelectedItem.Value) > 0)
        {
            // Update Password and Unlock Account
            API.Session.AdminUnlockUserAccount(int.Parse(cboUserList.SelectedItem.Value));

            // send email to both the user and the unlocked account
            System.Guid guid = API.Session.GetGUID(cboUserList.SelectedItem.ToString());
            API.Session.SendEmail(API.EmailType.UnlockAccount, guid);

            // Refresh list of locked accounts
            RefreshUserList();
        }
    }

    protected void Err(int num, string msg)
    {
        litError.Text += "Error " + num.ToString() + " - " + msg + "<br />";
    }
    protected void RefreshUserList()
    {
        /*
         * This function refreshes the UserList to be unlocked
         */
        cboUserList.Items.Clear();
        cboUserList.Items.Add(new ListItem("Select User Account", "0"));

        API.UserList[] oUserList = API.Session.AdminGetLockedAccounts();
        if (oUserList != null)
        {
            if(oUserList.Length > 0)
            {
                for(int a = 0; a < oUserList.Length;a++)
                {
                    cboUserList.Items.Add(new ListItem(oUserList[a].EmailAddress, oUserList[a].ID.ToString()));
                }
            }
        }

    }
    
}