using System;
using System.Collections.Generic;
using System.Text;

namespace WoTStats.Models.RestModels.XVM
{
    public class ReferencialWN8Data
    {
        public List<Data> data { get; set; }
        public Header header { get; set; }
    }

    public class Data
    {
        public int IDNum { get; set; }
        public double expDef { get; set; }
        public double expFrag { get; set; }
        public double expSpot { get; set; }
        public double expDamage { get; set; }
        public double expWinRate { get; set; }
    }

    public class Header
    {
        public string url { get; set; }
        public string source { get; set; }
        public string version { get; set; }
    }
}
