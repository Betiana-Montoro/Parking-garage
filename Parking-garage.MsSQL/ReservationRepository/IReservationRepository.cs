using Parking_garage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_garage.MsSQL.GarageRepository
{
    public interface IReservationRepository
    {
        Task AddReservation(Reservation reservation);

        Task UpdateReservation(Reservation reservation);

        Task<Reservation> GetReservationByID(Guid Id);
    }
}
