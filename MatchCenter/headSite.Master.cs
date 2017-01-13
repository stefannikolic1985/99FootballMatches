using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace MatchCenter
{
    public partial class headSite : System.Web.UI.MasterPage
    {

        HttpCookie myCookie = new HttpCookie("LoginCookie");
        protected void Page_Load(object sender, EventArgs e)
        {
            myCookie = Request.Cookies["LoginCookie"];

            string pageName = Request.Url.Segments.Last();
            HtmlGenericControl divJS = new HtmlGenericControl();
            divJS.Attributes["class"] = "hiddenPage";
            divJS.TagName = "div";
            divJS.InnerHtml = pageName;

            if (Request.Cookies["LoginCookie"] != null)
            {
                // Check if session variable _logged is 1
                if (myCookie["_logged"].ToString() == "1")
                {
                    LoginLogout.Text = "<span class='glyphicon glyphicon-log-out'></span> Logout";
                    LoginLogout.Attributes["class"] = "logoutClick";
                    LoginCreate.Text = "<span class='glyphicon glyphicon-folder-open'></span>  My matches";
                    LoginCreate.Attributes["href"] = "/home";
                    LabelName.Text =  myCookie["_username"].ToString();
                    

                }
            }
            
            
            else
            {
                LoginLogout.Text = "<span class='glyphicon glyphicon-log-in'></span> Login";
                //LoginLogout.Attributes["href"] = "login.aspx";
                LoginLogout.Attributes["href"] = ResolveUrl("~/login");
                if (pageName == "home")
                {
                    Response.Redirect("403_login.aspx");
                }

            }

            //PanelHidden.Controls.Add(divJS);
            //LoginCreate.Visible = true;
            //LoginCreate.Text = "<span class='glyphicon glyphicon-user'></span> Bli medlem";
            //LoginCreate.Attributes["href"] = "register.aspx";
        }
    }
}