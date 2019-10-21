using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.RestModels.WoT.PlayerVehicleStatistics;
using WoTStats.Models.RestModels.XVM;
using WoTStats.Services.RestServices.WoT;
using WoTStats.Services.RestServices.XVM;
using WoTStats.Models.DataTemplates;
using WoTStats.Services.EventArguments;

namespace WoTStats.Services.VisibleDataProviders
{
    class VehiclesVisibleDataProvider
    {
        private ReferencialWN8Data referencialWN8Data;

        public delegate void VehiclesVisibleDataChangedEventHandler(object source, OnVehiclesVisibleDataChangedArgs args);

        public event VehiclesVisibleDataChangedEventHandler VehiclesVisibleDataChanged;


        protected virtual void OnVehiclesVisibleDataChanged(OnVehiclesVisibleDataChangedArgs args)
        {
            VehiclesVisibleDataChanged?.Invoke(this, args);
        }

        public VehiclesVisibleDataProvider()
        {
            referencialWN8Data = null;
        }

        public async void ProvideVehiclesVisibleData(User user)
        {
            await Task.Run(async () =>
            {
                var vehiclesVisibleData = await GetVehiclesVisibleDataAsync(user);
                OnVehiclesVisibleDataChanged(new OnVehiclesVisibleDataChangedArgs
                {
                    VehiclesVisibleData = vehiclesVisibleData
                });
            });


        }

        public async Task<List<VehicleVisibleData>> GetVehiclesVisibleDataAsync(User user)
        {
            var statisticsRestService = new PlayerVehiclesStatisticsRestService();

            var tankopediaRestService = new TankopediaVehicleRestService();

            var vehiclesStatistics = await statisticsRestService.GetPlayerVehiclesStatisticsAsync(user.AccountId, user.WoTServer);

            var tanksData = new List<VehicleVisibleData>();

            foreach (var stat in vehiclesStatistics)
            {
                var vehicle = await tankopediaRestService.GetTankopediaVehicleAsync(stat.tank_id, user.WoTServer);

                if (vehicle != null)
                {
                    double wn8 = await GetTankWN8(stat);

                    tanksData.Add(new VehicleVisibleData
                    {
                        Name = vehicle.name,
                        AvgDamage = ((double)stat.all.damage_dealt / stat.all.battles).ToString("F", CultureInfo.InvariantCulture),
                        Battles = stat.all.battles.ToString(),
                        WinRate = ((double)stat.all.wins / stat.all.battles).ToString("P", CultureInfo.InvariantCulture),
                        WN8 = wn8.ToString("F", CultureInfo.InvariantCulture),
                    });
                }
            }
            
            return tanksData;
        }

        private async Task<double> GetTankWN8(PlayerVehicleStatistics vehicleStats)
        {
            if (referencialWN8Data == null)
            {
                var dataProvider = new ReferentialWN8DataRestService();
                referencialWN8Data = await dataProvider.GetReferencialWN8DataAsync();
            }

            var tankReferentialData = referencialWN8Data.data
                .Where(data => data.IDNum == Int32.Parse(vehicleStats.tank_id))
                .FirstOrDefault();

            var wn8calc = new CalculatorWN8
            {
                AverageDamage = (double)vehicleStats.all.damage_dealt / vehicleStats.all.battles,
                AverageDefense = (double)vehicleStats.all.dropped_capture_points / vehicleStats.all.battles,
                AverageFrag = (double)vehicleStats.all.frags / vehicleStats.all.battles,
                AverageSpot = (double)vehicleStats.all.spotted / vehicleStats.all.battles,
                WinRate = (double)vehicleStats.all.wins / vehicleStats.all.battles,

                ExpectedDamage = tankReferentialData.expDamage,
                ExpectedDefense = tankReferentialData.expDef,
                ExpectedFrag = tankReferentialData.expFrag,
                ExpectedSpot = tankReferentialData.expSpot,
                ExpectedWinRate = tankReferentialData.expWinRate
            };

            return wn8calc.GetWN8Value();
        }
    }
}
