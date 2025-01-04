using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class SalonDTO
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }  
        public bool Estado { get; set; }
        public string Telefono { get; set; }
        public int Capacidad { get; set; }
        public float DimensionesMt2 { get; set; }
        public decimal PrecioBase { get; set; }
        public decimal PrecioHora { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public string UrlImagen { get; set; }
    }

    public class SalonDTOConId : SalonDTO
    {
        public int Id { get; set; }
    }
}
