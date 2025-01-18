using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } // Nombre completo
        public string Email { get; set; } // Correo electrónico (único)
        public string Telefono { get; set; } // Teléfono de contacto opcional

        // Propiedades relacionadas con autenticación
        public string UserName { get; set; }
        public string PasswordHash { get; set; } // Contraseña encriptada
        public string PasswordSalt { get; set; } // Salt para la encriptación

        // Propiedades de auditoría
        public DateTime FechaCreacion { get; private set; }
        public Guid UsuarioCreador { get;  set; }
        public DateTime? FechaActualizacion { get;  set; }
        public Guid UsuarioActualizador { get;  set; }
        public bool StateUser { get; set; }

        public User()
        {
            // Establecer la fecha de creación al momento de la creación del objeto
            FechaCreacion = DateTime.UtcNow;
        }
    }
}
