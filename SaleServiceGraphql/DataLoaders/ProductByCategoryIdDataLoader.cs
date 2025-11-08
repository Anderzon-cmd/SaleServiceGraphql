using MongoDB.Driver;
using SaleServiceGraphql.Data;
using SaleServiceGraphql.Models;

namespace SaleServiceGraphql.DataLoaders
{
    public class ProductByCategoryIdDataLoader : BatchDataLoader<string, Product[]>
    {
        private readonly SaleContext _saleContext;

        public ProductByCategoryIdDataLoader(
            SaleContext saleContext,
            IBatchScheduler batchScheduler, 
            DataLoaderOptions options) : base(batchScheduler, options)
        {
            _saleContext = saleContext;
        }

        protected override async Task<IReadOnlyDictionary<string, Product[]>> LoadBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            var products = await _saleContext.Products
                .Find(p => keys.Contains(p.CategoryId))
                .ToListAsync(cancellationToken);

            return products
                .GroupBy(p => p.CategoryId)
                .ToDictionary(
                    group => group.Key, 
                    group => group.OrderBy(p => p.Name).ToArray()
                );
        }
    }
}
