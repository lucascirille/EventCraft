namespace CORE.DTOs
{
    public class ServicioDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }

    public class ServicioDTOConId : ServicioDTO
    {
        public int Id { get; set; }
    }
}
