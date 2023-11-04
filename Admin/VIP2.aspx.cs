using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_VIP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Error Events
        API.Session.AdminAccountExistsError += Err;
        API.Session.AdminCreateMasterAccountError += Err;
        API.Session.AdminGetMasterAccountsError += Err;
        API.Session.AdminSaveMasterChildError += Err;
        //Normal Events
        btnSave.Click += BtnSave_Click;
        btnSend.Click += BtnSend_Click;
        btnAdd.Click += BtnAdd_Click;
        btnSaveChild.Click += BtnSaveChild_Click;
        btnConvYes.Click += BtnConvYes_Click;
        btnConvNo.Click += BtnConvNo_Click;
        cboMasterAccount.SelectedIndexChanged += CboMasterAccount_SelectedIndexChanged;
        //init objects
        litError.Text = "";

        if (!IsPostBack)
        {
            API.Session.AdminMasterAccountID = 0;
            RefreshMasterAccounts();
            SetMasterAccount();
        }
    }

    private void BtnConvNo_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
    private void BtnConvYes_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
    private void BtnSaveChild_Click(object sender, EventArgs e)
    {
        if (ValidateSaveChild())
        {
            //API.Session.AdminSaveMasterChild()
        }
    }
    private void BtnAdd_Click(object sender, EventArgs e)
    {
        pnlChild.Visible = true;
    }
    private void CboMasterAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboMasterAccount.Items.Count > 0)
        {
            if (cboMasterAccount.SelectedIndex > 0)
            {
                API.Session.AdminMasterAccountID = int.Parse(cboMasterAccount.SelectedItem.Value.ToString());
                txtEmailAddress.Text = API.Session.GetEmailAddress(API.Session.AdminMasterAccountID);
                pnlChildren.Visible = true;
                btnSend.Enabled = true;
                LoadChildren();
            }
            else
            {
                API.Session.AdminMasterAccountID = 0;
                txtEmailAddress.Text = "";
                pnlChildren.Visible = false;
                btnSend.Enabled = false;
            }
        }
    }
    private void SetMasterAccount()
    {
        foreach (ListItem item in cboMasterAccount.Items)
        {
            item.Selected = (item.Value == API.Session.AdminMasterAccountID.ToString());
        }
    }
    private void BtnSend_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
    private void BtnSave_Click(object sender, EventArgs e)
    {
        /* This function is used to either 'create' or 'update' a master account's email address if the 
           account is already a VIP.  If not already a VIP, user will choose whether or not to conver
           normal account to a VIP account. */
        if (ValidateSave())
        {
            if (!API.Session.AdminAccountExists(txtEmailAddress.Text))
            {
                /* Email Address does not exist in the database as a normal account (it could be a VIP) 
                   This is where we will try to either update or create a master account */
                API.Session.AdminSaveMasterAccount(API.Session.AdminMasterAccountID, txtEmailAddress.Text);
                RefreshMasterAccounts();
                SetMasterAccount();
            }
            else
            {
                /* email address already exists as a normal account.  present user with the prompt and 
                   transfer control to that section. */
                pnlConvert.Visible = true;
            }
        }
    }
    protected void RefreshMasterAccounts()
    {
        cboMasterAccount.Items.Clear();
        cboMasterAccount.Items.Add(new ListItem("New Master Account", "0"));
        API.UserList[] aMasters = API.Session.AdminGetMasterAccounts();
        if (aMasters != null)
        {
            for (int a = 0; a < aMasters.Length; a++)
            {
                cboMasterAccount.Items.Add(new ListItem(aMasters[a].EmailAddress, aMasters[a].ID.ToString()));
            }
        }
    }
    protected bool ValidateSave()
    {
        bool bReturn = true;
        if (txtEmailAddress.Text == "") { Err(1148, "Master account EmailAddress cannot be blank."); bReturn = false; }
        return bReturn;
    }
    protected bool ValidateSaveChild()
    {
        bool bReturn = true;
        if (txtName.Text == "") { Err(1146, "Child account name cannot be blank."); bReturn = false; }
        if (txtNPI.Text == "") { Err(1147, "Child NPI number cannot be blank."); bReturn = false; }
        return bReturn;
    }
    protected void Err(int ErrNum, string Msg)
    {
        litError.Text += "Error " + ErrNum.ToString() + " - " + Msg + "<br />";
    }
    protected void LoadChildren()
    {
        litChildren.Text = "";
        API.ChildAcct[] Children = API.Session.AdminGetMasterChildren();
        if (Children != null)
        {
            for(int a = 0; a < Children.Length; a++)
            {
                litChildren.Text += "<tr>";
                //Options
                litChildren.Text += "<td></td>";
                //Name
                litChildren.Text += "<td>" + Children[a].Name + "</td>";
                //NPI 
                litChildren.Text += "<td>" + Children[a].NPI + "</td>";
                litChildren.Text += "</tr>";
            }
        }
    }
}