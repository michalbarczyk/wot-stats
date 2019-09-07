using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace WoTStats.Models.DatabaseModels
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string AccountId { get; set; }

        public WoTServer WoTServer { get; set; }
    }
}
