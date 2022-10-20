namespace ApiGatewayEjemplo.Dto.Aeronave
{
    public class AeronaveDatosDto
    {
        public Guid AeronaveId { get; set; }
        public Guid ModeloAeronaveId { get; set; }        
        public int AeronaveEstado { get; set; }
        public string AeronaveMatricula { get; set; }
        public Guid AereopuertoEstacionamientoId { get; set; }

        public Guid AeropuertoId { get; set; }
        public string AeropuertoNombre { get; set; }
        public string AeropuertoPais { get; set; }
        public string AeropuertoCiudad { get; set; }

        public Guid ModeloId { get; set; }
        public string ModeloDescripcion { get; set; }
        public string ModeloMarca { get; set; }
        public decimal ModeloCapacidadCarga { get; set; }
        public decimal ModeloCapacidadCargaCombustible { get; set; }

        public AeronaveDatosDto()
        {
            AeronaveId = Guid.Empty;
            ModeloAeronaveId = Guid.Empty;
            AereopuertoEstacionamientoId= Guid.Empty;
            AeropuertoId = Guid.Empty;
            ModeloId = Guid.Empty;
            ModeloId = Guid.Empty;

        }
    }
}
