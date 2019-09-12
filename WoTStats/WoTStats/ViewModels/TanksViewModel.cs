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
using WoTStats.Services;
using WoTStats.Services.RestServices.WoT;
using WoTStats.ViewModels.DataTemplates;

namespace WoTStats.ViewModels
{
    class TanksViewModel : BaseViewModel
    {
        private IList<TankVisibleData> tanksData;

        public IList<TankVisibleData> TanksData
        {
            set
            {
                tanksData = value;
                OnPropertyChanged();
            }
            get { return tanksData; }
        }

        public async void OnAppearing()
        {
            var dataProvider = new VisibleTanksDataProvider();

            TanksData = await dataProvider.GetTanksVisibleDataAsync();
        }
    }
}

