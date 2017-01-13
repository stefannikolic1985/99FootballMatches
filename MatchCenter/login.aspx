<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="MatchCenter.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Login</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <link rel="stylesheet" href="StyleSheet.css" />
     <script type='text/javascript'>
           
        //function hideLabel() {
        //    document.getElementById('PanelResponse').hidden = true;
        //}

        function hidelabel() {
            document.getElementById('LabelResponse').style.visibility = "hidden";
        }

    </script>

     
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <form id="formLogin" runat="server">
<asp:ScriptManager ID="ScriptManagerLogin" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanelLogin" runat="server">
        <ContentTemplate>
            <%--<div class="loginForm">
                <label>E-mail: </label>
                <asp:TextBox runat="server" type="email" id="TextEmailLogin" placeholder="Email address" CssClass="form-control" required="required" autofocus="autofocus" /><br />
                <label>Password: </label>
                <asp:TextBox runat="server" id="TextPasswordLogin" CssClass="form-control" placeholder="Password"  TextMode="Password" required="required" /><br />
                <asp:Button id="ButtonLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-lg" OnClick="ButtonLogin_Click" /><br /><br />
                <asp:Panel ID="PanelResponse" runat="server" CssClass="alert alert-warning">
                    <asp:Label ID="LabelResponse" runat="server" Text="asd"></asp:Label>
                </asp:Panel>
           </div>--%>
           

          
            <div class="card card-container">
                <!-- <img class="profile-img-card" src="//lh3.googleusercontent.com/-6V8xOA6M7BA/AAAAAAAAAAI/AAAAAAAAAAA/rzlHcD0KYwo/photo.jpg?sz=120" alt="" /> -->
                <img id="profile-img" class="profile-img-card" src="//ssl.gstatic.com/accounts/ui/avatar_2x.png" />
                <p id="profile-name" class="profile-name-card"></p>
                <div class="form-signin">
                    <span id="reauth-email" class="reauth-email"></span>
                    <asp:TextBox runat="server" onblur="hidelabel();" onfocus="hidelabel();"  type="email"  id="TextEmailLogin" placeholder="Email address" CssClass="form-control" required="required" autofocus="autofocus" />
                    <asp:TextBox runat="server"   id="TextPasswordLogin" CssClass="form-control" placeholder="Password"  TextMode="Password" required="required" /><br />
              
                    
                     <asp:Panel ID="PanelResponse" runat="server" CssClass="alert alert-warning">
                    <asp:Label ID="LabelResponse" runat="server" Text="asd"></asp:Label> </asp:Panel>
                    <asp:Button id="ButtonLogin" runat="server" Text="Login" class="btn btn-lg btn-primary btn-block btn-signin"  OnClick="ButtonLogin_Click" /><br />
                  

                    <div class="form-group">
                                    <div class="noAccount">
                                        <div class="NoAccountLogin">
                                           <p> Don't have an account?</p>
                                       <p> <a href="registration">
                                            Sign Up Here
                                        </a></p>
                                        </div>
                                    </div>
                                </div>
                </div><!-- /form -->
            </div><!-- /card-container -->
       
        </ContentTemplate>
    </asp:UpdatePanel>
    
      
    
</form>
</asp:Content>

