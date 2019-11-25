using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoTStats.Models.DatabaseModels;
using WoTStats.ViewModels;
using WoTStats.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WoTStats
{
    [DesignTimeVisible(false)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("auth", typeof(AuthPage));
        }
    }
}
