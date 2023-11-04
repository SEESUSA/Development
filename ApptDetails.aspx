<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApptDetails.aspx.cs" Inherits="ApptDetails" %>
<%@ Register Src="CheckLogon.ascx" TagName="CheckLogon" TagPrefix="uc1" %>
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
        <h2 class="PageTitle">Appointment Details</h2>
    </header>
    <form id="form1" runat="server">
        <uc1:Tasks runat="server" ID="Tasks1" />
        <p>You can print this page to give the appointment details to the patient.</p>
        <div>
            <label>Referring Dr:
                <asp:Label runat="server" ID="lblRefDr" /></label>
            <label>Facility:
                <asp:Label runat="server" ID="lblFacility" />
                <asp:Label runat="server" ID="lblFacAddress" />
                <asp:Label runat="server" ID="lblFacCSZ" /></label>
            <label>Phone:
                <asp:Label runat="server" ID="lblPhone" /></label>
            <label>Fax:
                <asp:Label runat="server" ID="lblFax" /></label>
            <label>Doctor:
                <asp:Label runat="server" ID="lblDoctor" /></label>
            <label>Appt Type:
                <asp:Label runat="server" ID="lblApptType" /></label>
            <label>Date/Time:
                <asp:Label runat="server" ID="lblApptSlot" /></label>
            <label>Eye:
                <asp:Label runat="server" ID="lblEye" /></label>
        </div>
        <p>Please fax your referral note to our fax number above.</p>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
    </form>
</body>
</html>