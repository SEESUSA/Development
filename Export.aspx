<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Export.aspx.cs" Inherits="Export" %>

<!DOCTYPE html>
<style>
    .ui-datepicker {
    font-size: 12px;
}
.Error {
    margin-top: 10px !important;
}
</style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1"  runat="server">
     <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%=Application.Get("Title").ToString() %></title>
    <link href="styles.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.6.0.min.js"></script>
     
    <script src="Scripts/jquery-ui-1.13.2.min.js"></script>
     <link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css"/>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    
    <%--<script src="Scripts/jquery-3.6.4.min.js"></script>--%>
    <script>
        $(function () {
            $(".date-picker").datepicker();
        });
    </script>
     <script type="text/javascript">
         function ShowModal(newMessage) {
             $('#myModal').css('display', 'block');
             $('#modal-message').text(newMessage);
         }
     </script>
     <script type="text/javascript">
         $(document).ready(function () {
             // Initialize the multiselect dropdown
             //$('#ddlLoginMode').multiselect({
             //    includeSelectAllOption: true
             //});
         });
     </script>
     <script type="text/javascript">
         // When the document is ready
         $(document).ready(function () {

             // When the user clicks the close button or outside the modal, close it
             $(".close, .modal").click(function () {
                 $("#myModal").css("display", "none");
                 $("#ddlLoginMode").val('0');
                 $("#cboEHPFacility").empty();
                 $("#CalCreationDate").val('');
                 $("#CalEndDate").val('');
                 $("#CalAppStartDate").val('');
                 $("#CalAppEndDate").val('');
                 $("#ddlStatus").val('0');
                // window.location.href = '/Logoff.aspx';

             });

             // Prevent the modal from closing when clicking inside it
             $(".modal-content").click(function (e) {
                 e.stopPropagation();
             });

            

             $("#CalCreationDate").change(function () {
                 // Get the TextBox controls
                 var startDateTextBox = $("#CalCreationDate").val();
                 var endDateTextBox = $("#CalEndDate").val();
                 var validationResult = document.getElementById('validationResult');

                 // Parse the selected dates
                 var startDate = new Date(startDateTextBox);
                 var endDate = new Date(endDateTextBox);

                 // Check if end date is greater than or equal to start date
                 if (endDate < startDate) {
                     //  alert('End date must be greater than or equal to start date');
                     validationResult.innerText = 'End date must be greater than or equal to start date';
                     // Clear the end date or take other appropriate action
                     endDateTextBox.value = '';
                 }
                 else {
                     validationResult.innerText = '';
                 }
             });

                  $("#CalEndDate").change(function () {
                 // Get the TextBox controls
                 var startDateTextBox = $("#CalCreationDate").val();
                 var endDateTextBox = $("#CalEndDate").val();
                 var validationResult = document.getElementById('validationResult');

                 // Parse the selected dates
                 var startDate = new Date(startDateTextBox);
                 var endDate = new Date(endDateTextBox);

                 // Check if end date is greater than or equal to start date
                 if (endDate < startDate) {
                     //  alert('End date must be greater than or equal to start date');
                     validationResult.innerText = 'End date must be greater than or equal to start date';
                     // Clear the end date or take other appropriate action
                     endDateTextBox.value = '';
                 }
                 else {
                     validationResult.innerText = '';
                 }
                  });

             $("#CalAppStartDate").change(function () {
                 // Get the TextBox controls
                 var startDateTextBox = $("#CalAppStartDate").val();
                 var endDateTextBox = $("#CalAppEndDate").val();
                 var validationResult = document.getElementById('validationResult1');

                 // Parse the selected dates
                 var startDate = new Date(startDateTextBox);
                 var endDate = new Date(endDateTextBox);

                 // Check if end date is greater than or equal to start date
                 if (endDate < startDate) {
                     //  alert('End date must be greater than or equal to start date');
                     validationResult.innerText = 'End date must be greater than or equal to start date';
                     // Clear the end date or take other appropriate action
                     endDateTextBox.value = '';
                 }
                 else {
                     validationResult.innerText = '';
                 }
             });

             $("#CalAppEndDate").change(function () {
                 // Get the TextBox controls
                 var startDateTextBox = $("#CalAppStartDate").val();
                 var endDateTextBox = $("#CalAppEndDate").val();
                 var validationResult = document.getElementById('validationResult1');

                 // Parse the selected dates
                 var startDate = new Date(startDateTextBox);
                 var endDate = new Date(endDateTextBox);

                 // Check if end date is greater than or equal to start date
                 if (endDate < startDate) {
                     //  alert('End date must be greater than or equal to start date');
                     validationResult.innerText = 'End date must be greater than or equal to start date';
                     // Clear the end date or take other appropriate action
                     endDateTextBox.value = '';
                 }
                 else {
                     validationResult.innerText = '';
                 }
             });
           
         });

     </script>
     <style>
      /* Style for the modal container */
