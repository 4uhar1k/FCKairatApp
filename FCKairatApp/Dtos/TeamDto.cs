using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCKairatApp.Dtos
{
    public class TeamDto
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string CoachName { get; set; }
        public int WinsAmount { get; set; }
        public int DrawsAmount { get; set; }
        public int LosesAmount { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsMissed { get; set; }
        public int Points { get; set; }
        public Byte[] TeamLogo { get; set; }

    }
}
