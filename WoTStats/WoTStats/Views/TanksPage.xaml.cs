using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WoTStats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("Name", "name")]
    public partial class TanksPage : ContentPage
    {
        public string Name { set; get; }



        public TanksPage()
        {
            InitializeComponent();
            BindingContext = Name;
        }
    }
}