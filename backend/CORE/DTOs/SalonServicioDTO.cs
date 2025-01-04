using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class SalonServicioDTO
    {
        public decimal Precio { get; set; }
        public int ServicioId { get; set; }
        public int SalonId { get; set; }
    }

    public class SalonServicioDTOConId : SalonServicioDTO
    {
        public int Id { get; set; }
    }
}
