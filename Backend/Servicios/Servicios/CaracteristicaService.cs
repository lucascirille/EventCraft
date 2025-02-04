using DataBase.Data;
using CORE.DTOs;
using Servicios.Interfaces;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace Servicios.Servicios
{
    public class CaracteristicaService : ICaracteristicaService
    {
        private readonly EventCraftContext _context;

        public CaracteristicaService(EventCraftContext context)
        {
            _context = context;
        }

        public async Task<RespuestaPrivada<ICollection<CaracteristicaDTOConId>>> GetCaracteristicas()
        {
            var respuesta = new RespuestaPrivada<ICollection<CaracteristicaDTOConId>>();
            respuesta.Datos = null;

            try
            {
                var caracteristicasBD = await _context.Caracteristicas.ToListAsync();
                if (caracteristicasBD.Count() != 0)
                {
                    var caracteristicasDTO = new List<CaracteristicaDTOConId>(); 
                    foreach (var caracteristica in caracteristicasBD)
                    {
                        var caracteristicaDTO = caracteristica.Adapt<CaracteristicaDTOConId>();
                        //respuesta.Datos.Add(new CaracteristicaDTOConId()
                        //{
                        //    Id = caracteristica.Id,
                        //    Nombre = caracteristica.Nombre,
                        //    Descripcion = caracteristica.Descripcion,
                        //    Estado = caracteristica.Estado
                        //});
                        caracteristicasDTO.Add(caracteristicaDTO);
                    }
                    respuesta.Datos = caracteristicasDTO;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron correctamente todas las caracteristicas";
                    return respuesta;
                }

                respuesta.Mensaje = "No se hallaron caracteristicas";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<RespuestaPrivada<CaracteristicaDTO>> PostCaracteristica(CaracteristicaDTO caracteristicaDTO)
        {
            var respuesta = new RespuestaPrivada<CaracteristicaDTO>();
            respuesta.Datos = null;
            var caracteristicaNueva = new Caracteristica();

            try
            {
                var caracteristicaBD = await _context.Caracteristicas.FirstOrDefaultAsync(x => x.Nombre.ToLower() == caracteristicaDTO.Nombre.ToLower());
                if (caracteristicaBD == null) 
                {
                     caracteristicaNueva = caracteristicaDTO.Adapt<Caracteristica>();

                    //caracteristicaNueva.Nombre = caracteristicaDTO.Nombre;
                    //caracteristicaNueva.Descripcion = caracteristicaDTO.Descripcion;
                    //caracteristicaNueva.Estado = caracteristicaDTO.Estado;
                     

                    await _context.Caracteristicas.AddAsync(caracteristicaNueva);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La caracteristica se ha creado correctamente";
                    respuesta.Datos = caracteristicaDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "La caracteristica que intenta crear ya existe";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<RespuestaPrivada<Caracteristica>> DeleteCaracteristica(int id)
        {
            var respuesta = new RespuestaPrivada<Caracteristica>();
            respuesta.Datos = null;

            try
            {
                var caracteristicaBD = await _context.Caracteristicas.FindAsync(id);
                if (caracteristicaBD != null)
                {
                    _context.Caracteristicas.Remove(caracteristicaBD);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = caracteristicaBD;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La caracteristica se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "La caracteristica a eliminar no fue hallada ";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno" + ex.Message;
                return respuesta;
            }
        }

        public async Task<RespuestaPrivada<CaracteristicaDTO>> PutCaracteristica(int id, CaracteristicaDTO caracteristicaDTO)
        {
            var respuesta = new RespuestaPrivada<CaracteristicaDTO>();
            respuesta.Datos = null;

            try
            {
                var caracteristicaBD = await _context.Caracteristicas.FindAsync(id);
                if (caracteristicaBD != null)
                {

                    var caracteristicaConMismoNombre = await _context.Caracteristicas
                        .AnyAsync(x => x.Nombre.ToLower() == caracteristicaDTO.Nombre.ToLower() && x.Id != id);

                    if (caracteristicaConMismoNombre)
                    {
                        respuesta.Mensaje = "Ya existe una caracteristica con el mismo nombre. Elija otro nombre.";
                        return respuesta;
                    }

                    caracteristicaBD.Nombre = caracteristicaDTO.Nombre;
                    caracteristicaBD.Descripcion = caracteristicaDTO.Descripcion;
                    caracteristicaBD.Estado = caracteristicaDTO.Estado;
                    
                    await _context.SaveChangesAsync();
                    respuesta.Datos = caracteristicaBD.Adapt<CaracteristicaDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La caracteristica se ha modificado correctamente";
                    return respuesta;
                }
                respuesta.Mensaje = "La caracteristica a modificar no fue hallada";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return respuesta;
            }
        }

        public async Task<RespuestaPrivada<CaracteristicaDTOConId>> PatchCaracteristicaEstado(int id, bool nuevoEstado)
        {
            var respuesta = new RespuestaPrivada<CaracteristicaDTOConId>();
            respuesta.Datos = null;

            try
            {
                var caracteristicaBD = await _context.Caracteristicas.FirstOrDefaultAsync(x => x.Id == id);
                if (caracteristicaBD != null)
                {
                    caracteristicaBD.Estado = nuevoEstado;
                    await _context.SaveChangesAsync();
                    respuesta.Datos = caracteristicaBD.Adapt<CaracteristicaDTOConId>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El estado de la caracteristica fue cambiado con exito";
                    return respuesta;
                }
                respuesta.Mensaje = "No se ha podido hallar la caracteristica";
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
