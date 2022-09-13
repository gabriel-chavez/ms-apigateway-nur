namespace ApiGatewayEjemplo.Dto
{
    public class UserDto
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }
        public List<PostDto> posts = new();
    }




    public class PostDto
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string? title { get; set; }
        public string? body { get; set; }
    }

}
