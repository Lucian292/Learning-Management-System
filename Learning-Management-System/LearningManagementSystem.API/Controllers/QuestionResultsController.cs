using LearningManagementSystem.Application.Features.QuestionResult.Queries.GetByUserId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/[controller]")]
    [ApiController]
    public class QuestionResultsController : ApiControllerBase
    {
        [Authorize]
        [HttpGet("byUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserQuestionResults()
        {
            var result = await Mediator.Send(new GetByUserIdQuestionResultQuery());
            return Ok(result.QuestionResults);
        }
    }
}
