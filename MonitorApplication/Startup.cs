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
using MonitorApplication.HttpClient;
using MonitorApplication_Models.Scheduling;
using MonitorApplication_Models.Scheduling.Models;
using IContainer = Autofac.IContainer;
using MonitorApplication_Scheduler.SchedulerExtensions;

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
               
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton(Configuration)
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddHttpClient<GoldClient>(client => client.BaseAddress = new Uri(Configuration["GoldDataUri"]));
            services.AddSingleton<IScheduledTask, QuoteOfTheDayTask>();
            services.AddSingleton<IScheduledTask, SomeOtherTask>();
            services.AddScheduler((sender, args) => { args.SetObserved(); });

            ContainerBuilder container = new ContainerBuilder();
            container.Populate(services);

            AppContainer = container.Build();

            return new AutofacServiceProvider(AppContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (loggerFactory != null)
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
