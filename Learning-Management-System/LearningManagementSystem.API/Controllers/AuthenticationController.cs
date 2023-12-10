using LearningManagementSystem.API.Models;
using LearningManagementSystem.Application.Contracts.Identity;
using LearningManagementSystem.Application.Contracts.Interfaces;
using LearningManagementSystem.Application.Models.Identity;
using LearningManagementSystem.Identity.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly GetRegistrationStrategy _registrationStrategy;
        private readonly ICurrentUserService currentUserService;

        public AuthenticationController(ILoginService authService, ILogger<AuthenticationController> logger, GetRegistrationStrategy registrationStrategy, ICurrentUserService currentUserService)
        {
            _loginService = authService;
            _logger = logger;
            _registrationStrategy = registrationStrategy;
            this.currentUserService = currentUserService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _loginService.Login(model);

                if (status == 0)
                {
                    return BadRequest(message);
                }

                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }
                IRegistrationServiceStrategy registrationStrategy = _registrationStrategy.GetRegistrationRoleStrategy(model.Role);
                var (status, message) = await registrationStrategy.Registration(model);

                if (status == 0)
                {
                    return BadRequest(message);
                }


                return CreatedAtAction(nameof(Register), model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _loginService.Logout();
            return Ok();
        }

        [HttpGet]
        [Route("currentuserinfo")]
        public CurrentUser CurrentUserInfo()
        {
            if (this.currentUserService.GetCurrentUserId() == null)
            {
                return new CurrentUser
                {
                    IsAuthenticated = false
                };
            }
            return new CurrentUser
            {
                IsAuthenticated = true,
                UserName = this.currentUserService.GetCurrentUserId(),
                Claims = this.currentUserService.GetCurrentClaimsPrincipal().Claims.ToDictionary(c => c.Type, c => c.Value)
            };
        }
    }
}
