using MonitorApplication_Models.OrderModel;
using MonitorApplication_Models.Units;
using MonitorApplication_Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_Models.Interfaces
{
    public abstract class AbstractOrder
    {
        public int OrderId { get; set; }
        public Currency Currencies { get; set; }
        public WeightUnits Units { get; set; }
        public MineralPriceData PriceChange {get; set;}
        public PurchaseOrder PurchaseOrder { get; set; }
        public double PricePerUnit { get; set; }  
        public int Quantity { get; set; }
    }
}
