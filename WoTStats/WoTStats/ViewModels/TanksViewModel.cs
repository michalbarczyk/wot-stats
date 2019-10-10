using System;

using System.Collections.Generic;
using WoTStats.Models.DataTemplates;

namespace WoTStats.ViewModels
{
    class TanksViewModel : BaseViewModel
    {
        private IList<VehicleVisibleData> tanksData;
        private bool isLoading;
        public IList<VehicleVisibleData> TanksData
        {
            set
            {
                tanksData = value;
                OnPropertyChanged();
            }
            get { return tanksData; }
        }

        public bool IsLoading
        {
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
            get { return isLoading; }
        }

        public TanksViewModel()
        {
            App.ContentManager.VehiclesVisibleDataChanged += OnVehiclesVisibleDataChanged;
        }

        private void OnVehiclesVisibleDataChanged(object source, EventArgs args)
        {
            TanksData = App.ContentManager.VehiclesVisibleData;
            IsLoading = false;
        }

        public void OnAppearing()
        {
            IsLoading = true;
            App.ContentManager.CreateVehiclesVisibleData();
            // TODO tanks -> vehicles
        }
    }
}

