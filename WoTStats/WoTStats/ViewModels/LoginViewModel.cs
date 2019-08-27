using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using WoTStats.Models;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.RestModels;
using WoTStats.Services;
using Xamarin.Forms;

namespace WoTStats.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string nickname;
        public string Nickname
        {
            get { return nickname; }
            set
            {
                nickname = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand SubmitCommand { protected set; get; }
        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }
        public async void OnSubmit()
        {
            WoTApiService apiService = new WoTApiService();
            AccountBasicInfo accountBasicInfo = await apiService.GetAccountBasicInfoAsync(nickname);

            if (accountBasicInfo.Meta.Count == 1)
            {
                User user = new User
                {
                    Nickname = accountBasicInfo.Datas[0].Nickname,
                    AccountId = accountBasicInfo.Datas[0].AccountId
                };

                var result = await App.Database.InsertUserAsync(user);

                Application.Current.MainPage = new MainShell();
            }
            else
            {
                DisplayInvalidLoginPrompt();
            }
            
        }

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
