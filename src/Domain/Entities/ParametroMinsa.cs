using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ParametroMinsa
    {
        public int ParametroID { get; set; }
        public int MedicamentoID { get; set; }
        public string RegistroSanitario { get; set; }
        public string FormaFarmaceutica { get; set; }
        public string CondicionVenta { get; set; } // Ejemplo: "Receta Médica", "Venta Libre"
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string IP { get; set; }

        public Medicamento Medicamento { get; set; }
    }
}
