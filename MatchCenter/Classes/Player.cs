using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatchCenter.Classes
{
    public class Player
    {
        public string name { get; set; }
        public int number { get; set; }
        public int goal { get; set; }
        public int assist { get; set; }
        public int yellow { get; set; }
        public int secondYellow { get; set; }
        public int red { get; set; }
        public int inGame { get; set; }
        public int outGame { get; set; }
        public bool start11 { get; set; }
        public bool goalkeeper { get; set; }
    }
}