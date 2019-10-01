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
       
        public ICommand LogoutCommand { protected set; get; }

        public SettingsViewModel()
        {
            LogoutCommand = new Command(async () => await Shell.Current.GoToAsync("auth"));
        }

        public async void OnAppearing()
        {
           
        }
    }
}
