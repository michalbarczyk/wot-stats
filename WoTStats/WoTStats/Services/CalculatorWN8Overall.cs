using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoTStats.Models.DatabaseModels;
using WoTStats.Services.RestServices.WoT;
using WoTStats.Services.RestServices.XVM;

/**
 * experimental class providing overall WN8 value per player
 */

namespace WoTStats.Services
{
    public class CalculatorWN8Overall
    {

        private readonly double winRateConst = 0.71;
        private readonly double damageConst = 0.22;
        private readonly double fragConst = 0.12;
        private readonly double spotConst = 0.22;
        private readonly double defenseConst = 0.10;



        public async Task<double> GetWN8OverallValueAsync()
        {

            var vehicleStatsProvider = new PlayerVehiclesStatisticsRestService();
            var stats = await vehicleStatsProvider
                .GetPlayerVehiclesStatisticsAsync(App.Database.GetUsers()[0].AccountId, App.Database.GetUsers()[0].WoTServer);
                

            
            
            var dataProvider = new ReferentialWN8DataRestService();
            var referencialWN8Data =  await dataProvider.GetReferencialWN8DataAsync();
            

            

            double RatingWN8_TotalDamage = 0;
            double RatingWN8_TotalFrag = 0;
            double RatingWN8_TotalSpot = 0;
            double RatingWN8_TotalDef = 0;
            double RatingWN8_TotalWinrate = 0;

            double RatingWN8_ExpDamage = 0;
            double RatingWN8_ExpFrag = 0;
            double RatingWN8_ExpSpot = 0;
            double RatingWN8_ExpDef = 0;
            double RatingWN8_ExpWinrate = 0;

            foreach (var s in stats.Where(d => d.all.battles > 0))
            {
                var exp = referencialWN8Data.data
                    .Where(d => d.IDNum == Int32.Parse(s.tank_id))
                    .FirstOrDefault();

                RatingWN8_TotalDamage += s.all.damage_dealt;
                RatingWN8_ExpDamage += exp.expDamage * s.all.battles;
                
                RatingWN8_TotalFrag += s.all.frags;
                RatingWN8_ExpFrag += exp.expFrag * s.all.battles;
                RatingWN8_TotalSpot += s.all.spotted;
                RatingWN8_ExpSpot += exp.expSpot * s.all.battles;
                RatingWN8_TotalDef += s.all.dropped_capture_points;
                RatingWN8_ExpDef += exp.expDef * s.all.battles;
                RatingWN8_ExpWinrate += (exp.expWinRate) / 100 * s.all.battles;
            }



            double rDamage = RatingWN8_TotalDamage / RatingWN8_ExpDamage;
            double rSpot = RatingWN8_TotalSpot / RatingWN8_ExpSpot;
            double rFrag = RatingWN8_TotalFrag / RatingWN8_ExpFrag;
            double rDefense = RatingWN8_TotalDef / RatingWN8_ExpDef;
            double rWinRate = RatingWN8_TotalWinrate / RatingWN8_ExpWinrate;



            double cWinRate = Math.Max(0.0, fractionUtil(rWinRate, winRateConst));
            double cDamage = Math.Max(0.0, fractionUtil(rDamage, damageConst));
            double cFrag = Math.Max(0.0, Math.Min(cDamage + 0.2, fractionUtil(rFrag, fragConst)));
            double cSpot = Math.Max(0.0, Math.Min(cDamage + 0.1, fractionUtil(rSpot, spotConst)));
            double cDefense = Math.Max(0.0, Math.Min(cDamage + 0.1, fractionUtil(rDefense, defenseConst)));

            double WN8 = 980.0 * cDamage +
                         210.0 * cDamage * cFrag +
                         155.0 * cFrag * cSpot +
                         75.00 * cDefense * cFrag +
                         145.0 * Math.Min(1.8, cWinRate);

            return WN8;

        }

        private double fractionUtil(double value, double constant)
        {
            return (value - constant) / (1.0 - constant);
        }
    }
}
