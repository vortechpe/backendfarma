using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Venta
    {
        public int VentaID { get; set; }
        public int PersonaID { get; set; }
        public string TipoComprobante { get; set; } // Boleta, Factura, Nota de Venta
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string IP { get; set; }

        public Persona Persona { get; set; }
        public ICollection<DetalleVenta> DetallesVenta { get; set; }
    }
}
