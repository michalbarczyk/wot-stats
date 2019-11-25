using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WoTStats.Models.RestModels.WoT.PlayerBasicInfo
{
    class PlayerBasicInfo
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public List<Data> Datas { get; set; }
    }
}
