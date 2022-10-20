namespace ApiGatewayEjemplo.Dto.Aeronave
{
    public class ModeloAeronave
    {
        public Guid id { get; set; }
        public string modelo { get; set; }
        public string marca { get; set; }
        public decimal capacidadCarga { get; set; }
        public decimal capacidadCargaCombustible { get; set; }
    }

}
