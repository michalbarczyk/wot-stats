using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Input;
using WoTStats.Services;
using WoTStats.Services.EventArguments;
using WoTStats.Services.RestServices.WoT;
using WoTStats.Services.VisibleDataProviders;
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

        private bool isLoading;

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

        public ICommand RefreshCommand { protected set; get; }

        public bool IsLoading
        {
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
            get { return isLoading; }
        }

        private PersonalVisibleDataProvider personalVisibleDataProvider;

        public PersonalStatisticsViewModel()
        {
            personalVisibleDataProvider = new PersonalVisibleDataProvider();
            personalVisibleDataProvider.PersonalVisibleDataChanged += OnPersonalVisibleDataChanged;
            personalDataCreated = false;

        }

        private void OnPersonalVisibleDataChanged(object source, OnPersonalVisibleDataChangedArgs args)
        {
            var visibleData = args.PersonalVisibleData;

            Nickname = visibleData.Nickname;
            Battles = visibleData.Battles;
            MaxDamage = visibleData.MaxDamage;
            MaxFrags = visibleData.MaxFrags;
            AvgExperience = visibleData.AvgExperience;
            HitRatio = visibleData.HitRatio;
            WinRate = visibleData.WinRate;
            PersonalRating = visibleData.PersonalRating;
            WN8 = visibleData.WN8;

            IsLoading = false;
        }


        private bool personalDataCreated;

        public void OnAppearing()
        {
            if (!personalDataCreated)
            {
                personalDataCreated = true;
                IsLoading = true;
                personalVisibleDataProvider.ProvidePersonalVisibleData(App.Database.GetUsers()[0]);
            }
                
        }
    }
}
