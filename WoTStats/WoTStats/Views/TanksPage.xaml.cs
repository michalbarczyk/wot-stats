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
    [QueryProperty("Name", "name")]
    public partial class TanksPage : ContentPage
    {
        private TanksViewModel viewModel;

        public IList<User> VehiclesStatistics { get; set; }

        public TanksPage()
        {
            Debug.WriteLine("\n\n TanksPage constructor invoked \n\n");

            BindingContext = viewModel = new TanksViewModel();

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            viewModel.OnAppearing();
            Debug.WriteLine("\n\n OnAppearing invoked in TanksPage\n\n");
        }
    }
}