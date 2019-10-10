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
using WoTStats.Services.RestServices.WoT;
using WoTStats.Services.RestServices.XVM;

namespace WoTStats.Services.VisibleDataProviders
{
    class PersonalVisibleDataProvider
    {
        private ReferencialWN8Data referencialWN8Data;

        public async Task<PersonalVisibleData> GetPersonalVisibleDataAsync(string accountId, WoTServer server)
        {
            var statisticsRestService = new PlayerPersonalStatisticsRestService();

            var personalStatistics = await statisticsRestService.GetPlayerPersonalStatisticsAsync(accountId, server);

            var statsAll = personalStatistics.statistics.all;

            //var wn8 = await new CalculatorWN8Overall().GetWN8OverallValueAsync();

            var visibleData = new PersonalVisibleData
            {
                Nickname = App.ContentManager.Nickname,
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

        
        private async Task<double> CountOverallWN8(string accountId, WoTServer server)
        {
            // TODO to be repaired

            var statisticsRestService = new PlayerVehiclesStatisticsRestService();

            var vehiclesStatistics = await statisticsRestService.GetPlayerVehiclesStatisticsAsync(accountId, server);

            var wn8List = new List<double>();

            double wnSum = 0;
            double wnRes = 0;

            foreach (var stat in vehiclesStatistics.Where(d => d.all.battles > 0)) ///////
            {
                wnSum += await GetTankWN8(stat) * stat.all.battles;
                wnRes += stat.all.battles;

                wn8List.Add(await GetTankWN8(stat));
            }

            return wnSum / wnRes; //wn8List.Average();
        }

        private async Task<double> GetTankWN8(PlayerVehicleStatistics vehicleStats)
        {
            if (referencialWN8Data == null)
            {
                var dataProvider = new ReferentialWN8DataRestService();
                referencialWN8Data = await dataProvider.GetReferencialWN8DataAsync();
            }

            var tankReferentialData = referencialWN8Data.data
                .Where(data => data.IDNum == Int32.Parse(vehicleStats.tank_id))
                .FirstOrDefault();

            var wn8calc = new CalculatorWN8
            {
                AverageDamage = (double)vehicleStats.all.damage_dealt / vehicleStats.all.battles,
                AverageDefense = (double)vehicleStats.all.dropped_capture_points / vehicleStats.all.battles,
                AverageFrag = (double)vehicleStats.all.frags / vehicleStats.all.battles,
                AverageSpot = (double)vehicleStats.all.spotted / vehicleStats.all.battles,
                WinRate = (double)vehicleStats.all.wins / vehicleStats.all.battles,

                ExpectedDamage = tankReferentialData.expDamage,
                ExpectedDefense = tankReferentialData.expDef,
                ExpectedFrag = tankReferentialData.expFrag,
                ExpectedSpot = tankReferentialData.expSpot,
                ExpectedWinRate = tankReferentialData.expWinRate
            };

            return wn8calc.GetWN8Value();
        }


    }
}
