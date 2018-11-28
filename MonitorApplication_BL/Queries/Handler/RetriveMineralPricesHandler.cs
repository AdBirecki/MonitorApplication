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
        private readonly ILogger _logger;
        private const string HandlerName = nameof(RetriveMineralPricesHandler);

        public RetriveMineralPricesHandler(
            ILoggerFactory loggerFactory,
            IOrdersDbFacade orderDbfacade)
        {
            _logger = loggerFactory.CreateLogger<RetriveMineralPricesHandler>();
            _orderDbfacade = orderDbfacade;
        }

        public IEnumerable<MineralPriceData> Execute(RetriveMineralPricesQuery tQuery)
        {
            IEnumerable<MineralPriceData> mineralData = null;
            try
            {
                mineralData = _orderDbfacade.MineraPriceData?
                    .OrderByDescending( mpd => mpd.Timestamp)
                    .Take(tQuery.EntriesCount)
                    .ToList();
            }
            catch (SqlException exception)
            {
                _logger.Log(LogLevel.Error, $" {HandlerName} caused an Sqlexception { exception.Message} ");
                throw;
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, $" {HandlerName} caused an Exception { exception.Message} ");
                throw;
            }

            return mineralData;
        }
    }
}
