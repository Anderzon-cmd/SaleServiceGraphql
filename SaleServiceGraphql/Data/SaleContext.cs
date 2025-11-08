using MongoDB.Driver;
using SaleServiceGraphql.Models;

namespace SaleServiceGraphql.Data
{
    public class SaleContext
    {
        private readonly IMongoDatabase _database;

        public SaleContext(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("categories");
        public IMongoCollection<Mark> Marks => _database.GetCollection<Mark>("marks");
        public IMongoCollection<Product> Products => _database.GetCollection<Product>("products");
    }
}
