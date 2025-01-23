using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
