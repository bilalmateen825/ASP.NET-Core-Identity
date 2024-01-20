﻿using Microsoft.AspNetCore.Identity;

namespace Identity.Data.BO
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
