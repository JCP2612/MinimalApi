using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ProductsNet.Source.Domain
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }


        [JsonIgnore]
        public ICollection<Product> Products { get; } = [];

    }
}