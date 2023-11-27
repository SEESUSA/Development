using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Xml;
using Newtonsoft.Json.Linq;

public partial class Export : System.Web.UI.Page
{
    static DataSet dsLocation = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        litError.Text = "";
        if (!IsPostBack)
        {
            RefreshLoginMode();
            ddlStatus.Items.Add(new ListItem("Select Status", "0"));
            ddlStatus.Items.Add(new ListItem("All Status", "3"));
            ddlStatus.Items.Add(new ListItem("Successful", "1"));
            ddlStatus.Items.Add(new ListItem("Failed", "2"));
           
        }
    }

    private void RefreshLoginMode()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = API.Session.GetCareCloudMasterUserList();
            string state = "";
            bool IsStateAvailable = false;
            API.Location[] data = API.Session.GetLocationsforTN(API.Session.Email);//API.Session.GUID
            ddlLoginMode.Items.Clear();
            ddlLoginMode.Items.Add(new ListItem("Select Your Location", "0"));
            ddlLoginMode.Items.Add(new ListItem("All Locations", "1"));
            if (data != null)
            {

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["state"].ToString() == "TN" || dr["state"].ToString() == "AL" || dr["state"].ToString() == "ETN")
                        {
                            ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                        }


                        //if (API.Session.IsAL == true && API.Session.IsMiddleTN == true && API.Session.IsEastTN == true)// && API.Session.IsEastTN == true
                        //{
                        //    if (dr["state"].ToString() == "TN" || dr["state"].ToString() == "AL" || dr["state"].ToString() == "ETN")
                        //    {
                        //        IsStateAvailable = true;

                        //        ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                        //    }
                        //}
                        //else if (API.Session.IsAL == true && API.Session.IsMiddleTN == true && API.Session.IsEastTN != true)// && API.Session.IsEastTN == true
                        //{
                        //    if (dr["state"].ToString() == "TN" || dr["state"].ToString() == "AL")
                        //    {
                        //        IsStateAvailable = true;

                        //        ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                        //    }
                        //}

                        //else if (API.Session.IsAL != true && API.Session.IsMiddleTN == true && API.Session.IsEastTN == true)// (API.Session.IsMiddleTN == true || API.Session.IsEastTN == true)
                        //{
                        //    if (dr["state"].ToString() == "TN" || dr["state"].ToString() == "ETN")
                        //    {
                        //        IsStateAvailable = true;

                        //        ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                        //    }
                        //}
                        //else if (API.Session.IsAL != true && API.Session.IsMiddleTN == true && API.Session.IsEastTN != true)// (API.Session.IsMiddleTN == true || API.Session.IsEastTN == true)
                        //{
                        //    if (dr["state"].ToString() == "TN")
                        //    {
                        //        IsStateAvailable = true;

                        //        ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                        //    }
                        //}
                        //else if (API.Session.IsAL != true && API.Session.IsMiddleTN != true && API.Session.IsEastTN == true)// (API.Session.IsMiddleTN == true || API.Session.IsEastTN == true)
                        //{
                        //    if (dr["state"].ToString() == "ETN")
                        //    {
                        //        IsStateAvailable = true;

                        //        ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                        //    }
                        //}
                        //else if (API.Session.IsAL == true && API.Session.IsMiddleTN != true && API.Session.IsEastTN == true)// && (API.Session.IsMiddleTN != true || API.Session.IsEastTN != true))
                        //{
                        //    if (dr["state"].ToString() == "AL" || dr["state"].ToString() == "ETN")
                        //    {
                        //        IsStateAvailable = true;

                        //        ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                        //    }
                        //}
                        //else if (API.Session.IsAL == true && API.Session.IsMiddleTN != true && API.Session.IsEastTN != true)// && (API.Session.IsMiddleTN != true || API.Session.IsEastTN != true))
                        //{
                        //    if (dr["state"].ToString() == "AL")
                        //    {
                        //        IsStateAvailable = true;

                        //        ddlLoginMode.Items.Add(new ListItem(dr["PracticeName"].ToString(), dr["practiceId"].ToString()));
                        //    }
                        //}


                    }
                }
            }
            //if (IsStateAvailable == false)
            //{ Response.Redirect("~/ScheduleAppt.aspx"); }
        }
        catch (Exception ex) { }
    }
    public bool ExportToExcel()
    {
        try
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Create a new Excel package
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet to the Excel package
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Create a SQL connection and command
                using (SqlConnection connection = new SqlConnection(Statics.EHPconnstring))
                using (SqlCommand command = new SqlCommand("USP_GetAppointmentLog", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@SEESEntity", SqlDbType.VarChar, 20).Value = ddlLoginMode.SelectedItem.Text.ToString();
                    command.Parameters.Add("@Facility", SqlDbType.VarChar, 50).Value = cboEHPFacility.SelectedValue=="0"? "All Facilities" : cboEHPFacility.SelectedItem.Text.ToString();
                    command.Parameters.Add("@CreationDate", SqlDbType.Date).Value = string.IsNullOrEmpty(CalCreationDate.Text) ? Convert.ToString("1900-01-01") : CalCreationDate.Text;
                    command.Parameters.Add("@EndDate", SqlDbType.Date).Value = string.IsNullOrEmpty(CalEndDate.Text) ? Convert.ToString("9999-12-31") : CalEndDate.Text;
                    command.Parameters.Add("@AppointmentStartDate", SqlDbType.Date).Value = string.IsNullOrEmpty(CalAppStartDate.Text) ? Convert.ToString("1900-01-01") : CalAppStartDate.Text;
                    command.Parameters.Add("@AppointmentEndDate", SqlDbType.Date).Value = string.IsNullOrEmpty(CalAppEndDate.Text) ? Convert.ToString("9999-12-31") : CalAppEndDate.Text;
                    command.Parameters.Add("@AppointmentStatus", SqlDbType.VarChar, 20).Value = ddlStatus.SelectedValue =="0" ? "All Status" : ddlStatus.SelectedItem.Text;
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    if(dataTable!=null && dataTable.Rows.Count > 0)
                    {
                        // Load the data from the DataTable to the Excel worksheet
                        worksheet.Cells.LoadFromDataTable(dataTable, true);

                        foreach (DataColumn col in dataTable.Columns)
                        {
                            if (col.DataType == typeof(DateTime))
                            {
                                // Assuming "MM/dd/yyyy hh:mm tt" is the desired format (adjust as needed)
                                worksheet.Column(col.Ordinal + 1).Style.Numberformat.Format = "MM/dd/yyyy";
                            }
                        }
                        // Save the Excel package to a MemoryStream
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            package.SaveAs(memoryStream);
                            string formattedDate = DateTime.Now.ToString("MMddyyyyHHmm").Replace("/", "").Replace(" ", "").Replace(":", "");

                            string filename = "RPPApptLog-" + formattedDate;
                            // Set the response headers for the file download
                            HttpContext.Current.Response.Clear();
                            HttpContext.Current.Response.Buffer = true;
                            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + filename + ".xlsx");
                            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            HttpContext.Current.Response.BinaryWrite(memoryStream.ToArray());
                            HttpContext.Current.Response.End();
                            //HttpContext.Current.Response.Flush();
                        }
                        return true;
                    }
                    else
                    {
                        // HttpContext.Current.Response.Clear();
                        return false;
                    }
                    
                   // HttpContext.Current.ApplicationInstance.CompleteRequest(); // Complete the request without ending the response
                }
            }

            // Indicate success
        }
        catch (Exception ex)
        {
            // Handle exceptions if needed
            return false; // Indicate failure
        }
    }



    protected void ddlLoginMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        string practiceId = "";
        API.Session.PracticeId = "";
        try
        {
            string SEESEntity = ddlLoginMode.SelectedItem.Text.ToString();
            if (!string.IsNullOrEmpty(SEESEntity) && SEESEntity!="All Locations")
            {
                string Qry = " Select * from Appointment_Facility where SEESEntity= '" + SEESEntity + "'";
                using (SqlConnection connection = new SqlConnection(Statics.EHPconnstring))
                using (SqlCommand command = new SqlCommand(Qry, connection))
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    cboEHPFacility.Items.Clear();
                    cboEHPFacility.Items.Add(new ListItem("Select Facility", "0"));
                    cboEHPFacility.Items.Add(new ListItem("All Facilities", "1"));

                    foreach (DataRow dr in dataTable.Rows)
                    {//Remaining to check enable.need to check when get details.

                        if (dr["Id"].ToString() == "46679" )//|| dr["Id"].ToString() == "39341") //|| dr["id"].ToString() == "44736"
                        {


                        }
                        else if (dr["Name"].ToString().Trim() == "OFFICE ONEONTA" || dr["Name"].ToString().Trim() == "OFFICE HAMILTON DR SHOTTS" || dr["Name"].ToString().Trim() == "OFFICE HAMILTON DR COBB" || dr["Name"].ToString().Trim() == "OFFICE GREENVILLE" || dr["Name"].ToString().Trim() == "OFFICE GARDENDALE" || dr["Name"].ToString().Trim() == "OFFICE CENTREVILLE")
                        {

                        }
                        else
                        {
                            cboEHPFacility.Items.Add(new ListItem(dr["Name"].ToString(), dr["Id"].ToString()));
                        }
                    }
                }
            }
            else
            {
                cboEHPFacility.Items.Clear();
                if(SEESEntity == "All Locations")
                {
                    cboEHPFacility.Items.Add(new ListItem("Select Facility", "0"));
                    cboEHPFacility.Items.Add(new ListItem("All Facilities", "1"));
                }
               
            }
            

        }
        catch (Exception ex) { }
    }
    protected void Err(int num, string msg)
    {
        litError.Text += "Error " + num.ToString() + " - " + msg + "<br />";
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
       if (ddlLoginMode.Items.Count <= 1) { Err(1099, "You must select a Location first."); return; }
        if (ddlLoginMode.SelectedIndex == 0) { Err(1100, "You must select a Location first."); return; }
        if (ddlLoginMode.SelectedValue != "1" && cboEHPFacility.Items.Count <= 1) { Err(1100, "You must select a Facility first."); return; }
        if (ddlLoginMode.SelectedValue != "1" && cboEHPFacility.SelectedIndex == 0) { Err(1100, "You must select a Facility first."); return; }
        bool exportSuccess = ExportToExcel();

        if (!exportSuccess)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModalScript", " $('#myModal').css('display', 'block');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModalScript", "ShowModal('No Record Found');", true);

            ddlLoginMode.ClearSelection();
            cboEHPFacility.Items.Clear();
            CalCreationDate.Text = "";
            CalEndDate.Text = "";
            CalAppStartDate.Text = "";
            CalAppEndDate.Text = "";
            ddlStatus.ClearSelection();
        }

    }

    protected void cboEHPFacility_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlLoginMode.ClearSelection();
        cboEHPFacility.Items.Clear();
        CalCreationDate.Text = "";
        CalEndDate.Text = "";
        CalAppStartDate.Text = "";
        CalAppEndDate.Text = "";
        ddlStatus.ClearSelection();
          Response.Redirect("~/");
       // Response.Redirect("./Admin/Default.aspx");
        
    }
}