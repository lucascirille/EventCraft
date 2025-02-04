using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class UsuarioDTO
    {
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Rol { get; set; }
        public string ClaveHasheada { get; set; }
    }

    public class UsuarioDTOConId : UsuarioDTO
    {
        public int Id { get; set; }
    }
}
