using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.RestModels.PlayerVehicleStatistics;
using WoTStats.Services.Rest.WoT;

namespace WoTStats.ViewModels
{
    class TanksViewModel : BaseViewModel
    {
        public IList<PlayerVehicleStatistics> VehiclesStatistics { get; set; }

        public async void OnAppearing()
        {
            var dataPrivider = new PlayerVehiclesStatisticsRestService();
            VehiclesStatistics = new List<PlayerVehicleStatistics>();

           var users = await App.Database.GetUsersAsync();

           var user = users[0];

            VehiclesStatistics = await dataPrivider.GetPlayerVehiclesStatisticsAsync(user.AccountId, user.WoTServer);

            OnPropertyChanged("VehiclesStatistics");
        }
    }
}

