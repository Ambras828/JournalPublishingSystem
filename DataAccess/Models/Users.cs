using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public List<UserRoles> UserRoles { get; set; } // Many-to-many relationship

    }
}
