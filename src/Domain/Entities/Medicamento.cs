using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Medicamento
    {
        public int MedicamentoID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int LaboratorioID { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string IP { get; set; }

        public Laboratorio Laboratorio { get; set; }
        public ICollection<ParametroMinsa> ParametrosMinsa { get; set; }
        public ICollection<DetalleVenta> DetallesVenta { get; set; }
    }
}
