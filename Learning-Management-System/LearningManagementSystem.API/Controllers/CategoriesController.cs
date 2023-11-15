using LearningManagementSystem.Application.Features.Categories.Commands.CreateCategory;
using LearningManagementSystem.Application.Features.Categories.Commands.DeleteCategory;
using LearningManagementSystem.Application.Features.Categories.Commands.UpdateCategory;
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdCategoryQuery(id));
            if (result.CategoryId == Guid.Empty)
            {
                return NotFound("Category not found.");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
                return NoContent(); // Categoria nu a fost gasita
            }

        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateCategoryCommand command)
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
