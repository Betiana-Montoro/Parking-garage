using Parking_garage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking_garage.Extensions
{
    public static class ReservationExtensions
    {
        public static Reservation SetGuid(this Reservation reservation)
        {
            reservation.ReservationId = Guid.NewGuid();
            return reservation;
        }
    }
}
