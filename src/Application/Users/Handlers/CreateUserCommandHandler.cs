using Application.Interfaces;
using Application.Users.Commands;
using Application.Users.Responses;
using Domain.Entities;
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
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Verificar si el correo ya existe
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                throw new Exception("El usuario con este correo ya existe.");

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
            var (passwordHash, passwordSalt) = _passwordHasher.HashPassword(request.Password);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

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
