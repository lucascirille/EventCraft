using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataBase.Models
{
    public class Salon
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public bool Estado { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public int Capacidad { get; set; }
        [Required]
        public float DimensionesMt2 { get; set; }
        [Required]
        public decimal PrecioBase { get; set; }
        [Required]
        public decimal PrecioHora { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Localidad { get; set; }
        [Required]
        public string UrlImagen { get; set; }

        //Propiedades de navegacion
        public List<Reserva> Reservas { get; set; }
        public List<SalonCaracteristica> Caracteristicas { get; set; }
        public List<SalonServicio> Servicios { get; set; }

    }
}

