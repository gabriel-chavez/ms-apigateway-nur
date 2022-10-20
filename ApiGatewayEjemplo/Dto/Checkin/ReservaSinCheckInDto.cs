namespace ApiGatewayEjemplo.Dto
{
    public class ReservaSinCheckInDto
    {
        public Guid id { get; set; }
        public int nroReserva { get; set; }
        public DateTime hora { get; set; }
        public Guid vueloId { get; set; }

    }
}
