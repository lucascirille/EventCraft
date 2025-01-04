using CORE.DTOs;
using Servicios.Interfaces;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EventCraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonServicioController : ControllerBase
    {
        private readonly ISalonServicioService _service;

        public SalonServicioController(ISalonServicioService service)
        {
            _service = service;
        }

        // GET: SalonServicioController
        [HttpGet("obtenerSalonesServicios")]
        public async Task<ActionResult<RespuestaPrivada<ICollection<SalonServicioDTOConId>>>> obtenerSalonesServicios()
        {
            var respuesta = await _service.GetSalonesServicios();
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

        // POST: SalonServicioController
        [Authorize(Roles = "Admin")]
        [HttpPost("crearSalonServicio")]
        public async Task<ActionResult<RespuestaPrivada<SalonServicioDTO>>> crearSalonServicio(SalonServicioDTO salonServicioDTO)
        {
            var respuesta = await _service.PostSalonServicio(salonServicioDTO);
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

        // DELETE: SalonServicioController/eliminarSalonServicio/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("eliminarSalonServicio")]
        public async Task<ActionResult<RespuestaPrivada<SalonServicio>>> eliminarSalonServicio(int id)
        {
            var respuesta = await _service.DeleteSalonServicio(id);
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

        // PUT: SalonServicioController/modificarSalonServicio/5
        [Authorize(Roles = "Admin")]
        [HttpPut("modificarSalonServicio")]
        public async Task<ActionResult<RespuestaPrivada<SalonServicioDTO>>> modificarSalonServicio(int id, SalonServicioDTO salonServicioDTO)
        {
            var respuesta = await _service.PutSalonServicio(id, salonServicioDTO);
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

        // GET: SalonServicioController/obtenerServicioPorSalonId
        [HttpGet("obtenerServicioPorSalonId")]
        public async Task<ActionResult<RespuestaPrivada<ICollection<ServicioPrecioDTO>>>> obtenerServicioPorSalonId(int id)
        {
            var respuesta = await _service.GetServiciosBySalonId(id);
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
