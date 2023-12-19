using LearningManagementSystem.Application.Contracts.Identity;
using LearningManagementSystem.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ApiControllerBase
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IUserService userService;

        public UserInfoController(ICurrentUserService currentUserService, IUserService userService)
        {
            this.currentUserService = currentUserService;
            this.userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get() 
        { 
            var userInfo = await userService.GetCurrentUserInfoAsync(currentUserService.UserId);

            if (userInfo.IsSuccess)
            {
                return Ok(userInfo.Value);
            }
            else
            { 
                return BadRequest(userInfo.Error); 
            }
        }
        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var userInfo = await userService.GetCurrentUserInfoAsync(userId.ToString());

            if (userInfo.IsSuccess)
            {
                return Ok(userInfo.Value);
            }
            else
            {
                return BadRequest(userInfo.Error);
            }
        }
    }
}
