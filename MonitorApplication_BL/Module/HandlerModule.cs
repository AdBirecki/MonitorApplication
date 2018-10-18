using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MonitorApplication_BL.Commands.Handler;
using MonitorApplication_BL.Commands.Interfaces;
using MonitorApplication_BL.Commands.RegisterCommand;

namespace MonitorApplication_BL.Module
{
    public class HandlerModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterUserHandler>()
                .As<ICommandHandler<RegisterUserCommand>>()
                .InstancePerDependency();
        }
    }
}
