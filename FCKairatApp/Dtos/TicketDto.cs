﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCKairatApp.Dtos
{
    public class TicketDto
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string UserLogin { get; set; }
        public int GameId { get; set; }

    }
}
