using ApiGatewayEjemplo.Dto;
using ApiGatewayEjemplo.Dto.Aeronave;
using ApiGatewayEjemplo.ShareKernel;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

namespace ApiGatewayEjemplo.Aggregator
{
    public class AeronaveObtnerDatosAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            if (responses.Any(x => x.Items.Errors().Count > 0))
            {
                return new DownstreamResponse(null, System.Net.HttpStatusCode.BadRequest, (List<Header>)null, null);
            }

            var aeronaveobtener = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var aeropuertolistar = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var modeloaeronavelistar = await responses[2].Items.DownstreamResponse().Content.ReadAsStringAsync();

            var aeronaveobtener_obj = JsonConvert.DeserializeObject<Respuesta<Aeronave>>(aeronaveobtener);
            var aeropuertolistar_lista = JsonConvert.DeserializeObject<Respuesta<List<Aeropuerto>>>(aeropuertolistar);
            var modeloaeronavelistar_lista = JsonConvert.DeserializeObject<Respuesta<List<ModeloAeronave>>>(modeloaeronavelistar);

            AeronaveDatosDto aeronaveDatosDto = new AeronaveDatosDto();
            Aeropuerto aeropuerto = new Aeropuerto();
            aeropuerto = aeropuertolistar_lista.value.FirstOrDefault(x => x.id == aeronaveobtener_obj.value.aereopuertoEstacionamientoId);
            ModeloAeronave modeloAeronave = new ModeloAeronave();
            modeloAeronave = modeloaeronavelistar_lista.value.FirstOrDefault(x => x.id == aeronaveobtener_obj.value.modeloAeronaveId);


            aeronaveDatosDto.AeronaveId = aeronaveobtener_obj.value.id;
            aeronaveDatosDto.ModeloAeronaveId = aeronaveobtener_obj.value.modeloAeronaveId;
            aeronaveDatosDto.AeronaveEstado= aeronaveobtener_obj.value.estado;
            aeronaveDatosDto.AeronaveMatricula= aeronaveobtener_obj.value.matricula;
            aeronaveDatosDto.AereopuertoEstacionamientoId = aeronaveobtener_obj.value.aereopuertoEstacionamientoId;
            if(aeropuerto != null)
            {
                aeronaveDatosDto.AeropuertoId = aeropuerto.id;
                aeronaveDatosDto.AeropuertoNombre = aeropuerto.nombre;
                aeronaveDatosDto.AeropuertoPais=aeropuerto.pais;
                aeronaveDatosDto.AeropuertoCiudad=aeropuerto.ciudad;
            }
            if (modeloAeronave != null)
            {
                aeronaveDatosDto.ModeloId = modeloAeronave.id;
                aeronaveDatosDto.ModeloDescripcion = modeloAeronave.modelo;
                aeronaveDatosDto.ModeloMarca = modeloAeronave.marca;
                aeronaveDatosDto.ModeloCapacidadCarga = modeloAeronave.capacidadCarga;
                aeronaveDatosDto.ModeloCapacidadCargaCombustible=modeloAeronave.capacidadCargaCombustible;
            }

            var contentBody = JsonConvert.SerializeObject(new Result<AeronaveDatosDto>(aeronaveDatosDto,true,"Datos de aeronave obtenidos correctamente"));

            var stringContent = new StringContent(contentBody)
            {
                Headers = { ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
        }
    }
}
