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
        public string Nickname { get; }
        private Task<PersonalVisibleData> PersonalVisibleDataTask { get; set; }
        private Task<List<VehicleVisibleData>> VehiclesVisibleDataTask { get; set; }

        public delegate void DataPreparedEventHandler(object source, EventArgs args);

        public event DataPreparedEventHandler DataPrepared;

        protected virtual void OnDataPrepared()
        {
            DataPrepared?.Invoke(this, EventArgs.Empty);
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
            this.PersonalVisibleDataTask = dataProvider.GetPersonalVisibleDataAsync(accountId, server);
        }

        private void PrepareVehiclesData()
        {
            var dataProvider = new VehiclesVisibleDataProvider();
            this.VehiclesVisibleDataTask = dataProvider.GetVehiclesVisibleDataAsync(accountId, server);
        }

        public async Task<PersonalVisibleData> GetPersonalVisibleDataAsync()
        {
            return await this.PersonalVisibleDataTask;
        }

        public async Task<List<VehicleVisibleData>> GetVehiclesVisibleDataAsync()
        {
            return await this.VehiclesVisibleDataTask;
        }

    }
}
