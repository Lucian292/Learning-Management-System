using LearningManagementSystem.Application.Contracts.Identity;
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

        public AuthenticationController(ILoginService authService, ILogger<AuthenticationController> logger, GetRegistrationStrategy registrationStrategy)
        {
            _loginService = authService;
            _logger = logger;
            _registrationStrategy = registrationStrategy;
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
    }
}
