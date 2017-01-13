using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;

namespace MatchCenter
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        static void RegisterRoutes(RouteCollection routes)
        {
            //routes.MapPageRoute("displaymatchreport", "matchreport/{user_id}/{match_id}/{slug}.aspx", "~/displaymatchreport.aspx");
            routes.MapPageRoute("displaymatchreport", "matchreport/{user_id}/{match_id}", "~/displaymatchreport.aspx");
            routes.MapPageRoute("index", "", "~/index.aspx");
            routes.MapPageRoute("addMatchHomeTeam", "addHomeTeam", "~/addMatchHomeTeam.aspx");
            routes.MapPageRoute("addMatchAwayTeam", "addAwayTeam", "~/addMatchAwayTeam.aspx");
            routes.MapPageRoute("addMatchAboutMatch", "addAboutMatch", "~/addMatchAboutMatch.aspx");
            routes.MapPageRoute("home", "home", "~/home.aspx");
            routes.MapPageRoute("login", "login", "~/login.aspx");
            routes.MapPageRoute("registration", "registration", "~/registration.aspx");
        }
    }
}