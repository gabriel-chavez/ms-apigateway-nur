namespace ApiGatewayEjemplo.Dto
{
    public class VueloDto
    {
        public Guid Id { get; set; }
        public int NroVuelo { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public int CantidadCheckIn { get; set; }

        public ICollection<CheckInDto> Lista = new List<CheckInDto>();
    }
}
