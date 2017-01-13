using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Web.Script.Serialization;

namespace MatchCenter
{
     
    public partial class addMatchAboutMatch : System.Web.UI.Page
    {
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        public string currentId;
        public int home_team_id,
                   away_team_id,
                   match_id;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        

        protected void btnAboutMatch_Click(object sender, EventArgs e)
        {

            //System.Threading.Thread.Sleep(3000);
            addMatchInfo();
            AddHomeTeam();
            AddAwayTeam();
            addTeamsIntoTheMatch();
            whoScored();
            //Response.Redirect("home.aspx");
            Response.Redirect("matchreport/"+ currentId + "/" + match_id);
        }

        #region Add match Info
        private void addMatchInfo()
        {
            string date = txtDate.Text,
                   time = txtTime.Text;
            string teamNameHome = (string)(Session["teamNameHome"]);
            string teamNameAway = (string)(Session["teamNameAway"]);
            string matchName = teamNameHome + " - " + teamNameAway;
            Classes.Match match = new Classes.Match();

            DateTime hDate, hTime;
            if (DateTime.TryParse(date, out hDate))
            {
                match.date = hDate;
            }



            if (DateTime.TryParse(time, out hTime))
            {
                match.time = hTime;
            }

            
            int? attendance = null;
            if (string.IsNullOrWhiteSpace(txtAttendance.Text))
                attendance = null;
            else
            {
                int t;
                attendance = Int32.TryParse(txtAttendance.Text, out t) ? (int?)t : null;
            }

            int? halfTimeHome = null;
            if (string.IsNullOrWhiteSpace(txtHalftimeScoreHome.Text))
                halfTimeHome = null;
            else
            {
                int t;
                halfTimeHome = Int32.TryParse(txtHalftimeScoreHome.Text, out t) ? (int?)t : null;
            }

            int? halfTimeAway = null;
            if (string.IsNullOrWhiteSpace(txtHalftimeScoreAway.Text))
                halfTimeAway = null;
            else
            {
                int t;
                halfTimeAway = Int32.TryParse(txtHalftimeScoreAway.Text, out t) ? (int?)t : null;
            }


            match.final_result_Home_team = Convert.ToInt16(finalScoreHomeTeam.Text);
            match.final_result_Away_team = Convert.ToInt16(finalScoreAwayTeam.Text);
            match.half_time_result_Home_team = halfTimeHome;
            match.half_time_result_Away_team = halfTimeAway;
            match.place = String.IsNullOrWhiteSpace(txtPlace.Text) ? null : txtPlace.Text;
            match.competition = String.IsNullOrWhiteSpace(txtCompetition.Text) ? null : txtCompetition.Text;
            match.attendance = attendance;
            //match.referee = String.IsNullOrWhiteSpace(txtReferee.Text) ? null : txtReferee.Text;
            match.name = matchName;

            myCookie = Request.Cookies["LoginCookie"];
            currentId = myCookie["_id"].ToString();

            string query = "INSERT INTO matches (final_result_home_team, final_result_away_team, user_id, name, place, date, time, competition, attendance, referee, half_time_result_home_team, half_time_result_away_team) VALUES (@final_result_home_team, @final_result_away_team, @user_id, @name, @place, @date, @time, @competition, @attendance, @referee, @half_time_result_home_team, @half_time_result_away_team) returning match_id";
            
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@final_result_home_team", match.final_result_Home_team);
                cmd.Parameters.AddWithValue("@final_result_away_team", match.final_result_Away_team);
                cmd.Parameters.AddWithValue("@half_time_result_home_team", match.half_time_result_Home_team);
                cmd.Parameters.AddWithValue("@half_time_result_away_team", match.half_time_result_Away_team);
                cmd.Parameters.AddWithValue("@user_id", Convert.ToInt16(currentId));
                cmd.Parameters.AddWithValue("@name", match.name);
                cmd.Parameters.AddWithValue("@date", match.date);
                cmd.Parameters.AddWithValue("@time", match.time);
                cmd.Parameters.AddWithValue("@place", match.place);
                cmd.Parameters.AddWithValue("@competition", match.competition);
                cmd.Parameters.AddWithValue("@attendance", match.attendance);
                cmd.Parameters.AddWithValue("@referee", match.referee);
                conn.Open();
                match_id = (int)cmd.ExecuteScalar(); 
                conn.Close();
            }
        }
        #endregion
        #region Add line up Away team
        private void AddHomeTeam()
        {
            myCookie = Request.Cookies["LoginCookie"];
            currentId = myCookie["_id"].ToString();

            List<Classes.Player> PlayerListHome = (List<Classes.Player>)Session["lineupHome"];
            string teamNameHome = (string)(Session["teamNameHome"]);
            //ADD HOME TEAM
            string queryTeamName = "INSERT INTO team (name, user_id) VALUES (@name, @user_id) returning team_id ";
            using (NpgsqlCommand cmd = new NpgsqlCommand(queryTeamName, conn))
            {
                cmd.Parameters.AddWithValue("@name", teamNameHome);
                cmd.Parameters.AddWithValue("@user_id", Convert.ToInt16(currentId));
                conn.Open();
                home_team_id = (int)cmd.ExecuteScalar();
                conn.Close();
            }

            
            if ((List<Classes.Player>)Session["lineupHome"] != null)
            {
                //ADD LINEUP FOR HOME TEAM
                foreach (Classes.Player ph in PlayerListHome)
                {
                    int player_id;
                    string queryLineup = "INSERT INTO player (name, team_id) VALUES (@name, @team_id) returning player_id ";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(queryLineup, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", ph.name);
                        cmd.Parameters.AddWithValue("@team_id", home_team_id);
                        conn.Open();
                        player_id = (int)cmd.ExecuteScalar();
                        conn.Close();
                    }

                    string queryPlayerMatch = "INSERT INTO player_match (player_id, match_id, number, start_team, goalkeeper) VALUES (@player_id, @match_id, @number, @start_team, @goalkeeper)";
                    int? number;
                    if(ph.number == 0)
                    {
                        number = null;
                    }
                    else
                    {
                        number = ph.number;
                    }

                    
                    using (NpgsqlCommand cmd = new NpgsqlCommand(queryPlayerMatch, conn))
                    {
                        cmd.Parameters.AddWithValue("@player_id", player_id);
                        cmd.Parameters.AddWithValue("@match_id", match_id);
                        cmd.Parameters.AddWithValue("@number", number); 
                        cmd.Parameters.AddWithValue("@start_team", ph.start11);
                        cmd.Parameters.AddWithValue("@goalkeeper", ph.goalkeeper);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    if (ph.goal != 0)
                    {
                        string queryGoal = "INSERT INTO player_match_activity (player_id, activity_id, match_id, time) VALUES (@player_id, @activity_id, @match_id, @time) ";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryGoal, conn))
                        {
                            cmd.Parameters.AddWithValue("@player_id", player_id);
                            cmd.Parameters.AddWithValue("@activity_id", "1");
                            cmd.Parameters.AddWithValue("@time", ph.goal);
                            cmd.Parameters.AddWithValue("@match_id", match_id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    if (ph.assist != 0)
                    {
                        string queryAssist = "INSERT INTO player_match_activity (player_id, activity_id, match_id, time) VALUES (@player_id, @activity_id, @match_id, @time) ";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryAssist, conn))
                        {
                            cmd.Parameters.AddWithValue("@player_id", player_id);
                            cmd.Parameters.AddWithValue("@activity_id", "2");
                            cmd.Parameters.AddWithValue("@time", ph.assist);
                            cmd.Parameters.AddWithValue("@match_id", match_id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    if (ph.yellow != 0)
                    {
                        string queryYellow = "INSERT INTO player_match_activity (player_id, activity_id, match_id, time) VALUES (@player_id, @activity_id, @match_id, @time) ";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryYellow, conn))
                        {
                            cmd.Parameters.AddWithValue("@player_id", player_id);
                            cmd.Parameters.AddWithValue("@activity_id", "3");
                            cmd.Parameters.AddWithValue("@time", ph.yellow);
                            cmd.Parameters.AddWithValue("@match_id", match_id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    if (ph.secondYellow != 0)
                    {
                        string querySecondYellow = "INSERT INTO player_match_activity (player_id, activity_id, match_id, time) VALUES (@player_id, @activity_id, @match_id, @time) ";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(querySecondYellow, conn))
                        {
                            cmd.Parameters.AddWithValue("@player_id", player_id);
                            cmd.Parameters.AddWithValue("@activity_id", "4");
                            cmd.Parameters.AddWithValue("@time", ph.secondYellow);
                            cmd.Parameters.AddWithValue("@match_id", match_id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    if (ph.red != 0)
                    {
                        string queryRed = "INSERT INTO player_match_activity (player_id, activity_id, match_id, time) VALUES (@player_id, @activity_id, @match_id, @time) ";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryRed, conn))
                        {
                            cmd.Parameters.AddWithValue("@player_id", player_id);
                            cmd.Parameters.AddWithValue("@activity_id", "5");
                            cmd.Parameters.AddWithValue("@time", ph.red);
                            cmd.Parameters.AddWithValue("@match_id", match_id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    if (ph.inGame != 0)
                    {
                        string queryIn = "INSERT INTO player_match_activity (player_id, activity_id, match_id, time) VALUES (@player_id, @activity_id, @match_id, @time) ";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryIn, conn))
                        {
                            cmd.Parameters.AddWithValue("@player_id", player_id);
                            cmd.Parameters.AddWithValue("@activity_id", "6");
                            cmd.Parameters.AddWithValue("@time", ph.inGame);
                            cmd.Parameters.AddWithValue("@match_id", match_id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    if (ph.outGame != 0)
                    {
                        string queryOut = "INSERT INTO player_match_activity (player_id, activity_id, match_id, time) VALUES (@player_id, @activity_id, @match_id, @time) ";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryOut, conn))
                        {
                            cmd.Parameters.AddWithValue("@player_id", player_id);
                            cmd.Parameters.AddWithValue("@activity_id", "7");
                            cmd.Parameters.AddWithValue("@time", ph.outGame);
                            cmd.Parameters.AddWithValue("@match_id", match_id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                }
                Session.Remove("lineupHome");
            }
        }
        #endregion
        #region Add line up Away team
        private void AddAwayTeam()
        {
            List<Classes.Player> PlayerListAway = (List<Classes.Player>)Session["lineupAway"];
            string teamNameAway = (string)(Session["teamNameAway"]);
            //ADD AWAY TEAM
            string queryTeamName = "INSERT INTO team (name, user_id) VALUES (@name, @user_id) returning team_id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(queryTeamName, conn))
            {
                cmd.Parameters.AddWithValue("@name", teamNameAway);
                cmd.Parameters.AddWithValue("@user_id", Convert.ToInt16(currentId));
                conn.Open();
                away_team_id = (int)cmd.ExecuteScalar();
                conn.Close();
            }

            
            if ((List<Classes.Player>)Session["lineupAway"] != null)
            {
                //ADD LINEUP FOR AWAY TEAM
                foreach (Classes.Player ph in PlayerListAway)
                {
                    int player_id;
                    string queryLineup = "INSERT INTO player (name, team_id) VALUES (@name, @team_id) returning player_id ";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(queryLineup, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", ph.name);
                        cmd.Parameters.AddWithValue("@team_id", away_team_id);
                        conn.Open();
                        player_id = (int)cmd.ExecuteScalar();
                        conn.Close();
                    }

                    string queryPlayerMatch = "INSERT INTO player_match (player_id, match_id, number, start_team, goalkeeper) VALUES (@player_id, @match_id, @number, @start_team, @goalkeeper)";
                    int? number;
                    if (ph.number == 0)
                    {
                        number = null;
                    }
                    else
                    {
                        number = ph.number;
                    }

                    using (NpgsqlCommand cmd = new NpgsqlCommand(queryPlayerMatch, conn))
                    {
                        cmd.Parameters.AddWithValue("@player_id", player_id);
                        cmd.Parameters.AddWithValue("@match_id", match_id);
                        cmd.Parameters.AddWithValue("@number", number);
                        cmd.Parameters.AddWithValue("@start_team", ph.start11);
                        cmd.Parameters.AddWithValue("@goalkeeper", ph.goalkeeper);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    if (ph.goal != 0)
                    {
                        string queryGoal = "INSERT INTO player_match_activity (player_id, activity_id, match_id, time) VALUES (@player_id, @activity_id, @match_id, @time) ";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryGoal, conn))
                        {
                            cmd.Parameters.AddWithValue("@player_id", player_id);
                            cmd.Parameters.AddWithValue("@activity_id", "1");
                            cmd.Parameters.AddWithValue("@time", ph.goal);
                            cmd.Parameters.AddWithValue("@match_id", match_id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    if (ph.assist != 0)
                    {
                        string queryAssist = "INSERT INTO player_match_activity (player_id, activity_id, match_id, time) VALUES (@player_id, @activity_id, @match_id, @time) ";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryAssist, conn))
                        {
                            cmd.Parameters.AddWithValue("@player_id", player_id);
                            cmd.Parameters.AddWithValue("@activity_id", "2");
                            cmd.Parameters.AddWithValue("@time", ph.assist);
                            cmd.Parameters.AddWithValue("@match_id", match_id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    if (ph.yellow != 0)
                    {
                        string queryYellow = "INSERT INTO player_match_activity (player_id, activity_id, match_id, time) VALUES (@player_id, @activity_id, @match_id, @time) ";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryYellow, conn))
                        {
                            cmd.Parameters.AddWithValue("@player_id", player_id);
                            cmd.Parameters.AddWithValue("@activity_id", "3");
                            cmd.Parameters.AddWithValue("@time", ph.yellow);
                            cmd.Parameters.AddWithValue("@match_id", match_id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    if (ph.secondYellow != 0)
                    {
                        string querySecondYellow = "INSERT INTO player_match_activity (player_id, activity_id, match_id, time) VALUES (@player_id, @activity_id, @match_id, @time) ";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(querySecondYellow, conn))
                        {
                            cmd.Parameters.AddWithValue("@player_id", player_id);
                            cmd.Parameters.AddWithValue("@activity_id", "4");
                            cmd.Parameters.AddWithValue("@time", ph.secondYellow);
                            cmd.Parameters.AddWithValue("@match_id", match_id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    if (ph.red != 0)
                    {
                        string queryRed = "INSERT INTO player_match_activity (player_id, activity_id, match_id, time) VALUES (@player_id, @activity_id, @match_id, @time) ";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryRed, conn))
                        {
                            cmd.Parameters.AddWithValue("@player_id", player_id);
                            cmd.Parameters.AddWithValue("@activity_id", "5");
                            cmd.Parameters.AddWithValue("@time", ph.red);
                            cmd.Parameters.AddWithValue("@match_id", match_id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    if (ph.inGame != 0)
                    {
                        string queryIn = "INSERT INTO player_match_activity (player_id, activity_id, match_id, time) VALUES (@player_id, @activity_id, @match_id, @time) ";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryIn, conn))
                        {
                            cmd.Parameters.AddWithValue("@player_id", player_id);
                            cmd.Parameters.AddWithValue("@activity_id", "6");
                            cmd.Parameters.AddWithValue("@time", ph.inGame);
                            cmd.Parameters.AddWithValue("@match_id", match_id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    if (ph.outGame != 0)
                    {
                        string queryOut = "INSERT INTO player_match_activity (player_id, activity_id, match_id, time) VALUES (@player_id, @activity_id, @match_id, @time) ";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryOut, conn))
                        {
                            cmd.Parameters.AddWithValue("@player_id", player_id);
                            cmd.Parameters.AddWithValue("@activity_id", "7");
                            cmd.Parameters.AddWithValue("@time", ph.outGame);
                            cmd.Parameters.AddWithValue("@match_id", match_id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }


                }
                Session.Remove("lineupAway");
            }
        }
        #endregion
        #region Add team into the match
        private void addTeamsIntoTheMatch()
        {
            string queryH = "INSERT INTO team_match (team_id, match_id, home_away) VALUES (@team_id, @match_id, @home_away)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(queryH, conn))
            {
                cmd.Parameters.AddWithValue("@team_id", home_team_id);
                cmd.Parameters.AddWithValue("@match_id", match_id);
                cmd.Parameters.AddWithValue("@home_away", "home");
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            string queryA = "INSERT INTO team_match (team_id, match_id, home_away) VALUES (@team_id, @match_id,  @home_away)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(queryA, conn))
            {
                cmd.Parameters.AddWithValue("@team_id", away_team_id);
                cmd.Parameters.AddWithValue("@match_id", match_id);
                cmd.Parameters.AddWithValue("@home_away", "away");
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion
        #region  Who scored
        protected string ValueNames, ValueNamesAway;
        private void whoScored()
        {
            string[] textboxNames = Request.Form.GetValues("DynamicTextBoxName");
            
            if(textboxNames != null)
            {
                foreach (string name in textboxNames)
                {
                    string queryScore = "INSERT INTO goal (team_id, match_id, player_name) VALUES (@team_id, @match_id, @player_name)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(queryScore, conn))
                    {
                        cmd.Parameters.AddWithValue("@team_id", home_team_id);
                        cmd.Parameters.AddWithValue("@match_id", match_id);
                        cmd.Parameters.AddWithValue("@player_name", name);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }

            string[] textboxNamesAway = Request.Form.GetValues("DynamicTextBoxNameAway");
            if (textboxNamesAway != null)
            {
                foreach (string name in textboxNamesAway)
                {
                    string queryScore = "INSERT INTO goal (team_id, match_id, player_name) VALUES (@team_id, @match_id, @player_name)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(queryScore, conn))
                    {
                        cmd.Parameters.AddWithValue("@team_id", away_team_id);
                        cmd.Parameters.AddWithValue("@match_id", match_id);
                        cmd.Parameters.AddWithValue("@player_name", name);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }
        #endregion
        //public void addMatchname()
        //{
        //    myCookie = Request.Cookies["LoginCookie"];
        //    currentId = myCookie["_id"].ToString();

        //    string query = "INSERT INTO matches (final_result_home_team, final_result_away_team, user_id, place, date, time, competition, attendance, referee, half_time_result_home_team, half_time_result_away_team) VALUES (@final_result_home_team, @final_result_away_team, @user_id, @place, @date, @time, @competition, @attendance, @referee, @half_time_result_home_team, @half_time_result_away_team) returning match_id";

        //    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
        //    {
        //        cmd.Parameters.AddWithValue("@final_result_home_team", match.final_result_Home_team);
        //        cmd.Parameters.AddWithValue("@final_result_away_team", match.final_result_Away_team);
        //        cmd.Parameters.AddWithValue("@half_time_result_home_team", match.half_time_result_Home_team);
        //        cmd.Parameters.AddWithValue("@half_time_result_away_team", match.half_time_result_Away_team);
        //        cmd.Parameters.AddWithValue("@user_id", Convert.ToInt16(currentId));
        //        cmd.Parameters.AddWithValue("@date", match.date);
        //        cmd.Parameters.AddWithValue("@time", match.time);
        //        cmd.Parameters.AddWithValue("@place", match.place);
        //        cmd.Parameters.AddWithValue("@competition", match.competition);
        //        cmd.Parameters.AddWithValue("@attendance", match.attendance);
        //        cmd.Parameters.AddWithValue("@referee", match.referee);
        //        conn.Open();
        //        match_id = (int)cmd.ExecuteScalar();
        //        conn.Close();
        //    }
        //}
    }
}