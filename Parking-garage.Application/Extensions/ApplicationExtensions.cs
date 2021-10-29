using Microsoft.Extensions.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Parking_garage.Application.ReservationQueue;
using RabbitMQ.Client.Core.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking_garage.MsSQL.GarageRepository;


namespace Parking_garage.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration, bool isSend) 
        {
			var rabbitMqSection = configuration.GetSection("RabbitMq");
			var exchangeSection = configuration.GetSection("RabbitMqExchange");

			if (isSend)
			{
				services.AddRabbitMqClient(rabbitMqSection)
				.AddProductionExchange("exchange.Reservation", exchangeSection);
			}
			else
			{
				services.AddRabbitMqClient(rabbitMqSection)
				.AddConsumptionExchange("exchange.Reservation", exchangeSection);
			}


			services.AddMediatR(typeof(IApplicationAnchor));
			services.AddScoped<IAddReservationQueue, AddReservationQueue>();
			services.AddScoped<IFinishReservationQueue, FinishReservationQueue>();
			services.AddScoped<IReservationRepository, ReservationRepository>();
			services.AddScoped<IGarageRepository, GarageRepository>();
			return services;
		}
    }
}
