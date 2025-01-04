using CORE.DTOs;
using DataBase.Models;

namespace Servicios.Interfaces
{
    public interface IServicioService
    {
        Task<RespuestaPrivada<ICollection<ServicioDTOConId>>> GetServicios();
        Task<RespuestaPrivada<ServicioDTO>> PostServicio(ServicioDTO servicioDTO);
        Task<RespuestaPrivada<Servicio>> DeleteServicio(int id);
        Task<RespuestaPrivada<ServicioDTO>> PutServicio(int id, ServicioDTO servicioDTO);
        Task<RespuestaPrivada<ServicioDTOConId>> PatchServicioEstado(int id, bool nuevoEstado);
    }
}