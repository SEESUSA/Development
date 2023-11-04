using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AdminTask : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RefreshTasks();
        }
    }
    protected void cboTask_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (cboTask.Items[cboTask.SelectedIndex].Text)
        {
            case "Select Task":
                Response.Redirect("~/Admin/Default.aspx");
                break;
            case "SignUp User":
                Response.Redirect("~/Admin/UserSignup.aspx");
                break;
            case "Admin Account Administration":
                Response.Redirect("~/Admin/AdminAccounts.aspx");
                break;
            case "Test Email":
                Response.Redirect("~/Admin/TestEmail.aspx");
                break;
            case "Facility Administration":
                Response.Redirect("~/Admin/Facilities.aspx");
                break;
            case "Unlock Accounts":
                Response.Redirect("~/Admin/UnlockAccounts.aspx");
                break;
            case "Log Off":
                Response.Redirect("~/Logoff.aspx");
                break;
            case "Change Password":
                Response.Redirect("~/ChangePassword.aspx");
                break;
            case "VIP Administration":
                Response.Redirect("~/Admin/VIP.aspx");
                break;
            case "NON-VIP Administration":
                Response.Redirect("~/Admin/NONVIP.aspx");
                break;
            default:
                break;
        }
    }
    protected void RefreshTasks()
    {
        cboTask.Items.Clear();
        cboTask.Items.Add("Select Task");
        cboTask.Items.Add(new ListItem("SignUp User"));
        if (API.Session.IsSetupAdmin) cboTask.Items.Add(new ListItem("Admin Account Administration"));
        if (API.Session.IsSetupAdmin) cboTask.Items.Add(new ListItem("Test Email"));
        cboTask.Items.Add(new ListItem("Facility Administration"));
        cboTask.Items.Add(new ListItem("VIP Administration"));
        cboTask.Items.Add(new ListItem("NON-VIP Administration"));
        cboTask.Items.Add(new ListItem("Unlock Accounts"));
        cboTask.Items.Add(new ListItem("Change Password"));
        cboTask.Items.Add(new ListItem("Log Off"));
        //cboTask.SelectedIndex = 0;
        SetTask();
    }
    protected void SetTask()
    {
        if(cboTask.Items.Count > 0)
        {
            switch (Request.Path)
            {
                case "/EHPortal/Admin/AdminAccounts.aspx":
                    if (API.Session.IsSetupAdmin) cboTask.SelectedIndex = 1; else cboTask.SelectedIndex = 0;
                    break;
                case "/EHPortal/Admin/TestEmail.aspx":
                    if (API.Session.IsSetupAdmin) cboTask.SelectedIndex = 2; else cboTask.SelectedIndex = 0;
                    break;
                case "/EHPortal/Admin/Facilities.aspx":
                    if (API.Session.IsSetupAdmin) cboTask.SelectedIndex = 3; else cboTask.SelectedIndex = 1;
                    break;
                case "/EHPortal/Admin/VIP.aspx":
                    if (API.Session.IsSetupAdmin) cboTask.SelectedIndex = 4; else cboTask.SelectedIndex = 2;
                    break;
                case "/EHPortal/Admin/NONVIP.aspx":
                    if (API.Session.IsSetupAdmin) cboTask.SelectedIndex = 5; else cboTask.SelectedIndex = 3;
                    break;
                case "/EHPortal/Admin/UnlockAccounts.aspx":
                    if (API.Session.IsSetupAdmin) cboTask.SelectedIndex = 6; else cboTask.SelectedIndex = 4;
                    break;
                case "/EHPortal/ChangePassword.aspx":
                    if (API.Session.IsSetupAdmin) cboTask.SelectedIndex = 7; else cboTask.SelectedIndex = 5;
                    break;
                default:
                    cboTask.SelectedIndex = 0;
                    break;
            }
        }
    }
}