using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCKairatApp
{
    public class TeamDto
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string CoachName { get; set; }
        //public Image TeamLogo { get; set; }

    }
}
