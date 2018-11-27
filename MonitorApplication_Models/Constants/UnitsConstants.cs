using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_Models.Units
{
    
    public enum WeightUnits { Ounce = 0, Kilogram = 1, Gram = 2}
    public enum Currency { PLN = 0,  USD = 1, EUR = 2 }
    public enum CrudeMineralKind {Gold = 0, Silver = 1, Palladium = 2 }
    static class UnitsConstants
    {

    }
    public static class CurrencyNames
    {
        public const string USD = "USD";
        public const string EUR = "EUR";
        public const string PLN = "PLN";
    }
}
