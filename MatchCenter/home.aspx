<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="MatchCenter.home" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>99footballmatches.com/</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <link rel="stylesheet" href="StyleSheet.css" />
     <script type = "text/javascript" 
         src = "http://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>

    <link href="CSS/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/four_button.js"></script>

     
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="formHome" runat="server">
        &nbsp;<!-- Container --><asp:ScriptManager ID="ScriptManagerHome" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />
         

        <div class="containerHome">
            <%--<asp:Label ID="LabelHidden" runat="server" Text="" CssClass="lbl-hid"></asp:Label>
            <asp:Label ID="LabelTest" runat="server" Text=""></asp:Label>--%>
            <a href="addHomeTeam" class="btn btn-success buttonRegister customBtn" role="button">Create match</a>
            <%-- <input   type="button" class="btn btn-info" value="Input Button"/> --%>
            
             <%--<asp:HyperLink NavigateUrl="~/addmatch.aspx" Text="Add Blog" runat="server" />
            <hr />--%>
            
            <asp:Button ID="btnDeleteMatch"  runat="server" class="btn btn-danger customBtn" Text="Delete selected" OnClick="btnDeleteMatch_Click" />
            <asp:Repeater ID="rptPages" runat="server">
                <HeaderTemplate>
                        <table id="tableMyMatches" class="table-scores table table-striped table-hover " role="grid" aria-describedby="example_info" style="width: 100%";>
                        <tr>
                        <th>#</th>
                        <th>Match</th>
                        <th>Result</th>
                        <th>Date</th>
                        <th></th>
                      
                        </tr>
                        </HeaderTemplate>

                        <ItemTemplate>
                        <tr>
                            <td>  <%# Container.ItemIndex + 1 %>  </td>
                            <td> <asp:HyperLink NavigateUrl='<%# string.Format("/matchreport/{0}/{1}", Eval("user_id"), Eval("match_id")) %>' Text='<%# Eval("name") %>' runat="server" /> </td>  
                            <td> <asp:Label ID="Label2" runat="server" Text='<%# Eval("final_result_home_team").ToString() + ":" + Eval("final_result_away_team").ToString() %>'></asp:Label> </td>
                                        <td> <asp:Label ID="Label1" runat="server" Text='<%# Eval("date", "{0:d}") %>' DataFormatString="{0:MM/dd/yy}"></asp:Label> </td>
                            <td> <input  data-rel="match" value="false"  name="checkboxDeleteMatch" type="checkbox" /></td>
                            <td  style="display:none;">  <input class="playerOut" name="lblMatch" type="text" size="2" value='<%# Eval("match_id") %>' /> </td>
                        </tr>
                        </ItemTemplate>

                       <%-- <AlternatingItemTemplate>
                            <tr >
                               <td>  <%# Container.ItemIndex + 1 %>  </td>
                               <td> <asp:HyperLink NavigateUrl='<%# string.Format("/matchreport/{0}/{1}.aspx", Eval("user_id"), Eval("match_id")) %>' Text='<%# Eval("name") %>'
                                runat="server" /> </td>  
                                <td> <input  data-rel="match" value="false" name="checkboxDeleteMatch"  type="checkbox" /></td>
                                 <td > <asp:Label name="lblMatch" runat="server" text='<%# Eval("match_id") %>'  value='<%# Eval("match_id") %>'></asp:Label></td>  
                            </tr>
                        </AlternatingItemTemplate>--%>

                        <FooterTemplate>
                        </table>
                        </FooterTemplate>
            </asp:Repeater>
</div>

          <script type='text/javascript'>
           
            //function RemoveTournament(id_tournament) {
            //    var id_member = $(".lbl-hid").html();
            //    PageMethods.RemoveTournamentBooking(id_tournament, id_member, Remove_Success, Remove_Fail);
            //}
               $(document).ready(function () {
                   $('#tableMyMatches').dataTable({
                       "sPaginationType": "four_button"
                   });
               });

               //ADD HIDDEN INPUT FOR CHECKBOX 

               var chk = $('input[name="checkboxDeleteMatch"]');
               chk.each(function () {
                   var v = $(this).attr('checked') == 'checked' ? 1 : 0;
                   $(this).after('<input type="hidden" name="' + $(this).attr('data-rel') + '" value="' + v + '" />');
               });

               chk.change(function () {
                   var v = $(this).is(':checked') ? 1 : 0;
                   $(this).next('input[type="hidden"]').val(v);
               });

        </script>
    </form>

</asp:Content>

