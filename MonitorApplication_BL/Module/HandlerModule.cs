﻿using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MonitorApplication_BL.Commands.Handler;
using MonitorApplication_BL.Commands.Interfaces;
using MonitorApplication_BL.Commands.RegisterCommand;
using MonitorApplication_BL.Queries.Handler;
using MonitorApplication_BL.Queries.Interfaces;
using MonitorApplication_BL.Queries.RetriveUserQueries;
using MonitorApplication_Models.UserModels;

namespace MonitorApplication_BL.Module
{
    public class HandlerModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Commands 
            builder.RegisterType<RegisterUserHandler>()
                .As<ICommandHandler<RegisterUserCommand>>()
                .InstancePerDependency();

            //Queries
            builder.RegisterType<RetriveUserHandler>()
                .As<IQueryHandler<RetriveUserQuery, User>>()
                .InstancePerDependency();

            builder.RegisterType<RetriveAllUsersHandler>()
                .As<IQueryHandler<RetriveAllUsersQuery, IEnumerable<User>>>()
                .InstancePerDependency();
        }
    }
}
