using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MonitorApplication_BL.Commands.Commands;
using MonitorApplication_BL.Commands.Handler;
using MonitorApplication_BL.Commands.Interfaces;
using MonitorApplication_BL.Commands.RegisterCommand;
using MonitorApplication_BL.Queries.Handler;
using MonitorApplication_BL.Queries.Interfaces;
using MonitorApplication_BL.Queries.Queries;
using MonitorApplication_BL.Queries.RetriveUserQueries;
using MonitorApplication_Models.OrderModel;
using MonitorApplication_Models.UserModels;

namespace MonitorApplication_BL.Module
{
    /* Registrations for BL were split into two modules. This one is responsible for handlers. */
    public class HandlerModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Commands 
            builder.RegisterType<RegisterUserHandler>()
                .As<ICommandHandler<RegisterUserCommand>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SaveMineralPriceHandler>()
                .As<ICommandHandler<SaveMineralDataCommand>>()
                .InstancePerLifetimeScope();

            //Queries
            builder.RegisterType<RetriveUserHandler>()
                .As<IQueryHandler<RetriveUserQuery, User>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RetriveUsersHandler>()
                .As<IQueryHandler<RetriveUsersQuery, IEnumerable<User>>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RetriveMineralPricesHandler>()
                .As<IQueryHandler<RetriveMineralPricesQuery, IEnumerable<MineralPriceData>>>()
                .InstancePerLifetimeScope();
        }
    }
}
