using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.DataTemplates;
using WoTStats.Models.RestModels.WoT.PlayerPersonalData;
using WoTStats.Services;
using WoTStats.Services.RestServices.WoT;

namespace WoTStats
{
    public class ContentManager
    {
        private readonly WoTServer server;
        private readonly string accountId;
        public string Nickname { get; }
        public Task<PlayerPersonalData> PlayerPersonalDataTask { get; set; }
        public Task<List<TankVisibleData>> TanksVisibleDataTask { get; set; }

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

            Task.Run(() => PreparePersonalData());
            Task.Run(() => PrepareTanksData());
        }

        private void PreparePersonalData()
        {
            var dataProvider = new PlayerPersonalDataRestService();
            this.PlayerPersonalDataTask = dataProvider.GetPlayerPersonalDataAsync(accountId, server);
        }

        private void PrepareTanksData()
        {
            var dataProvider = new VisibleTanksDataProvider();
            this.TanksVisibleDataTask = dataProvider.GetTanksVisibleDataAsync(accountId, server);
        }

        public async Task<PlayerPersonalData> GetPlayerPersonalDataAsync()
        {
            var result = await this.PlayerPersonalDataTask;
            return result;
        }

        public async Task<List<TankVisibleData>> GetTanksVisibleDataAsync()
        {
            var result = await this.TanksVisibleDataTask;
            return result;
        }

    }
}
