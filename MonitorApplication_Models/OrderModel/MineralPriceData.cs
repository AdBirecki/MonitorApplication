using MonitorApplication_Models.Interfaces;
using MonitorApplication_Models.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_Models.OrderModel
{
    public class MineralPriceData
    {
        public Currency Currency { get; set; }

        public AbstractOrder Order { get; set; }

        public double GoldPrice { get; set; }

        public double SilverPrice { get; set; }

        public double GoldChange { get; set; }

        public double SilverChange { get; set; }

        public double GoldPcExchange { get; set; }

        public double SilverPcExchange { get; set; }

        public double GoldClose { get; set; }

        public double SilverClose { get; set; }

        public long timestamp { get; set; }

        public DateTime dateTime { get; set; }
    }
}
