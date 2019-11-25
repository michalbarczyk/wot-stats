using System;
using System.Collections.Generic;
using System.Text;

namespace WoTStats.Models.RestModels.WoT.PlayerVehicleStatistics
{
    class PlayerVehicleStatistics
    {
        public Clan clan { get; set; }
        public StrongholdSkirmish stronghold_skirmish { get; set; }
        public RegularTeam regular_team { get; set; }
        public string account_id { get; set; }
        public int max_xp { get; set; }
        public Company company { get; set; }
        public All all { get; set; }
        public StrongholdDefense stronghold_defense { get; set; }
        public int max_frags { get; set; }
        public Team team { get; set; }
        public Globalmap globalmap { get; set; }
        public object frags { get; set; }
        public int mark_of_mastery { get; set; }
        public object in_garage { get; set; }
        public string tank_id { get; set; }

    }
}
