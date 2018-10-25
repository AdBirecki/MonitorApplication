using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MonitorApplication_BL.Commands.Interfaces;
using MonitorApplication_BL.Commands.RegisterCommand;
using MonitorApplication_Models.UserModels;
using MonitorApplication_USERS_DAL.Contexts;
using MonitorApplication_USERS_DAL.Interfaces;

namespace MonitorApplication_BL.Commands.Handler
{
    public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly OrdersDbContext _orderDbContext;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public RegisterUserHandler(ILoggerFactory loggerFactory, OrdersDbContext orderDbContext )
        {
            _orderDbContext = orderDbContext;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<RegisterUserHandler>();
        }

        public void Execute(RegisterUserCommand command)
        {
            User user = new User {
                Username = command.UserName,
                Password = command.Password
            };

            try
            {
                _orderDbContext.Users.Add(user);
                _orderDbContext.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                string typeName = nameof(RegisterUserCommand);
                _logger.Log(LogLevel.Error, $" {typeName} caused an exception { exception.Message} ");
            }
           _logger.LogWarning($"Register user command is being executed for user {command.UserName}  password: {command.Password}");
        }
    }
}
