using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.DataTemplates;
using WoTStats.Models.RestModels.WoT.PlayerVehicleStatistics;
using WoTStats.Models.RestModels.XVM;
using WoTStats.Services.EventArguments;
using WoTStats.Services.RestServices.WoT;
using WoTStats.Services.RestServices.XVM;

namespace WoTStats.Services.VisibleDataProviders
{
    class PersonalVisibleDataProvider
    {

        public delegate void PersonalVisibleDataChangedEventHandler(object source, OnPersonalVisibleDataChangedArgs args);

        public event PersonalVisibleDataChangedEventHandler PersonalVisibleDataChanged;

        protected virtual void OnPersonalVisibleDataChanged(OnPersonalVisibleDataChangedArgs args)
        {
            PersonalVisibleDataChanged?.Invoke(this, args);
        }

        public async void ProvidePersonalVisibleData(User user)
        {
            var personalVisibleData = await GetPersonalVisibleDataAsync(user);
            OnPersonalVisibleDataChanged(new OnPersonalVisibleDataChangedArgs
            {
                PersonalVisibleData = personalVisibleData
            });
            

        }

        private async Task<PersonalVisibleData> GetPersonalVisibleDataAsync(User user)
        {
            var statisticsRestService = new PlayerPersonalStatisticsRestService();

            var personalStatistics = await statisticsRestService.GetPlayerPersonalStatisticsAsync(user.AccountId, user.WoTServer);

            var statsAll = personalStatistics.statistics.all;

            //var wn8 = await new CalculatorWN8Overall().GetWN8OverallValueAsync();

            var visibleData = new PersonalVisibleData
            {
                Nickname = user.Nickname,
                Battles = statsAll.battles.ToString("D", CultureInfo.InvariantCulture),
                MaxDamage = statsAll.max_damage.ToString("D", CultureInfo.InvariantCulture),
                MaxFrags = statsAll.max_frags.ToString("D", CultureInfo.InvariantCulture),
                AvgExperience = statsAll.battle_avg_xp.ToString(),
                WinRate = ((double) statsAll.wins / statsAll.battles).ToString("P", CultureInfo.InvariantCulture),
                HitRatio = ((double) statsAll.hits / statsAll.shots).ToString("P", CultureInfo.InvariantCulture),
                PersonalRating = personalStatistics.global_rating.ToString("D", CultureInfo.InvariantCulture),
                WN8 = "(-_-)" // wn8.ToString("F", CultureInfo.InvariantCulture) //
            };

            return visibleData;
        }

        
        


    }
}
