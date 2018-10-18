using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using MonitorApplication_BL.Commands.Dispatcher;
using MonitorApplication_BL.Commands.Interfaces;
using MonitorApplication_BL.Queries.Dispatchers;
using MonitorApplication_BL.Queries.Interfaces;

namespace MonitorApplication_BL.Module
{
    public class DispatcherModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerDependency();

            builder.RegisterType<QueryDispatcher>()
                .As<IQueryDispatcher>()
                .InstancePerDependency();
        }
    }
}
