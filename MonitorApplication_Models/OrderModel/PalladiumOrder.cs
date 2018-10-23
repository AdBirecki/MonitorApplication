using MonitorApplication_Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_Models.OrderModel
{
    public class PalladiumOrder: AbstractOrder
    {
        public double PalladiumFiness { get; set; }
    }
}
