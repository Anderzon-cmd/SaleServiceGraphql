
using Microsoft.EntityFrameworkCore;
using SaleServiceGraphql.Data;
using SaleServiceGraphql.Exceptions;
using SaleServiceGraphql.Models;
using SaleServiceGraphql.Inputs.Category;

namespace SaleServiceGraphql.Services
{
    public sealed class CategoryService : IAsyncDisposable
    {
        private readonly SaleContext _saleContext;

        public CategoryService(IDbContextFactory<SaleContext> saleContextFactory)
        {
            _saleContext = saleContextFactory.CreateDbContext();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _saleContext.Categories.FindAsync(id);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _saleContext.Categories.ToListAsync();
        }

        public async Task<Category> CreateCategoryAsync(AddCategoryInput input)
        {
            var category = new Category() { 
                Description = input.Description,
                Name = input.Name,
            };

            _saleContext.Categories.Add(category);
            await _saleContext.SaveChangesAsync();
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
        
            await _saleContext.SaveChangesAsync();
            return category;

        }

        public async Task<Category> DeleteCategoryByIdAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);

            if (category is null)
            {
                throw new NotFoundException("Category not found.");
            }

            _saleContext.Remove(category);
            await _saleContext.SaveChangesAsync();
            return category;

        }


        public ValueTask DisposeAsync()
        {
            return _saleContext.DisposeAsync();
        }
    }
}
