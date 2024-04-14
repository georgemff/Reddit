using System.ComponentModel;

namespace Reddit.Models;

public class Communities
{
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public List<Community> Data { get; set; }
}