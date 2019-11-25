using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class AuthPage : ContentPage
    {
        private AuthViewModel viewModel;
        public AuthPage()
        {
            BindingContext = viewModel = new AuthViewModel();
            viewModel.DisplayInvalidLoginPrompt += () => 
                    DisplayAlert("Invalid login: 0 matching nicknames found", "Type correct login", "OK");

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Debug.WriteLine("\nOnAppearing invoked in AuthPage\n");
        }
    }
}