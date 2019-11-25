namespace WoTStats.Models.RestModels.WoT.TankopediaVehicle
{
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
}