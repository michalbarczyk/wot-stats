using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private MainStatisticsViewModel viewModel;
        public MainStatisticsPage()
        {
            BindingContext = viewModel = new MainStatisticsViewModel();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            viewModel.OnAppearing();
            Debug.WriteLine("\n\n OnAppearing invoked in MainStatisticsPage\n\n");
        }
    }
}