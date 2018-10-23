using MonitorApplication_Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_Models.UserModels
{
    public class PurchaseOrder
    {
        public int PurchaseId { get; set; }
        public UserOrders UserOrders { get; set; }
        public DateTime dateTime { get; set; }
        public long TimeStamp { get; set; }
        public string Info { get; set; }
        public virtual List<AbstractOrder> OrderData { get; set; }
    }
}
