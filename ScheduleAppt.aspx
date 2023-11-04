<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScheduleAppt.aspx.cs" Inherits="ScheduleAppt" %>
<%@ Register Src="~/CheckLogon.ascx" TagName="CheckLogon" TagPrefix="uc1" %>
<%@ Register Src="~/Tasks.ascx" TagName="Tasks" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="styles.css" rel="stylesheet" />
    <title><%=Application.Get("Title").ToString() %></title>
    <uc1:CheckLogon ID="CheckLogon1" runat="server" />
</head>
<body>
    <header>
        <img src="img/header.png" alt="eye health partners/vision america portal" width="397" height="44">
        <h2 class="PageTitle">Schedule Appointment Wizard</h2>
    </header>
    <form id="form1" runat="server">
        <uc1:Tasks runat="server" ID="Tasks1" />

        <div>
            <label>Referring From
                <asp:DropDownList runat="server" ID="cboLocation" CssClass="TextField" AutoPostBack="true" OnSelectedIndexChanged="cboLocation_SelectedIndexChanged" /><label>
            <label>Facility
                <asp:DropDownList runat="server" ID="cboEHPFacility" CssClass="TextField" AutoPostBack="true" OnSelectedIndexChanged="cboEHPFacility_SelectedIndexChanged" /></label>
            <label>Doctor
                <asp:DropDownList runat="server" ID="cboEHPDoctor" CssClass="TextField" AutoPostBack="true" OnSelectedIndexChanged="cboEHPDoctor_SelectedIndexChanged" /></label>
            <label>Appointment Type
                <asp:DropDownList runat="server" ID="cboApptType" CssClass="TextField" AutoPostBack="true" OnSelectedIndexChanged="cboApptType_SelectedIndexChanged" /></label>
            <label>Appointment Slot
                <asp:DropDownList runat="server" ID="cboApptSlot" CssClass="TextField" AutoPostBack="true" OnSelectedIndexChanged="cboApptSlot_SelectedIndexChanged" /></label>
                <div class="input-links"><asp:LinkButton runat="server" ID="lbApptSlotMore" CssClass="LinkButton" Text="More" Visible="false" OnClick="lbApptSlotMore_Click" /></div>
            <label>Patient
                <asp:DropDownList runat="server" ID="cboPatient" CssClass="TextField" AutoPostBack="true" OnSelectedIndexChanged="cboPatient_SelectedIndexChanged" /></label>
                <div class="input-links"><asp:LinkButton runat="server" ID="lbNewPatient" CssClass="LinkButton" Text="New" OnClick="lbNewPatient_Click" Visible="false" /></div>
            <label>Phone Number
                <asp:TextBox runat="server" ID="txtPhone" CssClass="TextField" MaxLength="15" /></label>
            <label>Eye</label>
                <asp:RadioButton runat="server" ID="rbEyeLeft" Text="Left" Checked="false" AutoPostBack="true" OnCheckedChanged="rbEyeLeft_CheckedChanged" />
                <asp:RadioButton runat="server" ID="rbEyeRight" Text="Right" Checked="false" AutoPostBack="true" OnCheckedChanged="rbEyeRight_CheckedChanged" />
                <asp:RadioButton runat="server" ID="rbBoth" Text="Both" Checked="true" AutoPostBack="true" OnCheckedChanged="rbBoth_CheckedChanged" />
            <label>Reason for Visit
                <asp:TextBox runat="server" ID="txtReason" CssClass="TextField" TextMode="MultiLine" Rows="4" Width="100%" MaxLength="50" /></label>
            <div class="buttons">
                <asp:Button runat="server" ID="btnBookAppt" CssClass="Button" Text="Book Appointment" OnClick="btnBookAppt_Click" />
                <asp:Button runat="server" ID="btnCancel" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
            <p>Please call our office if your practice location(s) has changed from the list above.</p>
        </div>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
    </form>
</body>
</html>