using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


/// <summary>
/// This page holds the available tasks that a user can perform.  
/// It redirects the user to the appropriate page based on the
/// selected task.
/// </summary>

public partial class Tasks : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RefreshTasks();
        }
    }
    protected void RefreshTasks()
    {
        cboTask.Items.Clear();
        cboTask.Items.Add(new ListItem("Select Task", "0"));
        cboTask.Items.Add(new ListItem("Schedule Appointment", "3"));
        cboTask.Items.Add(new ListItem("Edit Profile", "1"));
        cboTask.Items.Add(new ListItem("Change Password", "4"));
        cboTask.Items.Add(new ListItem("Logoff", "5"));
        SetTask();
    }
    protected void cboTask_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (cboTask.SelectedItem.Text)
        {
            case "Edit Profile":
                Response.Redirect("~/EditProfiles.aspx");
                break;
            case "Schedule Appointment":
                Response.Redirect("~/CareCloudLogin.aspx");
                break;
            case "Change Password":
                Response.Redirect("~/ChangePassword.aspx");
                break;
            case "Logoff":
                Response.Redirect("~/Logoff.aspx");
                break;
            case "CareCloudLogin":
                Response.Redirect("~/CareCloudLogin.aspx");
                break;
            default:
                break;
        }
    }
    protected void SetTask()
    {
        switch (Request.Path)
        {
            case "/EHPortal/ScheduleAppt.aspx":
                cboTask.SelectedIndex = 1;
                break;
            case "/EHPortal/EditProfiles.aspx":
                cboTask.SelectedIndex = 2;
                break;
            case "/EHPortal/ChangePassword.aspx":
                cboTask.SelectedIndex = 3;
                break;
            default:
                cboTask.SelectedIndex = 0;
                break;
        }
    }

}