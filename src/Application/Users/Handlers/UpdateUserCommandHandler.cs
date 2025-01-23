using Application.Interfaces;
using Application.Users.Commands;
using Domain.Exceptions;
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
        private readonly ISecurityService _securityService;

        public UpdateUserCommandHandler(IUserRepository userRepository, ISecurityService securityService)
        {
            _userRepository = userRepository;
            _securityService = securityService;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
                throw new CustomException("Usuario no encontrado.");

            user.Nombre = request.Nombre ?? user.Nombre;
            user.Telefono = request.Telefono ?? user.Telefono;
            user.UserName = request.UserName ?? user.UserName;
            if (!string.IsNullOrEmpty(request.Password))
            {
                // Hashear la nueva contraseña con el salt
                var encryptedPasswordResult = _securityService.Encrypt(user.UserName, request.Password);
                user.PasswordHash = encryptedPasswordResult.EncryptedText;
            }
            user.FechaActualizacion = DateTime.UtcNow;
            user.UsuarioActualizador = request.UsuarioActualizador;

            // Guardar los cambios
            await _userRepository.UpdateAsync(user);
        }
    }
}
