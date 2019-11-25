namespace WoTStats.Models.RestModels.WoT.TankopediaVehicle
{
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
}