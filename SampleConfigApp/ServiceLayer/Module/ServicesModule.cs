using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;

namespace ServiceLayer.Module
{
    public class ServicesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ValueService>().As<IValuesInterface>().InstancePerLifetimeScope();
        }
    }
}
