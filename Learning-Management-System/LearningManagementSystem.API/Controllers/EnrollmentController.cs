using LearningManagementSystem.Application.Features.Chapters.Commands.CreateChapter;
using LearningManagementSystem.Application.Features.Chapters.Commands.DeleteChapter;
using LearningManagementSystem.Application.Features.Chapters.Queries.GetAll;
using LearningManagementSystem.Application.Features.Chapters.Queries.GetById;
using LearningManagementSystem.Application.Features.Enrollments.Commands.CreateEnrollment;
using LearningManagementSystem.Application.Features.Enrollments.Commands.DeleteEnrollment;
using LearningManagementSystem.Application.Features.Enrollments.Queries.GetAll;
using LearningManagementSystem.Application.Features.Enrollments.Queries.GetById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.API.Controllers
{

    public class EnrollmentController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateEnrollmentCommand command)
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
            var result = await Mediator.Send(new GetAllEnrollmentsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdEnrollmentQuery(id));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteEnrollmentCommand { EnrollmentId = id };
            var result = await Mediator.Send(command);

            if (result.Success)
            {
                return Ok(result); // Enrollment-ul a fost sters cu succes
            }
            else
            {
                return BadRequest(result); // Enrollment-ul nu a fost gasit
            }
        }
    }
}
