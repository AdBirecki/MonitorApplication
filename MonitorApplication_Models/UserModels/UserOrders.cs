﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_Models.UserModels
{
    public class UserOrders
    {
        public int OrderId { get; set; }
        public User User { get; set; }
        public string OrderInfo { get; set; }
        public List<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
