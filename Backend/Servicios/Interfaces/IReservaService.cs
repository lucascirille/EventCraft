using CORE.DTOs;
using DataBase.Models;

namespace Servicios.Interfaces
{
    public interface IReservaService
    {
        Task<RespuestaPrivada<ICollection<ReservaDTOConId>>> GetReservas();
        Task<RespuestaPrivada<ReservaDTO>> PostReserva(ReservaDTO reservaDTO);
        Task<RespuestaPrivada<Reserva>> DeleteReserva(int id);
        Task<RespuestaPrivada<ReservaDTO>> PutReserva(int id, ReservaDTO reservaDTO);
        Task<RespuestaPrivada<ICollection<string>>> GetFechasOcupadasBySalonIdYFH(int salonId, string franjaHoraria);
        Task<RespuestaPrivada<ICollection<ReservaDTOConNombreSalon>>> GetReservasDeSalonesSociales();
        Task<RespuestaPrivada<ICollection<ReservaDTOConNombreSalon>>> GetReservasDeSalonesCorporativos();
        Task<RespuestaPrivada<int>> GetReservaIdByReservaNombre(string nombreReserva);

    }
}