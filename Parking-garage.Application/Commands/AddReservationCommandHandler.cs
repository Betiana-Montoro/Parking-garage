using MediatR;
using Parking_garage.Application.Commands;
using Parking_garage.MsSQL;
using Parking_garage.MsSQL.GarageRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parking_garage.MsSQL.GarageRepository
{
    class AddReservationCommandHandler : IRequestHandler<AddReservationCommand>
    {
        private readonly IReservationRepository _addReservationRepository;
        public AddReservationCommandHandler(IReservationRepository reservationRepository)
        {
            _addReservationRepository = reservationRepository;
        }
        public async Task<Unit> Handle(AddReservationCommand request, CancellationToken cancellationToken)
        {
            await _addReservationRepository.AddReservation(request.Reservation);
            return Unit.Value;
        }
    }
}
