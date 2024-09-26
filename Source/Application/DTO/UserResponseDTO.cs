namespace ProductsNet.Source.Application.DTO
{

    public class UserResponseDTO
    {

        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Fullname { get; set; }

    }
}