using MonitorApplication_BL.Commands.Commands;
using MonitorApplication_BL.Queries.Interfaces;
using MonitorApplication_Models.OrderModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_BL.Queries.Queries
{
    public class RetriveMineralPricesQuery : IQuery<IEnumerable<MineralPriceData>>
    {
       public int EntriesCount { get; set; } = 10;
    }
}
