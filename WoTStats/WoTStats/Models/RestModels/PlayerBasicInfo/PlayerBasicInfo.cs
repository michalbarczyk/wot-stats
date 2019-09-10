using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WoTStats.Models.RestModels.PlayerBasicInfo
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

    public class Data
    {
        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }
    }

    public class Meta
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }





}
