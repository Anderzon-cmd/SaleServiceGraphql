using SaleServiceGraphql.Inputs.Product;
using SaleServiceGraphql.Models;
using SaleServiceGraphql.Services;

namespace SaleServiceGraphql.Mutations
{
    [MutationType]
    public static class ProductMutation
    {
         public static async Task<Product> AddProduct(AddProductInput input,ProductService productService)
        {
            return await productService.CreateProductAsync(input);
        }
    }
}
