namespace SaleServiceGraphql.Inputs.Category
{
    public sealed record UpdateCategoryInput(
        string Id,
        string Name,
        string? Description
        );
}
