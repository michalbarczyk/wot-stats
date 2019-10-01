using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.RestModels.WoT.PlayerPersonalStatistics;

namespace WoTStats.Services.RestServices.WoT
{
    class PlayerPersonalStatisticsRestService : BaseRestService
    {
        public PlayerPersonalStatisticsRestService()
        {
            base.BareUrlSpecificPart = $"/account/info/?application_id={Const.WOT_API_APPLICATION_ID}&account_id=";
        }

        public async Task<PlayerPersonalStatistics> GetPlayerPersonalStatisticsAsync(string accountId, WoTServer server)
        {
            PlayerPersonalStatistics playerPersonalStatistics = null;

            var fullUrl = GetFullUrl(accountId, server);

            try
            {
                HttpResponseMessage response = await client.GetAsync(fullUrl);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var wholeJObject = JObject.Parse(content);
                    var data = wholeJObject["data"];

                    var playerPersonalDataJToken = data[accountId];

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    
                    playerPersonalStatistics = JsonConvert.DeserializeObject<PlayerPersonalStatistics>(playerPersonalDataJToken.ToString(), settings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return playerPersonalStatistics;
        }

        private string GetFullUrl(string accountId, WoTServer server)
        {
            return $"{base.GetServerEndpoint(server)}{BareUrlSpecificPart}{accountId}";
        }
    }
}
