using DataBase.Data;
using CORE.DTOs;
using Servicios.Interfaces;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace Servicios.Servicios
{
    public class SalonServicioService : ISalonServicioService 
    {
        private readonly EventCraftContext _context;

        public SalonServicioService(EventCraftContext context)
        {
            _context = context;
        }
        public async Task<RespuestaPrivada<ICollection<SalonServicioDTOConId>>> GetSalonesServicios()
        {
            var respuesta = new RespuestaPrivada<ICollection<SalonServicioDTOConId>>();
            respuesta.Datos = null;

            try
            {
                var salonServicioBD = await _context.SalonServicios.ToListAsync();
                if (salonServicioBD.Count() != 0)
                {
                    var salonServiciosDTO = new List<SalonServicioDTOConId>();
                    foreach (var salonServicio in salonServicioBD)
                    {
                        var salonServicioDTO = salonServicio.Adapt<SalonServicioDTOConId>();
                        //respuesta.Datos.Add(new SalonServicioDTOConId()
                        //{
                        //    Id = salonServicio.Id,
                        //    Precio = salonServicio.Precio,
                        //    SalonId = salonServicio.SalonId,
                        //    ServicioId = salonServicio.ServicioId
                        //});
                        salonServiciosDTO.Add(salonServicioDTO);
                    }
                    respuesta.Datos = salonServiciosDTO;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron correctamente todos los salonesServicios";
                    return respuesta;
                }

                respuesta.Mensaje = "No se hallaron los salonesServicios";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<RespuestaPrivada<SalonServicioDTO>> PostSalonServicio(SalonServicioDTO salonServicioDTO)
        {
            var respuesta = new RespuestaPrivada<SalonServicioDTO>();
            respuesta.Datos = null;
            var vinculoNuevo = new SalonServicio();

            try
            {
                //var salonServicioBD = await _context.SalonServicios.FirstOrDefaultAsync(x => x.Id == salonServicioDTO.Id);
                //if (salonServicioBD == null)
                //{
                    var servicioBD = await _context.Servicios.FirstOrDefaultAsync(x => x.Id == salonServicioDTO.ServicioId);
                    var salonBD = await _context.Salones.FirstOrDefaultAsync(x => x.Id == salonServicioDTO.SalonId);
                    if (servicioBD == null)
                    {
                        respuesta.Mensaje = "El servicio que intenta agregar al salon no existe";
                        return (respuesta);
                    }
                    else if (salonBD == null)
                    {
                        respuesta.Mensaje = "El salon al que quiere agregarle el servicio no existe";
                        return (respuesta);
                    }
                    else
                    {
                        //var vinculoNuevo = salonServicioDTO.Adapt<SalonServicio>();
                        vinculoNuevo.Precio = salonServicioDTO.Precio;
                        vinculoNuevo.SalonId = salonServicioDTO.SalonId;
                        vinculoNuevo.ServicioId = salonServicioDTO.ServicioId;
                        vinculoNuevo.Servicio = servicioBD;
                        vinculoNuevo.Salon = salonBD;


                        await _context.SalonServicios.AddAsync(vinculoNuevo);
                        await _context.SaveChangesAsync();
                        respuesta.Exito = true;
                        respuesta.Mensaje = "El salonServicio se ha creado correctamente";
                        respuesta.Datos = salonServicioDTO;
                        return (respuesta);
                    }
                //}
                //respuesta.Mensaje = "El salonServicio que intenta crear ya existe";
                //return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<RespuestaPrivada<SalonServicio>> DeleteSalonServicio(int id)
        {
            var respuesta = new RespuestaPrivada<SalonServicio>();
            respuesta.Datos = null;

            try
            {
                var salonServicioBD = await _context.SalonServicios.FindAsync(id);
                if (salonServicioBD != null)
                {
                    _context.SalonServicios.Remove(salonServicioBD);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = salonServicioBD;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El salonServicio se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "El salonServicio a eliminar no fue hallado ";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno" + ex.Message;
                return respuesta;
            }
        }

        public async Task<RespuestaPrivada<SalonServicioDTO>> PutSalonServicio(int id, SalonServicioDTO salonServicioDTO)
        {
            var respuesta = new RespuestaPrivada<SalonServicioDTO>();
            respuesta.Datos = null;

            try
            {
                var salonServicioBD = await _context.SalonServicios.FindAsync(id);
                if (salonServicioBD != null)
                {
                    var servicioBD = await _context.Servicios.FirstOrDefaultAsync(x => x.Id == salonServicioDTO.ServicioId);
                    var salonBD = await _context.Salones.FirstOrDefaultAsync(x => x.Id == salonServicioDTO.SalonId);
                    if (servicioBD == null)
                    {
                        respuesta.Mensaje = "El servicio que intenta agregar al salon no existe";
                        return (respuesta);
                    }
                    else if (salonBD == null)
                    {
                        respuesta.Mensaje = "El salon al que quiere agregarle el servicio no existe";
                        return (respuesta);
                    }
                    else
                    {
                        salonServicioBD.Precio = salonServicioDTO.Precio;
                        salonServicioBD.SalonId = salonServicioDTO.SalonId;
                        salonServicioBD.ServicioId = salonServicioDTO.ServicioId;
                        salonServicioBD.Servicio = servicioBD;
                        salonServicioBD.Salon = salonBD;

                        await _context.SaveChangesAsync();
                        respuesta.Datos = salonServicioBD.Adapt<SalonServicioDTO>();
                        respuesta.Exito = true;
                        respuesta.Mensaje = "El salonServicio se ha modificado correctamente";
                        return respuesta;
                    }
                }
                respuesta.Mensaje = "El salonServicio a modificar no fue hallado";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return respuesta;
            }
        }

        public async Task<RespuestaPrivada<ICollection<ServicioPrecioDTO>>> GetServiciosBySalonId(int id)
        {
            var respuesta = new RespuestaPrivada<ICollection<ServicioPrecioDTO>>();
            respuesta.Datos = null;

            try
            {
                var salonServiciosBD = await _context.SalonServicios.Include(x => x.Servicio).Where(x => x.SalonId == id).ToListAsync();
                if (salonServiciosBD.Count() != 0)
                {
                    respuesta.Datos = new List<ServicioPrecioDTO>();
                    foreach (var salonServicio in salonServiciosBD)
                    {
                        respuesta.Datos.Add(new ServicioPrecioDTO()
                        {
                            servicio = salonServicio.Servicio.Adapt<ServicioDTOConId>(),
                            precio = salonServicio.Precio
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron correctamente todos los servicios";
                    return respuesta;
                }

                respuesta.Mensaje = "No se hallaron los servicios";
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
