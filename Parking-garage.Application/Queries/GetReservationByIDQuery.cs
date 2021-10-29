using MediatR;
using Parking_garage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_garage.Application.Queries
{
    public class GetReservationByIDQuery: IRequest<Reservation>
    {
        public Guid Id { get; }
        public GetReservationByIDQuery(Guid id)
        {
            Id = id;
        }
    }
}
