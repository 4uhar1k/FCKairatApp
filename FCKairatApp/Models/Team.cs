using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCKairatApp.Models
{
    public class Team
    {
        public string TeamName { get; set; }
        public List<Player> Players { get; set; }
        public string CoachName { get; set; }
    }
}
