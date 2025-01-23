using Application.Interfaces;
using Application.Users.Commands;
using Application.Users.Responses;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISecurityService _securityService;

        public CreateUserCommandHandler(IUserRepository userRepository, ISecurityService securityService)
        {
            _userRepository = userRepository;
            _securityService = securityService;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
          
            // Verificar si el correo ya existe
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                throw new CustomException("El usuario con este correo ya existe.");

            // Crear un nuevo usuario
            var user = new User
            {
                Nombre = request.Nombre,
                Email = request.Email,
                Telefono = request.Telefono,
                UserName = request.UserName,
                StateUser = true
            };

            // Hashear la contraseña antes de guardarla
            var encryptedPasswordResult = _securityService.Encrypt(user.UserName, request.Password);

            // Asignar la contraseña encriptada al usuario
            user.PasswordHash = encryptedPasswordResult.EncryptedText;
            user.Key = encryptedPasswordResult.Key;
            user.Iv = encryptedPasswordResult.IV;

            // Guardar el usuario en la base de datos
            await _userRepository.AddAsync(user);

            return new CreateUserResponse
            {
                Id = user.Id,
                Email = user.Email
            };
        }
    }
}
