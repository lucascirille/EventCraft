namespace DataBase.Model

{
    public class Cupon
    {
        public string? Codigo { get; set; }
        public int Descuento { get; set; }
        public bool Utilizado { get; set; }
        public DateTime Vencimiento { get; set; }

    }
}

