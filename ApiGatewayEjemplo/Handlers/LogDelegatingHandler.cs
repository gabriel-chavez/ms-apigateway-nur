using System.Net.Http;

namespace ApiGatewayEjemplo.Handlers
{
    public class LogDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Request:{0}", request.ToString());

            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}
