using System;

using System.Collections.Generic;
using WoTStats.Models.DataTemplates;
using WoTStats.Services.EventArguments;
using WoTStats.Services.VisibleDataProviders;

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

        private VehiclesVisibleDataProvider vehiclesVisibleDataProvider;
        public TanksViewModel()
        {
            vehiclesVisibleDataProvider = new VehiclesVisibleDataProvider();
            vehiclesVisibleDataProvider.VehiclesVisibleDataChanged += OnVehiclesVisibleDataChanged;
        }

        private void OnVehiclesVisibleDataChanged(object source, OnVehiclesVisibleDataChangedArgs args)
        {
            TanksData = args.VehiclesVisibleData;
            IsLoading = false;
        }

        private bool vehiclesDataCreated;
        public void OnAppearing()
        {
            if (!vehiclesDataCreated)
            {
                vehiclesDataCreated = true;
                IsLoading = true;
                vehiclesVisibleDataProvider.ProvideVehiclesVisibleData(App.Database.GetUsers()[0]);
            }

            
            
        }
    }
}

