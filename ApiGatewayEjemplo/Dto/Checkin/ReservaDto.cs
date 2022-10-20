namespace ApiGatewayEjemplo.Dto.Checkin
{
    public class ReservaDto
    {
        public Guid Id { get; set; }
        public int NroReserva { get; set; }
        public DateTime Hora { get; set; }
        public Guid VueloId { get; set; }
    }
}
