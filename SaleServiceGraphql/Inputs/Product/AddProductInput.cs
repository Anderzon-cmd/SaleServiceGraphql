namespace SaleServiceGraphql.Inputs.Product
{
    public sealed record AddProductInput(
        string Name,
        string? Description,
        string? ImageUrl,
        int Stock,
        float Price,
        int CategoryId,
        int MarkId
        );
}
