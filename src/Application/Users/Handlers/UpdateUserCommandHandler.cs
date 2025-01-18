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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UpdateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
                throw new Exception("Usuario no encontrado.");

            user.Nombre = request.Nombre ?? user.Nombre;
            user.Telefono = request.Telefono ?? user.Telefono;
            user.UserName = request.UserName ?? user.UserName;
            if (!string.IsNullOrEmpty(request.Password))
            {
                // Hashear la nueva contraseña con el salt
                var (passwordHash, passwordSalt) = _passwordHasher.HashPassword(request.Password);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            user.FechaActualizacion = DateTime.UtcNow;
            user.UsuarioActualizador = request.UsuarioActualizador;

            // Guardar los cambios
            await _userRepository.UpdateAsync(user);
        }
    }
}
