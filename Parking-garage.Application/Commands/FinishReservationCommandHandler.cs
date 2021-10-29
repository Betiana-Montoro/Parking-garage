using MediatR;
using Parking_garage.Application.Queries;
using Parking_garage.Application.ReservationQueue;
using Parking_garage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parking_garage.Application.Commands
{
    public class FinishReservationCommandHandler : IRequestHandler<FinishReservationCommand, Reservation>
    {
        private readonly IFinishReservationQueue _finishReservationQueue;
        private readonly IMediator _mediator;

        public FinishReservationCommandHandler(IFinishReservationQueue finishReservationQueue, IMediator mediator)
        {
            _finishReservationQueue = finishReservationQueue;
            _mediator = mediator;
        }

        public async Task<Reservation> Handle(FinishReservationCommand request, CancellationToken cancellationToken)
        {
            var query = new GetReservationByIDQuery(request.Id);
            Reservation reservation = await _mediator.Send(query);

            decimal hourlyPrice = 3;
            decimal dayPrice = 57;

            decimal reservationCost = 0;

            var now = DateTime.Now;
            TimeSpan reservationTime = now.Subtract(reservation.CheckIn);

            if (reservationTime.TotalMinutes <= new TimeSpan(3, 0, 0).TotalMinutes) //The first three hours count as one
            {
                reservationCost = hourlyPrice;
            }
            else if (reservationTime.TotalMinutes > new TimeSpan(3, 0, 0).TotalMinutes && reservationTime.TotalMinutes <= new TimeSpan(8, 0, 0).TotalMinutes)
            {
                reservationCost = hourlyPrice + hourlyPrice * (reservationTime.Hours - 3);
            }
            else if (reservationTime.TotalMinutes > new TimeSpan(8, 0, 0).TotalMinutes && reservationTime.TotalMinutes < new TimeSpan(24, 0, 0).TotalMinutes) //After eight hours the fee is the amount of one day
            {
                reservationCost = dayPrice;
            }
            else if (reservationTime.TotalMinutes > new TimeSpan(24, 0, 0).TotalMinutes)
            {
                int hoursDays = reservationTime.Days * 24;
                int totalDays = hoursDays < reservationTime.TotalHours ? reservationTime.Days + 1 : hoursDays;
                reservationCost = dayPrice * totalDays;
            }

            reservation.Cost = reservationCost;
            reservation.CheckOut = now;

            await _finishReservationQueue.SendFinishReservationMessage(reservation);

            return reservation;
        }
    }
}
