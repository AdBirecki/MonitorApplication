using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MonitorApplication_BL.Commands.Interfaces;

namespace MonitorApplication_BL.Commands.Dispatcher
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _componentContext;
        public CommandDispatcher(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public void Execute<TCommand>(TCommand commmand) where TCommand : ICommand
        {
            ICommandHandler<TCommand> handler = _componentContext.Resolve<ICommandHandler<TCommand>>();
            handler.Execute(commmand);
        }
    }
}
