using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Proyecto_Zetta.DB.Data;
using Proyecto_Zetta.DB.Data.Entity;
using Proyecto_Zetta.Server.repositorio;
using Proyecto_Zetta.Shared.DTO;

namespace Proyecto_Zetta.Server.Controllers
{
    [ApiController]
    [Route("api/Presupuestos")]
    public class PresupuestosControllers : ControllerBase
    {
        private readonly IPresupuestoRepositorio repositorio;
        private readonly IMapper mapper;

        public PresupuestosControllers(IPresupuestoRepositorio repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]//select
        public async Task<ActionResult<List<Presupuesto>>> Get()
        {
            return await repositorio.Select();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Presupuesto>> GetById(int id)
        {
            var zetta = await repositorio.SelectById(id);
            if (zetta == null)
            {
                return NotFound();
            }
            return zetta;
        }

        [HttpGet("Existe/{id:int}")]

        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
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
                
                return await repositorio.Insert(entidad);
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
            var zetta = await repositorio.SelectById(id);

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
               
                await repositorio.Update(id, zetta);
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
            var existe = await repositorio.Existe(id);
            if(!existe)
            {
                return NotFound($"El Presupuesto{id} no existe");
            }
            if (await repositorio.Delete(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
