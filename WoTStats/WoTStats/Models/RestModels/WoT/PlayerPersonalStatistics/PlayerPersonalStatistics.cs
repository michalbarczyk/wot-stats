using System;
using System.Collections.Generic;
using System.Text;

namespace WoTStats.Models.RestModels.WoT.PlayerPersonalStatistics
{
    public class PlayerPersonalStatistics
    {
        public string client_language { get; set; }
        public int last_battle_time { get; set; }
        public int account_id { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
        public object @private { get; set; }
        public int global_rating { get; set; }
        public int? clan_id { get; set; }
        public Statistics statistics { get; set; }
        public string nickname { get; set; }
        public int logout_at { get; set; }
    }
}
