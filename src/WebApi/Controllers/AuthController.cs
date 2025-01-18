using Application.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false, // La cookie no es accesible desde JavaScript
                Secure = false,   // Solo en HTTPS
                SameSite = SameSiteMode.Lax, // Permite cookies entre dominios
                Expires = DateTime.Now.AddHours(1), // Duración de la cookie
                Path = "/"
            };

            Response.Cookies.Append("JwtToken", result.Token, cookieOptions);
            return Ok(result);
        }
    }
}
