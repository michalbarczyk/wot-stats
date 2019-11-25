using Newtonsoft.Json;

namespace WoTStats.Models.RestModels.WoT.PlayerBasicInfo
{
    public class Meta
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}