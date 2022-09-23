namespace ApiGatewayEjemplo.Dto
{
    public class EquipajeDto
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Peso { get; set; }
        public int EsFragil { get; set; }
    }
}
