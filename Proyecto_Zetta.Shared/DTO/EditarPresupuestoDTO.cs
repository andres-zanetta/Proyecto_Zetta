using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Zetta.Shared.DTO
{
    public class EditarPresupuestoDTO
    {
        [Required(ErrorMessage = "Inserte el Id correspondiente.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Estado del presupuesto es obligatorio.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "El Insumo del presupuesto es obligatorio.")]
        public long Insumos { get; set; }

        [Required(ErrorMessage = "La mano de Obra es obligatoria.")]
        public long ManodeObra { get; set; }

        public long PrecioFinal { get; set; }
    }
}
