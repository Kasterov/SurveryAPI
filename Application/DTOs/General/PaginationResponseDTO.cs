namespace Application.DTOs.General;

public class PaginationResponseDTO<T>
{
    public IEnumerable<T> ResponseList { get; set; }
    public int TotalCount { get; set; }
}
