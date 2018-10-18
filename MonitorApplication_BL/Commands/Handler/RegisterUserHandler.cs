using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using MonitorApplication_BL.Commands.Interfaces;
using MonitorApplication_BL.Commands.RegisterCommand;

namespace MonitorApplication_BL.Commands.Handler
{
    public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public RegisterUserHandler(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<RegisterUserHandler>();
        }

        public void Execute(RegisterUserCommand command)
        {
           _logger.LogWarning($"Register user command is being executed for user {command.UserName}  password: {command.Password}");
        }
    }
}
