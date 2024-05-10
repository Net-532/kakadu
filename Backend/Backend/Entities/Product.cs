namespace Kakadu.Backend.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }

        public Product() { }

        public Product(int id, string title, decimal price, string photoUrl, string description)
        {
            Id = id;
            Title = title;
            Price = price;
            PhotoUrl = photoUrl;
            Description = description;
        }
    }
}
