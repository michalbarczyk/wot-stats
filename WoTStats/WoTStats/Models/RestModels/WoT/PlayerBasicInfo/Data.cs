using Newtonsoft.Json;

namespace WoTStats.Models.RestModels.WoT.PlayerBasicInfo
{
    public class Data
    {
        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }
    }
}