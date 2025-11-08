using HotChocolate.Authorization;
using SaleServiceGraphql.DataLoaders;
using SaleServiceGraphql.Models;
using SaleServiceGraphql.Services;

namespace SaleServiceGraphql.Queries
{
    [QueryType]
    //[Authorize]
    public static class CategoryQuery
    {
        public static async Task<List<Category>> GetCategories(CategoryService categoryService)
        {
            return await categoryService.GetCategoriesAsync();
        }
        
        public static async Task<Category?> GetCategoryById(string id,CategoryService categoryService)
        {
            return await categoryService.GetCategoryByIdAsync(id);
        }


    }
}
