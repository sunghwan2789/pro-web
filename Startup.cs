﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using pro_web.Middleware;
using pro_web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            StartupConfigureServices(services);

            services.AddDbContext<ProContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseMySql(Configuration.GetConnectionString("DefaultConnection")));
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            StartupConfigureServices(services);

            services.AddDbContext<ProContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseMySql(Configuration.GetConnectionString("DefaultConnection")));
        }

        private void StartupConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddHostedService<CompileAndGoService>();
            services.AddSingleton<ICompileAndGoQueue, CompileAndGoQueue>();
            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromDays(60);
                o.Cookie = new CookieBuilder
                {
                    Name = "PRO_WEB_SESSION",
                    HttpOnly = true,
                    MaxAge = TimeSpan.FromDays(60),
                    SecurePolicy = CookieSecurePolicy.None,
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMiddleware<BasePathMiddleware>();

            app.UseSession();
            app.UseMiddleware<AuthenticateMiddleware>();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
