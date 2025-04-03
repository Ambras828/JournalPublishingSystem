namespace Core.DTOs
{
    public class UserQueryParameters
    {
        public int PageNumber { get; set; } = 1; // Default to first page
        public int PageSize { get; set; } = 10;  // Default page size
        public string SortBy { get; set; } = "Id"; // Default sorting field
        public string SortOrder { get; set; } = "asc"; // Default sort direction
        public string? Search { get; set; } // Optional search term
        public bool? IsActive { get; set; }
    }

}
