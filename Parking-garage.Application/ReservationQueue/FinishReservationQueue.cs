using Parking_garage.Model;
using RabbitMQ.Client.Core.DependencyInjection.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_garage.Application.ReservationQueue
{
    public class FinishReservationQueue : IFinishReservationQueue
    {
        readonly IQueueService _queueService;
        public FinishReservationQueue(IQueueService queueService)
        {
            _queueService = queueService;
        }
        public async Task SendFinishReservationMessage(Reservation reservation)
        {
            await _queueService.SendAsync(
                @object: reservation,
                exchangeName: "exchange.Reservation",
                routingKey: "FinishReservation"
                );
        }
    }
}
