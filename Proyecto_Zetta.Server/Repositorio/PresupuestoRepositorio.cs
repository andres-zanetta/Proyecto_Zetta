using Microsoft.EntityFrameworkCore;
using Proyecto_Zetta.DB.Data;
using Proyecto_Zetta.DB.Data.Entity;

namespace Proyecto_Zetta.Server.repositorio
{
    public class PresupuestoRepositorio:Repositorio<Presupuesto>,IPresupuestoRepositorio
    {
        private readonly Context context;
        public PresupuestoRepositorio(Context context):base(context)
        {
            this.context = context;
        }

        public async Task<Presupuesto>SelectById(int id)//este debe estar aca
        {
            Presupuesto? zetta = await context.Presupuestos.FirstOrDefaultAsync(x => x.Id == id);

            return zetta;
        }
    }
}
