using SmatJobPortal.Data;

namespace SmatJobPortal.Models
{
    public class ListJobModel
    {
        public List<Job> Jobs { get; set; }
        public string? Query { get; set; }
        public string? Location { get; set; }
        public string? Category { get; set; }
        public string? Sort { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
    }
}
