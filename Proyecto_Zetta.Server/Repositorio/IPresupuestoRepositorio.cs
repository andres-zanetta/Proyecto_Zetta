using Proyecto_Zetta.DB.Data.Entity;

namespace Proyecto_Zetta.Server.repositorio
{
    public interface IPresupuestoRepositorio : IRepositorio<Presupuesto>
    {
        Task<Presupuesto> SelectById(int id);
    }
}
