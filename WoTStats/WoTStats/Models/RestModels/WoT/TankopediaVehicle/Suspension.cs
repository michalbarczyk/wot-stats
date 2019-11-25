namespace WoTStats.Models.RestModels.WoT.TankopediaVehicle
{
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
}