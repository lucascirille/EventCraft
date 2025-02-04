using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class ReservaDTO
    {
        public string Titulo { get; set; }
        public string Fecha { get; set; }
        public string FranjaHoraria { get; set; }
        public bool HoraExtra { get; set; }
        public int CantidadPersonas { get; set; }
        public int SalonId { get; set; }
        public int UsuarioId { get; set; }
    }

    public class ReservaDTOConId : ReservaDTO
    {
        public int Id { get; set; }
    }

    public class ReservaDTOConNombreSalon : ReservaDTO
    {
        public int Id { get; set; }
        public string NombreSalon { get; set; } 
    }
}
