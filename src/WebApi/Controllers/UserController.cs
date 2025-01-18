using Application.Users.Commands;
using Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarios([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            // Crear la consulta
            var query = new ListarUsuariosQuery(pageNumber, pageSize);

            // Ejecutar la consulta a través de MediatR
            var resultado = await _mediator.Send(query);

            // Devolver el resultado como respuesta HTTP
            return Ok(resultado);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
                return BadRequest("El ID del usuario no coincide.");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new DeleteUserCommand { Id = id, UsuarioActualizador = Guid.Parse(userId) };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
