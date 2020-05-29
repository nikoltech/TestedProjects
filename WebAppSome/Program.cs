using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAppSome
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseIISIntegration();
                    //webBuilder.UseKestrel(options =>
                    //{
                    //    options.Listen(IPAddress.Loopback, 5000);
                    //    options.Listen(IPAddress.Loopback, 5001,
                    //        listenOptions =>
                    //        {
                    //            listenOptions.UseHttps();
                    //        });
                    //})
                    //.UseUrls(new string[] { "http://*:5000", "https://*:5001" });
                });
    }
}
