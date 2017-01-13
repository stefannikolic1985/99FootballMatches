using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Npgsql;
using System.Web.Security;
using MatchCenter.Classes;

namespace MatchCenter
{
    public partial class login : System.Web.UI.Page
    {

        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonLogin.Click += ButtonLogin_Click;
            PanelResponse.Visible = false;
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            LogIn();
        }


        private void LogIn()
        {
            Encryption SHA256 = new Encryption();

            // Create variables for easy handling from textboxes
            string emailInput = TextEmailLogin.Text;
            string passwordInput = TextPasswordLogin.Text;

            string sql = "SELECT * FROM users WHERE email = '" + emailInput + "'";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                if (!DBNull.Value.Equals(dr["user_id"]))
                {
                    // Create object 
                    User user = new User();

                    // Populate properties
                    user.id = Convert.ToInt16(dr["user_id"]);
                    user.username = dr["username"].ToString();
                    user.password = dr["password"].ToString();

                    if (SHA256.Confirm(passwordInput, user.password, Supported_HA.SHA256))
                    {

                        HttpCookie cookie = user.CreateCookie();
                        Response.Cookies.Add(cookie);
                        Response.Redirect("~/home");
                        
                    }
                    else
                    {
                        PanelResponse.Visible = false;
                        PanelResponse.Visible = true;
                        TextEmailLogin.BorderColor = System.Drawing.Color.Red;
                        LabelResponse.Text = "<span class='spacer-glyph glyphicon glyphicon-exclamation-sign'></span> The password is invalid";
                    }
                }
                conn.Close();
            }
            

            else
            {
                PanelResponse.Visible = false;
                PanelResponse.Visible = true;// LabelResponse.Text = "<span class='spacer-glyph glyphicon glyphicon-exclamation-sign'></span> Ingen med den e-mailen registrerad.";
                LabelResponse.Text = "<span class='spacer-glyph glyphicon glyphicon-exclamation-sign'></span> There is no user registered with that email address";
                conn.Close();
            }
        }
    }
}