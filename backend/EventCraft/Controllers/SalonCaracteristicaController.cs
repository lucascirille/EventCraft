using CORE.DTOs;
using Servicios.Interfaces;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EventCraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonCaracteristicaController : ControllerBase 
    {
        private readonly ISalonCaracteristicaService _service;

        public SalonCaracteristicaController(ISalonCaracteristicaService service)
        {
            _service = service;
        }

        // GET: SalonCaracteristicaController
        [HttpGet("obtenerSalonesCaracteristicas")]
        public async Task<ActionResult<RespuestaPrivada<ICollection<SalonCaracteristicaDTOConId>>>> obtenerSalonesCaracteristicas()
        {
            var respuesta = await _service.GetSalonesCaracteristicas();
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

        // POST: SalonCaracteristicaController
        [Authorize(Roles = "Admin")]
        [HttpPost("crearSalonCaracteristica")]
        public async Task<ActionResult<RespuestaPrivada<SalonCaracteristicaDTO>>> crearSalonCaracteristica(SalonCaracteristicaDTO salonCaracteristicaDTO)
        {
            var respuesta = await _service.PostSalonCaracteristica(salonCaracteristicaDTO);
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

        // DELETE: SalonCaracteristicaController/eliminarSalonCaracteristica/5 
        [Authorize(Roles = "Admin")]
        [HttpDelete("eliminarSalonCaracteristica")]
        public async Task<ActionResult<RespuestaPrivada<SalonCaracteristica>>> eliminarSalonCaracteristica(int id)
        {
            var respuesta = await _service.DeleteSalonCaracteristica(id);
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

        // PUT: SalonCaracteristicaController/modificarSalonCaracteristica/5
        [Authorize(Roles = "Admin")]
        [HttpPut("modificarSalonCaracteristica")]
        public async Task<ActionResult<RespuestaPrivada<SalonCaracteristicaDTO>>> modificarSalonCaracteristica(int id, SalonCaracteristicaDTO salonCaracteristicaDTO)
        {
            var respuesta = await _service.PutSalonCaracteristica(id, salonCaracteristicaDTO);
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

        // GET: SalonCaracteristicaController/obtenerCaracteristicaPorSalonId
        [HttpGet("obtenerCaracteristicaPorSalonId")]
        public async Task<ActionResult<RespuestaPrivada<ICollection<CaracteristicaDTO>>>> obtenerCaracteristicaPorSalonId(int id)
        {
            var respuesta = await _service.GetCaracteristicasBySalonId(id);
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
    }
}
