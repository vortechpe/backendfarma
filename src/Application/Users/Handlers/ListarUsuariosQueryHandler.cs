using Application.Common;
using Application.Interfaces;
using Application.Users.Dtos;
using Application.Users.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Handlers
{
    public class ListarUsuariosQueryHandler : IRequestHandler<ListarUsuariosQuery, PagedResult<UsuarioDto>>
    {
        private readonly IUserRepository _usuarioRepository;

        public ListarUsuariosQueryHandler(IUserRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<PagedResult<UsuarioDto>> Handle(ListarUsuariosQuery request, CancellationToken cancellationToken)
        {
            var usuarios =  _usuarioRepository
                .GetAllAsync(request.QueryFilter)  // Pasamos el único parámetro de búsqueda
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();  // Convierte la consulta en una lista

            // Contar el total de usuarios con el filtro aplicado
            var totalUsuarios = await _usuarioRepository
                .CountAsync(request.QueryFilter);

            // Convertir a DTO
            var usuariosDto = usuarios.Select(usuario => new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                UserName = usuario.UserName,
                Telefono = usuario.Telefono,
                StateUser = usuario.StateUser,

                // Otros campos que necesites
            }).ToList();

            // Retornar el resultado paginado
            return new PagedResult<UsuarioDto>(usuariosDto, totalUsuarios);
        }
    }
}
