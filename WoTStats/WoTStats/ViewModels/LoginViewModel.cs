using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.RestModels.PlayerBasicInfo;
using WoTStats.Services.Rest;
using Xamarin.Forms;

namespace WoTStats.ViewModels
{
    public class LoginViewModel : BaseViewModel
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

        //public string PickedServer { set; get; }

        public IList<WoTServer> ServerOptions { set; get; }
        public WoTServer WoTServer
        {
            get { return wotServer; }
            set
            {
                wotServer = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand SubmitCommand { protected set; get; }
        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
            ServerOptions = new List<WoTServer>(new WoTServer[]
            {
                WoTServer.ru,
                WoTServer.eu,
                WoTServer.na,
                WoTServer.asia
            });

            //WoTServer = new WoTServer();

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

                GoToMainShellAsync();
            }
            else
            {
                DisplayInvalidLoginPrompt(playerBasicInfo.Meta.Count.ToString());
                
            }
            
        }

        private async void GoToMainShellAsync()
        {
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync("//main");
        }
    }
}
