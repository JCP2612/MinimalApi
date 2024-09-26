namespace ProductsNet.Source.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { set; get; }

        public int Price { set; get; }

        public string Description { set; get; } = "El usuario no ha ingresado una descripcion";

        public string Image { set; get; } = "http://images.net/undefined.webp";

        public ICollection<Category> Categories { get; set; } = [];

    }
}