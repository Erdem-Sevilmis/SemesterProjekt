using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); // Add this line to load appsettings.json
                                                                                                       // Add other configuration sources if needed
                
                })
            .Build();

            IConfiguration configuration = host.Services.GetRequiredService<IConfiguration>();
            string connectionString = configuration.GetConnectionString("ConnectionString");

            // Use the connection string for database operations or other initialization tasks
        }
    }
}
