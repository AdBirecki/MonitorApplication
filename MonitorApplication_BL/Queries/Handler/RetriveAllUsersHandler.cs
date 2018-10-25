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
    public class RetriveAllUsersHandler : IQueryHandler<RetriveAllUsersQuery, IEnumerable<User>>
    {
        private readonly IOrdersDbFacade _orderDbfacade;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public RetriveAllUsersHandler(
            ILoggerFactory loggerFactory, 
            IOrdersDbFacade orderDbfacade)
        {
            _logger = loggerFactory.CreateLogger<RetriveAllUsersHandler>();
            _orderDbfacade = orderDbfacade;
        }

        public IEnumerable<User> Execute(RetriveAllUsersQuery tQuery)
        {
            IList<User> userList = _orderDbfacade.Users?.ToList();
            return userList;
        }
    }
}
