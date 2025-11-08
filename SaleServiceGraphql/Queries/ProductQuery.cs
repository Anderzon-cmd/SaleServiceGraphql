using SaleServiceGraphql.DataLoaders;
using SaleServiceGraphql.Models;
using SaleServiceGraphql.Services;

namespace SaleServiceGraphql.Queries
{
    [QueryType]
    public static class ProductQuery
    {
        public static async Task<IEnumerable<Product>> GetProducts(ProductService productService)
        {
            return await productService.GetAllProductsAsync();
        }
        public static async Task<Product?> GetProductByIdAsync(
            string id,
            ProductByIdDataLoader productByIdDataLoader,
            CancellationToken cancellationToken
            )
        {
            return await productByIdDataLoader.LoadAsync( id, cancellationToken);
        }
    }


}
