<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CareCloudLogin.aspx.cs" Inherits="CareCloudLogin" %>
<%@ Register Src="~/CheckLogon.ascx" TagName="CheckLogon" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="styles.css" />
    <title><%=Application.Get("Title") %></title>
    <uc1:CheckLogon ID="CheckLogon1" runat="server" />
    <style>
.footer {
   position: fixed;
   left: 0;
   bottom: 100px;
   width: 100%;
   font-weight:bold;
   color: black;
   text-align: center;
}
</style>
    <script type="text/javascript">
               
            window.onload = function (event) {
            
            //$("#ddlLoginMode").Empty();
                document.getElementById("ddlLoginMode").selectedIndex = 0;
   
        };
    </script>
   

   
    
</head>
<body>
    <header>
      <img src="img/MicrosoftTeams-image.png" alt="eye health partners/vision america portal" width="397" height="44">
    <%--  <h2 class="PageTitle">Welcome</h2>--%>
        <br /><br />
       <div style="text-align:center; font-size:20px "> Welcome to Schedule Appointment Wizard </div> 
    </header>
    <form id="form2" runat="server" style="  height:522px">
      
        <label> Please Select Location:
      <asp:DropDownList ID="ddlLoginMode" CssClass="TextField" runat="server" OnSelectedIndexChanged="ddlLoginMode_SelectedIndexChanged" AutoPostBack="true" Width="230px">
           
                                 </asp:DropDownList>
         </label>
        <div><br /></div>
        <div><br /></div>
          
        <div class="footer" style="display:none;">
            
           
        
            Please click <asp:LinkButton ID="LnkBtnCareCloudLogin" runat="server" OnClick="LnkBtnCareCloudLogin_Click" >here </asp:LinkButton>to login to Care Cloud.
        
            <br />
        </div>
        
        
        </form>
           </body>
</html>
