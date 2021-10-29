using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parking_garage.Application.Commands;
using Parking_garage.Application.Queries;
using Parking_garage.Extensions;
using Parking_garage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking_garage.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]/[Action]")]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<Guid> AddReservation(Reservation reservation)
        {
            reservation.SetGuid();
            var command = new SendReservationCommand(reservation);
            await _mediator.Send(command);
            return reservation.ReservationId;
        }

        [HttpPut]
        public async Task<Reservation> FinishReservation(Guid id)
        {
            var command = new FinishReservationCommand(id);
            return await _mediator.Send(command);
        }

        [HttpGet("{Id:guid}")]
        public async Task<Reservation> GetReservationByID(Guid Id)
        {
            var query = new GetReservationByIDQuery(Id);
            return await _mediator.Send(query);
        }
    }

}
