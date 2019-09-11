using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WoTStats.Models.RestModels.XVM;

namespace WoTStats.Services.RestServices.XVM
{
    class ReferentialWN8DataRestService
    {
        private string endpointXVM = "https://static.modxvm.com/wn8-data-exp/json/wn8exp.json";

        private HttpClient client;

        public ReferentialWN8DataRestService()
        {
            this.client = new HttpClient();
        }

        public async Task<ReferencialWN8Data> GetReferencialWN8DataAsync()
        {
            ReferencialWN8Data referencialWN8Data = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(endpointXVM);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    referencialWN8Data = JsonConvert.DeserializeObject<ReferencialWN8Data>(content, settings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return referencialWN8Data;
        }

    }
}
