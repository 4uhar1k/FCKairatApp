using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCKairatApp.Dtos
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string UserLogin { get; set; }
        public string FirstTeamName { get; set; }
        public string SecondTeamName { get; set; }
        public string GameTime { get; set; }

    }
}
