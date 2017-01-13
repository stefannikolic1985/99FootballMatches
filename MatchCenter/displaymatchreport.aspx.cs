using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Npgsql;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

namespace MatchCenter
{
    public partial class displaymatchreport : System.Web.UI.Page
    {
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        public string currentId, matchId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.PopulateBlog();
                whoscored();
                PopulateTabelLineupHome();
                PopulateTabelLineuAway();


            }
        }

        private void whoscored()
        {
            //SELECT BlogId, Title, REPLACE(Title, ' ', '-') SLUG, Body FROM Blogs
            string query = @"SELECT DISTINCT g.player_name
                            FROM goal g

                            INNER JOIN matches m
                            ON g.match_id = m.match_id

                            INNER JOIN team_match tm
                            ON tm.match_id = m.match_id
                            AND tm.team_id = g.team_id
                            and tm.home_away = 'home'

                            WHERE g.match_id = '"+ matchId +"'";

      
                using (NpgsqlCommand cmd = new NpgsqlCommand(query))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            rptScoredHome.DataSource = dt;
                            rptScoredHome.DataBind();
                        }
                    }
                
                }

            string queryA = @"SELECT DISTINCT g.player_name 
                            FROM goal g

                            INNER JOIN matches m
                            ON g.match_id = m.match_id

                            INNER JOIN team_match tm
                            ON tm.match_id = m.match_id
                            AND tm.team_id = g.team_id
                            and tm.home_away = 'away'

                            WHERE g.match_id = '" + matchId + "'";


            using (NpgsqlCommand cmd = new NpgsqlCommand(queryA))
            {
                using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                {
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        rptScoredAway.DataSource = dt;
                        rptScoredAway.DataBind();
                    }
                }

            }

        }

        private void PopulateBlog()
        {
            HttpCookie myCookie = new HttpCookie("LoginCookie");
            myCookie = Request.Cookies["LoginCookie"];
            currentId = this.Page.RouteData.Values["user_id"].ToString();
            matchId = this.Page.RouteData.Values["match_id"].ToString();
            

            string query = "SELECT place, date, time, competition, attendance, referee,  final_result_home_team, final_result_away_team, half_time_result_home_team, half_time_result_away_team  FROM matches WHERE match_id = '" + matchId + "'";
            
                using (NpgsqlCommand cmd = new NpgsqlCommand(query))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                       // cmd.Parameters.AddWithValue("@match_id", matchId);
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;
                        using (DataTable dtHome = new DataTable())
                        {
                            sda.Fill(dtHome);
                            lblPlace.Text = dtHome.Rows[0]["place"].ToString();
                            lblDate.Text =  Convert.ToDateTime(dtHome.Rows[0]["date"]).ToString("d/MMMM");
                            lblTime.Text = Convert.ToDateTime(dtHome.Rows[0]["time"]).ToString("hh:mm");
                            lblCompetition.Text = dtHome.Rows[0]["competition"].ToString();
                            lblAttendance.Text = dtHome.Rows[0]["attendance"].ToString();
                            //lblReferee.Text = dtHome.Rows[0]["referee"].ToString();
                        if(!DBNull.Value.Equals(dtHome.Rows[0]["half_time_result_home_team"]) && !DBNull.Value.Equals(dtHome.Rows[0]["half_time_result_away_team"]))
                        {
                            lblHalfTimeResult.Text = "(" + dtHome.Rows[0]["half_time_result_home_team"].ToString() + ":" + dtHome.Rows[0]["half_time_result_away_team"].ToString() + ")";
                        }
                        lblFinalResult.Text =  dtHome.Rows[0]["final_result_home_team"].ToString() + ":" + dtHome.Rows[0]["final_result_away_team"].ToString();
                              }
                    }
                }

            string queryTeamName = @"SELECT t.name from team t
                                        inner join team_match tm
                                        on tm.team_id = t.team_id

                                        inner join matches m
                                        on m.match_id = tm.match_id

                                        where m.match_id = '"+ matchId + "'";

            using (NpgsqlCommand cmd = new NpgsqlCommand(queryTeamName))
            {
                using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                {
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;
                    using (DataTable dtHome = new DataTable())
                    {
                        sda.Fill(dtHome);

                        lblMatchHomeTeamName.Text = dtHome.Rows[0]["name"].ToString();
                        lblMatchAwayTeamName.Text = dtHome.Rows[1]["name"].ToString();
                    }
                }
            }
        }

     

        private void PopulateTabelLineupHome()
        {
            string home = "home";

            ////Populating a DataTable from database.
            Classes.Match clsMatch = new Classes.Match();
            DataTable dtHome = clsMatch.getLineup(matchId, home, "true");
            DataTable dtHomeReserves = clsMatch.getLineup(matchId, home, "false");

            dtHome.Columns["name"].Caption = "Player name";
            dtHome.Columns.Remove("player_id"); //remove column from database

            dtHomeReserves.Columns["name"].Caption = "Player name";
            dtHomeReserves.Columns.Remove("player_id"); //remove column from database

            if (dtHome.Rows.Count > 0)
            {
                //Building an HTML string.
                StringBuilder html = new StringBuilder();

                #region Table head
                //Table start.
                html.Append("<table class='tableTwoLineup tableLinups table table-striped' id='tableLineupHomeID'> ");
                
                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in dtHome.Columns)
                {
                    if (column.ColumnName.ToString() == "name")
                    {
                        html.Append("<th class='lineupTabelName'>");
                        html.Append(column.Caption);
                        html.Append("</th>");
                    }

                    if (column.ColumnName.ToString() == "goalkeeper")
                    {
                        html.Append("<th class='lineupTabelGoalkeeper'>");
                        html.Append("");
                        html.Append("</th>");
                    }

                    if (column.ColumnName.ToString() == "number")
                    {
                        html.Append("<th class='lineupTabelNumber'>");
                        html.Append("#");
                        html.Append("</th>");
                    }

                    if (column.ColumnName.ToString() == "goal")
                    {
                        html.Append("<th colspan = '7' class='lineupTabelActivity'>");
                        html.Append("Activity");
                    }

                    if (column.ColumnName.ToString() == "gameout")
                    {
                        html.Append("</th>");
                    }

                }
                html.Append("</tr>");

                #endregion End Table Head

                #region start11
                //Building the Data rows.
                foreach (DataRow row in dtHome.Rows)
                {
                    
                    //html.Append("<tr>");
                    foreach (DataColumn column in dtHome.Columns)
                    {
                        
                       
                        object valueStarTeam = row["start_team"];
                        if (Convert.ToBoolean(valueStarTeam) == true)
                        { 

                            if (column.ColumnName.ToString() != "start_team")
                            {
                                object valueGoal = row["goal"];
                                object valueGoalkeeper = row["goalkeeper"];
                                object valueAssist = row["assist"];
                                object valueYellow = row["yellow"];
                                object valueSecondYellow = row["second_yellow"];
                                object valueRed = row["red"];
                                object valueIn = row["gamein"];
                                object valueOut = row["gameout"];
                                object valueName = row["name"];

                                

                                if (column.ColumnName.ToString() == "goalkeeper")
                                {
                                    if (Convert.ToBoolean(valueGoalkeeper) == true)
                                    {
                                        html.Append("<td >");
                                        html.Append("<img src = '/Images/icon-goalkeeper.png' />");
                                        html.Append("</td>");
                                    }

                                    else
                                    {
                                        html.Append("<td>");
                                        html.Append("</td>");
                                    }

                                }

                                else
                                {
                                    html.Append("<td>");

                                    if (column.ColumnName.ToString() == "goal")
                                    {
                                        if (valueGoal != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-goal.png'   />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "assist")
                                    {
                                        if (valueAssist != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-assist.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "yellow")
                                    {
                                        if (valueYellow != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-yellow.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "second_yellow")
                                    {
                                        if (valueSecondYellow != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-second-yellow.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "red")
                                    {
                                        if (valueRed != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-red.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "gamein")
                                    {
                                        if (valueIn != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-in.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "gameout")
                                    {
                                        if (valueOut != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-out.png' />");
                                        }
                                    }

                                    
                                    html.Append(row[column.ColumnName]);

                                    if (column.ColumnName.ToString() == "name")
                                    {

                                        if (Convert.ToBoolean(valueGoalkeeper) == true)
                                        {

                                            html.Append("<img src = '/Images/icon-goalkeeper.png' class='iconGoalkeeper'/>");

                                        }

                                    }
                                    html.Append("</td>");
                                }
                            }
                         }
                       
                       
                    }
                    html.Append("</tr>");
                }
                #endregion End start11
                
                #region reservs


                //DataTable dtReservsHome = clsMatch.reservs(matchId, home);
                if (dtHomeReserves.Rows.Count > 0)
                {
                    //Building the Header row.
                    html.Append("<tr class='lineupTabelReservs'>");
                    foreach (DataColumn column in dtHomeReserves.Columns)
                    {


                        if (column.ColumnName.ToString() == "number")
                        {
                            html.Append("<th colspan = '10' class='lineupTabelReservs'>");
                            html.Append("Substitutes");
                        }


                        if (column.ColumnName.ToString() == "gameout")
                        {
                            html.Append("</th>");
                        }

                    }
                    html.Append("</tr>");

                }
                   

                //Building the Data rows.
                foreach (DataRow row in dtHomeReserves.Rows)
                {

                    html.Append("<tr>");
                    foreach (DataColumn column in dtHomeReserves.Columns)
                    {

                        
                        object valueStarTeam = row["start_team"];
                        if (Convert.ToBoolean(valueStarTeam) == false)
                        {

                            if (column.ColumnName.ToString() != "start_team")
                            {
                                object valueGoal = row["goal"];
                                object valueGoalkeeper = row["goalkeeper"];
                                object valueAssist = row["assist"];
                                object valueYellow = row["yellow"];
                                object valueSecondYellow = row["second_yellow"];
                                object valueRed = row["red"];
                                object valueIn = row["gamein"];
                                object valueOut = row["gameout"];



                                if (column.ColumnName.ToString() == "goalkeeper")
                                {
                                    if (Convert.ToBoolean(valueGoalkeeper) == true)
                                    {
                                        html.Append("<td>");
                                        html.Append("<img src = '/Images/icon-goalkeeper.png' />");
                                        html.Append("</td>");
                                    }

                                    else
                                    {
                                        html.Append("<td>");
                                        html.Append("</td>");
                                    }

                                }

                                else
                                {
                                    html.Append("<td>");

                                    if (column.ColumnName.ToString() == "goal")
                                    {
                                        if (valueGoal != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-goal.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "assist")
                                    {
                                        if (valueAssist != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-assist.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "yellow")
                                    {
                                        if (valueYellow != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-yellow.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "second_yellow")
                                    {
                                        if (valueSecondYellow != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-second-yellow.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "red")
                                    {
                                        if (valueRed != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-red.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "gamein")
                                    {
                                        if (valueIn != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-in.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "gameout")
                                    {
                                        if (valueOut != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-out.png' />");
                                        }
                                    }


                                    html.Append(row[column.ColumnName]);
                                    if (column.ColumnName.ToString() == "name")
                                    {

                                        if (Convert.ToBoolean(valueGoalkeeper) == true)
                                        {

                                            html.Append("<img src = '/Images/icon-goalkeeper.png' class='iconGoalkeeper'/>");

                                        }

                                    }
                                    html.Append("</td>");
                                }
                            }
                        }
                       

                    }
                    html.Append("</tr>");
                }
                #endregion End reserv
                
                //Table end.
                html.Append("</table>");
                //Append the HTML string to Placeholder.
                PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
            }
        }

        private void PopulateTabelLineuAway()
        {
            string  away = "away";

            ////Populating a DataTable from database.
            Classes.Match clsMatch = new Classes.Match();
            DataTable dtAway = clsMatch.getLineup(matchId, away, "true");
            DataTable dtAwayReserves = clsMatch.getLineup(matchId, "away", "false");

            dtAway.Columns["name"].Caption = "Player name";
            dtAway.Columns.Remove("player_id"); //remove column from database


            dtAwayReserves.Columns["name"].Caption = "Player name";
            dtAwayReserves.Columns.Remove("player_id"); //remove column from database

            if (dtAway.Rows.Count > 0)
            {
                //Building an HTML string.
                StringBuilder html = new StringBuilder();

                //Table start.
                //html.Append("<table id='tableLineupAwayID' class='tableTwoLineup  tableLinups table table-striped' > ");
                html.Append("<table id='tableLineupAwayID' class='tableTwoLineup  tableLinups table table-striped' > ");

                #region start11
                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in dtAway.Columns)
                {
                    if (column.ColumnName.ToString() == "name")
                    {
                        html.Append("<th class='lineupTabelName'>");
                        html.Append(column.Caption);
                        html.Append("</th>");
                    }

                    if (column.ColumnName.ToString() == "goalkeeper")
                    {
                        html.Append("<th class='lineupTabelGoalkeeper'>");
                        html.Append("");
                        html.Append("</th>");
                    }

                    if (column.ColumnName.ToString() == "number")
                    {
                        html.Append("<th class='lineupTabelNumber'>");
                        html.Append("#");
                        html.Append("</th>");
                    }

                    if (column.ColumnName.ToString() == "goal")
                    {
                        html.Append("<th colspan = '7' class='lineupTabelActivity' >");
                        html.Append("Activity");
                    }

                    if (column.ColumnName.ToString() == "gameout")
                    {
                        html.Append("</th>");
                    }

                }
                html.Append("</tr>");

                //Building the Data rows.
                foreach (DataRow row in dtAway.Rows)
                {

                    html.Append("<tr>");
                    foreach (DataColumn column in dtAway.Columns)
                    {

                      
                        object valueStarTeam = row["start_team"];
                        if (Convert.ToBoolean(valueStarTeam) == true)
                        {

                            if (column.ColumnName.ToString() != "start_team")
                            {
                                object valueGoal = row["goal"];
                                object valueGoalkeeper = row["goalkeeper"];
                                object valueAssist = row["assist"];
                                object valueYellow = row["yellow"];
                                object valueSecondYellow = row["second_yellow"];
                                object valueRed = row["red"];
                                object valueIn = row["gamein"];
                                object valueOut = row["gameout"];



                                if (column.ColumnName.ToString() == "goalkeeper")
                                {
                                    if (Convert.ToBoolean(valueGoalkeeper) == true)
                                    {
                                        html.Append("<td>");
                                        html.Append("<img src = '/Images/icon-goalkeeper.png' />");
                                        html.Append("</td>");
                                    }

                                    else
                                    {
                                        html.Append("<td>");
                                        html.Append("</td>");
                                    }

                                }

                                else
                                {
                                    html.Append("<td>");

                                    if (column.ColumnName.ToString() == "goal")
                                    {
                                        if (valueGoal != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-goal.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "assist")
                                    {
                                        if (valueAssist != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-assist.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "yellow")
                                    {
                                        if (valueYellow != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-yellow.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "second_yellow")
                                    {
                                        if (valueSecondYellow != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-second-yellow.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "red")
                                    {
                                        if (valueRed != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-red.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "gamein")
                                    {
                                        if (valueIn != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-in.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "gameout")
                                    {
                                        if (valueOut != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-out.png' />");
                                        }
                                    }


                                    html.Append(row[column.ColumnName]);

                                    if (column.ColumnName.ToString() == "name")
                                    {

                                        if (Convert.ToBoolean(valueGoalkeeper) == true)
                                        {

                                            html.Append("<img src = '/Images/icon-goalkeeper.png' class='iconGoalkeeper'/>");

                                        }

                                    }
                                    html.Append("</td>");
                                }
                            }
                        }
                    

                    }
                    html.Append("</tr>");
                }
                #endregion
                #region reservs

               

                if (dtAwayReserves.Rows.Count > 0)
                {
                    //Building the Header row.
                    html.Append("<tr class='lineupTabelReservs'>");
                    foreach (DataColumn column in dtAwayReserves.Columns)
                    {


                        if (column.ColumnName.ToString() == "number")
                        {
                            html.Append("<th colspan = '10' class='lineupTabelReservs'>");
                            html.Append("Substitutes");
                        }


                        if (column.ColumnName.ToString() == "gameout")
                        {
                            html.Append("</th>");
                        }

                    }
                    html.Append("</tr>");
                }

                //Building the Data rows.
                foreach (DataRow row in dtAwayReserves.Rows)
                {

                    html.Append("<tr>");
                    foreach (DataColumn column in dtAwayReserves.Columns)
                    {

                 
                        object valueStarTeam = row["start_team"];
                        if (Convert.ToBoolean(valueStarTeam) == false)
                        {

                            if (column.ColumnName.ToString() != "start_team")
                            {
                                object valueGoal = row["goal"];
                                object valueGoalkeeper = row["goalkeeper"];
                                object valueAssist = row["assist"];
                                object valueYellow = row["yellow"];
                                object valueSecondYellow = row["second_yellow"];
                                object valueRed = row["red"];
                                object valueIn = row["gamein"];
                                object valueOut = row["gameout"];



                                if (column.ColumnName.ToString() == "goalkeeper")
                                {
                                    if (Convert.ToBoolean(valueGoalkeeper) == true)
                                    {
                                        html.Append("<td>");
                                        html.Append("<img src = '/Images/icon-goalkeeper.png' />");
                                        html.Append("</td>");
                                    }

                                    else
                                    {
                                        html.Append("<td>");
                                        html.Append("</td>");
                                    }

                                }

                                else
                                {
                                    html.Append("<td>");

                                    if (column.ColumnName.ToString() == "goal")
                                    {
                                        if (valueGoal != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-goal.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "assist")
                                    {
                                        if (valueAssist != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-assist.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "yellow")
                                    {
                                        if (valueYellow != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-yellow.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "second_yellow")
                                    {
                                        if (valueSecondYellow != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-second-yellow.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "red")
                                    {
                                        if (valueRed != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-red.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "gamein")
                                    {
                                        if (valueIn != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-in.png' />");
                                        }
                                    }

                                    if (column.ColumnName.ToString() == "gameout")
                                    {
                                        if (valueOut != DBNull.Value)
                                        {
                                            html.Append("<img src = '/Images/icon-out.png' />");
                                        }
                                    }


                                    html.Append(row[column.ColumnName]);

                                    if (column.ColumnName.ToString() == "name")
                                    {

                                        if (Convert.ToBoolean(valueGoalkeeper) == true)
                                        {

                                            html.Append("<img src = '/Images/icon-goalkeeper.png' class='iconGoalkeeper'/>");

                                        }

                                    }
                                    html.Append("</td>");
                                }
                            }
                        }
                        

                    }
                    html.Append("</tr>");
                }
                #endregion

                //Table end.
                html.Append("</table>");
                //Append the HTML string to Placeholder.
                PlaceHolder2.Controls.Add(new Literal { Text = html.ToString() });
            }
        }
    }
}