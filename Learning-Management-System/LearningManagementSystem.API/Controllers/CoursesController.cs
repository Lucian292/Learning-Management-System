using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using LearningManagementSystem.Application.Features.Couerses.Commands.CreateCourse;
using LearningManagementSystem.Application.Features.Courses.Queries.GetAll;
using LearningManagementSystem.Application.Features.Courses.Queries.GetById;
using LearningManagementSystem.Application.Features.Courses.Commands.DeleteCourse;

namespace LearningManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateCourseCommand command)
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
            var result = await Mediator.Send(new GetAllCoursesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdCourseQuery(id));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCourseCommand { CourseId = id };
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