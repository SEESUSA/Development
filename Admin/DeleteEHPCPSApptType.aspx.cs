using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DeleteEHPCPSApptType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.EHPCPSApptType_GetDetailsError += Err;
        API.Session.EHPCPSApptType_DeleteError += Err;
        litError.Text = "";
        if (!IsPostBack)
        {

        }
    }
    protected void DeleteEHPCPSApptType()
    {
        API.EHPCPSApptType data = new API.EHPCPSApptType(API.Session.AdminEHPCPSApptTypeID);
        if (data != null)
        {
            data.Delete();
        }
    }
    protected void Err(int num, string msg)
    {
        litError.Text += "Error " + num.ToString() + " - " + msg + "<br />";
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        if (API.Session.AdminEHPCPSApptTypeID > 0)
        {
            DeleteEHPCPSApptType();
            API.Session.AdminEHPCPSApptType = "";
            API.Session.AdminEHPCPSApptTypeID = 0;
            API.Session.AdminGECPSApptType = "";
            API.Session.AdminGECPSApptTypeID = 0;
            Response.Redirect("~/Admin/EHPCPSApptTypes.aspx");
        }
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/EHPCPSApptTypes.aspx");
    }
}
