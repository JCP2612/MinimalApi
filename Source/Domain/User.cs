using Microsoft.EntityFrameworkCore;

namespace ProductsNet.Source.Domain
{

    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }

        public required string Fullname { get; set; }

        public DateTime LastLogin { get; set; }

    }
}