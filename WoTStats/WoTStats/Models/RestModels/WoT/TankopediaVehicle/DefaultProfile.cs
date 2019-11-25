using System.Collections.Generic;

namespace WoTStats.Models.RestModels.WoT.TankopediaVehicle
{
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