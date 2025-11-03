namespace SaleServiceGraphql.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int MarkId { get; set; }
        public Mark Mark { get; set; }
        
    }
}
