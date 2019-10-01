using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.DataTemplates;
using WoTStats.Services.RestServices.WoT;

namespace WoTStats.Services.VisibleDataProviders
{
    class PersonalVisibleDataProvider
    {
        public async Task<PersonalVisibleData> GetPersonalVisibleDataAsync(string accountId, WoTServer server)
        {
            var statisticsRestService = new PlayerPersonalStatisticsRestService();

            var personalStatistics = await statisticsRestService.GetPlayerPersonalStatisticsAsync(accountId, server);

            var statsAll = personalStatistics.statistics.all;

            var visibleData = new PersonalVisibleData
            {
                Nickname = App.ContentManager.Nickname,
                Battles = statsAll.battles.ToString("D", CultureInfo.InvariantCulture),
                MaxDamage = statsAll.max_damage.ToString("D", CultureInfo.InvariantCulture),
                MaxFrags = statsAll.max_frags.ToString("D", CultureInfo.InvariantCulture),
                AvgExperience = statsAll.battle_avg_xp.ToString(),
                WinRate = ((double) statsAll.wins / statsAll.battles).ToString("P", CultureInfo.InvariantCulture),
                HitRatio = ((double) statsAll.hits / statsAll.shots).ToString("P", CultureInfo.InvariantCulture),
                PersonalRating = personalStatistics.global_rating.ToString("D", CultureInfo.InvariantCulture)
            };

            return visibleData;
        }

        
    }
}
