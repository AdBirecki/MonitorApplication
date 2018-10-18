using System;
using System.Collections.Generic;
using System.Text;
using MonitorApplication_BL.Commands.Interfaces;

namespace MonitorApplication_BL.Commands.Dispatcher
{
    public class CommandDispatcher : ICommandDispatcher
    {
        public CommandDispatcher(IDepencencyResolver resolver)
        {

        }

        public void Execute<TCommand>(TCommand commmand) where TCommand : ICommand
        {
            throw new NotImplementedException();
        }
    }
}
