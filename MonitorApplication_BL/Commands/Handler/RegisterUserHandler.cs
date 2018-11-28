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

namespace MonitorApplication_BL.Commands.Handler
{
    public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly OrdersDbContext _orderDbContext;
        private readonly ILogger _logger;
        const string handlerName = nameof(RegisterUserCommand);

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
                if (_orderDbContext.Users.Any((registeredUser) => registeredUser.Username.Equals(user.Username)))
                {
                    string exceptionMessage = $"{handlerName} caused an exception User with {user.Username} already exists!";
                    _logger.Log(LogLevel.Error, exceptionMessage);
                    throw new ArgumentException(exceptionMessage);
                }

                _orderDbContext.Users.Add(user);
                _orderDbContext.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                _logger.Log(LogLevel.Error, $" {handlerName} database update caused an DbUpdateException {exception.Message} ");
                throw;
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, $" {handlerName} database update caused an Exception {exception.Message} ");
                throw;
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
