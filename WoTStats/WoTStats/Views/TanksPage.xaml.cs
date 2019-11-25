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
            Debug.WriteLine("\nTanksPage constructor invoked\n");

            BindingContext = viewModel = new TanksViewModel();

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Debug.WriteLine("\nOnAppearing invoked in TanksPage\n");
        }
    }
}