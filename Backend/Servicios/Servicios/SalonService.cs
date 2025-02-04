using DataBase.Data;
using CORE.DTOs;
using Servicios.Interfaces;
using DataBase.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Servicios.Servicios
{
    public class SalonService : ISalonService 
    {
        //instanciacion de la base de datos
        private readonly EventCraftContext _context;

        public SalonService(EventCraftContext context)
        {
            _context = context;
        }

        //consulta
        public async Task<RespuestaPrivada<ICollection<SalonDTOConId>>> GetSalones()
        {
            var respuesta = new RespuestaPrivada<ICollection<SalonDTOConId>>();
            respuesta.Datos = null;

            try
            {
                var salonesBD = await _context.Salones.ToListAsync();
                if (salonesBD.Count() != 0)
                {
                    var salonesDTO = new List<SalonDTOConId>();
                    foreach (var salon in salonesBD)
                    {
                        var salonDTO = salon.Adapt<SalonDTOConId>();
                        //respuesta.Datos.Add(new SalonDTOConId()
                        //{
                        //    //Id = salon.Id,
                        //    //Nombre = salon.Nombre,
                        //    //Tipo = salon.Tipo,
                        //    //Estado = salon.Estado,
                        //    //Telefono = salon.Telefono,
                        //    //Capacidad = salon.Capacidad,
                        //    //DimensionesMt2 = salon.DimensionesMt2,
                        //    //PrecioBase = salon.PrecioBase,
                        //    //PrecioHora = salon.PrecioHora,
                        //    //Direccion = salon.Direccion,
                        //    //Localidad = salon.Localidad,
                        //    //UrlImagen = salon.UrlImagen
                        //});
                        salonesDTO.Add(salonDTO);
                    }
                    respuesta.Datos = salonesDTO;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron correctamente todas los salones";
                    return respuesta;
                }

                respuesta.Mensaje = "No se hallaron salones";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        //alta
        public async Task<RespuestaPrivada<SalonDTO>> PostSalon(SalonDTO salonDTO)
        {
            var respuesta = new RespuestaPrivada<SalonDTO>();
            respuesta.Datos = null;

            try
            {
                var salonNuevo = salonDTO.Adapt<Salon>();
                /*salonNuevo.Id = salonDTO.Id;
                salonNuevo.Nombre = salonDTO.Nombre;
                salonNuevo.Tipo = salonDTO.Tipo;
                salonNuevo.Estado = salonDTO.Estado;
                salonNuevo.Telefono = salonDTO.Telefono;
                salonNuevo.Capacidad = salonDTO.Capacidad;
                salonNuevo.DimensionesMt2 = salonDTO.DimensionesMt2;
                salonNuevo.PrecioBase = salonDTO.PrecioBase;
                salonNuevo.PrecioHora = salonDTO.PrecioHora;
                salonNuevo.Direccion = salonDTO.Direccion;
                salonNuevo.Localidad = salonDTO.Localidad;
                salonNuevo.UrlImagen = salonDTO.UrlImagen*/

                await _context.Salones.AddAsync(salonNuevo);
                await _context.SaveChangesAsync();
                respuesta.Exito = true;
                respuesta.Mensaje = "El salon se ha creado correctamente";
                respuesta.Datos = salonDTO;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        //baja física
        public async Task<RespuestaPrivada<Salon>> DeleteSalon(int id)
        {
            var respuesta = new RespuestaPrivada<Salon>();
            respuesta.Datos = null;

            try
            {
                var salonBD = await _context.Salones.FindAsync(id);
                if (salonBD != null)
                {
                    _context.Salones.Remove(salonBD);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = salonBD;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El salon se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "El salon a eliminar no fue hallado ";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno" + ex.Message;
                return respuesta;
            }
        }

        //modificacion

        public async Task<RespuestaPrivada<SalonDTO>> PutSalon(int id, SalonDTO salonDTO)
        {
            var respuesta = new RespuestaPrivada<SalonDTO>();
            respuesta.Datos = null;

            try
            {
                var salonBD = await _context.Salones.FindAsync(id);
                if (salonBD != null)
                {
                    salonBD.Nombre = salonDTO.Nombre;
                    salonBD.Tipo = salonDTO.Tipo;
                    salonBD.Telefono = salonDTO.Telefono;
                    salonBD.Estado = salonDTO.Estado;
                    salonBD.Capacidad = salonDTO.Capacidad;
                    salonBD.DimensionesMt2 = salonDTO.DimensionesMt2;
                    salonBD.PrecioBase = salonDTO.PrecioBase;
                    salonBD.PrecioHora = salonDTO.PrecioHora;
                    salonBD.Direccion = salonDTO.Direccion;
                    salonBD.Localidad = salonDTO.Localidad;
                    salonBD.UrlImagen = salonDTO.UrlImagen;

                    await _context.SaveChangesAsync();
                    respuesta.Datos = salonBD.Adapt<SalonDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El salon se ha modificado correctamente";
                    return respuesta;
                }
                respuesta.Mensaje = "El salon a modificar no fue hallado";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return respuesta;
            }
        }

        //borrado logico

        public async Task<RespuestaPrivada<SalonDTOConId>> PatchSalonEstado(int id, bool nuevoEstado)
        {
            var respuesta = new RespuestaPrivada<SalonDTOConId>();
            respuesta.Datos = null;

            try
            {
                var salonBD = await _context.Salones.FirstOrDefaultAsync(x => x.Id == id);
                if (salonBD != null)
                {
                    salonBD.Estado = nuevoEstado;
                    await _context.SaveChangesAsync();
                    respuesta.Datos = salonBD.Adapt<SalonDTOConId>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El estado del salon fue cambiado con exito";
                    return respuesta;
                }
                respuesta.Mensaje = "No se ha podido hallar al salon";
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
