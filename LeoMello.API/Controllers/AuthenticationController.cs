using LeoMello.Application.DTOs.Request;
using LeoMello.Application.Interfaces;
using LeoMello.Shared.Exceptions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace LeoMello.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> logger;
        private readonly IAuthService authService;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            IAuthService authService) 
        { 
            this.logger = logger;
            this.authService = authService;
        }

        [HttpPost("/auth")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthModelRequest request)
        {
            if (request is null)
            {
                return BadRequest("Invalid request model");
            }

            try
            {
                // return the response token
                return Ok(await authService.AuthenticateAsync(request));
            }
            catch (AuthenticationException auex)
            {
                logger.LogError(auex.Message);
                throw auex;
            }
            catch (ConfigurationException cex)
            {
                logger.LogError(cex.Message);
                throw cex;
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}", ex);
                return Unauthorized();
            }
        }


        [HttpPost("/create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserModelRequest request)
        {
            if (request is null)
            {
                return BadRequest("Invalid request model");
            }

            try
            {
                // return the response token
                return Ok(await authService.CreateUserAsync(request));
            }
            catch (AuthenticationException auex)
            {
                logger.LogError(auex.Message);
                throw auex;
            }
            catch (ConfigurationException cex)
            {
                logger.LogError(cex.Message);
                throw cex;
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}", ex);
                return Unauthorized();
            }
        }
    }
}
