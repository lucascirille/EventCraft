using CORE.DTOs;
using Servicios.Interfaces;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EventCraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly IServicioService _service;

        public ServicioController(IServicioService service)
        {
            _service = service;
        }

        // GET: ServicioController
        [HttpGet("obtenerServicios")]
        public async Task<ActionResult<RespuestaPrivada<ICollection<ServicioDTOConId>>>> obtenerServicios()
        {
            var respuesta = await _service.GetServicios();
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

        // POST: ServicioController
        [Authorize(Roles = "Admin")]
        [HttpPost("crearServicio")]
        public async Task<ActionResult<RespuestaPrivada<ServicioDTO>>> crearServicio(ServicioDTO servicioDTO)
        {
            var respuesta = await _service.PostServicio(servicioDTO);
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

        // DELETE: ServicioController/eliminarServicio/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("eliminarServicio")]
        public async Task<ActionResult<RespuestaPrivada<Servicio>>> eliminarServicio(int id)
        {
            var respuesta = await _service.DeleteServicio(id);
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

        // PUT: ServicioController/modificarServicio/5
        [Authorize(Roles = "Admin")]
        [HttpPut("modificarServicio")]
        public async Task<ActionResult<RespuestaPrivada<ServicioDTO>>> modificarServicio(int id, ServicioDTO servicioDTO)
        {
            var respuesta = await _service.PutServicio(id, servicioDTO);
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

        //PATCH: ServicioController/modificarServicioEstado/5
        [Authorize(Roles = "Admin")]
        [HttpPatch("modificarServicioEstado")]
        public async Task<ActionResult<RespuestaPrivada<ServicioDTOConId>>> modificarServicioEstado(int id, bool nuevoEstado)
        {
            var respuesta = await _service.PatchServicioEstado(id, nuevoEstado);
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
