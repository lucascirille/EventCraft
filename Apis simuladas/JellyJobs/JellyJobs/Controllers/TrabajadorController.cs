using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DataBase.Model;
using CORE.DTOs;
using Servicios.Interfaces;

[ApiController]
[Route("api/[controller]")]

public class TrabajadorController : ControllerBase
{
    private readonly ITrabajadorService _trabajadorService;
    public TrabajadorController(ITrabajadorService trabajadorService)
    {
        _trabajadorService = trabajadorService;
    }


    [HttpGet("obtenerTrabajadores")]
    public ActionResult<RespuestaPrivada<ICollection<Trabajador>>> GetTrabajadores()
    {
       var respuesta = _trabajadorService.GetTrabajadores();
        if (respuesta.Datos == null)
        {
            return BadRequest(respuesta);
        }
        else
        {
            return Ok(respuesta);
        }
    }

    [HttpGet("obtenerTrabajadoresPorProfesion")]

    public ActionResult<RespuestaPrivada<ICollection<Trabajador>>> GetTrabajadoresPorProfesion(string profesion = "")
    {
        var respuesta = _trabajadorService.GetTrabajadoresPorProfesion(profesion);
        if (respuesta.Datos == null)
        {
            return BadRequest(respuesta);
        }
        else
        {
            return Ok(respuesta);
        }
    }
}


