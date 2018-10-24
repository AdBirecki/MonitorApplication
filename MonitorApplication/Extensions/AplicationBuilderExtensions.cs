﻿using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MonitorApplication_USERS_DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_Utilities.Extensions
{
    static class AplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope()) {

                OrdersContext context = serviceScope.ServiceProvider.GetRequiredService<OrdersContext>();
                context.Database.Migrate();
            }
            return app;
        }
    }
}