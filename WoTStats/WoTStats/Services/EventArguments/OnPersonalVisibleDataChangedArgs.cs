using System;
using WoTStats.Models.DataTemplates;

namespace WoTStats.Services.EventArguments
{
    public class OnPersonalVisibleDataChangedArgs : EventArgs
    {
        public PersonalVisibleData PersonalVisibleData { get; set; }
    }
}