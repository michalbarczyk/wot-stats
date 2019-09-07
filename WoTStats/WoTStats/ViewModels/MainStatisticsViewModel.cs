using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.RestModels;
using WoTStats.Services.Rest;
using Xamarin.Forms;

namespace WoTStats.ViewModels
{
    public class MainStatisticsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public string Nickname { get; set; }
        public string AccountId { get; set; }
        public string WinRate { get; set; }

        public ICommand GetData { get; set; }

        public IList<User> TmpList { get; set; }

        public MainStatisticsViewModel()
        {
            var usersTask = App.Database.GetUsersAsync();

            var user = usersTask.Result[0];

            Nickname = user.Nickname;
            AccountId = user.AccountId;

            //PlayerPersonalDataRestService restService = new PlayerPersonalDataRestService();

            //var playerPersonalData = restService
            //    .GetPlayerPersonalDataAsync(user.AccountId, user.WoTServer).Result;
                

            //var statsAll = playerPersonalData.statistics.all;

            GetData = new Command(Cmd);

            WinRate = 15.45.ToString(); //((double) statsAll.wins / statsAll.battles);

            /*TmpList = new List<User>();

            for (int i = 0; i < 23; i++)
            {
                TmpList.Add(new User
                {
                    Nickname = "named item " + i.ToString(),
                    AccountId = (2 * i - 1 + i * i).ToString()
                });
            }*/

            


        }

        private async void Cmd()
        {
            var users = await App.Database.GetUsersAsync();

            var user = users[0];

            PlayerPersonalDataRestService restService = new PlayerPersonalDataRestService();

            var playerPersonalData = await restService
                .GetPlayerPersonalDataAsync(user.AccountId, user.WoTServer);


            var statsAll = playerPersonalData.statistics.all;

            WinRate = playerPersonalData.nickname;
            OnPropertyChanged("WinRate");
        }

        void OnPropertyChanged(/*[CallerMemberName]*/ string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
