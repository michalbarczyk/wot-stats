using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.RestModels.WoT.PlayerPersonalData;

namespace WoTStats.Services.RestServices.WoT
{
    class PlayerPersonalDataRestService : BaseRestService
    {
        public PlayerPersonalDataRestService()
        {
            base.BareUrlSpecificPart = $"/account/info/?application_id={Const.WOT_API_APPLICATION_ID}&account_id=";
        }

        public async Task<PlayerPersonalData> GetPlayerPersonalDataAsync(string accountId, WoTServer server)
        {
            PlayerPersonalData playerPersonalData = null;

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
                    
                    playerPersonalData = JsonConvert.DeserializeObject<PlayerPersonalData>(playerPersonalDataJToken.ToString(), settings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return playerPersonalData;
        }

        private string GetFullUrl(string accountId, WoTServer server)
        {
            return $"{base.GetServerEndpoint(server)}{BareUrlSpecificPart}{accountId}";
        }
    }
}
