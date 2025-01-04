using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.DTOs;
using DataBase.Data;
using DataBase.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Servicios.Interfaces;

namespace Servicios.Servicios
{
    public class ReservaServicioService : IReservaServicioService
    {
        private readonly EventCraftContext _context;

        public ReservaServicioService(EventCraftContext context)
        {
            _context = context;
        }

        public async Task<RespuestaPrivada<ICollection<ReservaServicioDTOConId>>> GetReservasServicios()
        {
            var respuesta = new RespuestaPrivada<ICollection<ReservaServicioDTOConId>>();
            respuesta.Datos = null;

            try
            {
                var reservaServicioBD = await _context.ReservaServicios.ToListAsync();
                if (reservaServicioBD.Count() != 0)
                {
                    var reservaServiciosDTO = new List<ReservaServicioDTOConId>();
                    foreach (var reservaServicio in reservaServicioBD)
                    {
                        var reservaServicioDTO = reservaServicio.Adapt<ReservaServicioDTOConId>();
                        //respuesta.Datos.Add(new ReservaServicioDTOConId()
                        //{
                        //    Id = reservaServicio.Id,
                        //    Precio = reservaServicio.Precio,
                        //    ReservaId = reservaServicio.ReservaId,
                        //    ServicioId = reservaServicio.ServicioId
                        //});
                        reservaServiciosDTO.Add(reservaServicioDTO);
                    }
                    respuesta.Datos = reservaServiciosDTO;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron correctamente todas las reservasServicios";
                    return respuesta;
                }

                respuesta.Mensaje = "No se hallaron las reservasServicios";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<RespuestaPrivada<ReservaServicioDTO>> PostReservaServicio(ReservaServicioDTO reservaServicioDTO)
        {
            var respuesta = new RespuestaPrivada<ReservaServicioDTO>();
            respuesta.Datos = null;
            var vinculoNuevo = new ReservaServicio();

            try
            {
                var servicioBD = await _context.Servicios.FirstOrDefaultAsync(x => x.Id == reservaServicioDTO.ServicioId);
                var reservaBD = await _context.Reservas.FirstOrDefaultAsync(x => x.Id == reservaServicioDTO.ReservaId);
                if (servicioBD == null)
                {
                    respuesta.Mensaje = "El servicio que intenta agregar al salon no existe";
                    return (respuesta);
                }
                else if (reservaBD == null)
                {
                    respuesta.Mensaje = "El salon al que quiere agregarle el servicio no existe";
                    return (respuesta);
                }
                else
                {
                    //vinculoNuevo = salonServicioDTO.Adapt<SalonServicio>();
                    vinculoNuevo.Precio = reservaServicioDTO.Precio;
                    vinculoNuevo.ReservaId = reservaServicioDTO.ReservaId;
                    vinculoNuevo.ServicioId = reservaServicioDTO.ServicioId;
                    vinculoNuevo.Servicio = servicioBD;
                    vinculoNuevo.Reserva = reservaBD;


                    await _context.ReservaServicios.AddAsync(vinculoNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La reservaServicio se ha creado correctamente";
                    respuesta.Datos = reservaServicioDTO;
                    return (respuesta);
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<RespuestaPrivada<ReservaServicio>> DeleteReservaServicio(int id)
        {
            var respuesta = new RespuestaPrivada<ReservaServicio>();
            respuesta.Datos = null;

            try
            {
                var reservaServicioBD = await _context.ReservaServicios.FindAsync(id);
                if (reservaServicioBD != null)
                {
                    _context.ReservaServicios.Remove(reservaServicioBD);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = reservaServicioBD;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La reservaServicio se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "El reservaServicio a eliminar no fue hallado ";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno" + ex.Message;
                return respuesta;
            }
        }

        public async Task<RespuestaPrivada<ReservaServicioDTO>> PutReservaServicio(int id, ReservaServicioDTO reservaServicioDTO)
        {
            var respuesta = new RespuestaPrivada<ReservaServicioDTO>();
            respuesta.Datos = null;

            try
            {
                var reservaServicioBD = await _context.ReservaServicios.FindAsync(id);
                if (reservaServicioBD != null)
                {
                    var servicioBD = await _context.Servicios.FirstOrDefaultAsync(x => x.Id == reservaServicioDTO.ServicioId);
                    var reservaBD = await _context.Reservas.FirstOrDefaultAsync(x => x.Id == reservaServicioDTO.ReservaId);
                    if (servicioBD == null)
                    {
                        respuesta.Mensaje = "El servicio que intenta agregar al salon no existe";
                        return (respuesta);
                    }
                    else if (reservaBD == null)
                    {
                        respuesta.Mensaje = "La reserva a la que quiere agregarle el servicio no existe";
                        return (respuesta);
                    }
                    else
                    {
                        reservaServicioBD.Precio = reservaServicioDTO.Precio;
                        reservaServicioBD.ReservaId = reservaServicioDTO.ReservaId;
                        reservaServicioBD.ServicioId = reservaServicioDTO.ServicioId;
                        reservaServicioBD.Servicio = servicioBD;
                        reservaServicioBD.Reserva = reservaBD;

                        await _context.SaveChangesAsync();
                        respuesta.Datos = reservaServicioBD.Adapt<ReservaServicioDTO>();
                        respuesta.Exito = true;
                        respuesta.Mensaje = "La reservaServicio se ha modificado correctamente";
                        return respuesta;
                    }
                }
                respuesta.Mensaje = "La reservaServicio a modificar no fue hallada";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return respuesta;
            }
        }
    }
}
