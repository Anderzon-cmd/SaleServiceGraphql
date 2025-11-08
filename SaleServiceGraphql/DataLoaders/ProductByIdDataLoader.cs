using GreenDonut.Data;
using MongoDB.Driver;
using SaleServiceGraphql.Data;
using SaleServiceGraphql.Models;
using SaleServiceGraphql.Services;

namespace SaleServiceGraphql.DataLoaders
{
    public sealed class ProductByIdDataLoader : BatchDataLoader<string, Product>
    {
        private readonly SaleContext _saleContext;
        private readonly CategoryService _categoryService;
        private readonly MarkService _markService;

        public ProductByIdDataLoader(
            SaleContext saleContext,
            CategoryService categoryService,
            MarkService markService,
            IBatchScheduler batchScheduler, 
            DataLoaderOptions options) : base(batchScheduler, options)
        {
            _saleContext = saleContext;
            _categoryService = categoryService;
            _markService = markService;
        }

        protected override async Task<IReadOnlyDictionary<string, Product>> LoadBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            var products = await _saleContext.Products
                .Find(p => keys.Contains(p.Id))
                .ToListAsync(cancellationToken);

            // Load related entities
            foreach (var product in products)
            {
                product.Category = await _categoryService.GetCategoryByIdAsync(product.CategoryId);
                product.Mark = await _markService.GetMarkByIdAsync(product.MarkId);
            }

            return products.ToDictionary(p => p.Id);
        }
    }
}
