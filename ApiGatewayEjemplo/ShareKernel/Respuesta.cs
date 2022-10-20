namespace ApiGatewayEjemplo.ShareKernel
{
    public class Respuesta<T>
    {
        public T value { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
    }
}
