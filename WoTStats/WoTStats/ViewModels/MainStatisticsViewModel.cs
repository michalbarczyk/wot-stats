using System;
using System.Diagnostics;
using System.Globalization;
using WoTStats.Services;
using WoTStats.Services.RestServices.WoT;
using Xamarin.Forms;

namespace WoTStats.ViewModels
{
    public class MainStatisticsViewModel : BaseViewModel
    {
        private string nickname;
        private string battles;
        private string maxFrags;
        private string maxDamage;
        private string avgExperience;
        private string hitRatio;
        private string winRate;
        private string personalRating;
        private string wn8;



        public string Nickname
        {
            get
            {
                return nickname;
            }
            set
            {
                nickname = value;
                OnPropertyChanged();
            }
        }
        public string Battles
        {
            get
            {
                return battles;
            }
            set
            {
                battles = value;
                OnPropertyChanged();
            }
        }
        public string MaxFrags
        {
            get
            {
                return maxFrags;
            }
            set
            {
                maxFrags = value;
                OnPropertyChanged();
            }
        }
        public string MaxDamage
        {
            get
            {
                return maxDamage;
            }
            set
            {
                maxDamage = value;
                OnPropertyChanged();
            }
        }
        public string AvgExperience
        {
            get
            {
                return avgExperience;
            }
            set
            {
                avgExperience = value;
                OnPropertyChanged();
            }
        }

        public string WinRate
        {
            get
            {
                return winRate;
            }
            set
            {
                winRate = value;
                OnPropertyChanged();
            }
        }

        public string HitRatio
        {
            get
            {
                return hitRatio;
            }
            set
            {
                hitRatio = value;
                OnPropertyChanged();
            }
        }

        public string PersonalRating
        {
            get
            {
                return personalRating;
            }
            set
            {
                personalRating = value;
                OnPropertyChanged();
            }
        }

        public MainStatisticsViewModel()
        {
            //App.ContentManager.DataPrepared += OnDataPrepared;
            //App.ContentManager.PrepareData();
        }

        /*private void OnDataPrepared(object source, EventArgs args)
        {
            Debug.WriteLine("\n\n OnDataPrepared execution started\n\n");

            var statsAll = App.ContentManager.PlayerPersonalData.statistics.all; //playerPersonalData.statistics.all;

            //GetData = new Command(Cmd);
            Battles = statsAll.battles.ToString();
            MaxDamage = statsAll.max_damage.ToString();
            MaxFrags = statsAll.max_frags.ToString();
            AvgExperience = statsAll.battle_avg_xp.ToString();

            OnPropertyChanged("Battles");
            OnPropertyChanged("MaxDamage");
            OnPropertyChanged("AvgExperience");
            OnPropertyChanged("MaxFrags");
            OnPropertyChanged("Nickname");

            ExampleWN8 = "example ScorpionG WN8 = " + new CalculatorWN8
            {
                AverageDamage = 1272.46,
                AverageSpot = 0.487,
                AverageFrag = 0.778,
                AverageDefense = 0.379,
                WinRate = 0.48276,
                ExpectedDamage = 1432,
                ExpectedSpot = 0.558,
                ExpectedFrag = 0.995,
                ExpectedDefense = 0.627,
                ExpectedWinRate = 0.511
            }.GetWN8Value();

            OnPropertyChanged("ExampleWN8");
        }*/

        public async void OnAppearing()
        {
            // TODO visible data providers for all viewModels

            var playerPersonalData = await App.ContentManager.GetPlayerPersonalDataAsync();

            Nickname = App.ContentManager.Nickname;

            var statsAll = playerPersonalData.statistics.all;

            Battles = statsAll.battles.ToString("D", CultureInfo.InvariantCulture);
            MaxDamage = statsAll.max_damage.ToString("D", CultureInfo.InvariantCulture);
            MaxFrags = statsAll.max_frags.ToString("D", CultureInfo.InvariantCulture);
            AvgExperience = statsAll.battle_avg_xp.ToString();
            WinRate = ((double) statsAll.wins / statsAll.battles).ToString("P", CultureInfo.InvariantCulture);
            HitRatio = ((double)statsAll.hits / statsAll.shots).ToString("P", CultureInfo.InvariantCulture);
            PersonalRating = playerPersonalData.global_rating.ToString("D", CultureInfo.InvariantCulture);
        }
    }
}
