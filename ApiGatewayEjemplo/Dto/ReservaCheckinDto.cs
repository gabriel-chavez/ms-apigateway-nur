namespace ApiGatewayEjemplo.Dto
{
    public class ReservaCheckinDto
    {
        public Guid Id { get; set; }
        public int NroReserva { get; set; }
        public DateTime Hora { get; set; }
        public Guid VueloId { get; set; }
    }
}
