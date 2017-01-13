<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="403_login.aspx.cs" Inherits="MatchCenter._403_login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .localNavigation {
            display: none !important;
        }
        h1 {
            font-size: 3em;
        }
        .staff403 {
            width: 50%;
            margin-left: auto;
            margin-right: auto;
            padding: 5%;
            text-align: center;
        }
    </style>
    <form id="form1" runat="server">
        <div class="staff403 alert alert-danger">
            <h1>ERROR 403</h1>
            <h2>Du måste logga in för att få tillgång till denna sida!</h2>
            <span><a href="login">Logga in</a></span>
        </div>
    </form>
</asp:Content>
