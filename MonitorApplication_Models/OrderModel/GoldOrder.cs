using MonitorApplication_Models.Interfaces;
using MonitorApplication_Models.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_Models.RareMinerals
{
    public class GoldOrder : AbstractOrder
    { 
        public double GoldFiness { get; set; }
        public int Carat { get; set; }
    }
}
