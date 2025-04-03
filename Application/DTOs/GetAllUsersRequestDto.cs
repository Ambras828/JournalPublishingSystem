using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GetAllUsersRequestDto
    {
        public int PageNumber { get; set; } = 1; // Default to first page
        public int PageSize { get; set; } = 10;  // Default page size
        public string SortBy { get; set; } = "Id"; // Default sorting field
        public string SortOrder { get; set; } = "asc"; // Default sort direction
        public string Search { get; set; } // Optional search term
    }
}
