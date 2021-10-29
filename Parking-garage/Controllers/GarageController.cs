using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parking_garage.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking_garage.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]/[Action]")]
    public class GarageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GarageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<int> GetAvailableSlots()
        {
            var query = new GetAvailableSlotsQuery();
            return await _mediator.Send(query);
        }

        [HttpPut]
        public async Task<Unit> TakeSlot()
        {
            var query = new TakeSlotQuery();
            return await _mediator.Send(query);
        }

        [HttpPut]
        public async Task<Unit> ReleaseSlot()
        {
            var query = new ReleaseSlotQuery();
            return await _mediator.Send(query);
        }

    }
}
