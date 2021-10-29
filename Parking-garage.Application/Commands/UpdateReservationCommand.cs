using MediatR;
using Parking_garage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_garage.Application.Commands
{
    public class UpdateReservationCommand : IRequest
    {
        public Reservation Reservation { get; }
        public UpdateReservationCommand(Reservation reservation)
        {
            Reservation = reservation;
        }
    }
}

