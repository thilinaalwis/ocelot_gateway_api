using APIGateway.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jWTAuthenticationManager;

        public AuthController(IJwtAuthenticationManager jWTAuthenticationManager)
        {
            this.jWTAuthenticationManager = jWTAuthenticationManager;
        }

        [HttpPost("{key}")]
        [AllowAnonymous]
        public IActionResult Post(string key)
        {

            var token = jWTAuthenticationManager.Authenticate(key);
            
            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
    }
}
