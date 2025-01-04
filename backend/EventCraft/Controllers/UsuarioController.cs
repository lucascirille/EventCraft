using CORE.DTOs;
using Servicios.Interfaces;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Servicios.Servicios;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace EventCraft.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        // GET: UsuarioController
        [HttpGet("obtenerUsuarios")]
        public async Task<ActionResult<RespuestaPrivada<ICollection<UsuarioDTOConId>>>> obtenerUsuarios()
        {
            var respuesta = await _service.GetUsuarios();
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

        // GET: UsuarioController
        [HttpGet("obtenerUsuariosSinPass")]
        public async Task<ActionResult<RespuestaPrivada<ICollection<UsuarioSinPassDTO>>>> obtenerUsuariosSinPass()
        {
            var respuesta = await _service.GetUsuariosSinPass();
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

        // POST: UsuarioController
        [Authorize(Roles = "Admin")]
        [HttpPost("crearUsuario")]
        public async Task<ActionResult<RespuestaPrivada<UsuarioDTO>>> crearUsuario(UsuarioDTO usuarioDTO)
        {
            var respuesta = await _service.PostUsuario(usuarioDTO);
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

        // DELETE: UsuarioController/eliminarUsuario/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("eliminarUsuario")]
        public async Task<ActionResult<RespuestaPrivada<Usuario>>> eliminarUsuario(int id)
        {
            var respuesta = await _service.DeleteUsuario(id);
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

        // PUT: UsuarioController/modificarUsuario/5
        [Authorize(Roles = "Admin")]
        [HttpPut("modificarUsuario")]
        public async Task<ActionResult<RespuestaPrivada<UsuarioDTO>>> modificarUsuario(int id, UsuarioDTO usuarioDTO)
        {
            var respuesta = await _service.PutUsuario(id, usuarioDTO);
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

        //PATCH: UsuarioController/modificarUsuarioRol/5
        [Authorize(Roles = "Admin")]
        [HttpPatch("modificarUsuarioRol")]
        public async Task<ActionResult<RespuestaPrivada<UsuarioDTOConId>>> modificarUsuarioRol(int id, string nuevoRol)
        {
            var respuesta = await _service.PatchUsuarioRol(id, nuevoRol);
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

        //POST: UsuarioController/login
        [HttpPost("login")]
        public async Task<ActionResult<RespuestaPrivada<LoginUsuarioConRolDTO>>> Login(LoginUsuarioDTO loginUsuario)
        {
            var respuesta = await _service.AutenticarUsuario(loginUsuario);

            if (respuesta.Datos == null)
                if (respuesta.Mensaje.StartsWith("Error interno"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                else if(respuesta.Mensaje.StartsWith("Usuario no encontrado"))
                {
                    return Unauthorized(new { message = "Usuario incorrecto" });

                }
                else
                {
                    return Unauthorized(new { message = "Contraseña Incorrecta" });
                }

            // Guardar datos del usuario en la sesión
            //HttpContext.Session.SetString("UserName", respuesta.Datos.LoginUsuario.NombreUsuario);
            //HttpContext.Session.SetString("UserId", respuesta.Datos.Id.ToString());
            //HttpContext.Session.SetString("UserRole", respuesta.Datos.Rol);


            return Ok(new
            {
                message = "Login exitoso",
                userData = new
                {
                    nombreUsuario = respuesta.Datos.LoginUsuario.NombreUsuario,
                    id = respuesta.Datos.Id,
                    rol = respuesta.Datos.Rol,
                    token = respuesta.Datos.Token
                }
            });
        }

        //POST: UsuarioController/register
        [HttpPost("register")]
        public async Task<ActionResult<RespuestaPrivada<RegisterUsuarioDTO>>> Register(RegisterUsuarioDTO registerUsuario)
        {
            var respuesta = await _service.RegistrarUsuario(registerUsuario);

            if (respuesta.Datos == null)
                if (respuesta.Mensaje.StartsWith("Error interno"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                else 
                {
                    return Unauthorized(new { message = respuesta.Mensaje });

                }
            return Ok(new { message = respuesta.Mensaje, data = respuesta.Datos });
        }

        //POST: UsuarioController/logout
        [HttpPost("logout")]
        public IActionResult Logout()
        {

            //HttpContext.Session.Remove("UserName");
            //HttpContext.Session.Remove("UserRole");
            //HttpContext.Session.Remove("UserId");

            return Ok(new { message = "Logout exitoso" });
        }

        ////GET: UsuarioController/obtenerRol
        //[HttpGet("obtenerRol")]
        //public IActionResult obtenerRol()
        //{
        //    var rol = HttpContext.Session.GetString("UserRole");
        //    if (rol != null)
        //    {
        //        return Ok(new { rol });
        //    }
        //    return Unauthorized();
        //}
    }

}
