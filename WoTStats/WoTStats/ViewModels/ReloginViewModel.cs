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
    public class ReloginViewModel : INotifyPropertyChanged
    {
        public delegate void UserInsertedEventHandler(object source, EventArgs args);

        public event UserInsertedEventHandler UserInserted;

        protected virtual void OnUserInserted()
        {
            if (UserInserted != null)
                UserInserted(this, EventArgs.Empty);
        }


        public Action<string> DisplayInvalidLoginPrompt;

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
        public ReloginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }
        public async void OnSubmit()
        {
            WoTApiService apiService = new WoTApiService();
            AccountBasicInfo accountBasicInfo = await apiService.GetAccountBasicInfoAsync(nickname);

            if (accountBasicInfo.Meta.Count > 0)
            {
                User user = new User
                {
                    Nickname = accountBasicInfo.Datas[0].Nickname,
                    AccountId = accountBasicInfo.Datas[0].AccountId
                };

                var allUsers = await App.Database.GetUsersAsync();
                if (allUsers.Count > 0)
                {
                    var userToBeDeleted = allUsers[0];
                    await App.Database.DeleteUserAsync(userToBeDeleted);
                }


                var result = await App.Database.InsertUserAsync(user);
                OnUserInserted();

                await Shell.Current.GoToAsync("//main/personal");
            }
            else
            {
                DisplayInvalidLoginPrompt(accountBasicInfo.Meta.Count.ToString());

            }

        }

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
