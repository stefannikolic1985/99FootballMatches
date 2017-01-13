using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Web.Security;
using System.Data;
using System.Configuration;
using System.Web.Script.Serialization;

namespace MatchCenter
{
    public partial class home : System.Web.UI.Page
    {
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.IsPostBack)
            {
                this.PopulateBlogs();
            }
        }

        private void PopulateBlogs()
        {
            HttpCookie myCookie = new HttpCookie("LoginCookie");
            myCookie = Request.Cookies["LoginCookie"];
            string currentId = myCookie["_id"].ToString();

            string query = "SELECT user_id, match_id, name, REPLACE(' ', '-', name) slug, competition, date, final_result_home_team, final_result_away_team  FROM matches WHERE user_id='"+ currentId + "' order by date DESC, time DESC";
           
                using (NpgsqlCommand cmd = new NpgsqlCommand(query))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                        if(dt.Rows.Count > 0)
                        {
                            rptPages.DataSource = dt;
                            rptPages.DataBind();
                        }
                            
                        else
                        {
                            rptPages.Visible = false;
                        }
                        }
                    }
                }
        }

        protected void btnDeleteMatch_Click(object sender, EventArgs e)
        {
            string[] textboxCheckboxes = Request.Form.GetValues("match");
            string[] textboxLblMatchID = Request.Form.GetValues("lblMatch");
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            for (int i = 0; i < textboxCheckboxes.Length; i++)
            {

                //IF CHECKBOX IS MARKED
                if (textboxCheckboxes[i] == "1")
                {

                    //int match_id = Convert.ToInt16(textboxLblMatchID[i]);
                    string match_id = textboxLblMatchID[i];
                    string query = "Delete from matches where match_id ='"+ match_id +"'";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                }
            }

            Response.Redirect("home");
        }
    }
}