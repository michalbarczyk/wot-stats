using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using WoTStats.Models.DatabaseModels;

namespace WoTStats.ViewModels
{
    public class MainStatisticsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public string Nickname { get; set; }
        public string AccountId { get; set; }

        public IList<User> TmpList { get; set; }

        public MainStatisticsViewModel()
        {
            var usersTask = App.Database.GetUsersAsync();

            var user = usersTask.Result[0];

            Nickname = user.Nickname;
            AccountId = user.AccountId;

            TmpList = new List<User>();

            for (int i = 0; i < 23; i++)
            {
                TmpList.Add(new User
                {
                    Nickname = "named item " + i.ToString(),
                    AccountId = (2 * i - 1 + i * i).ToString()
                });
            }
        }
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        
    }
}
