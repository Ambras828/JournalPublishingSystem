﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserRoles
    {
        public int UserId { get; set; }
        public Users User { get; set; }

        public int RoleId { get; set; }
        public Roles Role { get; set; }
    }

}
