using CORE.DTOs;
using Servicios.Interfaces;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Servicios.Servicios;
using Microsoft.AspNetCore.Authorization;

namespace EventCraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _service;

        public ReservaController(IReservaService service)
        {
            _service = service;
        }

        // GET: ReservaController
        [HttpGet("obtenerReservas")]
        public async Task<ActionResult<RespuestaPrivada<ICollection<ReservaDTOConId>>>> obtenerReservas()
        {
            var respuesta = await _service.GetReservas();
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

        // POST: ReservaController
        [Authorize(Roles = "Admin, Cliente")]
        [HttpPost("crearReserva")]
        public async Task<ActionResult<RespuestaPrivada<ReservaDTO>>> crearReserva(ReservaDTO reservaDTO)
        {
            var respuesta = await _service.PostReserva(reservaDTO);
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

        // DELETE: ReservaController/eliminarReserva/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("eliminarReserva")]
        public async Task<ActionResult<RespuestaPrivada<Reserva>>> eliminarReserva(int id)
        {
            var respuesta = await _service.DeleteReserva(id);
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

        // PUT: ReservaController/modificarReserva/5
        [Authorize(Roles = "Admin")]
        [HttpPut("modificarReserva")]
        public async Task<ActionResult<RespuestaPrivada<ReservaDTO>>> modificarReserva(int id, ReservaDTO reservaDTO)
        {
            var respuesta = await _service.PutReserva(id, reservaDTO);
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

        [HttpGet("fechasOcupadas")]
        public async Task<ActionResult<RespuestaPrivada<ICollection<string>>>> obtenerFechasOcupadas(int salonId, string franjaHoraria)
        {
            var respuesta = await _service.GetFechasOcupadasBySalonIdYFH(salonId, franjaHoraria);
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

        // GET: ReservaController/obtenerReservasDeSalonesSociales
        [HttpGet("obtenerReservasDeSalonesSociales")]
        public async Task<ActionResult<RespuestaPrivada<ICollection<ReservaDTOConId>>>> obtenerReservasDeSalonesSociales()
        {
            var respuesta = await _service.GetReservasDeSalonesSociales();
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

        // GET: ReservaController/obtenerReservasDeSalonesCorporativos
        [HttpGet("obtenerReservasDeSalonesCorporativos")]
        public async Task<ActionResult<RespuestaPrivada<ICollection<ReservaDTOConId>>>> obtenerReservasDeSalonesCorporativos()
        {
            var respuesta = await _service.GetReservasDeSalonesCorporativos();
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

        // GET: ReservaController/obtenerReservaId
        [HttpGet("obtenerReservaId")]
        public async Task<ActionResult<RespuestaPrivada<int>>> obtenerReservaId(string nombreReserva)
        {
            var respuesta = await _service.GetReservaIdByReservaNombre(nombreReserva);
            if (respuesta.Datos == 0)
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
