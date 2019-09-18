﻿using System;
using System.Diagnostics;
using WoTStats.Services;
using WoTStats.Services.RestServices.WoT;
using Xamarin.Forms;

namespace WoTStats.ViewModels
{
    public class MainStatisticsViewModel : BaseViewModel
    {
        private string nickname;
        private string accountId;
        private string battles;
        private string maxFrags;
        private string maxDamage;
        private string avgExperience;
        private string exampleWN8;

        public string Nickname { get; set; }
        public string AccountId { get; set; }
        public string Battles { get; set; }
        public string MaxFrags { get; set; }
        public string MaxDamage { get; set; }
        public string AvgExperience { get; set; }

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
            var playerPersonalData = await App.ContentManager.GetPlayerPersonalDataAsync();

            Nickname = App.ContentManager.Nickname;

            var statsAll = playerPersonalData.statistics.all;

            Battles = statsAll.battles.ToString();
            MaxDamage = statsAll.max_damage.ToString();
            MaxFrags = statsAll.max_frags.ToString();
            AvgExperience = statsAll.battle_avg_xp.ToString();

            OnPropertyChanged("Battles");
            OnPropertyChanged("MaxDamage");
            OnPropertyChanged("AvgExperience");
            OnPropertyChanged("MaxFrags");
            OnPropertyChanged("Nickname");
        }
    }
}
