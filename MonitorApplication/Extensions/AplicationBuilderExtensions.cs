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
        /* I run migrations here. Database Is not currently initialized with any data so user 
         * has to wait for HttpClient to fill database. */
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetService<IServiceScopeFactory>()
                .CreateScope()) {
                OrdersDbContext context = serviceScope
                    .ServiceProvider
                    .GetRequiredService<OrdersDbContext>();
                context.Database.Migrate();
            }
            return app;
        }
    }
}
