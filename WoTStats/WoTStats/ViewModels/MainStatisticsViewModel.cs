using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WoTStats.ViewModels
{
    public class MainStatisticsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public string Nickname { get; set; }
        public string AccountId { get; set; }

        public MainStatisticsViewModel()
        {
            var usersTask = App.Database.GetUsersAsync();

            var user = usersTask.Result[0];

            Nickname = user.Nickname;
            AccountId = user.AccountId;
        }
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }


}
