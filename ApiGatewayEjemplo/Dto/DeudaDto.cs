namespace ApiGatewayEjemplo.Dto
{
    public class DeudaDto
    {
        public string deudaId { get; set; }
        public string estado { get; set; }
        public string reservaId { get; set; }
        public float total { get; set; }
        public object[] listaPagos { get; set; }
        public ReservaDto reserva { get; set; }
    }
}
