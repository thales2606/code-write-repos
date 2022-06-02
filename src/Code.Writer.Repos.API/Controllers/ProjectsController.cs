using Code.Writer.Repos.Domain.Commands;
using Code.Writer.Repos.Domain.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Code.Writer.Repos.API.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllProjects([FromQuery] GetAllProjectsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpPost]
        public async Task CreateProject([FromBody] CreateProjectCommand command)
        {
            await _mediator.Send(command);
        }
        [HttpPost("attach-repository")]
        public async Task AttachRepository([FromBody] AttachRepositoryInProjectCommand command)
        {
            await _mediator.Send(command);
        }
    }
}
