using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DeleteApptType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.EHPApptType_DeleteError += Err;
        API.Session.EHPApptType_GetDetailsError += Err;
        litError.Text = "";
        if (!IsPostBack)
        {
        }
    }
    protected void DeleteApptType()
    {
        if (API.Session.AdminEHPApptTypeID > 0)
        {
            API.EHPApptType data = new API.EHPApptType(API.Session.AdminEHPApptTypeID);
            data.Delete();
        }
    }
    protected void Err(int ErrNum, string Msg)
    {
        litError.Text += "Error " + ErrNum.ToString() + " - " + Msg + "<br />";
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        DeleteApptType();
        API.Session.AdminEHPCPSApptType = "";
        API.Session.AdminEHPCPSApptTypeID = 0;
        API.Session.AdminEHPApptType = "";
        API.Session.AdminEHPApptTypeID = 0;
        API.Session.AdminGECPSApptType = "";
        API.Session.AdminGECPSApptTypeID = 0;
        Response.Redirect("~/Admin/ApptTypes.aspx");
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/ApptTypes.aspx");
    }
}