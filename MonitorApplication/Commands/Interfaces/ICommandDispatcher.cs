using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorApplication.Commands.Interfaces
{
    interface ICommandDispatcher
    {
        void Execute<TCommand>(TCommand commmand) where TCommand : ICommand;
    }
}
