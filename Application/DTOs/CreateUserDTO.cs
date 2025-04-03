using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public  class CreateUserDTO
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string email { get; set; }

        [MaxLength(255)]
        public string fullName { get; set; }

        [Required]
        [MaxLength(255)]
        public string phoneNumber { get; set; }
        
    }
}
