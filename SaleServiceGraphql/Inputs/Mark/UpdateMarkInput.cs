namespace SaleServiceGraphql.Inputs.Mark
{
    public sealed record UpdateMarkInput(
        int Id,
        string Name,
        string? Description
        );
}
