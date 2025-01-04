using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    public class Servicio
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public bool Estado { get; set; }
        //propiedad de navegacion
        public List<SalonServicio> Salones { get; set; }
        public virtual List<ReservaServicio> Reservas { get; set; }
    }
}
