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
    public  class RetriveUserHandler : IQueryHandler<RetriveUserQuery,User>
    {
        private readonly IOrdersDbFacade _orderDbfacade;
        private readonly ILogger _logger;
        const string handlerName = nameof(RetriveUserHandler);

        public RetriveUserHandler(
            ILoggerFactory loggerFactory, 
            IOrdersDbFacade orderDbfacade) {
            _logger = loggerFactory.CreateLogger<RetriveUserHandler>();
            _orderDbfacade = orderDbfacade;
        }

        public User Execute(RetriveUserQuery tQuery)
        {
            User user = null;
            try
            {
                user = _orderDbfacade.Users.FirstOrDefault(u => u.Username.Equals(tQuery.UserName)) ;
            }
            catch (SqlException exception)
            {
                _logger.Log(LogLevel.Error, $" {handlerName} caused an SqlException { exception.Message} ");
                throw;
            }
            catch (Exception exception) {
                _logger.Log(LogLevel.Error, $" {handlerName} caused an exception { exception.Message} ");
                throw;
            }
            return user;
        }
    }
}
