using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        private readonly ILogger _logger;

        public RegisterUserHandler(ILoggerFactory loggerFactory, OrdersDbContext orderDbContext )
        {
            _orderDbContext = orderDbContext;
            _logger = loggerFactory.CreateLogger<RegisterUserHandler>();
        }

        public void Execute(RegisterUserCommand command)
        {
            /* We compute hash of a password. */
            User user = new User {
                Username = command.UserName,
                Password = ComputeHash(command.Password)
            };

            try
            {
                if (_orderDbContext.Users.Any( (registeredUser) => registeredUser.Username.Equals(user.Username))) {
                     string typeName = nameof(RegisterUserCommand);
                    _logger.Log(LogLevel.Error, $" {typeName} caused an exception User with {user.Username} already exists!");
                    return;
                }

                _orderDbContext.Users.Add(user);
                _orderDbContext.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                string typeName = nameof(RegisterUserCommand);
                _logger.Log(LogLevel.Error, $" {typeName} caused an exception { exception.Message} ");
            }
        }

        private string ComputeHash(string password) {
                using (var md5 = MD5.Create())
                {
                    var result = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
                    return Encoding.ASCII.GetString(result);
                }
        }
    }
}
