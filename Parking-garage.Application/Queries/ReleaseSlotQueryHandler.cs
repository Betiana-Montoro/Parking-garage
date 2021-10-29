using MediatR;
using Parking_garage.MsSQL.GarageRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parking_garage.Application.Queries
{
    class ReleaseSlotQueryHandler : IRequestHandler<ReleaseSlotQuery, Unit>
    {
        private readonly IGarageRepository _garageRepository;
        public ReleaseSlotQueryHandler(IGarageRepository garageRepository)
        {
            _garageRepository = garageRepository;
        }
        public async Task<Unit> Handle(ReleaseSlotQuery request, CancellationToken cancellationToken)
        {
            await _garageRepository.ReleaseSlot();
            return Unit.Value;
        }
    }
}
