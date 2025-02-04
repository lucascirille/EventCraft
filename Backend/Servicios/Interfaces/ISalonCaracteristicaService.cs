using CORE.DTOs;
using DataBase.Models;

namespace Servicios.Interfaces
{
    public interface ISalonCaracteristicaService
    {
        Task<RespuestaPrivada<ICollection<SalonCaracteristicaDTOConId>>> GetSalonesCaracteristicas();
        Task<RespuestaPrivada<SalonCaracteristicaDTO>> PostSalonCaracteristica(SalonCaracteristicaDTO salonCaracteristicaDTO);
        Task<RespuestaPrivada<SalonCaracteristica>> DeleteSalonCaracteristica(int id);
        Task<RespuestaPrivada<SalonCaracteristicaDTO>> PutSalonCaracteristica(int id, SalonCaracteristicaDTO salonCaracteristicaDTO);
        Task<RespuestaPrivada<ICollection<CaracteristicaDTO>>> GetCaracteristicasBySalonId(int id);
    }
}