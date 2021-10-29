using System;

namespace Parking_garage.Model
{
    public class Reservation
    {
        public Guid ReservationId { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public string CarPlate { get; set; }

        public string CarType { get; set; }

        public string CarColor { get; set; }

        public decimal Cost { get; set; }
    }
}
