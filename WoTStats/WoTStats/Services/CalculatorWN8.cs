using System;
using System.Collections.Generic;
using System.Text;

namespace WoTStats.Services
{
    public class CalculatorWN8
    {
        public double AverageDamage { get; set; }
        public double ExpectedDamage { get; set; }

        public double AverageSpot { get; set; }
        public double ExpectedSpot { get; set; }

        public double AverageFrag { get; set; }
        public double ExpectedFrag { get; set; }

        public double AverageDefense { get; set; }
        public double ExpectedDefense { get; set; }

        public double WinRate { get; set; }
        public double ExpectedWinRate { get; set; }

        private readonly double winRateConst = 0.71;
        private readonly double damageConst = 0.22;
        private readonly double fragConst = 0.12;
        private readonly double spotConst = 0.22;
        private readonly double defenseConst = 0.10;



        public double GetWN8Value()
        {
            double rDamage = AverageDamage / ExpectedDamage;
            double rSpot = AverageSpot / ExpectedSpot;
            double rFrag = AverageFrag / ExpectedFrag;
            double rDefense = AverageDefense / ExpectedDefense;
            double rWinRate = WinRate / ExpectedWinRate;

            double cWinRate = Math.Max(0.0, fractionUtil(rWinRate, winRateConst));
            double cDamage = Math.Max(0.0, fractionUtil(rDamage, damageConst));
            double cFrag = Math.Max(0.0, Math.Min(cDamage + 0.2, fractionUtil(rFrag, fragConst)));
            double cSpot = Math.Max(0.0, Math.Min(cDamage + 0.1, fractionUtil(rSpot, spotConst)));
            double cDefense = Math.Max(0.0, Math.Min(cDamage + 0.1, fractionUtil(rDefense, defenseConst)));

            double WN8 = 980.0 * cDamage +
                         210.0 * cDamage * cFrag +
                         155.0 * cFrag * cSpot +
                         75.00 * cDefense * cFrag +
                         145.0 * Math.Min(1.8, cWinRate);

            return WN8;

        }

        private double fractionUtil(double value, double constant)
        {
            return (value - constant) / (1.0 - constant);
        }
    }
}
