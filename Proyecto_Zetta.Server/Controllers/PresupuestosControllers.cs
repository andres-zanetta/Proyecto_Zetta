using Proyecto_Zetta.DB.Data;
using Proyecto_Zetta.DB.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Globalization;


namespace Proyecto_Zetta.Server.Controllers
{
    [ApiController]
    [Route("api/Presupuestos")]
    public class PresupuestosControllers : ControllerBase
    {
        private readonly Context context;

        public PresupuestosControllers(Context context)
        {
            this.context = context;
        }

        [HttpGet]

        public async Task<ActionResult<List<Presupuesto>>> Get()
        {
            return await context.Presupuestos.ToListAsync();
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Presupuesto>>Get(int id)
        {
            Presupuesto? zetta = await context.Presupuestos.FirstOrDefaultAsync(x => x.Id == id);
            if(zetta== null)
            {
                return NotFound("No se encontro el presupuesto indicado");
            }
            return zetta;
        }

        [HttpPost]

        public async Task<ActionResult<string>> Post(Presupuesto entidad)
        {
            try
            {
                context.Presupuestos.Add(entidad);
                await context.SaveChangesAsync();
                return entidad.Estado;
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPut]

        public async Task<ActionResult> Put(int id, [FromBody] Presupuesto entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos Incorrectos");
            }
            var zetta = await context.Presupuestos.Where(e => e.Id == id).FirstOrDefaultAsync();

            if (zetta == null)
            {
                return NotFound("No existe el Presupuesto busacado");

            }
            //salen del DTO
            zetta.Estado = entidad.Estado;
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

        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Presupuestos.AnyAsync(x => x.Id == id);
            if(!existe)
            {
                return NotFound($"El Presupuesto {id} no existe.");
            }
            Presupuesto EntidadABorrar = new Presupuesto();
            EntidadABorrar.Id = id;

            context.Remove(EntidadABorrar);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
