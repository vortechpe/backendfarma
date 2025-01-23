using Application.Auth.Commands;
using Application.Auth.Responses;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISecurityService _securityService;
        private readonly IConfiguration _configuration;
        public LoginCommandHandler(IUserRepository userRepository, ISecurityService securityService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _securityService = securityService;
            _configuration = configuration;
        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !_securityService.VerifyPassword(request.Password, user.PasswordHash, user.Key, user.Iv))
                throw new UnauthorizedAccessException("Credenciales incorrectas.");

            // Generar JWT
            var token = GenerateJwtToken(user);

            return new LoginResponse
            {
                Token = token,
                Email = user.Email
            };
        }
        private bool VerifyPassword(string password, string passwordHash)
        {
            // Simular validación (reemplazar con hashing real)
            return password == passwordHash;
        }
        private string GenerateJwtToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

            };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
