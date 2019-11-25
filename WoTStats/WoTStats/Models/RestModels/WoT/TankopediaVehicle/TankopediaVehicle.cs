using System;
using System.Collections.Generic;
using System.Text;

namespace WoTStats.Models.RestModels.WoT.TankopediaVehicle
{
    class TankopediaVehicle
    {
        public bool is_wheeled { get; set; }
        public List<int> radios { get; set; }
        public bool is_premium { get; set; }
        public string tag { get; set; }
        public Images images { get; set; }
        public string tank_id { get; set; }
        public List<int> suspensions { get; set; }
        public List<int> provisions { get; set; }
        public List<int> engines { get; set; }
        public List<Crew> crew { get; set; }
        public string type { get; set; }
        public List<int> guns { get; set; }
        public string description { get; set; }
        public string short_name { get; set; }
        public bool is_premium_igr { get; set; }
        public object next_tanks { get; set; }
        // public ModulesTree modules_tree { get; set; }
        public string nation { get; set; }
        public int tier { get; set; }
        public object prices_xp { get; set; }
        public bool is_gift { get; set; }
        public string name { get; set; }
        public object price_gold { get; set; }
        public object price_credit { get; set; }
        public DefaultProfile default_profile { get; set; }
        public List<object> turrets { get; set; }
    }
}
