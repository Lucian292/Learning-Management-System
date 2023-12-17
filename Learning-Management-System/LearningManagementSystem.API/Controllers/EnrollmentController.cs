﻿using LearningManagementSystem.Application.Features.Enrollments.Commands.CreateEnrollment;
using LearningManagementSystem.Application.Features.Enrollments.Commands.DeleteEnrollment;
using LearningManagementSystem.Application.Features.Enrollments.Queries.GetAll;
using LearningManagementSystem.Application.Features.Enrollments.Queries.GetById;
using LearningManagementSystem.Application.Features.Enrollments.Queries.GetByUserId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EnrollmentController : ApiControllerBase
    {
        [Authorize(Roles = "Professor, Admin, Student")]
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

        [Authorize(Roles = "Professor, Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllEnrollmentsQuery());
            return Ok(result);
        }

        [Authorize(Roles = "Student, Professor, Admin")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdEnrollmentQuery(id));
            return Ok(result);
        }

        [Authorize]
        [HttpGet("byUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserEnrollments()
        {
            var result = await Mediator.Send(new GetEnrollmentsByUserIdQuery());
            return Ok(result.Enrollments);
        }

        [Authorize(Roles = "Student, Professor, Admin")]
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
