using DataBase.Data;
using CORE.DTOs;
using Servicios.Interfaces;
using DataBase.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Servicios.Servicios
{
    public class SalonCaracteristicaService : ISalonCaracteristicaService 
    {

        private readonly EventCraftContext _context;

        public SalonCaracteristicaService(EventCraftContext context)
        {
            _context = context;
        }
        public async Task<RespuestaPrivada<ICollection<SalonCaracteristicaDTOConId>>> GetSalonesCaracteristicas()
        {
            var respuesta = new RespuestaPrivada<ICollection<SalonCaracteristicaDTOConId>>();
            respuesta.Datos = null;

            try
            {
                var salonCaracteristicasBD = await _context.SalonCaracteristicas.ToListAsync();
                if (salonCaracteristicasBD.Count() != 0)
                {
                    var salonCaracteristicasDTO = new List<SalonCaracteristicaDTOConId>();
                    foreach (var salonCaracteristica in salonCaracteristicasBD)
                    {
                        var salonCaracteristicaDTO = salonCaracteristica.Adapt<SalonCaracteristicaDTOConId>();
                        //respuesta.Datos.Add(new SalonCaracteristicaDTOConId()
                        //{
                        //    Id = salonCaracteristica.Id,
                        //    Valor = salonCaracteristica.Valor,
                        //    SalonId = salonCaracteristica.SalonId,
                        //    CaracteristicaId = salonCaracteristica.CaracteristicaId
                        //});
                        salonCaracteristicasDTO.Add(salonCaracteristicaDTO);
                    }
                    respuesta.Datos = salonCaracteristicasDTO;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron correctamente todos los salonesCaracteristicas";
                    return respuesta;
                }

                respuesta.Mensaje = "No se hallaron los salonesCaracteristicas";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<RespuestaPrivada<SalonCaracteristicaDTO>> PostSalonCaracteristica(SalonCaracteristicaDTO salonCaracteristicaDTO)
        {
            var respuesta = new RespuestaPrivada<SalonCaracteristicaDTO>();
            respuesta.Datos = null;
            var vinculoNuevo = new SalonCaracteristica();

            try
            {
                //var salonCaracteristicaBD = await _context.SalonCaracteristicas.FirstOrDefaultAsync(x => x.Id == salonCaracteristicaDTO.Valor);
                //if (salonCaracteristicaBD == null)
                //{
                    var caracteristicaBD = await _context.Caracteristicas.FirstOrDefaultAsync(x => x.Id == salonCaracteristicaDTO.CaracteristicaId);
                    var salonBD = await _context.Salones.FirstOrDefaultAsync(x => x.Id == salonCaracteristicaDTO.SalonId);
                    if (caracteristicaBD == null)
                    {
                        respuesta.Mensaje = "La caracteristica que intenta agregar al salon no existe";
                        return (respuesta);
                    } else if (salonBD == null)
                    {
                        respuesta.Mensaje = "El salon al que quiere agregarle la caracteristica no existe";
                        return (respuesta);
                    }
                    else
                    {
                        //vinculoNuevo = salonCaracteristicaDTO.Adapt<SalonCaracteristica>();
                        vinculoNuevo.Valor = salonCaracteristicaDTO.Valor;
                        vinculoNuevo.SalonId = salonCaracteristicaDTO.SalonId;
                        vinculoNuevo.CaracteristicaId = salonCaracteristicaDTO.CaracteristicaId;
                        vinculoNuevo.Caracteristica = caracteristicaBD;
                        vinculoNuevo.Salon = salonBD;


                    await _context.SalonCaracteristicas.AddAsync(vinculoNuevo);
                        await _context.SaveChangesAsync();
                        respuesta.Exito = true;
                        respuesta.Mensaje = "El salonCaracteristica se ha creado correctamente";
                        respuesta.Datos = salonCaracteristicaDTO;
                        return (respuesta);
                    }
                //}
                //respuesta.Mensaje = "El salonCaracteristica que intenta crear ya existe";
                //return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<RespuestaPrivada<SalonCaracteristica>> DeleteSalonCaracteristica(int id)
        {
            var respuesta = new RespuestaPrivada<SalonCaracteristica>();
            respuesta.Datos = null;

            try
            {
                var salonCaracteristicaBD = await _context.SalonCaracteristicas.FindAsync(id);
                if (salonCaracteristicaBD != null)
                {
                    _context.SalonCaracteristicas.Remove(salonCaracteristicaBD);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = salonCaracteristicaBD;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El salonCaracteristica se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "El salonCaracteristica a eliminar no fue hallado ";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno" + ex.Message;
                return respuesta;
            }
        }

        public async Task<RespuestaPrivada<SalonCaracteristicaDTO>> PutSalonCaracteristica(int id, SalonCaracteristicaDTO salonCaracteristicaDTO)
        {
            var respuesta = new RespuestaPrivada<SalonCaracteristicaDTO>();
            respuesta.Datos = null;

            try
            {
                var salonCaracteristicaBD = await _context.SalonCaracteristicas.FindAsync(id);
                if (salonCaracteristicaBD != null)
                {
                    var caracteristicaBD = await _context.Caracteristicas.FirstOrDefaultAsync(x => x.Id == salonCaracteristicaDTO.CaracteristicaId);
                    var salonBD = await _context.Salones.FirstOrDefaultAsync(x => x.Id == salonCaracteristicaDTO.SalonId);
                    if (caracteristicaBD == null)
                    {
                        respuesta.Mensaje = "La caracteristica que intenta agregar al salon no existe";
                        return (respuesta);
                    }
                    else if (salonBD == null)
                    {
                        respuesta.Mensaje = "El salon al que quiere agregarle la caracteristica no existe";
                        return (respuesta);
                    }
                    else
                    {
                        salonCaracteristicaBD.Valor = salonCaracteristicaDTO.Valor;
                        salonCaracteristicaBD.SalonId = salonCaracteristicaDTO.SalonId;
                        salonCaracteristicaBD.CaracteristicaId = salonCaracteristicaDTO.CaracteristicaId;
                        salonCaracteristicaBD.Caracteristica = caracteristicaBD;
                        salonCaracteristicaBD.Salon = salonBD;

                        await _context.SaveChangesAsync();
                        respuesta.Datos = salonCaracteristicaBD.Adapt<SalonCaracteristicaDTO>();
                        respuesta.Exito = true;
                        respuesta.Mensaje = "El salonCaracteristica se ha modificado correctamente";
                        return respuesta;
                    }
                }
                respuesta.Mensaje = "El salonCaracteristica a modificar no fue hallado";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return respuesta;
            }
        }

        public async Task<RespuestaPrivada<ICollection<CaracteristicaDTO>>> GetCaracteristicasBySalonId(int id)
        {
            var respuesta = new RespuestaPrivada<ICollection<CaracteristicaDTO>>();
            respuesta.Datos = null;

            try
            {
                var salonCaracteristicasBD = await _context.SalonCaracteristicas.Include(sc => sc.Caracteristica).Where(x => x.SalonId == id).ToListAsync();
                if (salonCaracteristicasBD.Count() != 0)
                {
                    respuesta.Datos = new List<CaracteristicaDTO>();
                    foreach (var salonCaracteristica in salonCaracteristicasBD)
                    {
                        respuesta.Datos.Add(new CaracteristicaDTO()
                        {
                            Nombre = salonCaracteristica.Caracteristica.Nombre,
                            Descripcion = salonCaracteristica.Caracteristica.Descripcion,
                            Estado = salonCaracteristica.Caracteristica.Estado
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron correctamente todos los Caracteristicas";
                    return respuesta;
                }

                respuesta.Mensaje = "No se hallaron las Caracteristicas";
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
