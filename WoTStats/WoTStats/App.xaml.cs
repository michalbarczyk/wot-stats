﻿using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using WoTStats.Models.DatabaseModels;
using WoTStats.Services;
using WoTStats.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WoTStats
{
    public partial class App : Application
    {
        private static UserDatabase database;
        private static ContentManager contentManager;

        public static UserDatabase Database
        {
            get
            {
                return database ?? (database = new UserDatabase(Path.Combine(
                           Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Users11.db3")));
            }
        }

        public static ContentManager ContentManager
        {
            get { return contentManager; }
            set { contentManager = value; }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AuthPage();

            // TODO AuthPage elimination -> authentication in one of registered shell pages
                
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
