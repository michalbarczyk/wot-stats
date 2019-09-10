using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WoTStats.Models.DatabaseModels;

using WoTStats.Models.RestModels.PlayerVehicleStatistics;

namespace WoTStats.Services.Rest.WoT
{
    class PlayerVehiclesStatisticsRestService : BaseRestService
    {
        public PlayerVehiclesStatisticsRestService()
        {
            base.BareUrlSpecificPart = $"/tanks/stats/?application_id={Const.WOT_API_APPLICATION_ID}&account_id=";
        }

        public async Task<List<PlayerVehicleStatistics>> GetPlayerVehiclesStatisticsAsync(string accountId, WoTServer server)
        {
            List<PlayerVehicleStatistics> playerVehiclesStatistics = null;

            var fullUrl = GetFullUrl(accountId, server);

            try
            {
                HttpResponseMessage response = await client.GetAsync(fullUrl);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var wholeJObject = JObject.Parse(content);
                    var data = wholeJObject["data"];

                    var playerVehiclesStatisticsJToken = data[accountId];

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    playerVehiclesStatistics = JsonConvert.DeserializeObject<List<PlayerVehicleStatistics>>(playerVehiclesStatisticsJToken.ToString(), settings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return playerVehiclesStatistics;
        }

        protected override string GetFullUrl(string accountId, WoTServer server)
        {
            return $"{base.GetServerEndpoint(server)}{BareUrlSpecificPart}{accountId}";
        }
    }
}
