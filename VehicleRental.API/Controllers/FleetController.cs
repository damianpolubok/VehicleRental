using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleRental.API.Features.Fleet.Queries;

namespace VehicleRental.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FleetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FleetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("report")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFleetReport()
        {
            var report = await _mediator.Send(new GetFleetReportQuery());
            return Ok(report);
        }
    }
}