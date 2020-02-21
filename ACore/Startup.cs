using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ACore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            int x = 2;
            app.Use(async (context, next) =>
            {
                x = x * 2;      // 2 * 2 = 4
                await next.Invoke();    // ����� app.Run
                x = x * 2;      // 8 * 2 = 16
                await context.Response.WriteAsync($"Result: {x}");
                await context.Response.WriteAsync($"Result: {x}");
            });

            app.Run( async context =>
            {
                await context.Response.WriteAsync($"Application Name: {env.ApplicationName} \n Result: {x}");
            });
        }
    }
}
