using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditProfiles : System.Web.UI.Page
{

    /// <summary>
    /// This page allows the user to edit information in their profile
    /// data stored in the EHPortal database
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>


    protected void Page_Load(object sender, EventArgs e)
    {
        API.Session.GetLocationsError += Err;
        API.Session.GetNPIError += Err;
        API.Session.Location_RefreshDataError += Err;
        API.Session.Location_SaveDataError += Err;
        litError.Text = "";
        if (!IsPostBack)
        {
            RefreshLocations();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("./");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (cboLocation.SelectedIndex == 0) { Err(1054, "You must select a location first."); return; }
        if (txtPhone.Text == "") { Err(1052, "Phone number cannot be blank."); return; }
        if (txtFax.Text == "") { Err(1053, "Fax number cannot be blank."); return; }

        int id = int.Parse(cboLocation.SelectedItem.Value);
        string database = cboLocation.SelectedItem.Text.Substring(1, 2);
        if (id > 0)
        {
            API.Location data = new API.Location(database, id);
            data.Phone = txtPhone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Replace("Ext.", "");
            data.Fax = txtFax.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Replace("Ext.", "");
            data.SaveData();
        }
        cboLocation.SelectedIndex = 0;
        pnlDetails.Visible = false;
    }
    protected void cboLocation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int id = int.Parse(cboLocation.Items[cboLocation.SelectedIndex].Value.ToString());
        string database = cboLocation.Items[cboLocation.SelectedIndex].Text.Substring(1, 2);
        if (id > 0)
        {
            EHP.Formatting f = new EHP.Formatting();
            API.Location data = new API.Location(database,id);
            lblFirst.Text = data.First;
            lblMiddle.Text = data.Middle;
            lblLast.Text = data.Last;
            lblOrgName.Text = data.OrgName;
            lblAddress1.Text = data.Address1;
            lblAddress2.Text = data.Address2;
            lblCity.Text = data.City;
            lblState.Text = data.State;
            lblZip.Text = data.Zip;
            if(data.Phone != "") txtPhone.Text = f.PhoneNumber(data.Phone);
            if (data.Fax != "") txtFax.Text = f.PhoneNumber(data.Fax);
            pnlDetails.Visible = true;
        }
        else
        {
            pnlDetails.Visible = false;
        }
    }

    #region Functions
    protected void RefreshLocations()
    {
        string db = "";
        cboLocation.Items.Clear();
        cboLocation.Items.Add(new ListItem("Select Location", "0"));
        API.Location[] locations = API.Session.GetLocations(API.Session.GUID);
        if (locations != null)
        {
            for(int a=0; a < locations.Length;a++)
            {
                if (locations[a].Database == "Birmingham") db = "AL";
                if (locations[a].Database == "Nashville")
                {
                    db = "TN";
                    continue;
                }
                cboLocation.Items.Add(new ListItem("[" + db + "] " + locations[a].Display, locations[a].ID.ToString()));
            }
        }
    }
    protected void Err(int num, string msg)
    {
        litError.Text += "Error " + num.ToString() + " - " + msg + "<br />";
    }
    #endregion
}