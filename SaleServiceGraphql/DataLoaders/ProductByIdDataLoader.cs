using GreenDonut.Data;
using Microsoft.EntityFrameworkCore;
using SaleServiceGraphql.Data;
using SaleServiceGraphql.Models;

namespace SaleServiceGraphql.DataLoaders
{
    public sealed class ProductByIdDataLoader : BatchDataLoader<int, Product>
    {
        private readonly IDbContextFactory<SaleContext> _dbContextFactory;
        public ProductByIdDataLoader(
            IDbContextFactory<SaleContext> dbContextFactory, 
            IBatchScheduler batchScheduler, 
            DataLoaderOptions options) : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Product>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            using SaleContext saleContext = _dbContextFactory.CreateDbContext();

            return await saleContext.Products
                .AsNoTracking()
                .Where(p => keys.Contains(p.Id))
                .Include(p=>p.Category)
                .Include(p=>p.Mark)
                .ToDictionaryAsync(p => p.Id, cancellationToken);
        }
    }
}
