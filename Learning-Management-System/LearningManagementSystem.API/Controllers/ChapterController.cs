using LearningManagementSystem.Application.Features.Chapters.Commands;
using LearningManagementSystem.Application.Features.Chapters.Commands.CreateChapter;
using LearningManagementSystem.Application.Features.Chapters.Commands.CreateQuiz;
using LearningManagementSystem.Application.Features.Chapters.Commands.DeleteChapter;
using LearningManagementSystem.Application.Features.Chapters.Commands.UpdateChapter;
using LearningManagementSystem.Application.Features.Chapters.Queries.GetAll;
using LearningManagementSystem.Application.Features.Chapters.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/chapters")]
    public class ChapterController : ApiControllerBase
    {
        [Authorize(Roles = "Professor, Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateChapterCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result.Chapter);
        }

        [Authorize(Roles = "Professor, Admin, Student")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllChaptersQuery());
            return Ok(result.Chapters);
        }

        [Authorize(Roles = "Professor, Admin, Student")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdChapterQuery(id));
            if (result.ChapterId == Guid.Empty)
            {
                return NotFound("Chapter not found.");
            }
            return Ok(result);
        }

        [Authorize(Roles = "Professor, Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteChapterCommand { ChapterId = id };
            var result = await Mediator.Send(command);

            if (result.Success)
            {
                return Ok(result); // Capitolul a fost sters cu succes
            }
            else
            {
                return BadRequest(result); // Capitolul nu a fost gasit
            }
        }

        [Authorize(Roles = "Professor, Admin")]
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateChapterCommand command)
        {
            var result = await Mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result.Chapter);
        }

        [Authorize(Roles = "Professor, Admin")]
        [HttpPut("{id}/upload-pdf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UploadPdf(Guid id, IFormFile file)
        {
            var command = new CreatePdfCommand { ChapterId = id, File = file };
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

        [Authorize(Roles = "Professor, Admin")]
        [HttpPost("create-quiz")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateQuiz(CreateQuizCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
