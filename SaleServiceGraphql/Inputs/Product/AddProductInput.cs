namespace SaleServiceGraphql.Inputs.Product
{
    public sealed record AddProductInput(
        string Name,
        string? Description,
        string? ImageUrl,
        int Stock,
        float Price,
        string CategoryId,
        string MarkId
        );
}
