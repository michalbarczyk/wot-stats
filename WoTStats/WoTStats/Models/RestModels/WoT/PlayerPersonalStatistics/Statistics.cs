namespace WoTStats.Models.RestModels.WoT.PlayerPersonalStatistics
{
    public class Statistics
    {
        public Clan clan { get; set; }
        public All all { get; set; }
        public RegularTeam regular_team { get; set; }
        public int trees_cut { get; set; }
        public Company company { get; set; }
        public StrongholdSkirmish stronghold_skirmish { get; set; }
        public StrongholdDefense stronghold_defense { get; set; }
        public Historical historical { get; set; }
        public Team team { get; set; }
        public object frags { get; set; }
    }
}