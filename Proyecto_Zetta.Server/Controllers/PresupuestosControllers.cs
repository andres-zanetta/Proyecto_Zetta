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

        [HttpGet("GetById/{id:int}")]

        public async Task<ActionResult<Presupuesto>>GetById(int id)
        {

            var zetta = await repositorio.SelectById(id);
            if(zetta== null)
            {
                return NotFound("No se encontro el presupuesto indicado");
            }
            return zetta;
        }

        [HttpPost]

        public async Task<ActionResult<int>> Post( CrearPresupuestoDTO entidadDTO)
        {
            
            try
            {
                Presupuesto entidad = mapper.Map<Presupuesto>(entidadDTO);

                return await repositorio.Insert(entidad);
             
            }


            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }

            //try
            //{

            //    Presupuesto entidad = mapper.Map<Presupuesto>(entidadDTO); 

            //    return await repositorio.Insert(entidad);
            //}
            //catch (Exception e)
            //{

            //    return BadRequest(e.Message);
            //}

        }

        [HttpPut("{Id:int}")]

        public async Task<ActionResult> Put(int Id, [FromBody] CrearPresupuestoDTO entidadDTO)
        {
            try
            {

                if (Id != entidadDTO.Id)
                {
                    return BadRequest("Datos Incorrectos");
                }
                Presupuesto entidad = mapper.Map<Presupuesto>(entidadDTO);
                var zetta = await repositorio.Update(Id,entidad);

                if (!zetta )
                {
                    return NotFound("No existe el Presupuesto busacado");

                }
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
            await repositorio.Delete(id);
            return Ok();
        }
    }
}
