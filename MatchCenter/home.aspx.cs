using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace MatchCenter
{
    public partial class home : System.Web.UI.Page
    {
        NpgsqlConnection conn = new NpgsqlConnection("Server=webblabb.miun.se;Port=5432; User Id=Stefan85;Password=Stefan85;Database=matchcenterDB;SSL=true;");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sql = @"select name from team";

                conn.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                NpgsqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    hello.Text = read[0].ToString();
                }
                conn.Close();
            }
        }
    }
}