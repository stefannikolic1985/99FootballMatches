<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="addMatchAboutMatch.aspx.cs" Inherits="MatchCenter.addMatchAboutMatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Add match</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />

    <%--<script src="Scripts/jquery-ui-timepicker-addon.js"></script>--%>
    <script src="Scripts/global.js"></script>
    <link href="StyleSheet.css" rel="stylesheet" />

    <script type="text/javascript2" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
     

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="formAddMatchAboutMatch" runat="server">
        <!-- Container -->
        <asp:ScriptManager ID="ScriptManagerAddMatchAboutMatc" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />

        <asp:UpdatePanel ID="UpdatePaneAddMatchAboutMatc" runat="server">
            <ContentTemplate>

                <div class="containeraddMatchHomeTeam">
                    <%--Progress bar--%>
                    <div class="row bs-wizard" style="border-bottom: 0;">

                        <div class="col-xs-4 bs-wizard-step complete">
                            <div class="text-center bs-wizard-stepnum">Home team</div>
                            <div class="progress">
                                <div class="progress-bar"></div>
                            </div>
                            <a href="#" class="bs-wizard-dot"></a>
                        </div>

                        <div class="col-xs-4 bs-wizard-step complete">
                            <!-- complete -->
                            <div class="text-center bs-wizard-stepnum">Away team</div>
                            <div class="progress">
                                <div class="progress-bar"></div>
                            </div>
                            <a href="#" class="bs-wizard-dot"></a>
                        </div>

                        <div class="col-xs-4 bs-wizard-step active">
                            <!-- complete -->
                            <div class="text-center bs-wizard-stepnum">About match</div>
                            <div class="progress">
                                <div class="progress-bar"></div>
                            </div>
                            <a href="#" class="bs-wizard-dot"></a>
                        </div>
                    </div>

                    <div class="aboutMatch">
                        <div class="aboutMatchRow">
                            <div class="aboutMatchLabel">
                                <label>Place: </label>
                            </div>

                            <div class="aboutMatchTextbox">
                                <asp:TextBox ID="txtPlace" runat="server" class="form-control-admin" />
                            </div>
                        </div>

                        <div class="aboutMatchRow">
                            <div class="aboutMatchLabel">
                                <label>Date: </label>
                            </div>
                            <div class="aboutMatchTextbox">
                                <asp:TextBox ID="txtDate" onblur="Toggle()" class="form-control-admin" required="required" runat="server" />
                            <span class="required">*</span>
                            </div>
                        </div>

                        <div class="aboutMatchRow">
                            <div class="aboutMatchLabel">
                                <label>Time: </label>
                            </div>
                            <div class="aboutMatchTextbox">
                                <asp:TextBox ID="txtTime" onblur="Toggle()" class="form-control-admin" required="required" runat="server" />
                            <span class="required">*</span>
                            </div>
                        </div>

                        <div class="aboutMatchRow">
                            <div class="aboutMatchLabel">
                                <label>Attendance: </label>
                            </div>
                            <div class="aboutMatchTextbox">
                                <asp:TextBox ID="txtAttendance" runat="server" class="form-control-admin" onkeypress="CheckNumeric(event);" />
                            </div>
                        </div>

                        <div class="aboutMatchRow">
                            <div class="aboutMatchLabel">
                                <label>Competition: </label>
                            </div>
                            <div class="aboutMatchTextbox">
                                <asp:TextBox ID="txtCompetition" placeholder="Liga, cup, friendly match..." class="form-control-admin" runat="server" />
                            </div>
                        </div>

                        <%--<div class="aboutMatchRow">
                            <div class="aboutMatchLabel">
                                <label>Referee: </label>
                            </div>
                            <div class="aboutMatchTextbox">
                                <asp:TextBox ID="txtReferee" class="form-control-admin" runat="server" />
                            </div>
                        </div>--%>

                        <div class="aboutMatchRow">
                            <div class="aboutMatchLabel">
                                <label>Final result: </label>
                            </div>
                            <div class="aboutMatchTextbox">
                                <asp:TextBox ID="finalScoreHomeTeam" class="form-control-admin little" onblur="Toggle()" onkeypress="CheckNumeric(event);" runat="server" />
                                
                                <asp:TextBox ID="finalScoreAwayTeam" class="form-control-admin little" onblur="Toggle()" onkeypress="CheckNumeric(event);" runat="server" />
                           <span class="required">*</span>
                                 </div>
                        </div>

                        <div class="aboutMatchRow">
                            <div class="aboutMatchLabel">
                                <label>Half-time result: </label>
                            </div>
                            <div class="aboutMatchTextbox">
                                <asp:TextBox ID="txtHalftimeScoreHome" class="form-control-admin little" runat="server" onkeypress="CheckNumeric(event);" />

                                <asp:TextBox ID="txtHalftimeScoreAway" class="form-control-admin little" runat="server" onkeypress="CheckNumeric(event);" />
                            </div>
                        </div>
                    </div>
                    <%--End of  AboutMatch--%>



                    <div class="whoScored">
                        <%--WHO SCORED FOR HOME TEAM --%>
                        <div class="whoScoredTeamContainer">
                            <label>Who scored for home team? </label>
                            <br />
                            <input id="btnAdd" type="button" value="Add player" class="btn btn-sm btn-primary customBtn" />
                            <br />
                            <div id="TextBoxContainer" class="whoScoredContainer">
                                <%--Textboxes for HOMETEAM players will be added here --%>
                            </div>
                        </div>

                        <%--WHO SCORED FOR AWAY TEAM --%>
                        <div class="whoScoredTeamContainer">
                            <label>Who scored for away team?</label>
                            <br />
                            <input id="btnAddAway" type="button" value="Add player" class="btn btn-sm btn-primary customBtn" />

                            <br />
                            <div id="TextBoxContainerAway" class="whoScoredContainer">
                                <%--Textboxes for HOMETEAM players will be added here --%>
                            </div>
                        </div>
                    </div>




                    <%-- <asp:Button ID="btnAboutMatch" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnAboutMatch_Click" UseSubmitBehavior="False" OnClientClick="Disable(this);" /><br />--%>

                    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
                    <%--  <ContentTemplate>--%>
                    <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                    <div id="divImage" class="loading-image" style="display: none">
                        <asp:Image ID="img1" runat="server" ImageUrl="/images/football-ball-processing.gif" />
                        Processing...
                    </div>
                    <br />
                    <%--<asp:Button ID="btnAboutMatch" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnAboutMatch_Click" UseSubmitBehavior="False" OnClientClick="Disable(this);" /><br />
                    --%>
                    <asp:Button ID="btnAboutMatch" runat="server" Text="Finish" CssClass="btn btn-primary btn-success customBtn" Enabled="false" OnClick="btnAboutMatch_Click" /><br />
                    <%--disabled="disabled" --%>
                    <%--   </ContentTemplate>--%>
                    <%--  </asp:UpdatePanel>--%>
                </div>

                <script type='text/javascript'>


                    function CheckNumeric(e) {

                        if (window.event) // IE 
                        {
                            if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8) {
                                event.returnValue = false;
                                return false;

                            }
                        }
                        else { // Fire Fox
                            if ((e.which < 48 || e.which > 57) & e.which != 8) {
                                e.preventDefault();
                                return false;

                            }
                        }
                    }
                    //////////*********ENABLE/DISSABLE BUTTON*********///////////

                    function Toggle() {
                        var txt1 = document.getElementById("<%=finalScoreHomeTeam.ClientID %>");
                        var txt2 = document.getElementById("<%=finalScoreAwayTeam.ClientID %>");
                        var txt3 = document.getElementById("<%=txtDate.ClientID %>");
                        var txt4 = document.getElementById("<%=txtTime.ClientID %>");
                        var btn = document.getElementById("<%=btnAboutMatch.ClientID %>");
                        if (txt1.value == "" || txt2.value == "" || txt3.value == "" || txt4.value == "") {
                            btn.disabled = true;
                        }
                        else {
                            btn.disabled = false;
                        }
                    }

                    // Get the instance of PageRequestManager.
                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                    // Add initializeRequest and endRequest
                    prm.add_initializeRequest(prm_InitializeRequest);
                    prm.add_endRequest(prm_EndRequest);

                    // Called when async postback begins
                    function prm_InitializeRequest(sender, args) {
                        // get the divImage and set it to visible
                        var panelProg = $get('divImage');
                        panelProg.style.display = '';
                        // reset label text
                        var lbl = $get('<%= this.lblText.ClientID %>');
                        lbl.innerHTML = '';

                        // Disable button that caused a postback
                        $get(args._postBackElement.id).disabled = true;
                    }

                    // Called when async postback ends
                    function prm_EndRequest(sender, args) {
                        // get the divImage and hide it again
                        var panelProg = $get('divImage');
                        panelProg.style.display = 'none';

                        // Enable button that caused a postback
                        $get(sender._postBackSettings.sourceElement.id).disabled = false;
                    }
                    //$(document).on('submit', 'form', function (e) {
                    //    $("#btnAboutMatch").hide();
                    //    // or change if to an loading gif image.
                    //    return true;
                    //});

                    //DISABLE BUTTON

                    function Disable(x) {
                        x.disabled = true;
                    }


                    var ticks = {
                        create: function (tp_inst, obj, unit, val, min, max, step) {
                            $('<input class="ui-timepicker-input" value="' + val + '" style="width:50%">')
                                .appendTo(obj)
                                .spinner({
                                    min: min,
                                    max: max,
                                    step: step,
                                    change: function (e, ui) { // key events
                                        // don't call if api was used and not key press
                                        if (e.originalEvent !== undefined)
                                            tp_inst._onTimeChange();
                                        tp_inst._onSelectHandler();
                                    },
                                    spin: function (e, ui) { // spin events
                                        tp_inst.control.value(tp_inst, obj, unit, ui.value);
                                        tp_inst._onTimeChange();
                                        tp_inst._onSelectHandler();
                                    }
                                });
                            return obj;
                        },
                        options: function (tp_inst, obj, unit, opts, val) {
                            if (typeof (opts) == 'string' && val !== undefined)
                                return obj.find('.ui-timepicker-input').spinner(opts, val);
                            return obj.find('.ui-timepicker-input').spinner(opts);
                        },
                        value: function (tp_inst, obj, unit, val) {
                            if (val !== undefined)
                                return obj.find('.ui-timepicker-input').spinner('value', val);
                            return obj.find('.ui-timepicker-input').spinner('value');
                        }
                    };



                    $(function () {
                        $('#ContentPlaceHolder1_txtDate').datetimepicker({
                            altField: "#ContentPlaceHolder1_txtTime",
                            dateFormat: 'yy-mm-dd',
                            timeFormat: 'HH:mm',
                            timeOnlyTitle: 'Choose start time',
                            timeText: 'Time: ',
                            hourText: 'Hour: ',
                            minuteText: 'Minute: ',
                            currentText: 'Now',
                            closeText: 'OK',
                        });
                    });



                    //////////////////******HOME TEAM*******////////////////////////
                    $(function () {
                        var valueName = eval('<%=ValueNames%>');

                        if (valueName != null) {
                            var l = valueName.length;
                            var html = "";
                            for (var i = 0; i < l; i++) {

                                var div = $("<div />");
                                div.html(GetDynamicTextBox(valueName[i]));
                                $("#TextBoxContainer").append(div);
                            }
                        }
                        //ADD PLAYER FOR HOME TEAM
                        $("#btnAdd").bind("click", function () {
                            var lastID = $('#TextBoxContainer').children().last().attr('id');
                            var div = $("<div />");
                            div.html(GetDynamicTextBox(''));
                            $("#TextBoxContainer").append(div);
                            $(div).addClass('rowPlayerHometeam');
                            $(div).attr('name', 'rowNamePlayerHometeam');
                            $(div).attr('runat', 'server');

                            var num = $('.rowPlayerHometeam').length;
                            if (num <= 1) {
                                $(div).attr('id', '0');

                            }
                            else {

                                var nr = parseInt(lastID);
                                var nextID = nr + 1;
                                $(div).attr('id', nextID);
                            }
                        });


                        //REMOVE ROW
                        $("body").on("click", ".remove", function () {
                            $(this).closest("div").remove();
                        });
                    });

                        //DEFINE TEXTBOXES IN THE ROW
                        function GetDynamicTextBox(valueName) {
                            return '<input class="playerName whoScoredTextBox" placeholder="Name + minutes" name="DynamicTextBoxName" type="text" required="required" value = "' + valueName + '" />&nbsp'
                            + '<input type="button" value="Remove" class="whoScoredBtn remove btn btn-xs btn-danger" />'
                        }



                        //////////////////******AWAY TEAM*******////////////////////////
                        $(function () {
                            var valueNameAway = eval('<%=ValueNamesAway%>');

                            if (valueNameAway != null) {
                                var l = valueNameAway.length;
                                var html = "";
                                for (var i = 0; i < l; i++) {

                                    var div = $("<div />");
                                    div.html(GetDynamicTextBoxAway(valueNameAway[i]));
                                    $("#TextBoxContainerAway").append(div);
                                }
                            }
                            //ADD PLAYER FOR AWAY TEAM
                            $("#btnAddAway").bind("click", function () {
                                var div = $("<div />");
                                div.html(GetDynamicTextBoxAway(''));
                                $("#TextBoxContainerAway").append(div);
                                $(div).addClass('rowPlayerHometeam');
                            });


                            //REMOVE ROW
                            $("body").on("click", ".removeAway", function () {
                                $(this).closest("div").remove();
                            });
                        });

                            //DEFINE TEXTBOXES IN THE ROW
                            function GetDynamicTextBoxAway(valueNameAway) {
                                return '<input class="playerName whoScoredTextBox" placeholder="Name + minutes" name="DynamicTextBoxNameAway" type="text" required="required"  value = "' + valueNameAway + '" />&nbsp'
                                + '<input type="button" value="Remove" class="removeAway whoScoredBtn remove btn btn-xs btn-danger" />'
                            }
                </script>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>

