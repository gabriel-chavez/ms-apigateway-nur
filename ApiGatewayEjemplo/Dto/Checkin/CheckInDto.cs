namespace ApiGatewayEjemplo.Dto
{
    public class CheckInDto
    {
        public Guid Id { get; set; }
        public string NroCheckIn { get; set; }
        public DateTime Hora { get; set; }
        public int EsAltaPrioridad { get; set; }
        public string LetraAsiento { get; set; }
        public int NroAsiento { get; set; }
        public Guid ReservaId { get; set; }
        public Guid VueloId { get; set; }
        public ICollection<EquipajeDto> DetalleEquipaje { get; set; }
    }
}
