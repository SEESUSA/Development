<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Facilities.aspx.cs" Inherits="Admin_Facilities" %>
<%@ Register Src="~/CheckLogon.ascx" TagName="CheckLogon" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/CheckAdmin.ascx" TagName="CheckAdmin" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/AdminTask.ascx" TagName="AdminTask" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="~/styles.css" rel="stylesheet" />
    <title><%=Application.Get("Title").ToString() %></title>
    <uc1:CheckAdmin ID="CheckAdmin1" runat="server" />
    <uc1:CheckLogon ID="CheckLogon1" runat="server" />
</head>
<body>
    <header>
        <img src="../img/header.png" alt="eye health partners/vision america portal" width="397" height="44">
        <h2 class="PageTitle">Facilities Administration</h2>
    </header>
    <form id="form1" runat="server">
        <div>
            <label>Task
                <uc1:AdminTask ID="AdminTask1" runat="server" /></label>
            <label>EHP Facility
                <asp:DropDownList runat="server" ID="cboEHPFacility" CssClass="TextField" AutoPostBack="true" OnSelectedIndexChanged="cboEHPFacility_SelectedIndexChanged" /></label>
            <div class="input-links">
                <asp:LinkButton runat="server" ID="lbNew" Text="New" CssClass="LinkButton" Visible="false" OnClick="lbNew_Click" /> 
                <asp:LinkButton runat="server" ID="lbEdit" Text="Edit" CssClass="LinkButton" Visible="false" OnClick="lbEdit_Click" /> 
                <asp:LinkButton runat="server" ID="lbDelete" Text="Delete" CssClass="LinkButton" Visible="false" OnClick="lbDelete_Click" /> 
                <asp:LinkButton runat="server" ID="lbDoctors" Text="Doctors" CssClass="LinkButton" Visible="false" OnClick="lbDoctors_Click" />
            </div>
            <asp:Panel runat="server" ID="pnlFacilityInfo" Visible="false">
                <label>Friendly Name
                    <asp:Label runat="server" ID="lblEHPName" /></label>
                <label>Centricity Name
                    <asp:Label runat="server" ID="lblGEName" /></label>
                <label>Enabled
                    <asp:Label runat="server" ID="lblEnabled" /></label>
            </asp:Panel>
        </div>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
    </form>
</body>
</html>