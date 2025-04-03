using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class UserResponseDTO
    {

        public int Id { get; set; }

        public string Username { get; set; }

        public string email { get; set; }

        public string fullName { get; set; }

        public string phoneNumber { get; set; }

        public bool IsActive { get; set; }
    }
}
