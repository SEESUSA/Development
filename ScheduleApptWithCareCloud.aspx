<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScheduleApptWithCareCloud.aspx.cs" Inherits="ScheduleApptWithCareCloud" %>

<%@ Register Src="~/CheckLogon.ascx" TagName="CheckLogon" TagPrefix="uc1" %>
<%--<%@ Register Src="~/Tasks.ascx" TagName="Tasks" TagPrefix="uc1" %>--%>
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
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="styles.css" rel="stylesheet" />
    <title><%=Application.Get("Title").ToString() %></title>
     <link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css"/>
  <link rel="stylesheet" href="/resources/demos/style.css"/>
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script src="Scripts/jquery-ui-1.13.2.min.js"></script>
    <uc1:CheckLogon ID="CheckLogon1" runat="server" />
    <%--<script type="text/javascript">
               
            window.onload = function (event) {
            
            //$("#ddlLoginMode").Empty();
                document.getElementById("cboLocation").selectedIndex = 0;
                document.getElementById("cboEHPFacility").selectedIndex = 0;
                document.getElementById("cboEHPDoctor").selectedIndex = 0;
                document.getElementById("cboApptType").selectedIndex = 0;
                document.getElementById("cboApptSlot").selectedIndex = 0;
                document.getElementById("cboPatient").selectedIndex = 0;
                document.getElementById("txtPhone").text = "";
        };
    </script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
          //  alert(1);
            //var availableTags = [
            //    "ActionScript",
            //    "AppleScript",
            //    "Asp",
            //    "BASIC",
            //    "C",
            //    "C++",
            //    "Clojure",
            //    "COBOL",
            //    "ColdFusion",
            //    "Erlang",
            //    "Fortran",
            //    "Groovy",
            //    "Haskell",
            //    "Java",
            //    "JavaScript",
            //    "Lisp",
            //    "Perl",
            //    "PHP",
            //    "Python",
            //    "Ruby",
            //    "Scala",
            //    "Scheme"
            //];
            var cboEHPFacilityitem = $("#cboEHPFacility option:selected").val();
            var getPVId = new Array();
            getPVId = $("#cboLocation option:selected").val().split('^');
           
            //alert(cboEHPFacilityitem);
            //alert(typeof(cboEHPFacilityitem) == "undefined" ? "true" : "false");
           
             $("#cboPatient1").autocomplete({
                        //  source: availableTags
                 source: function (req, res) {
                     if (typeof (cboEHPFacilityitem) == "undefined") { }
                     else {
                         if (typeof (cboEHPFacilityitem) != "" || typeof (cboEHPFacilityitem) != "0") {
                             $("#hdnpId").val("");
                             $.ajax({
                                 url: "ScheduleApptWithCareCloud.aspx/GetPatientList", //+ you can add other filters here,
                                 type: "POST",
                                 data: "{ 'SearchParam': '" + req.term + "' ,'cboEHPFacilityitem':'" + cboEHPFacilityitem + "' , 'PVId':'" + getPVId[1] +"' }",
                                 dataType: "json",
                                 contentType: "application/json; charset=utf-8",
                                 success: function (data) {
                                     var items = [];
                                     //res($.map(data, function (item) {
                                     //    return {
                                     //        label: item.PatientName,
                                     //        value: item.PatientName,
                                     //        id: item.PID,
                                     //        costFactor: item.PID
                                     //    };
                                     //    items.push(item);
                                     //});
                                     $.each(data.d, function (key, val) {
                                         var item = {
                                             label: val.PatientName,
                                             value: val.PatientName,
                                             id: val.PID,
                                             costFactor: val.PhoneNumber
                                         };
                                         items.push(item);
                                     });
                                     source = items;
                                     res(items);

                                 }
                                 ,
                                 error: function (XMLHttpRequest, textStatus, errorThrown) {
                                     alert(mymessage);
                                 }
                             });
                         }
                     }
                           
                        },
                        minLength: 3,
                        delay: 500,
                 select: function (event, ui) {
                  //   alert(ui.item.id + " " + ui.item.value);
                     $("#hdnpId").val(ui.item.id);
                     //Filter only numbers from the input
                     let cleaned = ('' + ui.item.costFactor).replace(/\D/g, '');

                     //Check if the input is of correct length
                     let match = cleaned.match(/^(\d{3})(\d{3})(\d{4})$/);

                     if (match) {
                         // return '(' + match[1] + ') ' + match[2] + '-' + match[3]
                         $("#txtPhone").val(match[1] + '-' + match[2] + '-' + match[3]);
                         $("#Label2").text("Please verify the contact number. If different, please provide correct contact number in " + '"Reason for Visit"' + " section.");
                     } else {
                        // $("#txtPhone").val("");
                     }
                    // $("#txtPhone").val(ui.item.costFactor);
                    // $("#txtPhone").val();
                        }

             });
            $("#cboPatient1").focusout(function () {
                if ($("#hdnpId").val() == "" && $("#cboPatient1").val() != "") {
                    alert("Please Provide Patient Name or click New to create new Patient");
                    $("#cboPatient1").val("");
                    $("#txtPhone").val("");
                }
                if ($("#cboPatient1").val() == "") {
                    $("#hdnpId").val("");
                    $("#txtPhone").val("");
                }
            })
           
            // Add keypress event listener
            $("#txtReason").on('keypress', function (event) {

                // Get the key 
                let key = event.key;

                let regex = new RegExp("^[a-zA-Z0-9 ]+$");

                // Check if key is in the reg exp
                if (!regex.test(key)) {

                    // Restrict the special characters
                    event.preventDefault();
                    return false;
                }
            });

            $("#txtReason").bind('paste', function () {
                setTimeout(function () {
                    //get the value of the input text
                    var data = $('#txtReason').val();
                    //replace the special characters to '' 
                    var dataFull = data.replace(/[^\w\s]/gi, '');
                    //set the new value of the input text without special characters
                    $('#txtReason').val(dataFull);
                });

            });

         
            var $limitNum = 46;
            $("#txtReason").keyup(function () {
                var $this = $(this);

                //if ($this.val().length > $limitNum) {

                    $this.val($this.val().substring(0, $limitNum-1));
                  //  $this.val($this.val().substring(0, $limitNum));
               // }
            });
            $("#txtReason").focusout(function () {
                var $this = $(this);

                if ($this.val().length > $limitNum) {

                $this.val($this.val().substring(0, $limitNum - 1));
                //  $this.val($this.val().substring(0, $limitNum));
                 }
            });
            $("#btnBookAppt").click(function () {

                $("#Label1").html("Please wait, system is processing your request!");
            })
            //function preventEnterSubmit(event) {
            //    if (event.keyCode === 13) {
            //        event.preventDefault();
            //        return false;
            //    }
            //}

            $("#txtReason").keydown(function (event) {
                if (event.keyCode === 13) {
                    event.preventDefault();
                    return false;
                }
            })
            
        });
    </script>
