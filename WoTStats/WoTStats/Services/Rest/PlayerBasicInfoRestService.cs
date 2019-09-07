using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.RestModels;

namespace WoTStats.Services.Rest
{
    class PlayerBasicInfoRestService
    {
        private HttpClient client;

        private readonly string bareUrlPartFirst = "https://api.worldoftanks.";
        private readonly string bareUrlPartSecond = $"/wot/account/list/?application_id={Const.WOT_API_APPLICATION_ID}&search=";
            

        public PlayerBasicInfoRestService()
        {
            client = new HttpClient();
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

        private string GetFullUrl(string nickname, WoTServer server)
        {
            string urlServerRepresentation = null;

            switch (server)
            {
                case WoTServer.ru:
                    urlServerRepresentation = "ru";
                    break;
                case WoTServer.eu:
                    urlServerRepresentation = "eu";
                    break;
                case WoTServer.na:
                    urlServerRepresentation = "com";
                    break;
                case WoTServer.asia:
                    urlServerRepresentation = "asia";
                    break;
            }

            return $"{bareUrlPartFirst}{urlServerRepresentation}{bareUrlPartSecond}{nickname}";
        }
    }
}
