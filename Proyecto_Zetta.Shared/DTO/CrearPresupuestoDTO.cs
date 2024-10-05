using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Zetta.Shared.DTO
{
     public class CrearPresupuestoDTO
     {
        [Required(ErrorMessage = "El Estado del presupuesto es obligatorio.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Los insumos del presupuesto son obligatorios.")]
        public long Insumos { get; set; }

        [Required(ErrorMessage = "La mano de obra del presupuesto es obligatoria.")]
        public long ManodeObra { get; set; }

        [Required(ErrorMessage = "El Precio Final del presupuesto es obligatorio.")]
        public long PrecioFinal { get; set; }

     }
}
