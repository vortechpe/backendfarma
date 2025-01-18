using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    public class UpdateUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } // Nueva contraseña (opcional)
        public Guid UsuarioActualizador { get; set; }

    }
}
