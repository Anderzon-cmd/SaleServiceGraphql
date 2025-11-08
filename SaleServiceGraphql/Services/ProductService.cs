using MongoDB.Driver;
using SaleServiceGraphql.Data;
using SaleServiceGraphql.Inputs.Product;
using SaleServiceGraphql.Models;

namespace SaleServiceGraphql.Services
{
    public class ProductService
    {
        private readonly SaleContext _saleContext;
        
        public ProductService(SaleContext saleContext)
        {
            _saleContext = saleContext;
        }

        public async Task<Product> CreateProductAsync(AddProductInput input)
        {
            var product = new Product
            {
                Name = input.Name,
                Stock = input.Stock,
                CategoryId = input.CategoryId,
                MarkId = input.MarkId,
                Description = input.Description,
                Price = input.Price,
                ImageUrl = input.ImageUrl,
            };
            
            await _saleContext.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _saleContext.Products.Find(_ => true).ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(string id)
        {
            return await _saleContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(string categoryId)
        {
            return await _saleContext.Products.Find(p => p.CategoryId == categoryId).ToListAsync();
        }
    }
}
