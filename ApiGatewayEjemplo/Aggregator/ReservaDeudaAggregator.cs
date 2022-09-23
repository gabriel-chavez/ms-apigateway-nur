using ApiGatewayEjemplo.Dto;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

namespace ApiGatewayEjemplo.Aggregator
{
    public class ReservaDeudaAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            if (responses.Any(x => x.Items.Errors().Count > 0))
            {
                return new DownstreamResponse(null, System.Net.HttpStatusCode.BadRequest, (List<Header>)null, null);
            }
            var deudaStr = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var reservaStr  = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();

           
            var deudaObject = JsonConvert.DeserializeObject<DeudaDto>(deudaStr);
            var reservaObject = JsonConvert.DeserializeObject<List<ReservaDto>>(reservaStr);

           
            deudaObject.reserva = reservaObject?.Find(a => a.reservaId == deudaObject.reservaId);

            var contentBody = JsonConvert.SerializeObject(deudaObject);
            var stringContent = new StringContent(contentBody)
            {
                Headers = { ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") }
            };
            return new DownstreamResponse(stringContent, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
        }
    }

    


}
