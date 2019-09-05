using System;
using System.IO;
using System.Runtime.CompilerServices;
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

        public static UserDatabase Database
        {
            get
            {
                return database ?? (database = new UserDatabase(Path.Combine(
                           Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Users06.db3")));
            }
        }
        public App()
        {
            InitializeComponent();

            if (Database.GetUsersQuantity() == 0)
            {
                MainPage = new LoginPage();
            }
            else
            {
                MainPage = new AppShell();
            }
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
