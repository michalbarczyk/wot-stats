using System;
using System.Collections.Generic;
using WoTStats.Models.DataTemplates;

namespace WoTStats.Services.EventArguments
{
    public class OnVehiclesVisibleDataChangedArgs : EventArgs
    {
        public IList<VehicleVisibleData> VehiclesVisibleData { get; set; }
        
    }
}