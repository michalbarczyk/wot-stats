using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.RestModels.WoT.PlayerBasicInfo;
using WoTStats.Services.RestServices.WoT;
using WoTStats.Views;
using Xamarin.Forms;

namespace WoTStats.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        public Action<string> DisplayInvalidLoginPrompt;
        
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
        public AuthViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
            ServerOptions = new List<WoTServer>(new WoTServer[]
            {
                WoTServer.ru,
                WoTServer.eu,
                WoTServer.na,
                WoTServer.asia
            });
        }
        public async void OnSubmit()
        {
            PlayerBasicInfoRestService apiService = new PlayerBasicInfoRestService();
            PlayerBasicInfo playerBasicInfo = await apiService.GetPlayerBasicInfoAsync(nickname, wotServer);

            if (playerBasicInfo.Meta.Count > 0)
            {
                User user = new User
                {
                    Nickname = playerBasicInfo.Datas[0].Nickname,
                    AccountId = playerBasicInfo.Datas[0].AccountId,
                    WoTServer = wotServer
                };

                var allUsers = await App.Database.GetUsersAsync();
                if (allUsers.Count > 0)
                {
                    var userToBeDeleted = allUsers[0];
                    await App.Database.DeleteUserAsync(userToBeDeleted);
                }

                
                await App.Database.InsertUserAsync(user);

                PrepareAndGoToMainShellAsync();
            }
            else
            {
                DisplayInvalidLoginPrompt(playerBasicInfo.Meta.Count.ToString());
            }
            
        }

        public void OnAppearing()
        {
            if (App.Database.GetUsersQuantity() > 0 && Application.Current.MainPage is AuthPage)
            {
                PrepareAndGoToMainShellAsync();
            }

            // else new user to be authenticated 
        }

        private async void PrepareAndGoToMainShellAsync()
        {
            App.ContentManager = new ContentManager();
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync("//main");
        }
    }
}
