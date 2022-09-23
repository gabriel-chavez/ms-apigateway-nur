using ApiGatewayEjemplo.Dto;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

namespace ApiGatewayEjemplo.Aggregator
{
    public class CheckInVueloAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            if (responses.Any(x => x.Items.Errors().Count > 0))
            {
                return new DownstreamResponse(null, System.Net.HttpStatusCode.BadRequest, (List<Header>)null, null);
            }

            var VueloStr = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var checkInStr = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();

            var vuelo_obj = JsonConvert.DeserializeObject<VueloDto>(VueloStr);
            var checkIn_lista = JsonConvert.DeserializeObject<List<CheckInDto>>(checkInStr);

            foreach (var c in checkIn_lista)
            {
                if (vuelo_obj.Id.Equals(c.VueloId))
                {
                    vuelo_obj.Lista.Add(c);
                    continue;
                }
            }
            vuelo_obj.CantidadCheckIn = vuelo_obj.Lista.Count;

            var contentBody = JsonConvert.SerializeObject(vuelo_obj);

            var stringContent = new StringContent(contentBody)
            {
                Headers = { ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");

        }
    }
}
