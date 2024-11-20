using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCKairatApp.Models
{
    public class Game
    {
        public Team FirstTeam { get; set; }
        public Team SecondTeam { get; set; }
        public int FirstTeamScore { get; set; }
        public int SecondTeamScore { get; set; }
        public string GameTime { get; set; }
        public string Tournament { get; set; }
        public bool IsLive { get; set; }
    }
}
