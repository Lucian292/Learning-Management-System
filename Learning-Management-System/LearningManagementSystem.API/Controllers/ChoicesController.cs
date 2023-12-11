using LearningManagementSystem.Application.Features.Choice.Commands.CreateChoice;
using LearningManagementSystem.Application.Features.Choice.Commands.DeleteChoice;
using LearningManagementSystem.Application.Features.Choice.Queries.GetAll;
using LearningManagementSystem.Application.Features.Choice.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.API.Controllers
{   
    [ApiController]
    [Route("api/v1/choices")]
    public class ChoicesController : ApiControllerBase
    {
        [Authorize(Roles = "Professor, Admin")]
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

        [Authorize(Roles = "Professor, Admin, Student")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllChoicesQuery());
            return Ok(result);
        }

        [Authorize(Roles = "Professor, Admin, Student")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdChoiceQuery(id));
            return Ok(result);
        }

        [Authorize(Roles = "Professor, Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteChoiceCommand { ChoiceId = id };
            var result = await Mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
