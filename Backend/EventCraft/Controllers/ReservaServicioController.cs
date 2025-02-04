using CORE.DTOs;
using Servicios.Interfaces;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EventCraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaServicioController : ControllerBase
    {
        private readonly IReservaServicioService _service;

        public ReservaServicioController(IReservaServicioService service)
        {
            _service = service;
        }

        // GET: ReservaServicioController
        [HttpGet("obtenerReservasServicios")]
        public async Task<ActionResult<RespuestaPrivada<ICollection<ReservaServicioDTOConId>>>> obtenerReservasServicios()
        {
            var respuesta = await _service.GetReservasServicios();
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

        // POST: ReservaServicioController
        [Authorize(Roles = "Admin, Cliente")]
        [HttpPost("crearReservaServicio")]
        public async Task<ActionResult<RespuestaPrivada<ReservaServicioDTO>>> crearReservaServicio(ReservaServicioDTO reservaServicioDTO)
        {
            var respuesta = await _service.PostReservaServicio(reservaServicioDTO);
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

        // DELETE: ReservaServicioController/eliminarReservaServicio/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("eliminarReservaServicio")]
        public async Task<ActionResult<RespuestaPrivada<ReservaServicio>>> eliminarReservaServicio(int id)
        {
            var respuesta = await _service.DeleteReservaServicio(id);
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

        // PUT: ReservaServicioController/modificarReservaServicio/5
        [Authorize(Roles = "Admin")]
        [HttpPut("modificarReservaServicio")]
        public async Task<ActionResult<RespuestaPrivada<ReservaServicioDTO>>> modificarReservaServicio(int id, ReservaServicioDTO reservaServicioDTO)
        {
            var respuesta = await _service.PutReservaServicio(id, reservaServicioDTO);
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
