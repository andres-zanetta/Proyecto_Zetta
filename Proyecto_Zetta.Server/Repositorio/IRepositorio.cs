﻿using Proyecto_Zetta.DB.Data;

namespace Proyecto_Zetta.Server.repositorio
{
    public interface IRepositorio<E> where E : class, IEntityBase
    {
        Task<bool> Delete(int id);
        Task<bool> Existe(int id);
        Task<int> Insert(E entidad);
        Task<List<E>> Select();
        Task<E> SelectById(int id);
        Task<bool> Update(int id, E entidad);
    }
}