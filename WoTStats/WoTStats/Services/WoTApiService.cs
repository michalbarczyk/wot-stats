using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WoTStats.Models.RestModels;

namespace WoTStats.Services
{
    class WoTApiService
    {
        HttpClient client;

        public WoTApiService()
        {
            client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<AccountBasicInfo> GetAccountBasicInfoAsync(string uri)
        {
            AccountBasicInfo accountBasicInfo = null;

            uri =
                "https://api.worldoftanks.eu/wot/account/list/?application_id=85a2009c52d5d8feb52b45ec88454405&search=michalbarczyk";

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    accountBasicInfo = JsonConvert.DeserializeObject<AccountBasicInfo>(content);

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return accountBasicInfo;
        }
    }
}
