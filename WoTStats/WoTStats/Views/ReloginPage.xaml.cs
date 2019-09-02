using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoTStats.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WoTStats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReloginPage : ContentPage
    {
        public ReloginPage()
        {
            var reloginViewModel = new ReloginViewModel();
            this.BindingContext = reloginViewModel;
            reloginViewModel.DisplayInvalidLoginPrompt += (string s) => DisplayAlert($"Invalid login: {s} matching nicknames", "Type correct login", "OK");
            InitializeComponent();
        }
    }
}