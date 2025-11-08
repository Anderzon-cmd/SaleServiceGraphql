using SaleServiceGraphql.Exceptions;
using SaleServiceGraphql.Inputs.Category;
using SaleServiceGraphql.Models;
using SaleServiceGraphql.Services;

namespace SaleServiceGraphql.Mutations
{
    [MutationType]
    public static class CategoryMutation
    {
        public static async Task<Category> AddCategory(AddCategoryInput input, CategoryService categoryService)
        {
            var category = await categoryService.CreateCategoryAsync(input);
            return category;
        }

        [Error<NotFoundException>]
        public static async Task<Category> UpdateCategory(UpdateCategoryInput input, CategoryService categoryService)
        {
            return await categoryService.UpdateCategoryAsync(input);

        }

        [Error<NotFoundException>]
        public static async Task<Category> DeleteCategory(string id,CategoryService categoryService)
        {
            return await categoryService.DeleteCategoryByIdAsync(id);

        }
    }
}
