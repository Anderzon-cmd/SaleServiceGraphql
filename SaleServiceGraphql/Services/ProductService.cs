using Microsoft.EntityFrameworkCore;
using SaleServiceGraphql.Data;
using SaleServiceGraphql.Inputs.Product;
using SaleServiceGraphql.Models;

namespace SaleServiceGraphql.Services
{
    public class ProductService
    {
        private readonly SaleContext _saleContext;
        public ProductService(IDbContextFactory<SaleContext> contextFactory)
        {
            _saleContext = contextFactory.CreateDbContext();
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
             _saleContext.Add(product);
            await _saleContext.SaveChangesAsync();
            return product;

        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _saleContext.Products.ToListAsync();
        }
    }
}
