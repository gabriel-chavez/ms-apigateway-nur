using ApiGatewayEjemplo.Dto;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

namespace ApiGatewayEjemplo.Aggregator
{
    public class UsersPostsAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            if (responses.Any(x => x.Items.Errors().Count > 0))
            {
                return new DownstreamResponse(null, System.Net.HttpStatusCode.BadRequest, (List<Header>)null, null);
            }
            var userStr = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var postStr = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();

            var usersObject = JsonConvert.DeserializeObject<List<UserDto>>(userStr);
            var postsObject = JsonConvert.DeserializeObject<List<PostDto>>(postStr);

            usersObject?.ForEach(user =>
            {
                user.posts.AddRange(postsObject?.Where(a => a.userId == user.id));
            });

            var contentBody = JsonConvert.SerializeObject(usersObject);
            var stringContent = new StringContent(contentBody)
            {
                Headers = { ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") }
            };
            return new DownstreamResponse(stringContent, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
        }
    }
}
