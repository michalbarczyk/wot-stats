using System;
using System.Diagnostics;
using System.Globalization;
using WoTStats.Services;
using WoTStats.Services.RestServices.WoT;
using Xamarin.Forms;

namespace WoTStats.ViewModels
{
    public class PersonalStatisticsViewModel : BaseViewModel
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

        public string WN8
        {
            get
            {
                return wn8;
            }
            set
            {
                wn8 = value;
                OnPropertyChanged();
            }
        }

        public PersonalStatisticsViewModel()
        {
            //App.ContentManager.DataPrepared += OnDataPrepared;
            //App.ContentManager.PrepareData();
        }

        private void OnDataPrepared(object source, EventArgs args)
        {
            
        }

        public async void OnAppearing()
        {
            var visibleData = await App.ContentManager.GetPersonalVisibleDataAsync();

            Nickname = visibleData.Nickname;
            Battles = visibleData.Battles;
            MaxDamage = visibleData.MaxDamage;
            MaxFrags = visibleData.MaxFrags;
            AvgExperience = visibleData.AvgExperience;
            HitRatio = visibleData.HitRatio;
            WinRate = visibleData.WinRate;
            PersonalRating = visibleData.PersonalRating;
            WN8 = "[wn8]";
        }
    }
}
