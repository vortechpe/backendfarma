using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Laboratorio
    {
        public int LaboratorioID { get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string IP { get; set; }

        public ICollection<Medicamento> Medicamentos { get; set; }
    }
}
