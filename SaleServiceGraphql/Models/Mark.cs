using HotChocolate.ApolloFederation.Types;

namespace SaleServiceGraphql.Models
{
    public class Mark
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}