.modal {
    display: none;
    position: fixed;
    z-index: 1;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.7);
}

/* Style for the modal content */
.modal-content {
    background-color: #fff;
    margin: 15% auto;
    padding: 0px;
    border: 1px solid #888;
    width: 80%;
    max-width: 400px;
    text-align: center;
    position: relative;
}

/* Style for the close button */
.close {
    position: absolute;
    top: 0;
    right: 0;
    padding: 10px;
    cursor: pointer;
}
 .form-group {
        display: inline-flex;
        margin-top: -6px; /* Add some margin between the label and textboxes */
    }
  </style>
</head>
<body>
    <header>
        <img src="img/MicrosoftTeams-image.png" alt="eye health partners/vision america portal" width="397" height="44">
        <h2 class="PageTitle">Export Appointment Requests List </h2>
    </header>
    <form id="form1" runat="server">
        <div>
             <label> Please Select Location: <i style="color:red">*</i>
                 
      <asp:DropDownList ID="ddlLoginMode" CssClass="TextField" runat="server" OnSelectedIndexChanged="ddlLoginMode_SelectedIndexChanged" AutoPostBack="true" >
        </asp:DropDownList>
         </label>
              <label>Facility: <i style="color:red;" >*</i>
                <asp:DropDownList runat="server" ID="cboEHPFacility" CssClass="TextField" AutoPostBack="true"  OnSelectedIndexChanged="cboEHPFacility_SelectedIndexChanged" />

              </label>
            <label >Request(s) Received:

            </label>
            <div class="form-group">
                 <asp:TextBox ID="CalCreationDate" runat="server" CssClass="TextField date-picker" ></asp:TextBox> 
                <span style="font-weight: normal; margin-left: 5px; margin-right: 5px;  margin-top: 9px;">To</span>
                 <asp:TextBox ID="CalEndDate" runat="server" CssClass="TextField date-picker" ></asp:TextBox>
            </div>
            <p id="validationResult" class="Error"></p>
              <label>Appointment(s) Date:
                  
            </label>
            <div class="form-group">
                <asp:TextBox ID="CalAppStartDate" runat="server" CssClass="TextField date-picker" ></asp:TextBox>
                  <span style="font-weight: normal; margin-left: 5px; margin-right: 5px;  margin-top: 9px;">To</span>
                   <asp:TextBox ID="CalAppEndDate" runat="server" CssClass="TextField date-picker"></asp:TextBox>
            </div>
             <p id="validationResult1" class="Error"></p>
            <label>Status:  
                <asp:DropDownList runat="server" ID="ddlStatus" CssClass="TextField" AutoPostBack="true" />

              </label>
             <div class="buttons">
                <asp:Button runat="server" ID="btnExport" CssClass="Button" Text="Export" OnClick="btnExport_Click" />
                  <asp:Button runat="server" ID="btnCancel" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
         <div id="myModal" class="modal">
    <div class="modal-content">
        <span class="close">&times;</span>
        <p id="modal-message"> Export Completed Successfully</p>
    </div>
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    $(function () {
       
        $("#btnExport").click(function () {
            setTimeout(function () {
                $('#myModal').css('display', 'block');
            }, 3000);
        });
    });
</script>
