using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_garage.Application.Queries
{
    public class ReleaseSlotQuery: IRequest<Unit>
    {
    }
}
