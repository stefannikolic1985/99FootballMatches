using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatchCenter.Classes
{
    public class User : System.Web.UI.Page
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }


        public void CreateSession()
        {
            Session["_id"] = this.id;
            Session["_logged"] = "1";
            Session["_username"] = this.username;
        }
        
        public HttpCookie CreateCookie()
        {
            HttpCookie loginCookie = new HttpCookie("LoginCookie");
            loginCookie.Expires = DateTime.Now.AddDays(-1d);

            loginCookie["_id"] = this.id.ToString();
            loginCookie["_logged"] = "1";
            loginCookie["_username"] = this.username;
            //loginCookie.Expires = DateTime.Now.AddDays(2d);
            loginCookie.Expires = DateTime.MaxValue;


            return loginCookie;
        }

        public void DestroySession()
        {
            Session.Abandon();
        }

        public override string ToString()
        {
            return username + " " + email + " (" + id + ")";
        }
    }
}