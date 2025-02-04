using CORE.DTOs;
using Servicios.Interfaces;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EventCraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonController : ControllerBase 
    {
        private readonly ISalonService _service;

        public SalonController(ISalonService service)
        {
            _service = service;
        }

        // GET: SalonController
        [HttpGet("obtenerSalones")]
        public async Task<ActionResult<RespuestaPrivada<ICollection<SalonDTOConId>>>> obtenerSalones()
        {
            var respuesta = await _service.GetSalones();
            if (respuesta.Datos == null)
            {
                if (respuesta.Mensaje.StartsWith("Error interno"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }

        // POST: SalonController
        [Authorize(Roles = "Admin")]
        [HttpPost("crearSalon")]
        public async Task<ActionResult<RespuestaPrivada<SalonDTO>>> crearSalon(SalonDTO salonDTO)
        {
            var respuesta = await _service.PostSalon(salonDTO);
            if (respuesta.Datos == null)
            {
                if (respuesta.Mensaje.StartsWith("Error interno"))                               
                {                                                                               
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                return BadRequest(respuesta);
            }
            return StatusCode(StatusCodes.Status201Created, respuesta);
        }

        // DELETE: SalonController/eliminarSalon/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("eliminarSalon")]
        public async Task<ActionResult<RespuestaPrivada<Salon>>> eliminarSalon(int id)
        {
            var respuesta = await _service.DeleteSalon(id);
            if (respuesta.Datos == null)
            {
                if (respuesta.Mensaje.StartsWith("Error interno"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                return NotFound(respuesta);
            }
            return Ok(respuesta);
        }

        // PUT: SalonController/modificarSalon/5
        [Authorize(Roles = "Admin")]
        [HttpPut("modificarSalon")]
        public async Task<ActionResult<RespuestaPrivada<SalonDTO>>> modificarSalon(int id, SalonDTO salonDTO)
        {
            var respuesta = await _service.PutSalon(id, salonDTO);
            if (respuesta.Datos == null)
            {
                if (respuesta.Mensaje.StartsWith("Error interno"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }

        //PATCH: SalonController/modificarSalonEstado/5
        [Authorize(Roles = "Admin")]
        [HttpPatch("modificarSalonEstado")]
        public async Task<ActionResult<RespuestaPrivada<SalonDTOConId>>> modificarSalonEstado(int id, bool nuevoEstado)
        {
            var respuesta = await _service.PatchSalonEstado(id, nuevoEstado);
            if (respuesta.Datos == null)
            {
                if (respuesta.Mensaje.StartsWith("Error interno"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                return NotFound(respuesta);
            }
            return Ok(respuesta);
        }
    }
}
