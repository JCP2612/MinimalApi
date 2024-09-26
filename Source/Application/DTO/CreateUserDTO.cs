namespace ProductsNet.Source.Application.DTO
{
    public class CreateUserDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }

        public required string Fullname { get; set; }

    }
}