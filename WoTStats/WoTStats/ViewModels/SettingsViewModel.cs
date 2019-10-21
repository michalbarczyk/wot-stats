using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WoTStats.Services.RestServices.XVM;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WoTStats.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
       
        public ICommand LogoutCommand { protected set; get; }
        public ICommand TankIconAuthorCommand { protected set; get; }

        public string TankIconAuthorUrl { get; }

        public SettingsViewModel()
        {
            TankIconAuthorUrl = "www.flaticon.com/authors/freepik";
            LogoutCommand = new Command(async () => await Shell.Current.GoToAsync("auth"));
            TankIconAuthorCommand = new Command(async () =>
            {
                await Browser.OpenAsync(new Uri($"https://{TankIconAuthorUrl}"), BrowserLaunchMode.SystemPreferred);
            });
        }

        //public async void OnAppearing()
        //{
           
        //}
    }
}
