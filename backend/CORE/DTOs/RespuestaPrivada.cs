namespace CORE.DTOs
{
    public class RespuestaPrivada<T>
    {
            public T Datos { get; set; }
            public bool Exito { get; set; } = false;
            public string Mensaje { get; set; } = "";
    }
}
