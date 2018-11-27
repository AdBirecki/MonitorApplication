using Microsoft.Extensions.Logging;
using MonitorApplication_BL.Queries.Interfaces;
using MonitorApplication_BL.Queries.RetriveUserQueries;
using MonitorApplication_Models.UserModels;
using MonitorApplication_USERS_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MonitorApplication_BL.Queries.Handler
{
    public class RetriveUsersHandler : IQueryHandler<RetriveUsersQuery, IEnumerable<User>>
    {
        private readonly IOrdersDbFacade _orderDbfacade;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public RetriveUsersHandler(
            ILoggerFactory loggerFactory, 
            IOrdersDbFacade orderDbfacade)
        {
            _logger = loggerFactory.CreateLogger<RetriveUsersHandler>();
            _orderDbfacade = orderDbfacade;
        }

        public IEnumerable<User> Execute(RetriveUsersQuery tQuery)
        {
            IEnumerable<User> userList = null;
            try
            {
                 userList = _orderDbfacade.Users?.ToList();
            }
            catch (SqlException exception)
            {
                string typeName = nameof(RetriveUsersHandler);
                _logger.Log(LogLevel.Error, $" {typeName} caused an exception { exception.Message} ");
            }
            return userList;
        }
    }
}
