using System;

using System.Collections.Generic;
using WoTStats.Models.DataTemplates;

namespace WoTStats.ViewModels
{
    class TanksViewModel : BaseViewModel
    {
        private IList<VehicleVisibleData> tanksData;

        public IList<VehicleVisibleData> TanksData
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
            TanksData = await App.ContentManager.GetVehiclesVisibleDataAsync();


            // TODO tanks -> vehicles
        }
    }
}

