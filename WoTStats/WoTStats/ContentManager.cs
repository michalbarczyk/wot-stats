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
        public readonly WoTServer server;
        public readonly string accountId;
        private Task<PersonalVisibleData> personalVisibleDataTask;
        private Task<List<VehicleVisibleData>> vehiclesVisibleDataTask;

        public string Nickname { get; set; }

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
            var users = App.Database.GetUsers();
            var user = users[0];
            this.accountId = user.AccountId;
            this.server = user.WoTServer;
            this.Nickname = user.Nickname;
            PrepareVehiclesData();
            PreparePersonalData();
            
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
            OnPersonalVisibleDataChanged();
        }

        public async void CreateVehiclesVisibleData()
        {
            VehiclesVisibleData = await this.vehiclesVisibleDataTask;
            OnVehiclesVisibleDataChanged();
        }
    }
}
