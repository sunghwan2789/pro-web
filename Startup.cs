﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using pro_web.Middleware;

namespace pro_web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private readonly IConfiguration configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            StartupConfigureServices(services);

            services.AddDbContext<ProContext>(options =>
                options.UseInMemoryDatabase("pro"));
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            StartupConfigureServices(services);

            services.AddDbContext<ProContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseMySql(configuration.GetConnectionString("DefaultConnection")));
        }

        private void StartupConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<BasePathMiddleware>();

            app.UseMvc();
        }
    }
}
