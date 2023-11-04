using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DeleteDoctor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.EHPDoctor_GetDetailsError += Err;
        API.Session.EHPDoctor_DeleteError += Err;
        litError.Text = "";
        if (!IsPostBack)
        {
        }
    }
    protected void DeleteDoctor()
    {
        if (API.Session.AdminEHPDoctorID > 0)
        {
            API.EHPDoctor data = new API.EHPDoctor(API.Session.AdminEHPDoctorID);
            if(data!= null)
            {
                data.Delete();
                API.Session.AdminEHPDoctor = "";
                API.Session.AdminEHPDoctorID = 0;
                API.Session.AdminEHPApptType = "";
                API.Session.AdminEHPApptTypeID = 0;
                API.Session.AdminEHPCPSApptType = "";
                API.Session.AdminEHPCPSApptTypeID = 0;
                API.Session.AdminGECPSApptType = "";
                API.Session.AdminGECPSApptTypeID = 0;
            }
        }
    }
    protected void Err(int Num, string Msg)
    {
        litError.Text += "Error " + Num.ToString() + " - " + Msg + "<br />";
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        DeleteDoctor();
        Response.Redirect("~/Admin/Doctors.aspx");
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/Doctors.aspx");
    }
}