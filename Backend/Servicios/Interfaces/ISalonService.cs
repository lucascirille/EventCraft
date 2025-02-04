using CORE.DTOs;
using DataBase.Models;

namespace Servicios.Interfaces
{
    public interface ISalonService
    {
        Task<RespuestaPrivada<ICollection<SalonDTOConId>>> GetSalones();
        Task<RespuestaPrivada<SalonDTO>> PostSalon(SalonDTO salonDTO);
        Task<RespuestaPrivada<Salon>> DeleteSalon(int id);
        Task<RespuestaPrivada<SalonDTO>> PutSalon(int id, SalonDTO salonDTO);
        Task<RespuestaPrivada<SalonDTOConId>> PatchSalonEstado(int id, bool nuevoEstado);
    }
}