using DataBase.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using CORE.DTOs;
using Servicios.Services;
using DataBase.Model;
using Servicios.Interfaces;


namespace CuponMania.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuponController : ControllerBase
    {
        private readonly ICuponService _cuponService;

        public CuponController(ICuponService cuponService)
        {
            _cuponService = cuponService;
        }

        [HttpGet("obtenerCupon")]
        public ActionResult<RespuestaPrivada<Cupon>> obtenerCupon(string codigo = "")
        {
           var respuesta = _cuponService.obtenerCupon(codigo);
            if (respuesta.Datos == null)
            {
                return NotFound(respuesta);
            }
            else
            {
                return Ok(respuesta);
            }
        }

/*
        [HttpPatch("utilizarCupon")]
        public ActionResult<RespuestaPrivada<Cupon>> PatchDesabilitar(string codigo) {
            var respuesta = new RespuestaPrivada<Cupon>();

            var cupon = CuponesBaseDeDatos.FirstOrDefault(c => c.Codigo == codigo);
            if (cupon == null)
            {
                respuesta.Datos = null;
                respuesta.Mensaje = "Cupon No encontrado";
                respuesta.Exito = false;
                return NotFound(respuesta);
            }
            else
            {
                if (cupon.Utilizado == true)
                {
                   
                    respuesta.Mensaje = "cupon no disponible";
                    respuesta.Datos = cupon;
                    respuesta.Exito = false;
                    return Ok(respuesta);
                }
                else {
                    cupon.Utilizado = true;
                    respuesta.Mensaje = "Cupon desabilitado";
                    respuesta.Datos = cupon;
                    respuesta.Exito = true;
                    return Ok(respuesta);
                }
            } 
        }
*/
    }
}
