using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.RestModels.WoT.PlayerVehicleStatistics;
using WoTStats.Models.RestModels.WoT.TankopediaVehicle;
using WoTStats.Services.RestServices.WoT;
using WoTStats.ViewModels.DataTemplates;

namespace WoTStats.ViewModels
{
    class TanksViewModel : BaseViewModel
    {
        public IList<TankVisibleData> TanksData { set; get; }

        public async void OnAppearing()
        {
            var statisticsRestService = new PlayerVehiclesStatisticsRestService();

            var tankopediaRestService = new TankopediaVehicleRestService();

            var users = await App.Database.GetUsersAsync();

            var user = users[0];

            var vehiclesStatistics = await statisticsRestService.GetPlayerVehiclesStatisticsAsync(user.AccountId, user.WoTServer);

            TanksData = new List<TankVisibleData>();

            int cntr = 0;

            foreach (var stat in vehiclesStatistics)
            {
                Debug.WriteLine($"\n\nPROCESSING STARTED {cntr}: tank_id = {stat.tank_id}\n\n");

                var vehicle = await tankopediaRestService.GetTankopediaVehicleAsync(stat.tank_id, user.WoTServer);

                if (vehicle != null)
                {
                    TanksData.Add(new TankVisibleData
                    {
                        Name = vehicle.name,
                        AvgDamage = ((double)stat.all.damage_dealt / stat.all.battles).ToString("F", CultureInfo.InvariantCulture),
                        Battles = stat.all.battles.ToString(),
                        WinRate = ((double)stat.all.wins / stat.all.battles).ToString("F", CultureInfo.InvariantCulture),
                        WN8 = "-1",
                        //ImageUrl = vehicle.images.small_icon.Trim('"')
                    });
                }

                
                cntr++;
                
            }

            OnPropertyChanged("TanksData");
        }
    }
}

