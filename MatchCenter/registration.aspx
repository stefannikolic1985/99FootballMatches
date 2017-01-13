<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="registration.aspx.cs" Inherits="MatchCenter.registration" %>

<%@ Register Assembly="GoogleReCaptcha" Namespace="GoogleReCaptcha" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Registration</title>
    <link rel="stylesheet" href="StyleSheet.css" />
    <script src="Scripts/jquery-1.7.1.min.js"></script>

          
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="formRegister" runat="server">
        <asp:ScriptManager ID="ScriptManagerRegister" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        <%--<asp:UpdatePanel ID="UpdatePanelRegister" hildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>--%>

                <%--<h1 class="register-h1">Sign up</h1>
                <div class="registerForm">

                    <label>Username: <span class="required">*</span></label> 
                    <asp:TextBox ID="txtUsername" required="required" class="form-control" runat="server" />
                    <div id="checkusernameoremail" runat="server">
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </div>
                    <br />

                    <label>Password: <span class="required">*</span></label>
                    <div class="input-group">
                        <asp:TextBox ID="txtPassword" runat="server" type="password" class="form-control pwd" required="required"/>
                        <span class="input-group-btn">
                            <button class="btn btn-default reveal" type="button"><i class="glyphicon glyphicon-eye-open"></i></button>
                        </span>
                    </div>

                    <label>E-mail: <span class="required">*</span></label>
                    <asp:TextBox ID="txtEmail" class="form-control" runat="server" required="required"/><br />

                    <asp:Button ID="ButtonRegister" class="btn btn-success btn-lg" runat="server" Text="Sign up" /><br />
                    <br />
                    <asp:Panel ID="PanelResponse" runat="server"  CssClass="alert alert-warning">
                        <asp:Label ID="LabelResponse" runat="server" Text=""></asp:Label>

                    </asp:Panel>
                </div>--%>

        <div class="card card-container">
                <div class="form-signin">
                    <span id="reauth-email" class="reauth-email"></span>
                    <asp:TextBox runat="server"   id="txtUsername" placeholder="Username" CssClass="form-control" required="required" autofocus="autofocus" /><br />
                   
                    <asp:TextBox ID="txtEmail" class="form-control" runat="server" placeholder="E-mail" required="required"/><br />

                    
                    <div class="input-group">
                        <asp:TextBox ID="txtPassword" runat="server" type="password" class="form-control pwd" required="required" placeholder="Password"/><br />
                        <span class="input-group-btn">
                            <button class="btn btn-default reveal" type="button"><i class="glyphicon glyphicon-eye-open"></i></button>
                        </span>
                    </div>
                    
                      <asp:Panel ID="PanelGoogleRecaptcha" runat="server"></asp:Panel>
                     
                    
                   


                     <asp:Panel ID="PanelResponse" runat="server"  CssClass="alert alert-warning">
                        <asp:Label ID="LabelResponse" runat="server" Text=""></asp:Label>
                    </asp:Panel>


                      <asp:Button ID="ButtonRegister" class="btn btn-lg btn-primary btn-block btn-signin" runat="server" Text="Sign up" /><br />
                    <br />

                    <%--<div class="form-group">
                                    <div class="noAccount">
                                        <div class="ttt">
                                           <p> Don't have an account?</p>
                                       <p> <a href="registration">
                                            Sign Up Here
                                        </a></p>
                                        </div>
                                    </div>
                                </div>--%>
                </div><!-- /form -->
            </div><!-- /card-container -->

         <%--   </ContentTemplate>
        </asp:UpdatePanel>--%>
        <%--</form>--%>
        <script type="text/javascript">

        <%--  //This function call on text change.    
            function UsernameAvailability() {
                $.ajax({
                    type: "POST",
                    url: "registration/CheckUsername", // this for calling the web method function in cs code.  
                    data: '{username: "' + $("#<%=txtUsername.ClientID%>")[0].value + '" }',// user name or email value  
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response);
                }
            });

        // function OnSuccess  
        function OnSuccess(response) {
            var msg = $("#<%=lblStatus.ClientID%>")[0];
            switch (response.d) {
                case "true":
                    msg.style.display = "block";
                    msg.style.color = "red";
                    msg.innerHTML = "User Name already exists.";
                    break;
                case "false":
                    msg.style.display = "block";
                    msg.style.color = "green";
                    msg.innerHTML = "User Name  Available";
                    break;
            }
        }--%>

        // Show/hide password 
        $(".reveal").on('click', function () {
            var $pwd = $(".pwd");
            if ($pwd.attr('type') === 'password') {
                $pwd.attr('type', 'text');
            } else {
                $pwd.attr('type', 'password');
            }
        });

        </script>
    </form>
</asp:Content>
