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
        private readonly WoTServer server;
        private readonly string accountId;
        private Task<PersonalVisibleData> personalVisibleDataTask;
        private Task<List<VehicleVisibleData>> vehiclesVisibleDataTask;

        public string Nickname { get; set; }


        public PersonalVisibleData PersonalVisibleData { get; set; }

        public delegate void PersonalVisibleDataCreatedEventHandler(object source, EventArgs args);

        public event PersonalVisibleDataCreatedEventHandler PersonalVisibleDataCreated;


        public IList<VehicleVisibleData> VehiclesVisibleData { get; set; }

        public delegate void VehiclesVisibleDataCreatedEventHandler(object source, EventArgs args);

        public event VehiclesVisibleDataCreatedEventHandler VehiclesVisibleDataCreated;


        protected virtual void OnPersonalVisibleDataCreated()
        {
            PersonalVisibleDataCreated?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnVehiclesVisibleDataCreated()
        {
            VehiclesVisibleDataCreated?.Invoke(this, EventArgs.Empty);
        }

        public ContentManager()
        {
            var users = App.Database.GetUsers();
            var user = users[0];
            this.accountId = user.AccountId;
            this.server = user.WoTServer;
            this.Nickname = user.Nickname;

            PreparePersonalData();
            PrepareVehiclesData();
        }

        private void PreparePersonalData()
        {
            var dataProvider = new PersonalVisibleDataProvider();
            this.personalVisibleDataTask = dataProvider.GetPersonalVisibleDataAsync(accountId, server);
        }

        private void PrepareVehiclesData()
        {
            var dataProvider = new VehiclesVisibleDataProvider();
            this.vehiclesVisibleDataTask = dataProvider.GetVehiclesVisibleDataAsync(accountId, server);
        }

        public async void CreatePersonalVisibleData()
        {
            PersonalVisibleData = await this.personalVisibleDataTask;
            OnPersonalVisibleDataCreated();
        }

        public async void CreateVehiclesVisibleData()
        {
            VehiclesVisibleData = await this.vehiclesVisibleDataTask;
            OnVehiclesVisibleDataCreated();
        }

        public async Task<List<VehicleVisibleData>> GetVehiclesVisibleDataAsync()
        {
            return await this.vehiclesVisibleDataTask;
        }

    }
}
