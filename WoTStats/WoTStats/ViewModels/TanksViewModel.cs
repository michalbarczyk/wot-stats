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

        public TanksViewModel()
        {
            App.ContentManager.VehiclesVisibleDataCreated += OnVehiclesVisibleDataCreated;
        }

        private void OnVehiclesVisibleDataCreated(object source, EventArgs args)
        {
            TanksData = App.ContentManager.VehiclesVisibleData;
        }

        public void OnAppearing()
        {
            App.ContentManager.CreateVehiclesVisibleData();


            // TODO tanks -> vehicles
        }
    }
}

