using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Zetta.DB.Data;
using Proyecto_Zetta.DB.Data.Entity;
using Proyecto_Zetta.Shared.DTO;

namespace Proyecto_Zetta.Server.repositorio
{
    public class Repositorio<E> : IRepositorio<E> 
        where E : class, IEntityBase //este repo trabaja sobre una entidad
    {
        private readonly Context context;

        public Repositorio(Context context)
        {
            this.context = context;
        }

        public async Task<bool> Existe(int id)
        {
            var existe = await context.Set<E>().AnyAsync(x => x.Id == id);
            return existe;

        }
        public async Task<E> SelectById(int id)
        {
            E? zetta = await context.Set<E>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return zetta;
        }

        public async Task<List<E>> Select()
        {
            return await context.Set<E>().ToListAsync();
        }

        public async Task<int> Insert(E entidad)
        {
            try
            {

                await context.Set<E>().AddAsync(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {
                throw e;

            }
        }

        public async Task<bool> Update(int id, E entidad)
        {
            if (id != entidad.Id)
            {
                return false;
            }
            var zetta = await SelectById(id);

            if (zetta == null)
            {
                return false;

            }

            try
            {
                context.Set<E>().Update(entidad);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;

            }

        }

        public async Task<bool> Delete(int id)

        {
            var zetta = await SelectById(id);

            if (zetta == null)
            {
                return false;

            }

            context.Set<E>().Remove(zetta);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
