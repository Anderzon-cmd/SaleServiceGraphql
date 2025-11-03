using SaleServiceGraphql.Models;
using SaleServiceGraphql.Services;

namespace SaleServiceGraphql.Queries
{
    [QueryType]
    public static class MarkQuery
    {
        public static async Task<IEnumerable<Mark>> GetMarks(MarkService markService) 
        {
            return await markService.GetMarksAsync();
        }

        public async static Task<Mark?> GetMark(int id,MarkService markService)
        {
            return await markService.GetMarkByIdAsync(id);
        }

    }
}
