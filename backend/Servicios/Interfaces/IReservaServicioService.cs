using CORE.DTOs;
using DataBase.Models;

namespace Servicios.Interfaces
{
    public interface IReservaServicioService
    {
        Task<RespuestaPrivada<ICollection<ReservaServicioDTOConId>>> GetReservasServicios();
        Task<RespuestaPrivada<ReservaServicioDTO>> PostReservaServicio(ReservaServicioDTO reservaServicioDTO);
        Task<RespuestaPrivada<ReservaServicio>> DeleteReservaServicio(int id);
        Task<RespuestaPrivada<ReservaServicioDTO>> PutReservaServicio(int id, ReservaServicioDTO reservaServicioDTO);
    }
}