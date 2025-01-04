using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class ReservaServicio
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public int ServicioId { get; set; }
        [Required]
        public int ReservaId { get; set; }
        [ForeignKey("ServicioId")]
        public virtual Servicio Servicio { get; set; }
        [ForeignKey("ReservaId")]
        public virtual Reserva Reserva { get; set; }
    }
}
