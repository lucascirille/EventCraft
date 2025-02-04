
using CORE.DTOs;
using DataBase.Models;
using System.Security.Claims;

namespace Servicios.Interfaces
{
    public interface IJWT
    {
        public string GenerarToken(Usuario user);
        //Task<RespuestaPrivada<dynamic>> validarToken(ClaimsIdentity identity);
    }
}