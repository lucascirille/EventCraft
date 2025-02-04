using CORE.DTOs;
using DataBase.Models;

namespace Servicios.Interfaces
{
    public interface ICaracteristicaService
    {
        Task<RespuestaPrivada<ICollection<CaracteristicaDTOConId>>> GetCaracteristicas();
        Task<RespuestaPrivada<CaracteristicaDTO>> PostCaracteristica(CaracteristicaDTO caracteristicaDTO);
        Task<RespuestaPrivada<Caracteristica>> DeleteCaracteristica(int id);
        Task<RespuestaPrivada<CaracteristicaDTO>> PutCaracteristica(int id, CaracteristicaDTO caracteristicaDTO);
        Task<RespuestaPrivada<CaracteristicaDTOConId>> PatchCaracteristicaEstado(int id, bool nuevoEstado);
    }
}