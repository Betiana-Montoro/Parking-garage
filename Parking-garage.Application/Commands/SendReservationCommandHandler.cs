using MediatR;
using Parking_garage.Application.ReservationQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parking_garage.Application.Commands
{
    public class SendReservationCommandHandler : IRequestHandler<SendReservationCommand>
    {
        private readonly IAddReservationQueue _addReservationQueue;

        public SendReservationCommandHandler(IAddReservationQueue addReservationQueue)
        {
            _addReservationQueue = addReservationQueue;
        }
        public async Task<Unit> Handle(SendReservationCommand request, CancellationToken cancellationToken)
        {
            await _addReservationQueue.SendReservation(request.Reservation);
            return Unit.Value;
        }
    }
}
