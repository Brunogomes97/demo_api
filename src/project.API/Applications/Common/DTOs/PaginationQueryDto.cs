namespace project.API.Applications.Common.DTOs;

public class PaginationQueryDto
{
    private const int MaxLimit = 100;

    public int Offset { get; set; } = 0;

    private int _limit = 10;
    public int Limit
    {
        get => _limit;
        set => _limit = value > MaxLimit ? MaxLimit : value;
    }
}
