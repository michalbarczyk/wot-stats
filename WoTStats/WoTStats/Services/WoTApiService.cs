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

        public async Task<AccountBasicInfo> GetAccountBasicInfoAsync(string name)
        {
            AccountBasicInfo accountBasicInfo = null;

            name =
                $"https://api.worldoftanks.eu/wot/account/list/?application_id={Const.WOT_API_APPLICATION_ID}&search={name}";
           

            try
            {
                HttpResponseMessage response = await client.GetAsync(name);

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
