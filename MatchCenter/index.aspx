<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="MatchCenter.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Login</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <link rel="stylesheet" href="StyleSheet.css" />

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <link href="http://fonts.googleapis.com/css?family=Didact+Gothic" rel="stylesheet" />
    <link href="default.css" rel="stylesheet" type="text/css" media="all" />
    <link href="fonts.css" rel="stylesheet" type="text/css" media="all" />

    <style type="text/css">
        #DivContainer {
            display: none;
        }

        .designLicenss
    {
        display: unset;
        
    }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">


    <form id="formLogin" runat="server">

        <div id="header-wrapper">
           
            <div id="banner-wrapper">
                <div id="banner" class="containerIndex">
                    <div class="title">
                        <h2>Let everybody know about your match</h2>
                        <span class="byline">Create a match report and share it with everyone</span>
                    </div>
                    <ul class="actions">
                        <li><a href="registration.aspx" class="button">GET STARTED</a></li>
                    </ul>
                </div>
            </div>

             <div id="header" class="containerIndex">
               <%-- <div id="logo">
                    <h1><a href="#">FirstBase</a></h1>
                </div>
                <div id="menu">
                    <ul>
                        <li class="active"><a href="#" accesskey="1" title="">Homepage</a></li>
                        <li><a href="#" accesskey="2" title="">Our Clients</a></li>
                        <li><a href="#" accesskey="3" title="">About Us</a></li>
                        <li><a href="#" accesskey="4" title="">Careers</a></li>
                        <li><a href="#" accesskey="5" title="">Contact Us</a></li>
                    </ul>
                </div>--%>
            </div>
        </div>
        <%--<div id="wrapper">
            <div id="three-column" class="containerIndex">

                <div class="title">
                    <h2>Feugiat lorem ipsum dolor sed veroeros</h2>
                    <span class="byline">Donec leo, vivamus fermentum nibh in augue praesent a lacus at urna congue</span>
                </div>
                <div class="boxA">
                    <p>Phasellus pellentesque, ante nec iaculis dapibus, eros justo auctor lectus, a lobortis lorem mauris quis nunc. Praesent pellentesque facilisis elit. Class aptent taciti sociosqu ad  torquent per conubia nostra.</p>
                    <a href="#" class="button button-alt">More Info</a>
                </div>
                <div class="boxB">
                    <p>Etiam neque. Vivamus consequat lorem at nisl. Nullam  wisi a sem semper eleifend. Donec mattis. Phasellus pellentesque, ante nec iaculis dapibus, eros justo auctor lectus, a lobortis lorem mauris quis nunc.</p>
                    <a href="#" class="button button-alt">More Info</a>
                </div>
                <div class="boxC">
                    <p>Aenean lectus lorem, imperdiet at, ultrices eget, ornare et, wisi. Pellentesque adipiscing purus. Phasellus pellentesque, ante nec iaculis dapibus, eros justo auctor lectus, a lobortis lorem mauris quis nunc.</p>
                    <a href="#" class="button button-alt">More Info</a>
                </div>
            </div>
        </div>--%>
        <div id="welcome">
            <div class="containerIndex">
                <div class="title">
                    <h2>Impress your fans with high performance match reports which, until now, had only the pro clubs.</h2>
                </div>
                <img src="images/screenshot.png" alt="" class="image image-full" />
                <p>With <strong>99footballmatches.com</strong> you can easily create a match report and save all information about your match so it can always be online. Your fans and 
                    players will finally be glad to see info about their favorite match and you will have a place to store all your matches</p>
                <ul class="actions">
                    <li><a href="registration.aspx" class="button">CREATE YOUR MATCH REPORT NOW </a></li>
                </ul>
            </div>
        </div>

         <script type='text/javascript'>
           
           
        </script>
    </form>

</asp:Content>
