using LearningManagementSystem.Application.Features.Questions.Commands.CreateQuestion;
using LearningManagementSystem.Application.Features.Questions.Commands.DeleteQuestion;
using LearningManagementSystem.Application.Features.Questions.Queries.GetAll;
using LearningManagementSystem.Application.Features.Questions.Queries.GetQuestionById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/questions")]
    public class QuestionsController : ApiControllerBase
    {
        private readonly IMediator mediator;

        public QuestionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllQuestionQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await mediator.Send(new GetByIdQuestionQuery(id));

            if (result == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteQuestionCommand { QuestionId = id };
            var result = await mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /*[HttpPost("{questionId}/choices")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddChoiceToQuestionById(Guid questionId, AddChoiceToQuestionCommand command)
        {
            command.QuestionId = questionId;

            var result = await mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return CreatedAtAction(nameof(GetChoicesById), new { questionId = result.QuestionId }, result.Choices);
        }*/

        /*[HttpGet("{questionId}/choices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetChoicesById(Guid questionId)
        {
            var result = await mediator.Send(new GetChoicesById(questionId));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }*/
    }
}
