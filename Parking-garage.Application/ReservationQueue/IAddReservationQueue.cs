﻿using Parking_garage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_garage.Application.ReservationQueue
{
    public interface IAddReservationQueue
    {
        Task SendReservation(Reservation reservation);

    }
}
