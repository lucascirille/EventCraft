using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DataBase.Models
{
    public class Reserva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public string FranjaHoraria {  get; set; }
        [Required]
        public bool HoraExtra { get; set; }
        [Required]
        public int CantidadPersonas {  get; set; }
        [Required]
        public int SalonId { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        
        [ForeignKey("SalonId")]
        public virtual Salon Salon { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        //Propiedades de navegacion
        public virtual List<ReservaServicio> Servicios { get; set; }

    }
}
