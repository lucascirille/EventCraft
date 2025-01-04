using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataBase.Models
{
    public class SalonCaracteristica
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Valor { get; set; }
        [Required]
        public int SalonId { get; set; }
        [Required]
        public int CaracteristicaId { get; set; }
        [ForeignKey("SalonId")]
        public Salon Salon { get; set; }
        [ForeignKey("CaracteristicaId")]
        public Caracteristica Caracteristica { get; set; }
    }
}
