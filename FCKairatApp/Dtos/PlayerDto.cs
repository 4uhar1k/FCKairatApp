﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCKairatApp.Dtos
{
    public class PlayerDto
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Number { get; set; }
        public string Position { get; set; }
        public int GoalsAmount { get; set; }
        public int AssistsAmount { get; set; }
        public string StartDate { get; set; }
        public string ExpiryDate { get; set; }
        public Byte[] PlayerImage { get; set; }
    }
}
