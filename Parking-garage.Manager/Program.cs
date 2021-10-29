using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Parking_garage.Application.Extensions;
using Parking_garage.Manager;
using RabbitMQ.Client.Core.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection.Services;
using System;
using System.Configuration;
using System.IO;

namespace Parking_garage.Manager
{
    class Program 
    { 
        public static IConfiguration Configuration { get; set; }
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
             Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var queueService = serviceProvider.GetRequiredService<IQueueService>();
            queueService.StartConsuming();

            Console.ReadLine();
        }

        static void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationDependencies(Configuration, false);
            services.AddAsyncMessageHandlerSingleton<AddReservationsEventHandler>("AddReservation");
            services.AddAsyncMessageHandlerSingleton<FinishReservationsEventHandler>("FinishReservation");
        }
    }
}
