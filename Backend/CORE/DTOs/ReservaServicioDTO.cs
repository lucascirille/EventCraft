using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class ReservaServicioDTO
    {
        public decimal Precio { get; set; }
        public int ServicioId { get; set; }
        public int ReservaId { get; set; }
    }

    public class ReservaServicioDTOConId : ReservaServicioDTO
    {
        public int Id { get; set; }
    }
}

