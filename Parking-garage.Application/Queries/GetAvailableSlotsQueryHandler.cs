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
    class GetAvailableSlotsQueryHandler: IRequestHandler<GetAvailableSlotsQuery, int>
    {
        private readonly IGarageRepository _garageRepository;

        public GetAvailableSlotsQueryHandler(IGarageRepository garageRepository)
        {
            _garageRepository = garageRepository;
        }
        public async Task<int> Handle(GetAvailableSlotsQuery request, CancellationToken cancellationToken) {
            return await _garageRepository.GetAvailableSlots();
        }
    }
}
