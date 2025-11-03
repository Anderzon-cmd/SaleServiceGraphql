using Microsoft.EntityFrameworkCore;
using SaleServiceGraphql.Data;
using SaleServiceGraphql.Models;

namespace SaleServiceGraphql.DataLoaders
{
    public class ProductByCategoryIdDataLoader:BatchDataLoader<int,Product[]>
    {
        private readonly IDbContextFactory<SaleContext> _dbContextFactory;

        public ProductByCategoryIdDataLoader(
            IDbContextFactory<SaleContext> contextFactory,
            IBatchScheduler batchScheduler, 
            DataLoaderOptions options) : base(batchScheduler, options)
        {
            _dbContextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Product[]>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            using SaleContext saleContext = _dbContextFactory.CreateDbContext();
            return await saleContext.Products
                .Where(p=>keys.Contains(p.CategoryId))
                .GroupBy(p=>p.CategoryId)
                .Select(t=>new {t.Key,Items=t.OrderBy(p=>p.Name).ToArray()})
                .ToDictionaryAsync(t=>t.Key,t=>t.Items,cancellationToken);
                
                
        }
    }
}
