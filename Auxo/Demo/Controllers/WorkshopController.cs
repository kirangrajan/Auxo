using Demo.Queries;
using Demo.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkshopController : ControllerBase
    {
        private readonly ILabourQuery _labourQuery;
        private readonly IJobQuery _jobQuery;
        private readonly IClockingCommand _clockingCommand;

        public WorkshopController(ILabourQuery labourQuery, IJobQuery jobQuery, IClockingCommand clockingCommand)
        {
            _labourQuery = labourQuery;
            _jobQuery = jobQuery;
            _clockingCommand = clockingCommand;
        }

        [HttpGet("job/{jobId}/labour")]
        public IActionResult GetTotalLabour(string jobId)
        {
            if (false == _jobQuery.DoesJobExist(jobId))
            {
                var problem = new ProblemDetailDto(
                    "https://example.com/problems/no-such-job",
                    "No Such Job",
                    $"The job referenced (\"{jobId}\") does not exist."
                );

                return NotFound(problem);
            }

            var total = _labourQuery.GetTotalLabourCost(jobId, 150m);
            var dto = new TotalLabourCostDto(jobId, total);
            return Ok(dto);
        }

        [HttpPut("job/{jobId}/clock-on")]
        public IActionResult ClockOn(string jobId)
        {
            _clockingCommand.ClockOn(jobId);
            return NoContent();
        }

        [HttpPut("job/{jobId}/clock-off")]
        public IActionResult ClockOff(string jobId)
        {
            _clockingCommand.ClockOff(jobId);
            return NoContent();
        }
    }
}