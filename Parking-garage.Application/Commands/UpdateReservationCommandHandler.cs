using MediatR;
using Parking_garage.Model;
using Parking_garage.MsSQL.GarageRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parking_garage.Application.Commands
{
    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand>
    {
        private readonly IReservationRepository _reservationRepository;

        public UpdateReservationCommandHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<Unit> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            await _reservationRepository.UpdateReservation(request.Reservation);
            return Unit.Value;
        }
    }
}
