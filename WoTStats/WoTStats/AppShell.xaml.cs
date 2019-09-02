using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoTStats.Views;
using Xamarin.Forms;

namespace WoTStats
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("relogin", typeof(ReloginPage));

            /*if (App.Database.GetUsersQuantity() == 0)
            {
                Shell.Current.GoToAsync("login").Wait();


            }

            

            /*BindingContext = new Command(async () =>
            {
                

                //await Shell.Current.GoToAsync("login");
                //Shell.Current.FlyoutIsPresented = false;
                //Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;

            });*/


            
        }
    }

    
}
