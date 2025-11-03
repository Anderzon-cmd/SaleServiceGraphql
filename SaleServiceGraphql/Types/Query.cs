using SaleServiceGraphql.Models;

namespace SaleServiceGraphql.Types
{
    [QueryType]
    public static class Query
    {
        public static Book GetBook()
            => new Book("C# in depth.", new Author("Jon Skeet"));

        //public static List<Category> GetCategories()
        //{
            
        //}
    }
}
