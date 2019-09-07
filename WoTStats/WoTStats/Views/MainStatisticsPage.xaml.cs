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
            /*var TmpList = new List<User>();

            for (int i = 0; i < 23; i++)
            {
                TmpList.Add(new User
                {
                    Nickname = "named item " + i.ToString(),
                    AccountId = (i + i * i).ToString(),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c8/Golden_Snub-nosed_Monkeys%2C_Qinling_Mountains_-_China.jpg/165px-Golden_Snub-nosed_Monkeys%2C_Qinling_Mountains_-_China.jpg"
                });
            }

            cv.ItemsSource = TmpList;*/
        }

        protected override void OnAppearing() => viewModel.OnAppearing();
        
    }
}