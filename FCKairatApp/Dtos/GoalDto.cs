using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCKairatApp.Dtos
{
    public class GoalDto
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public int GameId { get; set; }
        public string ScoredPlayerSurname { get; set; }
        public string ScoredTeam { get; set; }
        public int ScoredMinute { get; set; }
    }
}
