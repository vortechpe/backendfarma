using Application.Interfaces;
using Application.Users.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            user.StateUser = user.StateUser != true ? user.StateUser : false;
            user.FechaActualizacion = DateTime.UtcNow;
            user.UsuarioActualizador = request.UsuarioActualizador;
            if (user == null)
                throw new Exception("Usuario no encontrado.");

            // Eliminar el usuario
            await _userRepository.UpdateAsync(user);
        }
    }
}
