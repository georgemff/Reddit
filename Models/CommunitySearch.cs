using System.Globalization;

namespace Reddit.Models;

public class CommunitySearch
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public bool? IsAscending { get; set; }
    public Enums.SortKey? SortKey { get; set; }
    public string? SearchKey { get; set; }

}