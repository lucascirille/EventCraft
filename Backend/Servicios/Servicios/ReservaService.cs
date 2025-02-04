using DataBase.Data;
using CORE.DTOs;
using Servicios.Interfaces;
using DataBase.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Servicios.Servicios
{
    public class ReservaService : IReservaService
    {
        private readonly EventCraftContext _context;
        public ReservaService(EventCraftContext context)
        {
            _context = context;
        }

        public async Task<RespuestaPrivada<ICollection<ReservaDTOConId>>> GetReservas()
        {
            var respuesta = new RespuestaPrivada<ICollection<ReservaDTOConId>>();
            respuesta.Datos = null;

            try
            {
                var reservasBD = await _context.Reservas.ToListAsync();
                if (reservasBD.Count() != 0)
                {
                    var reservasDTO = new List<ReservaDTOConId>();
                    foreach (var reserva in reservasBD)
                    {
                        var reservaDTO = reserva.Adapt<ReservaDTOConId>();
                        //respuesta.Datos.Add(new ReservaDTOConId()
                        //{
                        //    Id = reserva.Id,
                        //    Titulo = reserva.Titulo,
                        //    Fecha = reserva.Fecha.ToString(),
                        //    FranjaHoraria = reserva.FranjaHoraria,
                        //    HoraExtra = reserva.HoraExtra,
                        //    CantidadPersonas = reserva.CantidadPersonas,
                        //    SalonId = reserva.SalonId,
                        //    UsuarioId = reserva.UsuarioId 
                        //});
                        reservasDTO.Add(reservaDTO);
                    }
                    respuesta.Datos = reservasDTO;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron correctamente todas las reservas";
                    return respuesta;
                }

                respuesta.Mensaje = "No se hallaron reservas";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<RespuestaPrivada<ReservaDTO>> PostReserva(ReservaDTO reservaDTO)
        {
            var respuesta = new RespuestaPrivada<ReservaDTO>();
            respuesta.Datos = null;

            try
            {
                var reservaBD = await _context.Reservas.FirstOrDefaultAsync(x => x.Titulo.ToLower() == reservaDTO.Titulo.ToLower());
                if (reservaBD == null)
                {
                    var salonBD = await _context.Salones.FirstOrDefaultAsync(x => x.Id == reservaDTO.SalonId);
                    var usuarioBD = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == reservaDTO.UsuarioId);
                    if (salonBD == null)
                    {
                        respuesta.Mensaje = "El salon que quiere reservar no existe";
                        return (respuesta);

                    } else if (usuarioBD == null)
                    {
                        respuesta.Mensaje = "El usuario que quiere agregar a la reserva no existe";
                        return (respuesta);
                    }
                    else
                    {

                        var reservaNueva = new Reserva();
                        reservaNueva.Titulo = reservaDTO.Titulo;
                        reservaNueva.Fecha = DateTime.Parse(reservaDTO.Fecha).ToUniversalTime();
                        reservaNueva.FranjaHoraria = reservaDTO.FranjaHoraria;
                        reservaNueva.CantidadPersonas = reservaDTO.CantidadPersonas;
                        reservaNueva.HoraExtra = reservaDTO.HoraExtra;
                        reservaNueva.SalonId = reservaDTO.SalonId;
                        reservaNueva.UsuarioId = reservaDTO.UsuarioId;
                        reservaNueva.Usuario = usuarioBD;
                        reservaNueva.Salon = salonBD;

                        await _context.Reservas.AddAsync(reservaNueva);
                        await _context.SaveChangesAsync();
                        respuesta.Exito = true;
                        respuesta.Mensaje = "La reserva se ha creado correctamente";
                        respuesta.Datos = reservaNueva.Adapt<ReservaDTO>();
                        return (respuesta);
                    }
                }

                respuesta.Mensaje = "La reserva que intenta crear ya existe";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<RespuestaPrivada<Reserva>> DeleteReserva(int id)
        {
            var respuesta = new RespuestaPrivada<Reserva>();
            respuesta.Datos = null;

            try
            {
                var reservaBD = await _context.Reservas.FindAsync(id);
                if (reservaBD != null)
                {
                    _context.Reservas.Remove(reservaBD);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = reservaBD;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La reserva se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "La reserva a eliminar no fue hallada ";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno" + ex.Message;
                return respuesta;
            }
        }


        public async Task<RespuestaPrivada<ReservaDTO>> PutReserva(int id, ReservaDTO reservaDTO)
        {
            var respuesta = new RespuestaPrivada<ReservaDTO>();
            respuesta.Datos = null;

            try
            {
                var reservaBD = await _context.Reservas.FindAsync(id);
                if (reservaBD != null)
                {

                    var reservaConMismoNombre = await _context.Reservas
                        .AnyAsync(x => x.Titulo.ToLower() == reservaDTO.Titulo.ToLower() && x.Id != id);

                    if (reservaConMismoNombre)
                    {
                        respuesta.Mensaje = "Ya existe una reserva con el mismo nombre. Elija otro nombre.";
                        return respuesta;
                    }

                    var salonBD = await _context.Salones.FirstOrDefaultAsync(x => x.Id == reservaDTO.SalonId);
                    var usuarioBD = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == reservaDTO.UsuarioId);
                    if (salonBD == null)
                    {
                        respuesta.Mensaje = "El salon que quiere reservar no existe";
                        return (respuesta);

                    }
                    else if (usuarioBD == null)
                    {
                        respuesta.Mensaje = "El usuario que quiere agregar a la reserva no existe";
                        return (respuesta);
                    }
                    else
                    {
                        reservaBD.Titulo = reservaDTO.Titulo;
                        reservaBD.Fecha = DateTime.Parse(reservaDTO.Fecha).ToUniversalTime();
                        reservaBD.FranjaHoraria = reservaDTO.FranjaHoraria;
                        reservaBD.HoraExtra = reservaDTO.HoraExtra;
                        reservaBD.CantidadPersonas = reservaDTO.CantidadPersonas;
                        reservaBD.SalonId = reservaDTO.SalonId;
                        reservaBD.UsuarioId = reservaDTO.UsuarioId;
                        reservaBD.Salon = salonBD;
                        reservaBD.Usuario = usuarioBD;

                        await _context.SaveChangesAsync();
                        respuesta.Datos = reservaBD.Adapt<ReservaDTO>();
                        respuesta.Exito = true;
                        respuesta.Mensaje = "La reserva se ha modificado correctamente";
                        return respuesta;
                    }

                }
                respuesta.Mensaje = "La reserva a modificar no fue hallada";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return respuesta;
            }
        }

        public async Task<RespuestaPrivada<ICollection<string>>> GetFechasOcupadasBySalonIdYFH(int salonId, string franjaHoraria)
        {
            var respuesta = new RespuestaPrivada<ICollection<string>>();
            respuesta.Datos = null;

            try
            {
                var reservasBD = await _context.Reservas
                    .Where(x => x.SalonId == salonId && x.FranjaHoraria == franjaHoraria)
                    .ToListAsync();

                if (reservasBD.Count > 0)
                {
                    respuesta.Datos = new List<string>();
                    respuesta.Datos = reservasBD.Select(x => x.Fecha.ToString("yyyy-MM-dd")).ToList();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Fechas ocupadas recuperadas correctamente";
                }
                else
                {
                    respuesta.Mensaje = "No se hallaron reservas para el salón y franja horaria especificados.";
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno: " + ex.Message; 
            }

            return respuesta;
        }

        public async Task<RespuestaPrivada<ICollection<ReservaDTOConNombreSalon>>> GetReservasDeSalonesSociales()
        {
            var respuesta = new RespuestaPrivada<ICollection<ReservaDTOConNombreSalon>>();
            respuesta.Datos = null;

            try
            {
                var reservasBD = await _context.Reservas.Include(x => x.Salon).Where(x => x.Salon.Tipo == "Social").ToListAsync();
                if (reservasBD.Count() != 0)
                {
                    respuesta.Datos = new List<ReservaDTOConNombreSalon>();
                    foreach (var reserva in reservasBD)
                    {
                        respuesta.Datos.Add(new ReservaDTOConNombreSalon()
                        {
                            Id = reserva.Id,
                            Titulo = reserva.Titulo,
                            Fecha = reserva.Fecha.ToString(),
                            FranjaHoraria = reserva.FranjaHoraria,
                            HoraExtra = reserva.HoraExtra,
                            CantidadPersonas = reserva.CantidadPersonas,
                            SalonId = reserva.SalonId,
                            UsuarioId = reserva.UsuarioId,
                            NombreSalon = reserva.Salon.Nombre
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron correctamente todas las reservas";
                    return respuesta;
                }

                respuesta.Mensaje = "No se hallaron reservas";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<RespuestaPrivada<ICollection<ReservaDTOConNombreSalon>>> GetReservasDeSalonesCorporativos()
        {
            var respuesta = new RespuestaPrivada<ICollection<ReservaDTOConNombreSalon>>();
            respuesta.Datos = null;

            try
            {
                var reservasBD = await _context.Reservas.Include(x => x.Salon).Where(x => x.Salon.Tipo == "Corporativo").ToListAsync();
                if (reservasBD.Count() != 0)
                {
                    respuesta.Datos = new List<ReservaDTOConNombreSalon>();
                    foreach (var reserva in reservasBD)
                    {
                        respuesta.Datos.Add(new ReservaDTOConNombreSalon()
                        {
                            Id = reserva.Id,
                            Titulo = reserva.Titulo,
                            Fecha = reserva.Fecha.ToString(),
                            FranjaHoraria = reserva.FranjaHoraria,
                            HoraExtra = reserva.HoraExtra,
                            CantidadPersonas = reserva.CantidadPersonas,
                            SalonId = reserva.SalonId,
                            UsuarioId = reserva.UsuarioId,
                            NombreSalon = reserva.Salon.Nombre
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron correctamente todas las reservas";
                    return respuesta;
                }

                respuesta.Mensaje = "No se hallaron reservas";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            } 
        }

        public async Task<RespuestaPrivada<int>> GetReservaIdByReservaNombre(string nombreReserva)
        {
            var respuesta = new RespuestaPrivada<int>();
            respuesta.Datos = 0;

            try
            {
                var reservaId = await _context.Reservas
                    .Where(x => x.Titulo.ToLower() == nombreReserva.ToLower())
                    .Select(x => x.Id)  
                    .FirstOrDefaultAsync();

                if (reservaId != 0)
                {
                    respuesta.Datos = reservaId;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Id de la reserva recuperado correctamente";
                }
                else
                {
                    respuesta.Mensaje = "No se halló una reserva con el nombre especificado";
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno: " + ex.Message;
            }

            return respuesta;
        }
    }
}
