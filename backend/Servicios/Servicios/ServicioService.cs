using DataBase.Data;
using CORE.DTOs;
using Servicios.Interfaces;
using DataBase.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Servicios.Servicios
{
    public class ServicioService : IServicioService
    {
        private readonly EventCraftContext _context;

        public ServicioService(EventCraftContext context)
        {
            _context = context;
        }

        public async Task<RespuestaPrivada<ICollection<ServicioDTOConId>>> GetServicios()
        {
            var respuesta = new RespuestaPrivada<ICollection<ServicioDTOConId>>();
            respuesta.Datos = null;

            try
            {
                var servicioBD = await _context.Servicios.ToListAsync();
                if (servicioBD.Count() != 0)
                {
                    var serviciosDTO = new List<ServicioDTOConId>();
                    foreach (var servicio in servicioBD)
                    {
                        var servicioDTO = servicio.Adapt<ServicioDTOConId>();
                        //respuesta.Datos.Add(new ServicioDTOConId()
                        //{
                        //    Id = servicio.Id,
                        //    Nombre = servicio.Nombre,
                        //    Descripcion = servicio.Descripcion,
                        //    Estado = servicio.Estado
                        //});
                        serviciosDTO.Add(servicioDTO);
                    }
                    respuesta.Datos = serviciosDTO;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron correctamente todos los servicios";
                    return respuesta;
                }

                respuesta.Mensaje = "No se hallaron servicios";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<RespuestaPrivada<ServicioDTO>> PostServicio(ServicioDTO servicioDTO)
        {
            var respuesta = new RespuestaPrivada<ServicioDTO>();
            respuesta.Datos = null;
            var servicioNuevo = new Servicio();

            try
            {
                var servicioBD = await _context.Servicios.FirstOrDefaultAsync(x => x.Nombre.ToLower() == servicioDTO.Nombre.ToLower());
                if (servicioBD == null)
                {
                    servicioNuevo = servicioDTO.Adapt<Servicio>();
                    //servicioNuevo.Nombre = servicioDTO.Nombre;
                    //servicioNuevo.Descripcion = servicioDTO.Descripcion;
                    //servicioNuevo.Estado = servicioDTO.Estado;

                    await _context.Servicios.AddAsync(servicioNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El servicio se ha creado correctamente";
                    respuesta.Datos = servicioDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "El servicio que intenta crear ya existe";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }
        public async Task<RespuestaPrivada<Servicio>> DeleteServicio(int id)
        {
            var respuesta = new RespuestaPrivada<Servicio>();
            respuesta.Datos = null;

            try
            {
                var servicioBD = await _context.Servicios.FindAsync(id);
                if (servicioBD != null)
                {
                    _context.Servicios.Remove(servicioBD);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = servicioBD;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El servicio se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "El servicio a eliminar no fue hallado ";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno" + ex.Message;
                return respuesta;
            }
        }
        public async Task<RespuestaPrivada<ServicioDTO>> PutServicio(int id, ServicioDTO servicioDTO)
        {
            var respuesta = new RespuestaPrivada<ServicioDTO>();
            respuesta.Datos = null;

            try
            {
                var servicioBD = await _context.Servicios.FindAsync(id);
                if (servicioBD != null)
                {

                    var servicioConMismoNombre = await _context.Servicios
                        .AnyAsync(x => x.Nombre.ToLower() == servicioDTO.Nombre.ToLower() && x.Id != id);

                    if (servicioConMismoNombre)
                    {
                        respuesta.Mensaje = "Ya existe un servicio con el mismo nombre. Elija otro nombre.";
                        return respuesta;
                    }

                    servicioBD.Nombre = servicioDTO.Nombre;
                    servicioBD.Descripcion = servicioDTO.Descripcion;
                    servicioBD.Estado = servicioDTO.Estado;

                    await _context.SaveChangesAsync();
                    respuesta.Datos = servicioBD.Adapt<ServicioDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El servicio se ha modificado correctamente";
                    return respuesta;
                }
                respuesta.Mensaje = "El servicio a modificar no fue hallado";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return respuesta;
            }
        }
        public async Task<RespuestaPrivada<ServicioDTOConId>> PatchServicioEstado(int id, bool nuevoEstado)
        {
            var respuesta = new RespuestaPrivada<ServicioDTOConId>();
            respuesta.Datos = null;

            try
            {
                var servicioBD = await _context.Servicios.FirstOrDefaultAsync(x => x.Id == id);
                if (servicioBD != null)
                {
                    servicioBD.Estado = nuevoEstado;
                    await _context.SaveChangesAsync();
                    respuesta.Datos = servicioBD.Adapt<ServicioDTOConId>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El estado del servicio fue cambiado con exito";
                    return respuesta;
                }
                respuesta.Mensaje = "No se ha podido hallar el servicio";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }
    }
}
