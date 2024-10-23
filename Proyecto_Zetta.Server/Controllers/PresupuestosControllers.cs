using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Proyecto_Zetta.DB.Data;
using Proyecto_Zetta.DB.Data.Entity;
using Proyecto_Zetta.Shared.DTO;

namespace Proyecto_Zetta.Server.Controllers
{
    [ApiController]
    [Route("api/Presupuestos")]
    public class PresupuestosControllers : ControllerBase
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public PresupuestosControllers(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]//select
        public async Task<ActionResult<List<Presupuesto>>> Get()
        {
            return await context.Presupuestos.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Presupuesto>> Get(int id)
        {
            var zetta = await context.Presupuestos.FirstOrDefaultAsync(x => x.Id == id);
            if (zetta == null)
            {
                return NotFound();
            }
            return zetta;
        }

        [HttpGet("Existe/{id:int}")]

        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await context.Presupuestos.AnyAsync(x => x.Id == id);
            return existe; 
        
        }

        [HttpPost]//insert

        public async Task<ActionResult<int>>Post(CrearPresupuestoDTO entidadDTO)
        {
            try
            {
                //Presupuesto entidad = new Presupuesto();
                //entidad.Estado = entidadDTO.Estado;
                //entidad.Insumos = entidadDTO.Insumos;
                //entidad.ManodeObra = entidadDTO.ManodeObra;
                //entidad.PrecioFinal = entidadDTO.PrecioFinal;

                Presupuesto entidad = mapper.Map<Presupuesto>(entidadDTO);
                context.Presupuestos.Add(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

        [HttpPut("{id:int}")]//update
        public async Task<ActionResult> Put(int id, [FromBody] Presupuesto entidad)
        {
            if(id!= entidad.Id)
            {
                return BadRequest("Datos del presupuesto incorrecto");
            }
            var zetta = await context.Presupuestos.
                        Where(e => e.Id == id).FirstOrDefaultAsync();

            if (zetta == null)
            {
                return NotFound("No existe el Presupuesto Buscado ");

            }
            
            zetta.Estado= entidad.Estado;
            zetta.Insumos = entidad.Insumos;
            zetta.ManodeObra = entidad.ManodeObra;
            zetta.PrecioFinal = entidad.PrecioFinal;

            try
            {
                context.Presupuestos.Update(zetta);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult>Delete(int id)

        {
            var existe = await context.Presupuestos.AnyAsync(x => x.Id == id);//en la tabla Presupuesto existe un id si existe dveulve ToF
            if(!existe)
            {
                return NotFound($"El Presupuesto{id} no existe");
            }
            Presupuesto EntidadaBorrar = new Presupuesto();
            EntidadaBorrar.Id = id;

            context.Remove(EntidadaBorrar);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
