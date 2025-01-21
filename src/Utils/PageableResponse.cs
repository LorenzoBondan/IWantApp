namespace IWantApp.Utils;

public class PageableResponse<T>
{
    public int NumberOfElements { get; set; }
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public int Size { get; set; }
    public bool First { get; set; }
    public bool Last { get; set; }
    public string? SortedBy { get; set; }
    public List<T> Content { get; set; }
}

