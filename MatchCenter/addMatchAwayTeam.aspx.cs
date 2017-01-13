using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Npgsql;
using System.Web.Script.Serialization;

namespace MatchCenter
{
    public partial class addMatchAwayTeam : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        protected string ValueNumbers, ValueNames, ValueGoals, ValueAssists, ValueYellows, ValueSecondYellows, ValueReds, ValueIn, ValueOut;

        protected void btnAwayTeamNext_Click(object sender, EventArgs e)
        {
            Session.Remove("lineupAway");
            string[] textboxNames = Request.Form.GetValues("DynamicTextBoxName");

            JavaScriptSerializer serializer = new JavaScriptSerializer();
           // this.Values = serializer.Serialize(textboxValues);


            if (textboxNames != null)
            {
                string[] textboxNumbers = Request.Form.GetValues("DynamicTextBoxNumber");
                string[] textboxGoals = Request.Form.GetValues("DynamicTextBoxGoal");
                string[] textboxAssists = Request.Form.GetValues("DynamicTextBoxAssist");
                string[] textboxYellows = Request.Form.GetValues("DynamicTextBoxYellow");
                string[] textboxSecondYellows = Request.Form.GetValues("DynamicTextBoxSecondYellow");
                string[] textboxReds = Request.Form.GetValues("DynamicTextBoxRed");
                string[] textboxIn = Request.Form.GetValues("DynamicTextBoxIn");
                string[] textboxOut = Request.Form.GetValues("DynamicTextBoxOut");
                string[] checkboxYesNoStart11 = Request.Form.GetValues("start11");
                string[] checkboxGoalkeeper = Request.Form.GetValues("goalkeeper");

                //this.ValueNumbers = serializer.Serialize(textboxNumbers);
                //this.ValueNames = serializer.Serialize(textboxNames);
                //this.ValueGoals = serializer.Serialize(textboxGoals);
                //this.ValueAssists = serializer.Serialize(textboxAssists);
                //this.ValueYellows = serializer.Serialize(textboxYellows);
                //this.ValueSecondYellows = serializer.Serialize(textboxSecondYellows);
                //this.ValueReds = serializer.Serialize(textboxReds);
                //this.ValueIn = serializer.Serialize(textboxIn);
                //this.ValueOut = serializer.Serialize(textboxOut);

                List<Classes.Player> PlayerList = new List<Classes.Player>();

                for (int i = 0; i < textboxNames.Length; i++)
                {
                    Classes.Player p = new Classes.Player();
                    p.name = textboxNames[i];

                    //IF PLAYER IS GOALKEEPER
                    if (checkboxGoalkeeper[i] == "0")
                    {
                        p.goalkeeper = false;
                    }

                    else
                    {
                        p.goalkeeper = true;
                    }

                    //IF PLAYER IS IN START 11
                    if (checkboxYesNoStart11[i] == "0")
                    {
                        p.start11 = false;
                    }

                    else
                    {
                        p.start11 = true;
                    }

                    ///IF NUMBER FIELD IS NOT EMPTY
                    if (textboxNumbers[i] != string.Empty)
                    {
                        p.number = Convert.ToInt16(textboxNumbers[i]);
                    }

                    ///IF GOAL FIELD IS EMPTY
                    if (textboxGoals[i] != string.Empty)
                    {
                        p.goal = Convert.ToInt16(textboxGoals[i]);
                    }

                    ///IF ASSIST FIELD IS EMPTY
                    if (textboxAssists[i] != string.Empty)
                    {
                        p.assist = Convert.ToInt16(textboxAssists[i]);
                    }

                    ///IF FIEL YELLOW IS NOT EMPTY
                    if (textboxYellows[i] != string.Empty)
                    {
                        p.yellow = Convert.ToInt16(textboxYellows[i]);
                    }

                    ///IF FIEL SECOND YELLOW IS NOT EMPTY
                    if (textboxSecondYellows[i] != string.Empty)
                    {
                        p.secondYellow = Convert.ToInt16(textboxSecondYellows[i]);
                    }

                    ///IF FIEL RED IS NOT EMPTY
                    if (textboxReds[i] != string.Empty)
                    {
                        p.red = Convert.ToInt16(textboxReds[i]);
                    }
                    ///IF FIEL IN IS NOT EMPTY
                    if (textboxIn[i] != string.Empty)
                    {
                        p.inGame = Convert.ToInt16(textboxIn[i]);
                    }
                    ///IF FIEL OUT IS NOT EMPTY
                    if (textboxOut[i] != string.Empty)
                    {
                        p.outGame = Convert.ToInt16(textboxOut[i]);
                    }

                    PlayerList.Add(p);
                    Session["lineupAway"] = PlayerList;
                }
            }
            
            
            Session["teamNameAway"] = txtAwayTeamName.Text;
            Response.Redirect("addAboutMatch");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}