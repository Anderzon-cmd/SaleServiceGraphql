using SaleServiceGraphql.Exceptions;
using SaleServiceGraphql.Inputs.Mark;
using SaleServiceGraphql.Models;
using SaleServiceGraphql.Services;

namespace SaleServiceGraphql.Mutations
{
    [MutationType]
    public static class MarkMutation
    {
        public static async Task<Mark> AddMark(AddMarkInput input, MarkService markService)
        {
            var mark = await markService.CreateMarkAsync(input);
            return mark;
        }

        [Error<NotFoundException>]
        public static async Task<Mark> UpdateMark(UpdateMarkInput input, MarkService markService)
        {
            return await markService.UpdateMarkAsync(input);
        }

        [Error<NotFoundException>]
        public static async Task<Mark> DeleteMark(string id, MarkService markService)
        {
            return await markService.DeleteMarkByIdAsync(id);
        }
    }
}
