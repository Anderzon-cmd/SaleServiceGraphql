
using MongoDB.Driver;
using SaleServiceGraphql.Data;
using SaleServiceGraphql.Exceptions;
using SaleServiceGraphql.Inputs.Mark;
using SaleServiceGraphql.Models;

namespace SaleServiceGraphql.Services
{
    public class MarkService
    {
        private readonly SaleContext _saleContext;
        
        public MarkService(SaleContext saleContext)
        {
            _saleContext = saleContext;
        }

        public async Task<Mark?> GetMarkByIdAsync(string id)
        {
            return await _saleContext.Marks.Find(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Mark>> GetMarksAsync()
        {
            return await _saleContext.Marks.Find(_ => true).ToListAsync();
        }

        public async Task<Mark> CreateMarkAsync(AddMarkInput input)
        {
            var mark = new Mark()
            {
                Description = input.Description,
                Name = input.Name,
            };

            await _saleContext.Marks.InsertOneAsync(mark);
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

            await _saleContext.Marks.ReplaceOneAsync(m => m.Id == input.Id, mark);
            return mark;
        }

        public async Task<Mark> DeleteMarkByIdAsync(string id)
        {
            var mark = await GetMarkByIdAsync(id);

            if (mark is null)
            {
                throw new NotFoundException("Mark not found.");
            }

            await _saleContext.Marks.DeleteOneAsync(m => m.Id == id);
            return mark;
        }
    }
}
