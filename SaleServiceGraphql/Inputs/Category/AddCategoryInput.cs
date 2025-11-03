namespace SaleServiceGraphql.Inputs.Category
{
    public sealed record AddCategoryInput(
        string Name,
        string? Description
        );
}
