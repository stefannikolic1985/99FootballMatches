using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using MatchCenter.Classes;
using System.Web.Script.Serialization;
using System.Data;
using System.Net;
using System.IO;

namespace MatchCenter
{
    public partial class registration : System.Web.UI.Page
    {
        NpgsqlConnection con = new NpgsqlConnection("Server=webblabb.miun.se;Port=5432; User Id=Stefan85;Password=Stefan85;Database=matchcenterDB;SSL=true;");
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        GoogleReCaptcha.GoogleReCaptcha ctrlGoogleReCaptcha = new GoogleReCaptcha.GoogleReCaptcha();
        protected override void CreateChildControls()
        {

            base.CreateChildControls();
            ctrlGoogleReCaptcha.PublicKey = "6LccRgsUAAAAAOPWzhmDuK7crNKp1b27V23Usy6u";
            ctrlGoogleReCaptcha.PrivateKey = "6LccRgsUAAAAAFklYnQXe1yFU8ZufG_v5_hyxfRE";
            this.PanelGoogleRecaptcha.Controls.Add(ctrlGoogleReCaptcha);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            PanelResponse.Visible = false;
            ButtonRegister.Click += new EventHandler(ButtonRegister_Click);


        }

        [System.Web.Services.WebMethod]
        public static string CheckUsername(string username)
        {
            string retval = "";
            NpgsqlConnection con = new NpgsqlConnection("Server=webblabb.miun.se;Port=5432; User Id=Stefan85;Password=Stefan85;DatabasematchcenterDB=matchcenterDB;SSL=true;");
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select username from users where username=@UserName", con);
            cmd.Parameters.AddWithValue("@UserName", username);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                retval = "true";
            }
            else
            {
                retval = "false";
            }

            return retval;
        }


        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            //if (ctrlGoogleReCaptcha.Validate())
            //{
            //    //submit form
            //    Label1.Text = "Success";
            //}
            //else
            //{
            //    Label1.Text = "Captcha Failed!! Please try again!!";

            //    //Reload recaptcha
            //    //ScriptManager.RegisterClientScriptBlock(this.Page,
            //    //this.Page.GetType(), "whatever1",
            //    //"Recaptcha.reload();", true);
            //    //UpdatePanelRegister.Update();
            //}
            Encryption SHA256 = new Encryption();


            string email = txtEmail.Text,
                   password = SHA256.ComputeHash(txtPassword.Text, Supported_HA.SHA256, null),
                   username = txtUsername.Text;



            string sqlCheckUsername = "select username from users where username = '" + username + "'";
            NpgsqlCommand cmdCheckUsername = new NpgsqlCommand(sqlCheckUsername, con);
            con.Open();
            NpgsqlDataReader drCheckUsername = cmdCheckUsername.ExecuteReader();

            
            if (!drCheckUsername.Read())
            {
                PanelResponse.Visible = false;
                con.Close();
                //Check if email already exist 
                string sqlCheck = "select username from users where email = '" + email + "'";
                NpgsqlCommand cmdCheck = new NpgsqlCommand(sqlCheck, con);
                con.Open();
                NpgsqlDataReader drCheck = cmdCheck.ExecuteReader();


                
                if (ctrlGoogleReCaptcha.Validate())
                {
                    if (!drCheck.Read())
                    {
                        PanelResponse.Visible = false;
                        //Create new user
                        NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO users (username, email, password) VALUES (@username, @email, @password)", con);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.ExecuteNonQuery();

                       // LabelSuccess.Text = "<span class='spacer-glyph glyphicon glyphicon-exclamation-sign'></span> YEEEEEEEEEEEEES";
                        con.Close();

                        //Automatically login user after signup 
                        string emailInput = txtEmail.Text;
                        string sql = "SELECT * FROM users WHERE email = '" + emailInput + "'";
                        NpgsqlCommand cmd2 = new NpgsqlCommand(sql, conn);
                        conn.Open();
                        NpgsqlDataReader dr = cmd2.ExecuteReader();
                        if (dr.Read())
                        {
                            if (!DBNull.Value.Equals(dr["user_id"]))
                            {
                                //if (IsCaptchaValid)
                                //{
                                //Valid Request
                                // Create object 
                                User user = new User();

                                // Populate properties
                                user.id = Convert.ToInt16(dr["user_id"]);
                                user.username = dr["username"].ToString();
                                user.password = dr["password"].ToString();

                                HttpCookie cookie = user.CreateCookie();
                                Response.Cookies.Add(cookie);
                                Response.Redirect("~/home");
                            }
                            conn.Close();
                        }
                    }
                    else
                    {
                        PanelResponse.Visible = true;
                        LabelResponse.Text = "<span class='spacer-glyph glyphicon glyphicon-exclamation-sign'></span> Email already exists";

                    }
                }

                else
                {
                   // Label1.Text = "Captcha Failed!! Please try again!";
                    PanelResponse.Visible = true;
                    LabelResponse.Text = "<span class='spacer-glyph glyphicon glyphicon-exclamation-sign'></span> Captcha Failed!! Please try again! ";

                }
            }

            else
            {
                PanelResponse.Visible = true;
                LabelResponse.Text = "<span class='spacer-glyph glyphicon glyphicon-exclamation-sign'></span> Username already exists ";
            }


        }

        protected void btn_Click(object sender, EventArgs e)
        {
           
        }

        


    }
}