<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register src="CheckLogon.ascx" tagname="CheckLogon" tagprefix="uc1" %>
<%@ Register Src="~/Tasks.ascx" TagName="Tasks" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="styles.css" />
    <title><%=Application.Get("Title") %></title>
        <uc1:CheckLogon ID="CheckLogon1" runat="server" />

<script type="text/javascript">
               
    window.onload = function (event)
    {
            
                document.getElementById("Tasks1_cboTask").selectedIndex = 0;
        
    };
</script>
   

</head>
<body>
    <header>
      <img src="img/MicrosoftTeams-image.png" alt="eye health partners/vision america portal" width="397" height="44">
      <h2 class="PageTitle">Welcome</h2>
    </header>
    <form id="form1" runat="server">
      <uc1:Tasks ID="Tasks1" runat="server" />
        <%--<asp:Button ID="BtnAppointment" runat="server" CssClass="Button" Text="BookAppointmentwithCarecloud" OnClick="BtnAppointment_Click" />--%>
        <asp:Label ID="Label1" runat="server" Text="" Visible="false"></asp:Label>
        
        </form>
</body>
</html>