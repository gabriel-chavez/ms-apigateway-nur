namespace ApiGatewayEjemplo.Dto
{
    public class ReservaDto
    {
        public DateTime hora { get; set; }
        public string vueloId { get; set; }
        public int cantidadPasajero { get; set; }
        public string reservaId { get; set; }
        public string pasajeroId { get; set; }
        public float precio { get; set; }
        public string nroReserva { get; set; }
        public string estado { get; set; }
    }
}
