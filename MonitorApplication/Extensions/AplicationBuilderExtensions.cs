using Autofac.Extensions.DependencyInjection;
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
                
                OrdersDbContext context = serviceScope.ServiceProvider.GetRequiredService<OrdersDbContext>();
                //  context.Database.EnsureDeleted();
                context.Database.Migrate();
            }
            return app;
        }
    }
}
