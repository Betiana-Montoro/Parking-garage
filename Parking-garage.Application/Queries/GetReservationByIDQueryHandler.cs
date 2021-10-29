using MediatR;
using Parking_garage.Model;
using Parking_garage.MsSQL.GarageRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parking_garage.Application.Queries
{
    public class GetReservationByIDQueryHandler: IRequestHandler<GetReservationByIDQuery, Reservation>
    {
        private readonly IReservationRepository _reservationRepository;

        public GetReservationByIDQueryHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<Reservation> Handle(GetReservationByIDQuery request, CancellationToken cancellationToken)
        {
            return await _reservationRepository.GetReservationByID(request.Id);
        }
    }
}
