using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;

namespace MatchCenter.Classes
{
    public class Match
    {
        public string place { get; set; }
        public string competition { get; set; }
        public int? attendance { get; set; }
        public DateTime? date { get; set; }
        public DateTime? time { get; set; }
        public string referee { get; set; }
        public string name { get; set; }
        public int final_result_Home_team { get; set; }
        public int final_result_Away_team { get; set; }
        public int? half_time_result_Home_team { get; set; }
        public int? half_time_result_Away_team { get; set; }

        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
       // public string currentId, matchId;

        public DataTable getLineup(string matchId, string home_away, string trueOrNot)
        {
            DataTable dt = new DataTable();
            //string query = "SELECT place, competition FROM matches WHERE match_id = '" + matchId + "'";
            string query = @"SELECT p.player_id, pm.start_team, pm.number, pm.goalkeeper, p.name, MAX (CASE WHEN pma.activity_id = '1' THEN pma.time END) AS  goal
                                            , MAX (CASE WHEN pma.activity_id = '2' THEN pma.time END) AS  assist
                                            , MAX (CASE WHEN pma.activity_id = '3' THEN pma.time END) AS  yellow
                                            , MAX (CASE WHEN pma.activity_id = '4' THEN pma.time END) AS  second_yellow
                                            , MAX (CASE WHEN pma.activity_id = '5' THEN pma.time END) AS  red
                                            , MAX (CASE WHEN pma.activity_id = '6' THEN pma.time END) AS  GameIn
                                            , MAX (CASE WHEN pma.activity_id = '7' THEN pma.time END) AS  GameOut
                            FROM player p
                            INNER JOIN player_match pm
                                ON p.player_id = pm.player_id
                            INNER JOIN matches m
                                ON m.match_id = pm.match_id
                            INNER JOIN team_match tm
                                ON tm.team_id = p.team_id 
                            LEFT JOIN player_match_activity pma
                                ON pma.player_id = p.player_id
                                AND pma.activity_id IN ('1','2','3','4','5','6','7')
                                AND pma.match_id = m.match_id    
                            WHERE m.match_id = '" + matchId+ @"'
                                AND tm.home_away = '"+home_away+ @"'
                                AND pm.start_team = '" + trueOrNot + @"'
                            
                            GROUP BY p.player_id, pm.start_team, pm.number, pm.goalkeeper, p.name
                            ORDER BY pm.start_team  DESC, pm.goalkeeper DESC, pm.number ASC";
            
            using (NpgsqlCommand cmd = new NpgsqlCommand(query))
            {
                using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                {
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                    return dt;
                    
                }
            }
        }

       

        public DataTable whoScored(string matchId, string home_away)
        {
            DataTable dt = new DataTable();
            //string query = "SELECT place, competition FROM matches WHERE match_id = '" + matchId + "'";
            string query = @"SELECT DISTINCT g.player_name
                            FROM goal g

                            INNER JOIN matches m
                            ON g.match_id = m.match_id

                            INNER JOIN team_match tm
                            ON tm.match_id = m.match_id
                            AND tm.team_id = g.team_id
                            and tm.home_away = '"+ home_away+ @"'

                            WHERE g.match_id = '"+ matchId +"'";

            using (NpgsqlCommand cmd = new NpgsqlCommand(query))
            {
                using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                {
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                    return dt;

                }
            }
        }

        
    }
}