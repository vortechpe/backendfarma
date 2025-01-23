using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Personas.Commands
{
    internal class CreatePersonaCommand
    {
        public int Nombre { get; set; }
        public int Apellido { get; set; }
        public int TipoPersonaID { get; set; }
        public int TipoEntidadID { get; set; }
        public int DocumentoIdentidad { get; set; }
        public int RazonSocial { get; set; }
        public int Direccion { get; set; }
        public int Telefono { get; set; }
        public int Email { get; set; }
    }
}
