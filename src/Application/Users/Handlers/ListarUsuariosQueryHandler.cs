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
            var totalUsuarios =  await _usuarioRepository.CountAsync();

            // Obtener los usuarios con paginación
            var usuarios =  _usuarioRepository
                .GetAllAsync()  // Este método debe devolver IQueryable<Usuario>
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();  // Aquí se utiliza ToListAsync para convertir la consulta en una lista

            // Convertir a DTO
            var usuariosDto = usuarios.Select(usuario => new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                // Otros campos que necesites
            }).ToList();

            // Retornar el resultado paginado
            return new PagedResult<UsuarioDto>(usuariosDto, totalUsuarios);
        }
    }
}
