using ApiGatewayEjemplo.Dto.Aeronave;
using ApiGatewayEjemplo.ShareKernel;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

namespace ApiGatewayEjemplo.Aggregator
{
    public class AeronaveListarDatosAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            if (responses.Any(x => x.Items.Errors().Count > 0))
            {
                return new DownstreamResponse(null, System.Net.HttpStatusCode.BadRequest, (List<Header>)null, null);
            }

            var aeronavelistar = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var aeropuertolistar = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var modeloaeronavelistar = await responses[2].Items.DownstreamResponse().Content.ReadAsStringAsync();

            var aeronaveobtener_lista = JsonConvert.DeserializeObject<Respuesta<List<Aeronave>>>(aeronavelistar);
            var aeropuertolistar_lista = JsonConvert.DeserializeObject<Respuesta<List<Aeropuerto>>>(aeropuertolistar);
            var modeloaeronavelistar_lista = JsonConvert.DeserializeObject<Respuesta<List<ModeloAeronave>>>(modeloaeronavelistar);

            List<AeronaveDatosDto> lAeronaveDatosDto = new List<AeronaveDatosDto>();
            
            foreach (var item in aeronaveobtener_lista.value)
            {
                Aeropuerto aeropuerto = new Aeropuerto();
                aeropuerto = aeropuertolistar_lista.value.FirstOrDefault(x => x.id == item.aereopuertoEstacionamientoId);
                ModeloAeronave modeloAeronave = new ModeloAeronave();
                modeloAeronave = modeloaeronavelistar_lista.value.FirstOrDefault(x => x.id == item.modeloAeronaveId);

                AeronaveDatosDto aeronaveDatosDto =new AeronaveDatosDto();

                aeronaveDatosDto.AeronaveId = item.id;
                aeronaveDatosDto.ModeloAeronaveId = item.modeloAeronaveId;
                aeronaveDatosDto.AeronaveEstado = item.estado;
                aeronaveDatosDto.AeronaveMatricula = item.matricula;
                aeronaveDatosDto.AereopuertoEstacionamientoId = item.aereopuertoEstacionamientoId;
                if (aeropuerto != null)
                {
                    aeronaveDatosDto.AeropuertoId = aeropuerto.id;
                    aeronaveDatosDto.AeropuertoNombre = aeropuerto.nombre;
                    aeronaveDatosDto.AeropuertoPais = aeropuerto.pais;
                    aeronaveDatosDto.AeropuertoCiudad = aeropuerto.ciudad;
                }
                if (modeloAeronave != null)
                {
                    aeronaveDatosDto.ModeloId = modeloAeronave.id;
                    aeronaveDatosDto.ModeloDescripcion = modeloAeronave.modelo;
                    aeronaveDatosDto.ModeloMarca = modeloAeronave.marca;
                    aeronaveDatosDto.ModeloCapacidadCarga = modeloAeronave.capacidadCarga;
                    aeronaveDatosDto.ModeloCapacidadCargaCombustible = modeloAeronave.capacidadCargaCombustible;
                }
                lAeronaveDatosDto.Add(aeronaveDatosDto);
            }



            var contentBody = JsonConvert.SerializeObject(new Result<List<AeronaveDatosDto>>(lAeronaveDatosDto, true, "Datos de aeronave listados correctamente"));

            var stringContent = new StringContent(contentBody)
            {
                Headers = { ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");

        }
    }
}
