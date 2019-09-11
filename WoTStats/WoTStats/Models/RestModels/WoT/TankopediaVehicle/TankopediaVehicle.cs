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


    public class Images
    {
        public string small_icon { get; set; }
        public string contour_icon { get; set; }
        public string big_icon { get; set; }
    }

    public class Roles
    {
        public string commander { get; set; }
        public string loader { get; set; }
        public string radioman { get; set; }
        public string driver { get; set; }
        public string gunner { get; set; }
    }

    public class Crew
    {
        public Roles roles { get; set; }
        public string member_id { get; set; }
    }

    public class Engine
    {
        public string name { get; set; }
        public int power { get; set; }
        public int weight { get; set; }
        public string tag { get; set; }
        public double fire_chance { get; set; }
        public int tier { get; set; }
    }

    public class Suspension
    {
        public string name { get; set; }
        public int weight { get; set; }
        public int load_limit { get; set; }
        public string tag { get; set; }
        public int traverse_speed { get; set; }
        public int tier { get; set; }
        public int steering_lock_angle { get; set; }
    }

    public class Hull
    {
        public int front { get; set; }
        public int sides { get; set; }
        public int rear { get; set; }
    }

    public class Armor
    {
        public object turret { get; set; }
        public Hull hull { get; set; }
    }

    public class Modules
    {
        public int gun_id { get; set; }
        public int suspension_id { get; set; }
        public object turret_id { get; set; }
        public int radio_id { get; set; }
        public int engine_id { get; set; }
    }

    public class Gun
    {
        public int move_down_arc { get; set; }
        public int caliber { get; set; }
        public string name { get; set; }
        public int weight { get; set; }
        public int move_up_arc { get; set; }
        public double fire_rate { get; set; }
        public double dispersion { get; set; }
        public string tag { get; set; }
        public int traverse_speed { get; set; }
        public double reload_time { get; set; }
        public int tier { get; set; }
        public double aim_time { get; set; }
    }

    public class Turret
    {
        public string name { get; set; }
        public int weight { get; set; }
        public int view_range { get; set; }
        public int hp { get; set; }
        public string tag { get; set; }
        public object traverse_speed { get; set; }
        public int traverse_right_arc { get; set; }
        public int tier { get; set; }
        public int traverse_left_arc { get; set; }
    }

    public class Radio
    {
        public int tier { get; set; }
        public int signal_range { get; set; }
        public string tag { get; set; }
        public string name { get; set; }
        public int weight { get; set; }
    }

    public class Ammo
    {
        public List<int> penetration { get; set; }
        public object stun { get; set; }
        public string type { get; set; }
        public List<int> damage { get; set; }
    }

    public class DefaultProfile
    {
        public Engine engine { get; set; }
        public object siege { get; set; }
        public int max_ammo { get; set; }
        public Suspension suspension { get; set; }
        public int weight { get; set; }
        public Armor armor { get; set; }
        public int hp { get; set; }
        public Modules modules { get; set; }
        public Gun gun { get; set; }
        public Turret turret { get; set; }
        public int hull_weight { get; set; }
        public Radio radio { get; set; }
        public object rapid { get; set; }
        public int speed_forward { get; set; }
        public int hull_hp { get; set; }
        public int speed_backward { get; set; }
        public List<Ammo> ammo { get; set; }
        public int max_weight { get; set; }
    }
}
