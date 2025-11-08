
using MongoDB.Driver;
using SaleServiceGraphql.Data;
using SaleServiceGraphql.Exceptions;
using SaleServiceGraphql.Models;
using SaleServiceGraphql.Inputs.Category;

namespace SaleServiceGraphql.Services
{
    public sealed class CategoryService
    {
        private readonly SaleContext _saleContext;

        public CategoryService(SaleContext saleContext)
        {
            _saleContext = saleContext;
        }

        public async Task<Category?> GetCategoryByIdAsync(string id)
        {
            return await _saleContext.Categories.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _saleContext.Categories.Find(_ => true).ToListAsync();
        }

        public async Task<Category> CreateCategoryAsync(AddCategoryInput input)
        {
            var category = new Category() { 
                Description = input.Description,
                Name = input.Name,
            };

            await _saleContext.Categories.InsertOneAsync(category);
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(UpdateCategoryInput input)
        {
            var category = await GetCategoryByIdAsync(input.Id);

            if(category is null)
            {
                throw new NotFoundException("Category not found.");
            }
            
            category.Description = input.Description;
            category.Name = input.Name;
        
            await _saleContext.Categories.ReplaceOneAsync(c => c.Id == input.Id, category);
            return category;
        }

        public async Task<Category> DeleteCategoryByIdAsync(string id)
        {
            var category = await GetCategoryByIdAsync(id);

            if (category is null)
            {
                throw new NotFoundException("Category not found.");
            }

            await _saleContext.Categories.DeleteOneAsync(c => c.Id == id);
            return category;
        }
    }
}
