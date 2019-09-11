using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.RestModels.WoT.PlayerBasicInfo;

namespace WoTStats.Services.RestServices.WoT
{
    class PlayerBasicInfoRestService : BaseRestService
    {
        public PlayerBasicInfoRestService()
        {
            base.BareUrlSpecificPart = $"/account/list/?application_id={Const.WOT_API_APPLICATION_ID}&search=";
        }

        public async Task<PlayerBasicInfo> GetPlayerBasicInfoAsync(string nickname, WoTServer server)
        {
            PlayerBasicInfo playerBasicInfo = null;

            var fullUrl = GetFullUrl(nickname, server);

            try
            {
                HttpResponseMessage response = await client.GetAsync(fullUrl);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    playerBasicInfo = JsonConvert.DeserializeObject<PlayerBasicInfo>(content);

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return playerBasicInfo;
        }

        protected override string GetFullUrl(string nickname, WoTServer server)
        {
            return $"{base.GetServerEndpoint(server)}{BareUrlSpecificPart}{nickname}";
        }
    }
}
