using Microsoft.Extensions.Logging;
using MonitorApplication_BL.Commands.Interfaces;
using MonitorApplication_BL.Commands.RegisterCommand;
using MonitorApplication_USERS_DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_BL.Commands.Handler
{
    public class SaveFileHandler : ICommandHandler<SaveFileCommand>
    {
        private readonly OrdersDbContext _orderDbContext;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public SaveFileHandler(
            ILoggerFactory loggerFactory, 
            OrdersDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<RegisterUserHandler>();
        }

        public void Execute(SaveFileCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
