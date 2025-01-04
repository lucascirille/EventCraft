using CORE.DTOs;
using Servicios.Interfaces;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EventCraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaracteristicaController : ControllerBase 
    {
        private readonly ICaracteristicaService _service;

        public CaracteristicaController(ICaracteristicaService service)
        {
            _service = service;
        }

        // GET: CaracteristicaController
        [HttpGet("obtenerCaracteristicas")]
        public async Task<ActionResult<RespuestaPrivada<ICollection<CaracteristicaDTOConId>>>> obtenerCaracteristicas()
        {
            var respuesta = await _service.GetCaracteristicas();
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

        // POST: CaracteristicaController
        [Authorize(Roles = "Admin")] 
        [HttpPost("crearCaracteristica")]
        public async Task<ActionResult<RespuestaPrivada<CaracteristicaDTO>>> crearCaracteristica(CaracteristicaDTO caracteristicaDTO)
        {
            var respuesta = await _service.PostCaracteristica(caracteristicaDTO);
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

        // DELETE: CaracteristicaController/eliminarCaracteristica/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("eliminarCaracteristica")]
        public async Task<ActionResult<RespuestaPrivada<Caracteristica>>> eliminarCaracteristica(int id)
        {
            var respuesta = await _service.DeleteCaracteristica(id);
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

        // PUT: CaracteristicaController/modificarCaracteristica/5
        [Authorize(Roles = "Admin")]
        [HttpPut("modificarCaracteristica")]
        public async Task<ActionResult<RespuestaPrivada<CaracteristicaDTO>>> modificarCaracteristica(int id, CaracteristicaDTO caracteristicaDTO)
        {
            var respuesta = await _service.PutCaracteristica(id, caracteristicaDTO);
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

        //PATCH: CaracteristicaController/modificarCaracteristicaEstado/5
        [Authorize(Roles = "Admin")]
        [HttpPatch("modificarCaracteristicaEstado")]
        public async Task<ActionResult<RespuestaPrivada<CaracteristicaDTOConId>>> modificarCaracteristicaEstado(int id, bool nuevoEstado)
        {
            var respuesta = await _service.PatchCaracteristicaEstado(id, nuevoEstado);
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
