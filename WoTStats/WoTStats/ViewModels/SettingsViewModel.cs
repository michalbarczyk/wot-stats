using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace WoTStats.ViewModels
{
    class SettingsViewModel
    {
        public ICommand LogoutCommand { protected set; get; }


        public SettingsViewModel()
        {
            LogoutCommand = new Command(async () => await Shell.Current.GoToAsync("login"));
            

            
        }

        
    }
}
