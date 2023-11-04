using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DeleteChild : System.Web.UI.Page
{

    private int childid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        childid = int.Parse(Request.QueryString["ChildID"].ToString());

        if (childid > 0)
        {
            API.Session.AdminDeleteChild(childid);
            Response.Redirect("~/Admin/VIP.aspx");
        }
    }
}