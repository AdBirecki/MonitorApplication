using Microsoft.Extensions.Logging;
using MonitorApplication_BL.Queries.Interfaces;
using MonitorApplication_BL.Queries.RetriveUserQueries;
using MonitorApplication_Models.UserModels;
using MonitorApplication_USERS_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorApplication_BL.Queries.Handler
{
    public  class RetriveUserHandler : IQueryHandler<RetriveUserQuery,User>
    {
        private readonly IOrdersDbFacade _orderDbfacade;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public RetriveUserHandler(
            ILoggerFactory loggerFactory, 
            IOrdersDbFacade orderDbfacade) {
            _logger = loggerFactory.CreateLogger<RetriveUserHandler>();
            _orderDbfacade = orderDbfacade;
        }

        public User Execute(RetriveUserQuery tQuery)
        {
            User result = new User { Username = "Dummy User" };
            try
            {
                IEnumerable<User> users = _orderDbfacade.Users;
                User user = _orderDbfacade.Users.FirstOrDefault(u => u.Username.Equals(tQuery.UserName)) ;
                return user;
            }
            catch (Exception ex) {
                _logger.LogError($"User with a given username: { tQuery.UserName} Not found!");
            }
            return result;
        }
    }
}
