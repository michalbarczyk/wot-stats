using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.DataTemplates;
using WoTStats.Services.VisibleDataProviders;

namespace WoTStats
{
    public class ContentManager
    {
        public User CurrentUser { get; private set; }

        private Task<PersonalVisibleData> personalVisibleDataTask;
        private Task<List<VehicleVisibleData>> vehiclesVisibleDataTask;

        public PersonalVisibleData PersonalVisibleData { get; set; }

        public delegate void PersonalVisibleDataChangedEventHandler(object source, EventArgs args);

        public event PersonalVisibleDataChangedEventHandler PersonalVisibleDataChanged;


        public IList<VehicleVisibleData> VehiclesVisibleData { get; set; }

        public delegate void VehiclesVisibleDataChangedEventHandler(object source, EventArgs args);

        public event VehiclesVisibleDataChangedEventHandler VehiclesVisibleDataChanged;


        protected virtual void OnPersonalVisibleDataChanged()
        {
            PersonalVisibleDataChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnVehiclesVisibleDataChanged()
        {
            VehiclesVisibleDataChanged?.Invoke(this, EventArgs.Empty);
        }

        public ContentManager()
        {
            // RefreshCurrentUser();
        }

        public void RefreshCurrentUser(User newCurrentUser)
        {
            this.CurrentUser = newCurrentUser;
        }

        public async void CreatePersonalVisibleData()
        {
            await Task.Run(async () =>
            {
                var dataProvider = new PersonalVisibleDataProvider();
                this.personalVisibleDataTask = dataProvider.GetPersonalVisibleDataAsync(CurrentUser.AccountId, CurrentUser.WoTServer);
                PersonalVisibleData = await this.personalVisibleDataTask;
                OnPersonalVisibleDataChanged();
            });
            
        }

        public async void CreateVehiclesVisibleData()
        {
            await Task.Run(async () =>
            {
                var dataProvider = new VehiclesVisibleDataProvider();
                this.vehiclesVisibleDataTask = dataProvider.GetVehiclesVisibleDataAsync(CurrentUser.AccountId, CurrentUser.WoTServer);
                VehiclesVisibleData = await this.vehiclesVisibleDataTask;
                OnVehiclesVisibleDataChanged();
            });
        }
    }
}
