using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoTStats.Models.DatabaseModels;
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
            var usersTask =  App.Database.GetUsersAsync();

            var user = usersTask.Result[0];

            BindingContext = user;
        }

        
    }
}