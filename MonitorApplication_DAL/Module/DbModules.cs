using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MonitorApplication_USERS_DAL.DbFacades;
using MonitorApplication_USERS_DAL.Interfaces;

namespace MonitorApplication_USERS_DAL.Module
{
    public class DbModules: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrdersDbFacade>()
                .As<IOrdersDbFacade>()
                .InstancePerDependency();
        }
    }
}
