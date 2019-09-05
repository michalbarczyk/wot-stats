using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoTStats.Models.DatabaseModels;
using WoTStats.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WoTStats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainStatisticsPage : ContentPage
    {
        public  MainStatisticsPage()
        {

            
            InitializeComponent();
   
        }

        protected override void OnAppearing()
        {
            var mainStatisticsViewModel = new MainStatisticsViewModel();
            BindingContext = mainStatisticsViewModel;
        }
    }
}