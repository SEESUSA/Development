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
        btnSave.Click += BtnSave_Click;
        btnSend.Click += BtnSend_Click;
        //btnSend.Visible = false;
        //btnDelete.Visible = false;
        btnAdd.Click += BtnAdd_Click;
        btnDelete.Click += BtnDelete_Click;
        btnYes.Click += BtnYes_Click;
        btnNo.Click += BtnNo_Click;
        btnConvYes.Click += BtnConvYes_Click;
        btnConvNo.Click += BtnConvNo_Click;
        cboMasterAccount.SelectedIndexChanged += CboMasterAccount_SelectedIndexChanged;
        litError.Text = "";
        if (!IsPostBack)
        {
            RefreshMasterAccounts();
            SetMasterAccount();
        }
    }

    private void BtnNo_Click(object sender, EventArgs e)
    {
        pnlConfirmDelete.Visible = false;
    }

    private void BtnYes_Click(object sender, EventArgs e)
    {
        API.Session.DeleteVIPAccount(API.Session.AdminMasterAccountID);
        pnlConfirmDelete.Visible = false;
        txtEmailAddress.Text = "";
        RefreshMasterAccounts();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        pnlConfirmDelete.Visible = true;
    }

    private void BtnConvNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/VIP.aspx");
    }
    private void BtnConvYes_Click(object sender, EventArgs e)
    {
        API.Session.AdminMasterAccountID = API.Session.AdminCovertToMaster(txtEmailAddress.Text);
        Response.Redirect("~/Admin/VIP.aspx");
    }
    private void BtnAdd_Click(object sender, EventArgs e)
    {
        API.Session.AdminChildAccountID = 0;
        Response.Redirect("~/Admin/AddChild.aspx?ChildID=0");
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
                btnSend.Visible = true;
                btnSend.Enabled = true;
                btnDelete.Visible = true;
                LoadChildren();
            }
            else
            {
                API.Session.AdminMasterAccountID = 0;
                txtEmailAddress.Text = "";
                pnlChildren.Visible = false;
                btnSend.Visible = false;
                btnSend.Enabled = false;
                btnDelete.Visible = false;
            }
        }
    }
    private void SetMasterAccount()
    {
        if (API.Session.AdminMasterAccountID > 0)
        {
            foreach (ListItem item in cboMasterAccount.Items)
            {
                //    item.Selected = (item.Value == API.Session.AdminMasterAccountID.ToString());
                //    if (item.Selected)
                //    {
                //        txtEmailAddress.Text = API.Session.GetEmailAddress(API.Session.AdminMasterAccountID);
                //        pnlChildren.Visible = true;
                //        btnSend.Enabled = true;
                //        LoadChildren();
                //    }

                item.Selected = (item.Text == txtEmailAddress.Text);
                if (item.Selected)
                {
                    API.Session.AdminMasterAccountID = int.Parse(item.Value);
                    pnlChildren.Visible = true;
                    btnSend.Enabled = true;
                    btnSend.Visible = true;
                    LoadChildren();
                }

            }
        }
    }
    private void BtnSend_Click(object sender, EventArgs e)
    {
        if (ValidateSave())
        {
            Guid guid = API.Session.GetGUID(txtEmailAddress.Text);
            API.Session.SendEmail(API.EmailType.VIPSend, guid);
            Response.Redirect("~/Admin/VIP.aspx");
        }

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
                //API.Session.AdminSaveMasterAccount(API.Session.AdminMasterAccountID, txtEmailAddress.Text);
                API.Session.AdminMasterAccountID = API.Session.AdminSaveMasterAccount(API.Session.AdminMasterAccountID, txtEmailAddress.Text);
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
            for (int a = 0; a < Children.Length; a++)
            {
                litChildren.Text += "<tr>";
                //Options
                litChildren.Text += "<td>";
                litChildren.Text += "<a href=\"AddChild.aspx?ChildID=" + Children[a].UserID.ToString() + "\">Edit</a>";
                litChildren.Text += " - <a href=\"DeleteChild.aspx?ChildID=" + Children[a].UserID.ToString() + "\">Delete</a>";
                litChildren.Text += "</td>";
                //Name
                litChildren.Text += "<td>" + Children[a].Name + "</td>";
                //NPI 
                litChildren.Text += "<td>" + Children[a].NPI + "</td>";
                litChildren.Text += "</tr>";
            }
        }
    }
}