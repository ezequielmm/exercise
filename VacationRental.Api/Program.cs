using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using System;

namespace VacationRental.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HHmm");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File($"log-Lodgify-{timestamp}.txt")
                .CreateLogger();

            CreateWebHostBuilder(args).Build().Run();

            Log.CloseAndFlush();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().UseSerilog();
    }
}
