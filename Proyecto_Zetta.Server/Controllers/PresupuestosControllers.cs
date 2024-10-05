using Proyecto_Zetta.DB.Data;
using Proyecto_Zetta.DB.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Globalization;
using Proyecto_Zetta.Shared.DTO;
using Proyecto_Zetta.Server.Repositorio;


namespace Proyecto_Zetta.Server.Controllers
{
    [ApiController]
    [Route("api/Presupuestos")]
    public class PresupuestosControllers : ControllerBase
    {
        private readonly IPresupuestoRepositorio repositorio;
        private readonly IMapper mapper;

        public PresupuestosControllers(IPresupuestoRepositorio repositorio,
            IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]

        public async Task<ActionResult<List<Presupuesto>>> Get()
        {
            return await repositorio.Select(); 
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Presupuesto>>Get(int id)
        {

            Presupuesto? zetta = await repositorio.SelectById(id);
            if(zetta== null)
            {
                return NotFound("No se encontro el presupuesto indicado");
            }
            return zetta;
        }

        [HttpPost]

        public async Task<ActionResult<int>> Post(CrearPresupuestoDTO entidadDTO)
        {
            try
            {
             

                Presupuesto entidad = mapper.Map<Presupuesto>(entidadDTO); 


                
                return await repositorio.Insert(entidad);
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
            var zetta = await repositorio.SelectById(id);

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
                await repositorio.Update(id, zetta);
             
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
            var existe = await repositorio.Existe(id);
            if(!existe)
            {
                return NotFound($"El Presupuesto {id} no existe.");
            }
            if(await repositorio.Delete(id))
            {
                return Ok();
            }

            await repositorio.Delete(id);
            return Ok();
        }
    }
}
