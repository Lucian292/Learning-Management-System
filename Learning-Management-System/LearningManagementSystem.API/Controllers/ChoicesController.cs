using LearningManagementSystem.Application.Features.Choice.Commands.CreateChoice;
using LearningManagementSystem.Application.Features.Choice.Queries.GetAll;
using LearningManagementSystem.Application.Features.Choice.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.API.Controllers
{   
    [ApiController]
    [Route("api/v1/choices")]
    public class ChoicesController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateChoiceCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllChoicesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdChoiceQuery(id));
            return Ok(result);
        }
    }
}
