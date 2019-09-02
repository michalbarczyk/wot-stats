using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WoTStats.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WoTStats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            var loginViewModel = new LoginViewModel();
            this.BindingContext = loginViewModel;
            loginViewModel.DisplayInvalidLoginPrompt += (string s) => DisplayAlert($"Invalid login: {s} matching nicknames", "Type correct login", "OK");
            InitializeComponent();

            NicknameEntry.Completed += (object sender, EventArgs e) =>
               DisplayAlert("DEBUG", $"Quantity of users in DB = {App.Database.GetUsersQuantity().ToString()}", "OK");

        }
    }
}