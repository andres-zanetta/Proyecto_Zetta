using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Zetta.DB.Data;
using Proyecto_Zetta.DB.Data.Entity;
using Proyecto_Zetta.Shared.DTO;

namespace Proyecto_Zetta.Server.Repositorio
{
    public class Repositorio<E> : IRepositorio<E> 
        where E : class, IEntityBase
    {
        private readonly Context context;

        public Repositorio(Context context)
        {
            this.context = context;
        }

        public async Task<bool> Existe(int id)
        {
            var existe = await context.Set<E>()
                                      .AnyAsync(x => x.Id == id);
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
                //Presupuesto entidad = new Presupuesto();
                //entidad.Estado = entidadDTO.Estado;
                //entidad.Insumos = entidadDTO.Insumos;
                //entidad.ManodeObra = entidadDTO.ManodeObra;
                //entidad.PrecioFinal = entidadDTO.PrecioFinal;

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
            //salen del DTO


            try
            {
                context.Set<E>().Update(zetta);
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
