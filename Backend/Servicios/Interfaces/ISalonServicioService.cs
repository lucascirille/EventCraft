using CORE.DTOs;
using DataBase.Models;

namespace Servicios.Interfaces
{
    public interface ISalonServicioService
    {
        Task<RespuestaPrivada<ICollection<SalonServicioDTOConId>>> GetSalonesServicios();
        Task<RespuestaPrivada<SalonServicioDTO>> PostSalonServicio(SalonServicioDTO salonServicioDTO);
        Task<RespuestaPrivada<SalonServicio>> DeleteSalonServicio(int id);
        Task<RespuestaPrivada<SalonServicioDTO>> PutSalonServicio(int id, SalonServicioDTO salonServicioDTO);
        Task<RespuestaPrivada<ICollection<ServicioPrecioDTO>>> GetServiciosBySalonId(int id);
    } 
}