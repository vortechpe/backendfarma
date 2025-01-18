using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPasswordHasher
    {
        (string PasswordHash, string PasswordSalt) HashPassword(string password);

        // Verifica si una contraseña coincide con el hash y salt almacenados.
        bool VerifyPassword(string password, string storedHash, string storedSalt);
    }
}
