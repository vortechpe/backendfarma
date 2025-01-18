using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public (string PasswordHash, string PasswordSalt) HashPassword(string password)
        {
            // Generar el salt (puede ser una cadena aleatoria)
            var salt = GenerateSalt();
            // Crear el hash concatenando la contraseña y el salt
            var passwordHash = HashPasswordWithSalt(password, salt);

            return (passwordHash, salt);
        }

        public bool VerifyPassword(string password, string passwordHash, string passwordSalt)
        {
            // Verificar si el hash de la contraseña coincide con el hash original usando el salt
            var hashOfInputPassword = HashPasswordWithSalt(password, passwordSalt);
            return hashOfInputPassword == passwordHash;
        }

        private string GenerateSalt()
        {
            // Generar un salt aleatorio (puedes usar una función más robusta como RNGCryptoServiceProvider)
            var saltBytes = new byte[16];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPasswordWithSalt(string password, string salt)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var combined = password + salt;
                var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(combined));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
