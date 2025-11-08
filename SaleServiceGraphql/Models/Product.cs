using HotChocolate.ApolloFederation.Types;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SaleServiceGraphql.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        
        [BsonElement("name")]
        public required string Name { get; set; }
        
        [BsonElement("description")]
        public string? Description { get; set; }
        
        [BsonElement("imageUrl")]
        public string? ImageUrl { get; set; }
        
        [BsonElement("stock")]
        public int Stock { get; set; }
        
        [BsonElement("price")]
        public float Price { get; set; }

        [BsonElement("categoryId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string CategoryId { get; set; }
        
        [BsonIgnore]
        public Category? Category { get; set; }

        [BsonElement("markId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string MarkId { get; set; }
        
        [BsonIgnore]
        public Mark? Mark { get; set; }
        
    }
}
