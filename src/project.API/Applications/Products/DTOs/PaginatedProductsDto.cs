namespace project.API.Applications.Products.DTOs;

public class PaginatedProductsDto
{
    public int Total { get; set; }
    public IReadOnlyList<ProductResponseDto> Items { get; set; } = [];
}