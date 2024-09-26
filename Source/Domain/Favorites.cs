using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsNet.Source.Domain
{
    public class Favorites
    {
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateAdd { get; set; }
        public required User User { get; set; }
        public required Product Product { get; set; }
    }
}