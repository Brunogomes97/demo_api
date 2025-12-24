namespace project.API.Applications.Products.DTOs;

public class ProductPaginationQueryDto
{
    public int Offset { get; set; } = 0;
    public int Limit { get; set; } = 10;

    public string? Category { get; set; }
    public string? Name { get; set; }
}
