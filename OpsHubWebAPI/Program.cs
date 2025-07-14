using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpsHubWebAPI
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
                      var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                      var config = new ConfigurationBuilder().AddJsonFile($"appsettings.{env}.json", optional: false).Build();
                      var url = config.Providers.FirstOrDefault();
                      string port = string.Empty;
                      if (url.TryGet("Port", out string value))
                      {
                          port = value;
                      }
                      webBuilder.UseStartup<Startup>().UseUrls(new[] { $"http://localhost:{port}" });
                      webBuilder.ConfigureKestrel(options =>
                      {
                          options.ListenAnyIP(int.Parse(port)); // HTTP
                      });
                      // now the Kestrel server will listen on port
                  });
    }
}
