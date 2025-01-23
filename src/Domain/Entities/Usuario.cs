using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; }
        public string ContraseñaHash { get; set; }
        public string ContraseñaSalt { get; set; }
        public int RolID { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string IP { get; set; }

        public Persona Persona { get; set; } // Relación con Persona
        public Rol Rol { get; set; } // Relación con Rol
    }
}
