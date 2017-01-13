<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="addMatchAwayTeam.aspx.cs" Inherits="MatchCenter.addMatchAwayTeam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Add match away</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <link rel="stylesheet" href="StyleSheet.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="formAddMatchAwayTeam" runat="server">
        <!-- Container -->
        <asp:ScriptManager ID="ScriptManagerAddMatchHomeTeam" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />

        <asp:UpdatePanel ID="UpdatePanelAddMatchAwayTeam" runat="server">
            <ContentTemplate>

                <div class=" containerAddMatchHomeTeam">

                    <%--Progress bar--%>
                    <div class="row bs-wizard" style="border-bottom: 0;">

                        <div class="col-xs-4 bs-wizard-step complete">
                            <div class="text-center bs-wizard-stepnum">Home team</div>
                            <div class="progress">
                                <div class="progress-bar"></div>
                            </div>
                            <a href="#" class="bs-wizard-dot"></a>
                        </div>

                        <div class="col-xs-4 bs-wizard-step active ">
                            <!-- complete -->
                            <div class="text-center bs-wizard-stepnum">Away team</div>
                            <div class="progress">
                                <div class="progress-bar"></div>
                            </div>
                            <a href="#" class="bs-wizard-dot"></a>
                        </div>

                        <div class="col-xs-4 bs-wizard-step disabled">
                            <!-- complete -->
                            <div class="text-center bs-wizard-stepnum">About match</div>
                            <div class="progress">
                                <div class="progress-bar"></div>
                            </div>
                            <a href="#" class="bs-wizard-dot"></a>
                        </div>
                    </div>


                    <%--HOME TEAM--%>
                    <div class="homeTeamName">
                        <label>Away team name: </label>
                        <br />
                        <asp:TextBox ID="txtAwayTeamName" required="required" runat="server" />  <span class="required">*</span>
                        <br />
                    </div>


                    <div class="start11 HomeTeam">
                        <label>Line up: </label>
                       


                        <div style="display: none" class="homeTeamList" id="divAddHomeTeam" >
                             <div  class="divLineupLables" id="containerHeadlines">
                                <div class="lineupLabel lineupLabelN"># </div>
                                <div class="lineupLabel lineupLabelStart11">Start 11</div>
                                <div class="lineupLabel ">Number </div>
                                <div class="lineupLabel lineupLabelName">Name </div>
                                <div class="lineupLabel lineupLabelGoalkeeper">Goalkeeper </div>
                                <div class="lineupLabel ">Goals </div>
                                <div class="lineupLabel lineupLabelAssists">Assists</div>
                                <div class="lineupLabel ">Yellow card </div>
                                <div class="lineupLabel ">Second yellow </div>
                                <div class="lineupLabel">Red card </div>
                                <div class="lineupLabel">In </div>
                                <div class="lineupLabel">Out </div>
                                <div class="lineupLabel"> </div>
                            </div>

                            <div id="TextBoxContainer" class="divLineupRows">

                            <%--Textboxes for HOMETEAM players will be added here --%>
                        </div>
                        </div>
                            <br />
                        <input id="btnAdd" type="button" value="Add" class="btn btn-sm btn-primary customBtn" />
                        <br />
                        <br />
                    </div>

                    <%-- End div start11 HomeTeam--%>
                    </div>
                
                    <asp:Button ID="btnAwayTeamNext" runat="server" Text="Next" CssClass="btn btn-primary btn-success btnNext customBtn" OnClick="btnAwayTeamNext_Click" /><br />

               
                <script type='text/javascript'>
                    $(document).ready(function () {
                        var valueNumber = eval('<%=ValueNumbers%>');
             var valueName = eval('<%=ValueNames%>');
             var valueGoal = eval('<%=ValueGoals%>');
             var valueAssist = eval('<%=ValueAssists%>');
             var valueYellow = eval('<%=ValueYellows%>');
             var valueSecondYellow = eval('<%=ValueSecondYellows%>');
             var valueRed = eval('<%=ValueReds%>');
             var valueIn = eval('<%=ValueIn%>');
             var valueOut = eval('<%=ValueOut%>');

             if (valueName != null) {
                 var l = valueName.length;
                 var html = "";
                 for (var i = 0; i < l; i++) {

                     var div = $("<div />");
                     div.html(GetDynamicTextBox(valueNumber[i], valueName[i], valueGoal[i], valueAssist[i], valueYellow[i], valueSecondYellow[i], valueRed[i], valueIn[i], valueOut[i]));
                     $("#TextBoxContainer").append(div);
                 }
             }

             //ADD ROW NUMBERS 
             function rownumber() {
                 $("#divAddHomeTeam").hide();
                 var a = 0;
                 var n = $('label[class="players"]');
                 n.each(function () {
                     a += 1;
                     this.innerHTML = a;
                     $("#divAddHomeTeam").show();
                 });
             };

             //ADD PLAYER FOR AWAYTEAM
             $("#btnAdd").bind("click", function () {
                 var lastID = $('#TextBoxContainer').children().last().attr('id');
                 var div = $("<div />");
                 div.html(GetDynamicTextBox('', '', '', '', '', '', '', '', ''));
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
                 $(this).closest('div[class="rowPlayerHometeam"]').remove();
                 rownumber();
             });

             //ADD HIDDEN INPUT FOR EACH CHECKBOX DynamicYesNoStart11
             $("#btnAdd").live("click", function () {
                 var chk = $('input[name="DynamicYesNoStart11"]').last();
                 chk.each(function () {
                     var v = $(this).attr('checked') == 'checked' ? 1 : 0;
                     $(this).after('<input type="hidden" name="' + $(this).attr('data-rel') + '" value="' + v + '" />');
                 });

                 //ADD VALUE FOR HIDDEN INPUT 
                 chk.change(function () {
                     var v = $(this).is(':checked') ? 1 : 0;
                     $(this).next('input[type="hidden"]').val(v);
                 })
             });

             //ADD HIDDEN INPUT FOR EACH CHECKBOX DynamicGoalkeeper
             $("#btnAdd").live("click", function () {

                 rownumber();
                 var chk = $('input[name="DynamicGoalkeeper"]').last();
                 chk.each(function () {
                     var v = $(this).attr('checked') == 'checked' ? 1 : 0;
                     $(this).after('<input type="hidden" name="' + $(this).attr('data-rel') + '" value="' + v + '" />');
                 });

                 //ADD VALUE FOR HIDDEN INPUT 
                 chk.change(function () {
                     var v = $(this).is(':checked') ? 1 : 0;
                     $(this).next('input[type="hidden"]').val(v);
                 })
             });

             //DEFINE TEXTBOXES IN THE ROW
             function GetDynamicTextBox(valueNumber, valueName, valueGoal, valueAssist, valueYellow, valueSecondYellow, valueRed, valueIn, valueOut) {
             //    return '<label class="players"></label>&nbsp'
             //    + '<label class="switch"><input  data-rel="start11" value="false" name="DynamicYesNoStart11"  class="switch-input" type="checkbox" /><span class="switch-label" data-on="Yes" data-off="No"></span> <span class="switch-handle"></span></label>&nbsp'
             //    + '<input class="playerNumber" name="DynamicTextBoxNumber" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" type="text" size="2" value="' + valueNumber + '" />&nbsp'
             //    + '<input class="playerName" name="DynamicTextBoxName"  type="text" required="required" value = "' + valueName + '" />&nbsp'
             //    + '<label class="switch"><input  data-rel="goalkeeper" value="false" name="DynamicGoalkeeper" class="switch-input" type="checkbox" /><span class="switch-label" data-on="Yes" data-off="No"></span> <span class="switch-handle"></span></label>&nbsp'
             //    + '<input class="playerGoal" name="DynamicTextBoxGoal" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" type="text" size="2" value="' + valueGoal + '" />&nbsp'
             //    + '<input class="playerAssist" name="DynamicTextBoxAssist" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" type="text" size="2" value="' + valueAssist + '" />&nbsp'
             //    + '<input class="playerYellow" name="DynamicTextBoxYellow" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" type="text" size="2" value="' + valueYellow + '" />&nbsp'
             //    + '<input class="playerSecondYellow" name="DynamicTextBoxSecondYellow" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" type="text" size="2" value="' + valueSecondYellow + '" />&nbsp'
             //    + '<input class="playerRed" name="DynamicTextBoxRed" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" type="text" size="2" value="' + valueRed + '" />&nbsp'
             //    + '<input class="playerIn" name="DynamicTextBoxIn" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" type="text" size="2" value="' + valueIn + '" />&nbsp'
             //    + '<input class="playerOut" name="DynamicTextBoxOut" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" type="text" size="2" value="' + valueOut + '" />&nbsp'
             //    + '<input type="button" value="Remove" class="remove" />'
                 //}
                 return '<div class="lineupLabel lineupLabelN lineupLabelNnotL lineupLabeltTest"> <label class="players"></label></div>&nbsp'
                   + '<div class="lineupLabel lineupLabelStart11"> <label class="switch"><input  data-rel="start11" value="false" name="DynamicYesNoStart11" class="switch-input" type="checkbox" /><span class="switch-label" data-on="Yes" data-off="No"></span> <span class="switch-handle"></span></label></div>&nbsp'
                   + '<div class="lineupLabel lineupLabeltTest"> <input class="playerNumber numInput " name="DynamicTextBoxNumber"  type="text"  onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" value="' + valueNumber + '" /></div>&nbsp'
                   + '<div class="lineupLabel lineupLabelName lineupLabeltTest"> <input class="playerName " name="DynamicTextBoxName" type="text" required="required" value = "' + valueName + '" /></div>&nbsp'
                   + '<div class="lineupLabel lineupLabelGoalkeeper"> <label class="switch"><input  data-rel="goalkeeper" value="false" name="DynamicGoalkeeper" class="switch-input" type="checkbox" /><span class="switch-label" data-on="Yes" data-off="No"></span> <span class="switch-handle"></span></label></div>&nbsp'
                   + '<div class="lineupLabel lineupLabeltTest"> <input class="playerGoal numInput " name="DynamicTextBoxGoal" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" type="text"  value="' + valueGoal + '" /><span>total</span></div>&nbsp'
                   + '<div class="lineupLabel lineupLabelAssists lineupLabeltTest"> <input class="playerAssist  numInput " name="DynamicTextBoxAssist" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" type="text"  value="' + valueAssist + '" /><span>total</span></div>&nbsp'
                   + '<div class="lineupLabel lineupLabeltTest"> <input class="playerYellow numInput " name="DynamicTextBoxYellow" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" type="text"  value="' + valueYellow + '" /><span>min.</span></div>&nbsp'
                   + '<div class="lineupLabel lineupLabeltTest"> <input class="playerSecondYellow numInput " name="DynamicTextBoxSecondYellow" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" type="text"  value="' + valueSecondYellow + '" /><span>min.</span></div>&nbsp'
                   + '<div class="lineupLabel lineupLabeltTest"> <input class="playerRed numInput " name="DynamicTextBoxRed" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" type="text"  value="' + valueRed + '" /><span>min.</span></div>&nbsp'
                   + '<div class="lineupLabel lineupLabeltTest"> <input class="playerIn numInput " name="DynamicTextBoxIn" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" type="text" " value="' + valueIn + '" /> <span>min.</span></div>&nbsp'
                   + '<div class="lineupLabel lineupLabeltTest"> <input class="playerOut numInput " name="DynamicTextBoxOut" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" type="text"  value="' + valueOut + '" /><span>min.</span></div>&nbsp'
                   //+ '<div class="lineupLabel"> <a href="#" class="remove btn btn-xs btn-danger" ><span class="glyphicon glyphicon-remove"></span> Remove</a></div>'
                   + '<div class="lineupLabel"> <input type="button" value="Remove" class="remove btn btn-xs btn-danger" /></div>'
             }
         });

                </script>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>

