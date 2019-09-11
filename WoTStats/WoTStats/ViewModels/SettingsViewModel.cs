using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WoTStats.Services.RestServices.XVM;
using Xamarin.Forms;

namespace WoTStats.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        private string referWN;
        public ICommand LogoutCommand { protected set; get; }

        public string ReferWN
        {
            get { return referWN; }
            set
            {
                referWN = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel()
        {
            LogoutCommand = new Command(async () => await Shell.Current.GoToAsync("login"));
        }

        public async void OnAppearing()
        {
            var provider = new ReferencialWN8DataRestService();

            var result = await provider.GetReferencialWN8DataAsync();

            ReferWN = result.data[0].expWinRate.ToString();
        }
    }
}
