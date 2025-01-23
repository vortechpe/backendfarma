using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Dtos
{
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Telefono { get; set; }
        public bool StateUser { get; set; }

        // Otros campos que necesites
    }
}
