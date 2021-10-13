using MDFSchoolAppV2.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDFSchoolAppV2.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //get param arg to seed data.
            var seed = args.Contains("/seed");
            if (seed)
            {
                args = args.Except(new[] { "/seed" }).ToArray();
            }

            var host = CreateHostBuilder(args).Build();

            if (seed)
            {
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetService<MDFSchoolAppV2DbContext>();
                    SeedData.SeedInitialValues(context);
                }
            }

            //
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
