namespace SaleServiceGraphql.Inputs.Mark
{
    public sealed record UpdateMarkInput(
        string Id,
        string Name,
        string? Description
        );
}
