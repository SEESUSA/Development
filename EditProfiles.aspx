<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditProfiles.aspx.cs" Inherits="EditProfiles" %>
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
        <img src="img/MicrosoftTeams-image.png" alt="eye health partners/vision america portal" width="397" height="44">
        <h2 class="PageTitle">Edit Profile</h2>
    </header>
    <form id="form1" runat="server">
        <uc1:Tasks runat="server" ID="Tasks1" />

        <div>
            <asp:Panel runat="server" ID="pnlSearch" Visible="true">
                <label>Location
                    <asp:DropDownList runat="server" ID="cboLocation" CssClass="TextField" OnSelectedIndexChanged="cboLocation_OnSelectedIndexChanged"  AutoPostBack="true" /></label>
                <p>Please call our office if your practice location(s) has changed from the list above.</p>
                <hr>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlDetails" Visible="false">
                <label>First:
                    <asp:Label runat="server" ID="lblFirst" /></label>
                <label>Middle:
                    <asp:Label runat="server" ID="lblMiddle" /></label>
                <label>Last:
                    <asp:Label runat="server" ID="lblLast" /></label>
                <label>Organization:
                    <asp:Label runat="server" ID="lblOrgName" /></label>
                <label>Address:
                    <asp:Label runat="server" ID="lblAddress1" /></label>
                <label>Address 2:
                    <asp:Label runat="server" ID="lblAddress2" /></label>
                <label>City/State/Zip:
                    <asp:Label runat="server" id="lblCity" />
                    <asp:Label runat="server" ID="lblState" />
                    <asp:Label runat="server" ID="lblZip" /></label>
                <label>Phone
                    <asp:TextBox runat="server" ID="txtPhone" CssClass="TextField" /></label>
                <label>Fax:
                    <asp:TextBox runat="server" ID="txtFax" CssClass="TextField" /></label>
                <div class="buttons">
                    <asp:Button runat="server" ID="btnSave" CssClass="Button" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button runat="server" ID="btnCancel" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </asp:Panel>
        </div>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
    </form>
</body>
</html>