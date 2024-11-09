using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCKairatApp.Models
{
    public class Player
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Number { get; set; }
        public int GoalsAmount { get; set; }
        public int AssistsAmount { get; set; }
        public Image Image { get; set; }

    }
}
