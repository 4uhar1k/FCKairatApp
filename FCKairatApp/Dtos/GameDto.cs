using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCKairatApp.Dtos
{
    public class GameDto
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set;}
        public string FirstTeamName { get; set; }
        public string SecondTeamName { get; set; }
        public int FirstTeamScore { get; set; }
        public int SecondTeamScore { get; set; }
        public Byte[] FirstTeamLogo { get; set; }
        public Byte[] SecondTeamLogo { get; set; }
        public string GameTime { get; set; }
        public string Tournament { get; set; }
        public bool IsLive { get; set; }
        public string TicketsLink { get; set; }
    }
}
