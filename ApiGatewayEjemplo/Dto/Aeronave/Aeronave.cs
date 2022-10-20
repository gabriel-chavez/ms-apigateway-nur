namespace ApiGatewayEjemplo.Dto.Aeronave
{
    public class Aeronave
    {
        public Guid id { get; set; }
        public Guid modeloAeronaveId { get; set; }
        public Guid aereopuertoEstacionamientoId { get; set; }
        public int estado { get; set; }
        public string matricula { get; set; }
    }

}
