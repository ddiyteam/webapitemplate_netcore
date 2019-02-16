using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using $ext_safeprojectname$.BLL.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/token")]
    [ApiVersionNeutral]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;

        public TokenController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));           
        }

        /// <summary>
        /// Generate sample token
        /// </summary>       
        /// <returns>Generated token</returns>        
        [AllowAnonymous]
        [HttpGet("generate")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Generated token")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> GenerateToken()
        {            
            var token = await _jwtTokenService.GenerateToken();
            return Ok(token);
        }

        /// <summary>
        /// Validate sample token
        /// </summary>
        /// <param name="token">Token for validation</param>
        /// <returns>Token validation status</returns>        
        [HttpPost("validate")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Token validation status")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Bad request for missing or invalid parameter")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]       
        public async Task<IActionResult> ValidateToken([FromBody] string token) 
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }
            var isValid = await _jwtTokenService.ValidateToken(token);            

            return Ok(new { isValid });
        }        
    }
}
