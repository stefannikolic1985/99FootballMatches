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

        }


        private void LogIn()
        {
            Encryption SHA256 = new Encryption();

            // Create variables for easy handling from textboxes
            string emailInput = TextEmailLogin.Text;
            string passwordInput = TextPasswordLogin.Text;

            string sql = "SELECT * FROM member WHERE email = '" + emailInput + "'";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (!DBNull.Value.Equals(dr["id"]))
                {
                    // Create object 
                    Member member = new Member();

                    // Populate properties
                    member.id = Convert.ToInt16(dr["id"]);
                    member.firstName = dr["firstname"].ToString();
                    member.lastName = dr["lastname"].ToString();
                    member.address = dr["address"].ToString();
                    member.zipcode = dr["zipcode"].ToString();
                    member.place = dr["place"].ToString();
                    member.email = dr["email"].ToString();
                    member.gender = dr["gender"].ToString();
                    member.hcp = Convert.ToDouble(dr["hcp"]);
                    member.golf_id = dr["golf_id"].ToString();
                    member.id_category = Convert.ToInt16(dr["id_category"]);
                    member.password = dr["password"].ToString();
                    member.staff = Convert.ToBoolean(dr["staff"]);

                    if (SHA256.Confirm(passwordInput, member.password, Supported_HA.SHA256))
                    {
                        // Check with category and redirect
                        // Check if (Admin/staff) redirect to other page
                        if (member.staff == false)
                        {
                            HttpCookie cookie = member.CreateCookie(false);
                            Response.Cookies.Add(cookie);
                            Response.Redirect("~/booking.aspx");
                            Response.Redirect("~/minasidor.aspx");
                        }
                        else
                        {
                            HttpCookie cookie = member.CreateCookie(true);
                            Response.Cookies.Add(cookie);
                            Response.Redirect("~/booking.aspx");
                        }
                    }
                    else
                    {
                        PanelResponse.Visible = false;
                        PanelResponse.Visible = true;
                        LabelResponse.Text = "<span class='spacer-glyph glyphicon glyphicon-exclamation-sign'></span> Lösenordet stämmer inte.";
                    }
                }
                conn.Close();
            }
            else
            {
                PanelResponse.Visible = false;
                PanelResponse.Visible = true;
                LabelResponse.Text = "<span class='spacer-glyph glyphicon glyphicon-exclamation-sign'></span> Ingen med den e-mailen registrerad.";
            }
        }
    }
}