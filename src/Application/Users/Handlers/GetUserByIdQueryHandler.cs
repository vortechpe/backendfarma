using Application.Interfaces;
using Application.Users.Queries;
using Application.Users.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
                throw new Exception("Usuario no encontrado.");

            return new UserResponse
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Email = user.Email,
                Telefono = user.Telefono,
                UserName = user.UserName,
                FechaCreacion = user.FechaCreacion,
                Password = user.PasswordHash
            };
        }
    }
}
