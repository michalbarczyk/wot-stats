using System.Collections.Generic;

namespace WoTStats.Models.RestModels.WoT.TankopediaVehicle
{
    public class Ammo
    {
        public List<int> penetration { get; set; }
        public object stun { get; set; }
        public string type { get; set; }
        public List<int> damage { get; set; }
    }
}