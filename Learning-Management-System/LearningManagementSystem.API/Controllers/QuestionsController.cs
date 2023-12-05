using LearningManagementSystem.Application.Features.Questions.Commands.CreateQuestion;
using LearningManagementSystem.Application.Features.Questions.Commands.DeleteQuestion;
using LearningManagementSystem.Application.Features.Questions.Queries.GetAll;
using LearningManagementSystem.Application.Features.Questions.Queries.GetQuestionById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/questions")]
    public class QuestionsController : ApiControllerBase
    {
        [Authorize(Roles = "Professor, Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateQuestionCommand command)
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
            var result = await Mediator.Send(new GetAllQuestionQuery());
            return Ok(result);
        }

        [Authorize(Roles = "Professor, Admin, Student")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdQuestionQuery(id));

            if (result == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Professor, Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteQuestionCommand { QuestionId = id };
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
