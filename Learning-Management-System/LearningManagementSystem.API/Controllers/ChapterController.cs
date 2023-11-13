using LearningManagementSystem.Application.Features.Categories.Commands.CreateCategory;
using LearningManagementSystem.Application.Features.Categories.Commands.DeleteCategory;
using LearningManagementSystem.Application.Features.Categories.Queries.GetAll;
using LearningManagementSystem.Application.Features.Categories.Queries.GetById;
using LearningManagementSystem.Application.Features.Chapters.Commands.CreateChapter;
using LearningManagementSystem.Application.Features.Chapters.Commands.DeleteChapter;
using LearningManagementSystem.Application.Features.Chapters.Queries.GetAll;
using LearningManagementSystem.Application.Features.Chapters.Queries.GetById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.API.Controllers
{

    public class ChapterController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateChapterCommand command)
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
            var result = await Mediator.Send(new GetAllChaptersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdChapterQuery(id));
            return Ok(result);
        }

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

    }
}
