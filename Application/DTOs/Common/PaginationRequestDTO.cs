namespace Application.DTOs.General;

public class PaginationRequestDTO
{
    public string? SearchQuery { get; set; }
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
