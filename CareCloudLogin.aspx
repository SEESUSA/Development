<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CareCloudLogin.aspx.cs" Inherits="CareCloudLogin" %>
<%@ Register Src="~/CheckLogon.ascx" TagName="CheckLogon" TagPrefix="uc1" %>
<!DOCTYPE html>
<style>
    .loader-container {
            background: #07253121;
            top: 0;
            left: 0;
            z-index: 50;
            width: 100%;
            height: 100%;
            position: fixed;
        }

        .load {
            width: 100px;
            height: 100px;
            position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            margin: auto;
        }
    .lds-dual-ring {
  display: inline-block;
  width: 80px;
  height: 80px;
}
.lds-dual-ring:after {
  content: " ";
  display: block;
  width: 64px;
  height: 64px;
  margin: 8px;
  border-radius: 50%;
  border: 6px solid #fff;
  border-color: #5c9692 transparent #5c9692 transparent;
  animation: lds-dual-ring 1.2s linear infinite;
}
@keyframes lds-dual-ring {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
</style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="styles.css" />
    <title><%=Application.Get("Title") %></title>
    <script src="Scripts/jquery-3.6.0.min.js"></script>
     <script src="Scripts/jquery-ui-1.13.2.min.js"></script>
     <link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css"/>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
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
        $(document).ready(function () {
            $("#btnBack").click(function () {
                $(".loader-container").show();
                setTimeout(function () {
                    $(".loader-container").hide();
                }, 45000);
            });
        });
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
         <div class="buttons">
                <asp:Button runat="server" ID="btnBack" CssClass="Button" Text="Cancel" OnClick="btnBack_Click" />
            </div>
         <div class="loader-container" style="display:none">
             <div class="load">
            <div class="lds-dual-ring"></div>
            </div>
            </div>
        <div><br /></div>
          
        <div class="footer" style="display:none;">
            
           
        
            Please click <asp:LinkButton ID="LnkBtnCareCloudLogin" runat="server" OnClick="LnkBtnCareCloudLogin_Click" >here </asp:LinkButton>to login to Care Cloud.
        
            <br />
        </div>
        
        
        </form>
           </body>
</html>
