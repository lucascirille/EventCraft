namespace CORE.DTOs
{
    public class SalonCaracteristicaDTO
    {
        public int Valor { get; set; } //no sabemos para que es
        public int SalonId { get; set; }
        public int CaracteristicaId { get; set; }
    }

    public class SalonCaracteristicaDTOConId : SalonCaracteristicaDTO
    {
        public int Id { get; set; }
    }
}
