using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataBase.Models
{
    public class SalonServicio
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public int ServicioId { get; set; }
        [Required]
        public int SalonId { get; set; }
        [ForeignKey("ServicioId")]
        public virtual Servicio Servicio { get; set; }
        [ForeignKey("SalonId")]
        public virtual Salon Salon { get; set; }
    } 
}
