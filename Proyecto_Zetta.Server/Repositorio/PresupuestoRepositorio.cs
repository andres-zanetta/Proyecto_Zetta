using Proyecto_Zetta.DB.Data;
using Proyecto_Zetta.DB.Data.Entity;

namespace Proyecto_Zetta.Server.Repositorio
{
    public class PresupuestoRepositorio: Repositorio<Presupuesto>,IPresupuestoRepositorio
    {
        public PresupuestoRepositorio(Context context) : base(context)
        {
            
        }


    }
}
