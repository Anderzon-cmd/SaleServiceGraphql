
using Microsoft.EntityFrameworkCore;
using SaleServiceGraphql.Data;
using SaleServiceGraphql.Exceptions;
using SaleServiceGraphql.Inputs.Mark;
using SaleServiceGraphql.Models;

namespace SaleServiceGraphql.Services
{
    public class MarkService : IAsyncDisposable
    {
        private readonly SaleContext _saleContext;
        public MarkService(IDbContextFactory<SaleContext> saleContextFactory)
        {
            _saleContext = saleContextFactory.CreateDbContext();
        }

        public async Task<Mark?> GetMarkByIdAsync(int id)
        {
            return await _saleContext.Marks.FindAsync(id);
        }

        public async Task<List<Mark>> GetMarksAsync()
        {
            return await _saleContext.Marks.ToListAsync();
        }

        public async Task<Mark> CreateMarkAsync(AddMarkInput input)
        {
            var mark = new Mark()
            {
                Description = input.Description,
                Name = input.Name,
            };

            _saleContext.Marks.Add(mark);
            await _saleContext.SaveChangesAsync();
            return mark;

        }

        public async Task<Mark> UpdateMarkAsync(UpdateMarkInput input)
        {
            var mark = await GetMarkByIdAsync(input.Id);

            if (mark is null)
            {
                throw new NotFoundException("Mark not found.");
            }

            mark.Description = input.Description;
            mark.Name = input.Name;

            await _saleContext.SaveChangesAsync();
            return mark;

        }

        public async Task<Mark> DeleteMarkByIdAsync(int id)
        {
            var mark = await GetMarkByIdAsync(id);

            if (mark is null)
            {
                throw new NotFoundException("Mark not found.");
            }

            _saleContext.Remove(mark);
            await _saleContext.SaveChangesAsync();
            return mark;

        }

        public ValueTask DisposeAsync()
        {
            return _saleContext.DisposeAsync();
        }
    }
}
