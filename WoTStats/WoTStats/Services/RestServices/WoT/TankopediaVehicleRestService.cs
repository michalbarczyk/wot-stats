using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.RestModels.WoT.PlayerVehicleStatistics;
using WoTStats.Models.RestModels.WoT.TankopediaVehicle;

namespace WoTStats.Services.RestServices.WoT
{
    class TankopediaVehicleRestService : BaseRestService
    {
        private JToken allTanksJToken;

        public TankopediaVehicleRestService()
        {
            base.BareUrlSpecificPart = $"/encyclopedia/vehicles/?application_id={Const.WOT_API_APPLICATION_ID}";
            allTanksJToken = null;
        }

        public async Task<TankopediaVehicle> GetTankopediaVehicleAsync(string tankId, WoTServer server)
        {
            TankopediaVehicle tankopediaVehicle = null;

            var fullUrl = GetFullUrl(server);

            if (allTanksJToken == null)
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(fullUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();

                        var wholeJObject = JObject.Parse(content);

                        this.allTanksJToken = wholeJObject["data"];
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\tERROR", ex.Message);
                }
            }

            JToken tankopediaVehicleJToken = null;

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            try
            {
                tankopediaVehicleJToken = allTanksJToken[tankId];

                tankopediaVehicle = JsonConvert.DeserializeObject<TankopediaVehicle>(tankopediaVehicleJToken.ToString(), settings);
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("\t[possible problem: no vehicle having selected tankId]", ex.Message);
                return null;
            }

           
            return tankopediaVehicle;
        }

        

        private string GetFullUrl(WoTServer server)
        {
            return $"{base.GetServerEndpoint(server)}{BareUrlSpecificPart}";
        }
    }
}
