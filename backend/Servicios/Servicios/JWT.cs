using DataBase.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Servicios.Interfaces;
using Microsoft.Extensions.Configuration;
using CORE.DTOs;
using DataBase.Data;
using Microsoft.EntityFrameworkCore;

namespace Servicios.Servicios
{
    public class JWT : IJWT
    {
        private readonly IConfiguration _configuration;
        private readonly EventCraftContext _context;

        public JWT(IConfiguration configuration, EventCraftContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public string GenerarToken(Usuario user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.NombreUsuario),
                new Claim(ClaimTypes.Email, user.Correo),
                new Claim(ClaimTypes.Role, user.Rol)

            };
            var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Get<string>() ?? string.Empty));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public async Task<RespuestaPrivada<dynamic>> validarToken(ClaimsIdentity identity)
        //{
        //    var respuesta = new RespuestaPrivada<dynamic>();
        //    try
        //    {
        //        if (identity.Claims.Count() == 0)
        //        {
        //            respuesta.Mensaje = "Verificar si estas enviando un token valido";
        //            respuesta.Exito = false;
        //            respuesta.Datos = null;
        //            return respuesta;
        //        }

        //        var rol = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
        //        var usuario = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

        //        var usuarioBD = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == usuario && u.Rol == rol);

        //        respuesta.Mensaje = "Token validado correctamente";
        //        respuesta.Exito = true;
        //        respuesta.Datos = usuario;
        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.Mensaje = "Error interno: " + ex.Message;
        //        return respuesta;
        //    }
        //}
    }
}
