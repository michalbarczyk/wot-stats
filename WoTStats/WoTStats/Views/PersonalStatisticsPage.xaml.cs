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
    public partial class PersonalStatisticsPage : ContentPage
    {
        private PersonalStatisticsViewModel viewModel;
        public PersonalStatisticsPage()
        {
            BindingContext = viewModel = new PersonalStatisticsViewModel();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            
            Debug.WriteLine("\n\n XX: OnAppearing invoked in MainStatisticsPage\n\n");
            viewModel.OnAppearing();
        }
    }
}