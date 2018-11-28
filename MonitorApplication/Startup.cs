using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Autofac.Extensions.DependencyInjection;
using System.ComponentModel;
using Autofac;
using Microsoft.EntityFrameworkCore;
using IContainer = Autofac.IContainer;
using MonitorApplication_Scheduler.SchedulerExtensions;
using MonitorApplication_Scheduler.SchedulingModels.Interfaces;
using MonitorApplicationHttpClient;
using MonitorApplication_Scheduler.SchedulingModels.Models;
using MonitorApplication_BL.Module;
using MonitorApplication.Filters;
using MonitorApplication_USERS_DAL.Contexts;
using MonitorApplication_Utilities.Extensions;
using MonitorApplication_USERS_DAL.Module;
using Microsoft.AspNetCore.Http.Features;
using MonitorApplication_DAL.Interfaces;

namespace MonitorApplication
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer AppContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
               
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton(Configuration)
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // get uri of server
            services.AddHttpClient<GoldClient>(client => client.BaseAddress = new Uri(Configuration["GoldDataUri"]));
            services.AddSingleton<IScheduledTask, GoldPriceDataRecoveryTask>();
            services.AddScheduler();

            var connection = Configuration.GetConnectionString("UsersDatabase");
            services.AddDbContext<OrdersDbContext>(options =>
                options.UseSqlServer(connection, b => b.MigrationsAssembly("MonitorApplication_DAL")));

            // filters
            services.AddScoped<FilterWithDI>();

            ContainerBuilder container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule<DispatcherModule>();
            container.RegisterModule<HandlerModule>();
            container.RegisterModule<DbModules>();
            AppContainer = container.Build();

            return new AutofacServiceProvider(AppContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.PrepareDatabase();
            /* Logs Appear twice. It seems that going back to NLog is a better idea  */
            /*
            if (loggerFactory != null)
            {
               // loggerFactory.AddConsole(Configuration.GetSection("Logging"));
               // loggerFactory.AddDebug();
            }*/

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

             app.UseMvc();
        }
    }
}
