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
    class TakeSlotQueryHandler : IRequestHandler<TakeSlotQuery, Unit>
    {
        private readonly IGarageRepository _garageRepository;

        public TakeSlotQueryHandler(IGarageRepository garageRepository)
        {
            _garageRepository = garageRepository;
        }

        public async Task<Unit> Handle(TakeSlotQuery request, CancellationToken cancellationToken)
        {
            await _garageRepository.TakeSlot();
            return Unit.Value;
        }

    }
}
