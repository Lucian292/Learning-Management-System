using LearningManagementSystem.Application.Features.Categories.Commands.CreateCategory;
using LearningManagementSystem.Application.Features.Categories.Commands.DeleteCategory;
using LearningManagementSystem.Application.Features.Categories.Queries.GetAll;
using LearningManagementSystem.Application.Features.Categories.Queries.GetById;

using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.API.Controllers
{
    public class CategoriesController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
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
            var result = await Mediator.Send(new GetAllCategoriesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdCategoryQuery(id));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCategoryCommand { CategoryId = id };
            var result = await Mediator.Send(command);

            if (result.Success)
            {
                return Ok(result); // Categoria a fost stearsa cu succes
            }
            else
            {
                return BadRequest(result); // Categoria nu a fost gasita
            }

        }
    }
}
