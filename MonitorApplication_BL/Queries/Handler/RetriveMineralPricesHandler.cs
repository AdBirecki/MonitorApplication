using Microsoft.Extensions.Logging;
using MonitorApplication_BL.Queries.Interfaces;
using MonitorApplication_BL.Queries.Queries;
using MonitorApplication_Models.OrderModel;
using MonitorApplication_USERS_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MonitorApplication_BL.Queries.Handler
{
    public class RetriveMineralPricesHandler : IQueryHandler<RetriveMineralPricesQuery, IEnumerable<MineralPriceData>>
    {
        private readonly IOrdersDbFacade _orderDbfacade;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public RetriveMineralPricesHandler(
            ILoggerFactory loggerFactory,
            IOrdersDbFacade orderDbfacade)
        {
            _logger = loggerFactory.CreateLogger<RetriveMineralPricesQuery>();
            _orderDbfacade = orderDbfacade;
        }

        public IEnumerable<MineralPriceData> Execute(RetriveMineralPricesQuery tQuery)
        {
            IEnumerable<MineralPriceData> mineralData = null;
            try
            {
                mineralData = _orderDbfacade.MineraPriceData?.ToList();
            }
            catch (SqlException exception)
            {
                string typeName = nameof(RetriveMineralPricesHandler);
                _logger.Log(LogLevel.Error, $" {typeName} caused an exception { exception.Message} ");
            }
            return mineralData;
        }
    }
}
