using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.RestModels.WoT.PlayerBasicInfo;
using WoTStats.Services.RestServices.WoT;
using WoTStats.Services.UserAuthentication;
using WoTStats.Views;
using Xamarin.Forms;

namespace WoTStats.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        public Action DisplayInvalidLoginPrompt;
        
        private string nickname;

        private WoTServer wotServer;

        public string Nickname
        {
            get { return nickname; }
            set
            {
                nickname = value;
                OnPropertyChanged();
            }
        }
        public WoTServer WoTServer
        {
            get { return wotServer; }
            set
            {
                wotServer = value;
                OnPropertyChanged();
            }
        }

        public IList<WoTServer> ServerOptions { set; get; }

        public ICommand SubmitCommand { protected set; get; }

        private AuthEngine authEngine;
        public AuthViewModel()
        {
            authEngine = new AuthEngine();
            SubmitCommand = new Command(OnSubmit);
            ServerOptions = new List<WoTServer> 
            {
                WoTServer.ru,
                WoTServer.eu,
                WoTServer.na,
                WoTServer.asia
            };
        }
        public async void OnSubmit()
        {
            var authOk = await authEngine.Authenticate(Nickname, WoTServer);

            if (authOk)
            {
                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync("//main/personal");
            }
            else
            {
                DisplayInvalidLoginPrompt();
            }
        }
    }
}
