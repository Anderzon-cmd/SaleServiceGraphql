using HotChocolate.Authorization;
using SaleServiceGraphql.DataLoaders;
using SaleServiceGraphql.Models;

namespace SaleServiceGraphql.Queries
{
    [ObjectType<Category>]
    public static partial class CategoryNode
    {
        public static async Task<Product[]> GetProductByCategoryId(
            [Parent] Category category,
            ProductByCategoryIdDataLoader productByCategoryIdDataLoader,
            CancellationToken cancellationToken
            )
        {
            return await productByCategoryIdDataLoader.LoadAsync(category.Id, cancellationToken) ?? [];
        }
    }
}