</head>
<body>
    <header>
        <img src="img/MicrosoftTeams-image.png" alt="eye health partners/vision america portal" width="397" height="44">
        <h2 class="PageTitle">Schedule Appointment Wizard</h2>
    </header>
    <form id="form1" runat="server">
        <%--<uc1:Tasks runat="server" ID="Tasks1" />--%>

        <div>
            <label>Referring From  <i style="color:red">*</i>
                <asp:DropDownList runat="server" ID="cboLocation" CssClass="TextField" AutoPostBack="true" OnSelectedIndexChanged="cboLocation_SelectedIndexChanged" /></label>
            <label>Facility  <i style="color:red">*</i>
                <asp:DropDownList runat="server" ID="cboEHPFacility" CssClass="TextField" AutoPostBack="true" OnSelectedIndexChanged="cboEHPFacility_SelectedIndexChanged" /></label>
            <label>Doctor  <i style="color:red">*</i>
                <asp:DropDownList runat="server" ID="cboEHPDoctor" CssClass="TextField" AutoPostBack="true" OnSelectedIndexChanged="cboEHPDoctor_SelectedIndexChanged" /></label>
            <label>Appointment Type  <i style="color:red">*</i>
                <asp:DropDownList runat="server" ID="cboApptType" CssClass="TextField" AutoPostBack="true" OnSelectedIndexChanged="cboApptType_SelectedIndexChanged" /></label>
            <asp:Label ID="Label3" runat="server" Text="Appointment Slot"  ><%-- <asp:Label ID="Label3" runat="server" Text="" style="font-size:14px;" >--%></asp:Label>  <i style="color:red">*</i><%--</label>--%>
                <asp:DropDownList runat="server" ID="cboApptSlot" CssClass="TextField"  AutoPostBack="True" OnSelectedIndexChanged="cboApptSlot_SelectedIndexChanged" />
                <div class="input-links"><asp:LinkButton runat="server" ID="lbApptSlotMore" CssClass="LinkButton" Text="More" Visible="false" OnClick="lbApptSlotMore_Click" /></div>
           <%--<p>If you don’t find your desired appointment availability, please call our office to book an appointment.</p>--%>
            <label style="color:#322E95;">If you don’t find your desired appointment availability, please call our office to book an appointment.</label>
            <label>Patient  <i style="color:red">*</i></label> 

                <asp:TextBox ID="cboPatient1"  CssClass="TextField"  runat="server"  ></asp:TextBox>
                <asp:HiddenField runat="server" ID="hdnpId" />  
                <%--<asp:DropDownList runat="server" ID="cboPatient" CssClass="TextField" AutoPostBack="true" OnSelectedIndexChanged="cboPatient_SelectedIndexChanged" /></label>--%>
                <div class="input-links"><asp:LinkButton runat="server" ID="lbNewPatient" CssClass="LinkButton" Text="New" OnClick="lbNewPatient_Click" Visible="false" /></div>
            <label>Phone Number
                <asp:TextBox runat="server" ID="txtPhone" ReadOnly="true" CssClass="TextField" MaxLength="15"   /></label>
            <asp:Label ID="Label2" runat="server" style="font-size:small;color:#322E95;"   Text=""></asp:Label>
            <div class="loader-container" style="display:none">
             <div class="load">
            <div class="lds-dual-ring"></div>
            </div>
            </div>
            <label>Eye</label>
                <asp:RadioButton runat="server" ID="rbEyeLeft" Text="Left" Checked="false" AutoPostBack="true" OnCheckedChanged="rbEyeLeft_CheckedChanged" />
                <asp:RadioButton runat="server" ID="rbEyeRight" Text="Right" Checked="false" AutoPostBack="true" OnCheckedChanged="rbEyeRight_CheckedChanged" />
                <asp:RadioButton runat="server" ID="rbBoth" Text="Both" Checked="true" AutoPostBack="true" OnCheckedChanged="rbBoth_CheckedChanged" />
           
            <label>Reason for Visit  <i style="color:red">*</i>  
                <asp:TextBox runat="server" ID="txtReason" CssClass="TextField"  TextMode="MultiLine" Rows="4" Width="100%" MaxLength="45"  /></label>
            <label style="font-size:small;color:#322E95;">(Character Limit:45 | No special characters)</label>
            <div class="buttons">
                <asp:Button runat="server" ID="btnBookAppt" CssClass="Button" Text="Book Appointment" OnClick="btnBookAppt_Click" />
                <asp:Button runat="server" ID="btnCancel" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
            <%--<p>Please call our office if your practice location(s) has changed from the list above.</p>--%>
           <%--<p>If any change or update is required in your 'referring from' locations, please email us at RPPSupport@theseesgroup.com</p>--%>
            <label style="color:#322E95;">If any change or update is required in your 'referring from' locations, please email us at RPPSupport@theseesgroup.com</label>
                <br />
           <b> <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></b>
        </div>
        <div class="Error">
            <asp:Literal runat="server" ID="litError" />
        </div>
    </form>
    
</body>
</html>
<script>  
    $("#cboLocation").change(function () {
        $(".loader-container").show();
        setTimeout(function () {
            $(".loader-container").hide();
        }, 30000);
    })
    $("#cboEHPFacility").change(function () {
        $(".loader-container").show();
        setTimeout(function () {
            $(".loader-container").hide();
        }, 30000);
    })
    $("#cboEHPDoctor").change(function () {
        $(".loader-container").show();
        setTimeout(function () {
            $(".loader-container").hide();
        }, 30000);
    })
    $("#cboApptType").change(function () {
        $(".loader-container").show();
        setTimeout(function () {
            $(".loader-container").hide();
        }, 30000);
    })
    
    $("#btnBookAppt").click(function () {
        $(".loader-container").show();
        setTimeout(function () {
            $(".loader-container").hide();
        }, 30000);
    });

    $("#btnCancel").click(function () {
        $(".loader-container").show();
        setTimeout(function () {
            $(".loader-container").hide();
        }, 45000);
    });
</script> 