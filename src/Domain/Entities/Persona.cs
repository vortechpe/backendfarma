using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Persona
    {
        public int PersonaID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int TipoPersonaID { get; set; } // Relación con TiposPersona
        public int TipoEntidadID { get; set; } // Relación con TiposEntidad
        public string DocumentoIdentidad { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string IP { get; set; }

        public TipoPersona TipoPersona { get; set; }
        public TipoEntidad TipoEntidad { get; set; }
        public ICollection<Usuario> Usuarios { get; set; } // Relación con Usuarios
    }
}
