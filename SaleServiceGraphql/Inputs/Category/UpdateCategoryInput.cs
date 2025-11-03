namespace SaleServiceGraphql.Inputs.Category
{
    public sealed record UpdateCategoryInput(
        int Id,
        string Name,
        string? Description
        );
}
