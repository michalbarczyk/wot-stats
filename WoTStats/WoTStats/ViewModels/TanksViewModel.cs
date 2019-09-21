using System;

using System.Collections.Generic;
using WoTStats.Models.DataTemplates;

namespace WoTStats.ViewModels
{
    class TanksViewModel : BaseViewModel
    {
        private IList<TankVisibleData> tanksData;

        public IList<TankVisibleData> TanksData
        {
            set
            {
                tanksData = value;
                OnPropertyChanged();
            }
            get { return tanksData; }
        }

        public async void OnAppearing()
        {
            //var dataProvider = new VisibleTanksDataProvider();

            TanksData = await App.ContentManager.GetTanksVisibleDataAsync(); //await dataProvider.GetTanksVisibleDataAsync();
        }
    }
}

