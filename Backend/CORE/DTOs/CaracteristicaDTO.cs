using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class CaracteristicaDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
     
    public class CaracteristicaDTOConId : CaracteristicaDTO
    {
        public int Id { get; set; }
    }
}
