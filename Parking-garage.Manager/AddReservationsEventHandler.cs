using MediatR;
using Newtonsoft.Json;
using Parking_garage.Application.Commands;
using Parking_garage.Model;
using RabbitMQ.Client.Core.DependencyInjection.MessageHandlers;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_garage.Manager
{
    public class AddReservationsEventHandler : IAsyncMessageHandler
    {
        private readonly IMediator _mediator;

        public AddReservationsEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Handle(BasicDeliverEventArgs eventArgs, string matchingRoute)
        {
            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            var reservation = JsonConvert.DeserializeObject<Reservation>(message);
            var command = new AddReservationCommand(reservation);
            await _mediator.Send(command);
        }
    }
}
