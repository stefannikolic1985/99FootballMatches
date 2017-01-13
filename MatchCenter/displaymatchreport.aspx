<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="displaymatchreport.aspx.cs"  Inherits="MatchCenter.displaymatchreport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Displaymatchreport</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <%--<link rel="stylesheet" href="StyleSheet.css" />--%>
  
    <script type="text/javascript">var switchTo5x = true;</script>
    <script type="text/javascript" src="http://w.sharethis.com/button/buttons.js"></script>
    <script type="text/javascript">stLight.options({ publisher: "cae75a19-5fe0-4766-9a0f-593998350ed9", doNotHash: false, doNotCopy: false, hashAddressBar: false });</script>
    
      <style type="text/css">
          .container {
              /*background-color: rgba(29, 26, 26, 0.80);*/

             background-color: rgba(0, 0, 0, 0.36);
          }
      </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="formDisplaymatchreport" runat="server">
        <!-- Container -->
        <asp:ScriptManager ID="ScriptManagerDisplaymatchreport" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />
        

        <div class="containerDisplaymatchreport">

            <div class="matchDiv1">


                <div class="matchInfo">
                   <%-- <span class="glyphicon  glyphicon-flag"></span>--%>
                    <img class="iconMatchreport" src="/Images/icon-trophy-b.png" />    <asp:Label ID="lblCompetition" runat="server" class="matchInfoLabels" />

                    <span class="glyphicon glyphicon-calendar"></span>
                    <asp:Label ID="lblDate" runat="server" class="matchInfoLabels" />

                    <span class="glyphicon glyphicon-time"></span>
                    <asp:Label ID="lblTime" runat="server" class="matchInfoLabels" />

                    <span class="glyphicon glyphicon-map-marker "></span>
                    <asp:Label ID="lblPlace" runat="server" class="matchInfoLabels" />

                  <%-- <span class="glyphicon glyphicon-eye-open">  </span>--%>
                    <img class="iconMatchreport" src="/Images/icon-fans-b.png" />  <asp:Label ID="lblAttendance" runat="server" class="matchInfoLabels" />
                    <%--<asp:Label ID="lblReferee" runat="server" />--%>
                </div>

                <div class="matchHeadline">
                    <div class="matchTeamNames matchHometeamName">
                        <asp:Label ID="lblMatchHomeTeamName" runat="server" />
                    </div>
                    <div class="matchFinalResult">
                       
                            <asp:Label ID="lblFinalResult" runat="server" />
                        
                    </div>
                    <div class="matchTeamNames matchAwayteamName">
                        <asp:Label ID="lblMatchAwayTeamName" runat="server"  />
                    </div>
                </div>


                <div class="whoScoredList">
                    <div class=" whoScoredListHome">
                        <asp:Repeater ID="rptScoredHome" runat="server">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("player_name") %>'
                                    runat="server" />
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <br />
                            </SeparatorTemplate>
                        </asp:Repeater>
                    </div>

                    <div class="matchHalfTimeResult">
                        <p>HT</p>
                        <asp:Label ID="lblHalfTimeResult" runat="server" />

                    </div>

                    <div class=" whoScoredListAway">
                        <asp:Repeater ID="rptScoredAway" runat="server">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("player_name") %>'
                                    runat="server" />
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <br />
                            </SeparatorTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>

            
              

            <div class="MatchLineuppAll">
                <%-- <asp:Image ID="Image1" runat="server" />--%>
                <div class="tableLineupDiv tableLineupDivHome">
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
                </div>
                <div class="tableLineupDiv tableLineupDivAway">
                    <asp:PlaceHolder ID="PlaceHolder2" runat="server" />
                </div>
            </div>

            <div class="socialWidgetButtons">
                <span class='st_sharethis_large swBtn' data-displaytext='ShareThis'></span>
                <span class='st_facebook_large swBtn' data-displaytext='Facebook'></span>
                <span class='st_twitter_large swBtn' data-displaytext='Tweet'></span>
                <span class='st_linkedin_large swBtn' data-displaytext='LinkedIn'></span>
                <span class='st_pinterest_large swBtn' data-displaytext='Pinterest'></span>
                <%--   <span class='st_email_large' data-displaytext='Email'></span>--%>
            </div>
        </div>

        <script type='text/javascript'>

            //function RemoveTournament(id_tournament) {
            //    var id_member = $(".lbl-hid").html();
            //    PageMethods.RemoveTournamentBooking(id_tournament, id_member, Remove_Success, Remove_Fail);
            //}

            $(document).ready(function () {
                //ADD ROW NUMBERS 
                function rownumber() {
                    
                    var numItems = $('.tableLinups').length;
                   

                    if (numItems == 1)
                    {
                      

                        jQuery('.tableLineupDiv').addClass('divTableLinupOne');
                    }

                    if (numItems == 2) {
                       
                        jQuery('.tableLineupDiv').addClass('divTableLinupTwo');
                        jQuery('.tableLineupDivAway').addClass('divTableLinupAway');
                        jQuery('.tableLineupDivHome').addClass('divTableLinupHome');
                    }
                   
                };

                rownumber();
            });

            

        </script>
    </form>
</asp:Content>
